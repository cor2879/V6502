/***********************************************************************************************
 * 
 *  FileName: Processor.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
#pragma warning disable CS8618, CS8622, CS0169
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.EventHandling;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Exceptions;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;
using PSR = OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu.ProcessorStatusRegister;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu
{
    public class Processor : IProcessor
    {
        #region Fields

        private Accumulator _accumulator;
        private long _cycleCount = 0L;
        private CpuRegister<Byte> _indexerX;
        private CpuRegister<Byte> _indexerY;
        private CpuRegister<UInt16> _stackPointer;
        private ProcessorStatusRegister _processorStatus;
        private DWord6502 _instructionPointer;
        private IVirtualConsole _virtualConsole;
        private Memory _memory;
        private UInt16[] _addressBus = new UInt16[16];
        private Pipeline _pipeline;

        #endregion

        #region Constructors

        public Processor(IVirtualConsole virtualConsole)
        {
            Initialize();
            _virtualConsole = virtualConsole;
            Reset();
        }

        #endregion

        #region Properties

        public Accumulator Accumulator => _accumulator;

        public UInt16[] AddressBus => _addressBus;

        public CpuRegister<Byte> IndexerX => _indexerX;

        public CpuRegister<Byte> IndexerY => _indexerY;

        public long CycleCount => _cycleCount;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// The Stack exists in memory from 0x0100 to 0x01FF.
        /// Since the high order Byte is always 0x01, the Stack
        /// Pointer is only 8-bits wide and refers only to the
        /// low order byte.  Therefore the high order byte
        /// must always be prepended before using the stack
        /// pointer to retrieve stack memory.
        /// </remarks>
        public CpuRegister<UInt16> StackPointer
        {
            get { return _stackPointer; }
        }

        public ProcessorStatusRegister ProcessorStatus
        {
            get { return _processorStatus; }
        }

        public IVirtualConsole VirtualConsole
        {
            get { return _virtualConsole; }
        }

        public Memory Memory
        {
            get { return _memory; }
        }

        public Pipeline Pipeline
        {
            get { return _pipeline; }
        }

        public DWord6502 ProgramCounter
        {
            get { return _instructionPointer; }
            set { _instructionPointer = value; }
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _accumulator = new Accumulator();
            _accumulator.SignBitChange += Accumulator_SignBitChanged;

            _indexerX = new CpuRegister<Byte>();
            _indexerY = new CpuRegister<Byte>();
            _stackPointer = new CpuRegister<UInt16>();
            _processorStatus = new ProcessorStatusRegister();
            _memory = new Memory();
            _pipeline = new Pipeline(this);
        }

        internal void CompareResult(int value)
        {
            if (value < 0)
            {
                ProcessorStatus.NegativeFlag = true;
                ProcessorStatus.ZeroFlag = false;
                ProcessorStatus.CarryFlag = false;
            }
            else if (value == 0)
            {
                ProcessorStatus.NegativeFlag = false;
                ProcessorStatus.ZeroFlag = true;
                ProcessorStatus.CarryFlag = true;
            }
            else
            {
                ProcessorStatus.NegativeFlag = false;
                ProcessorStatus.ZeroFlag = false;
                ProcessorStatus.CarryFlag = true;
            }
        }

        public void AddCycles(int cycles)
        {
            _cycleCount += cycles;
        }

        public void Load(CpuRegister<Byte> register, Byte value)
        {
            register.Value = value;
        }

        public void Load(CpuRegister<Byte> register, UInt16 Address)
        {
            register.Value = Memory[Address];
        }

        public async Task LoadProgramAsync(byte[] program, ushort startAddress = 0x0600, bool autoRun = false)
        {
            if (program is null || program.Length == 0)
            {
                throw new ArgumentException($"{nameof(program)} cannot be null or empty.", nameof(program));
            }

            for (var i = 0; i < program.Length; i++)
            {
                Memory[startAddress + i] = program[i];
            }

            ProgramCounter = new DWord6502(startAddress);
            Pipeline.Clear();

            if (autoRun)
            {
                await RunAsync(new CancellationToken());
            }
        }

        public void Store(CpuRegister<Byte> register, UInt16 Address)
        {
            Memory[Address] = register.Value;
        }

        public void Jump(UInt16 address)
        {
            _instructionPointer = address;
        }

        public void JumpIndirect(UInt16 address)
        {
            _instructionPointer = (UInt16)((UInt16)(_memory[address + 1] << 8) | (UInt16)_memory[address]);
        }

        public void Branch(Byte offset)
        {
            if (offset < 0x80)
            {
                _instructionPointer += offset;
            }
            else
            {
                _instructionPointer -= (Byte)((offset ^ ((Byte)0xFF)) + (Byte)1);
            }
        }

        public void Reset()
        {
            /* When the RES ground is removed, the program counter is loaded with the contents of
             * memory at 0xFFFC and 0xFFFD.
             * (6502 Software Design, Scanlon, p. 20)
             */
            _instructionPointer.HighPart = _memory[0xFFFC];
            _instructionPointer.LowPart = _memory[0xFFFD];

            // (UInt16)((((UInt16)_memory[0xFFFC]) << 8) + _memory[0xFFFD]);

            // Reset the Stack Pointer.
            StackPointer.Value = 0x0200;

            // Reset the Decimal Mode Status Bit
            ProcessorStatus.DecimalFlag = false;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var cycleTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / Constants.Processor.NTSC_CLOCK_SPEED);

            while (!cancellationToken.IsCancellationRequested) 
            {
                var start = DateTime.UtcNow;
                var instruction = Step();

                var elapsed = DateTime.UtcNow - start;
                var remainingTime = cycleTime * instruction.Cycles - elapsed;

                if (remainingTime > TimeSpan.Zero)
                {
                    await Task.Yield();
                }
            }
        }

        /// <summary>
        /// Executes a single instruction
        /// </summary>
        public InstructionBase Step()
        {
            InstructionRegistry.Instance.TryGetInstruction(_memory[_instructionPointer++], out var instruction);

            if (instruction == null)
            {
                throw new InvalidOperationException("The next instruction could not be fetched");
            }

            _cycleCount += instruction.Cycles;
            instruction.Execute(this);
            return instruction;
        }

        /// <summary>
        /// Stops continuous execution.
        /// </summary>
        public async Task StopAsync()
        {
            await this.RunAsync(new CancellationToken(true));
        }

        public void Interrupt()
        {
            /* If the Interupt Request bit is set to zero, load the program counter with the
             * contents of memory at 0xFFFE and 0xFFFF
             * (6502 Software Design, Scanlon, pp. 20 & 21)
             */
            if (!ProcessorStatus.IrqDisabledFlag)
            {
                PushStack(_instructionPointer.HighPart);
                PushStack(_instructionPointer.LowPart);
                PushStack(ProcessorStatus.Value);

                _instructionPointer.HighPart = _memory[0xFFFE];
                _instructionPointer.LowPart = _memory[0xFFFF];
                ProcessorStatus.IrqDisabledFlag = true;

                // Allow Interrupting device to perform work...

                ReturnFromInterrupt();

                // (UInt16)((((UInt16)_memory[0xFFFE]) << 8) + _memory[0xFFFF]); 

            }

        }

        public void NonMaskableInterrupt()
        {
            PushStack(_instructionPointer.HighPart);
            PushStack(_instructionPointer.LowPart);
            PushStack(ProcessorStatus.Value);

            /* NMI is a demand, rather than request, for interrupt.
             * The Program Counter is loaded with the contents of
             * memory at 0xFFFA and 0xFFFB
             * (6502 Software Design, Scanlon, p. 21)
             */
            _instructionPointer.HighPart = _memory[0xFFFA];
            _instructionPointer.LowPart = _memory[0xFFFB];
            ProcessorStatus.IrqDisabledFlag = true;

            // Allow Interrupting device to perform work...

            ReturnFromInterrupt();

            // (UInt16)((((UInt16)_memory[0xFFFA]) << 8) + _memory[0xFFFB]); 
        }

        public void ReturnFromInterrupt()
        {
            ProcessorStatus.Value = PopStack();
            _instructionPointer.LowPart = PopStack();
            _instructionPointer.HighPart = PopStack();
        }

        public void PushStack(Byte value)
        {
            if (StackPointer.Value < 0x0100)
            {
                throw new StackOverflow6502Exception(GetStack());
            }

            Memory[--StackPointer.Value] = value;
        }

        public Byte PopStack()
        {
            if (StackPointer.Value > 0x01FF)
            {
                throw new StackUnderflow6502Exception();
            }

            return Memory[StackPointer.Value++];
        }

        public Byte PeekStack()
        {
            return Memory[StackPointer.Value];
        }

        public Byte[] GetStack()
        {
            int n = 0;
            Byte[] stack = new Byte[0x01FF - Math.Max(StackPointer.Value, ((UInt16)0x0100))];

            for (UInt16 i = 0x01FF; i >= Math.Max(StackPointer.Value, ((UInt16)0x0100)); i--)
            {
                stack[n++] = Memory[i];
            }

            return stack;
        }

        internal DWord6502 Add(Byte leftHandSide, Byte rightHandSide)
        {
            if (ProcessorStatus.DecimalFlag)
            {
                return AddDecimalMode(leftHandSide, rightHandSide);
            }
            else
            {
                DWord6502 value = (UInt16)(leftHandSide + rightHandSide + (ProcessorStatus.Value & PSR.CarryBit));

                ProcessorStatus.CarryFlag = value.HighPart > 0;
                ProcessorStatus.OverflowFlag = (((leftHandSide & 0x80) == (rightHandSide & 0x80)) && ((rightHandSide & 0x80) != (value & 0x80)));
                ProcessorStatus.NegativeFlag = ((value & 0x80) == 0x80);
                ProcessorStatus.ZeroFlag = (value == 0);

                return value;
            }
        }

        internal DWord6502 Subtract(Byte leftHandSide, Byte rightHandSide)
        {
            if (ProcessorStatus.DecimalFlag)
            {
                return SubtractDecimalMode(leftHandSide, rightHandSide);
            }
            else
            {
                DWord6502 value = (UInt16)(leftHandSide - rightHandSide - ((ProcessorStatus.CarryFlag) ? 0 : 1));

                if ((value & 0x8000) == 0x8000)
                {
                    ProcessorStatus.CarryFlag = false;
                    ProcessorStatus.NegativeFlag = true;
                }
                else
                {
                    ProcessorStatus.CarryFlag = true;
                    ProcessorStatus.NegativeFlag = false;
                }

                ProcessorStatus.ZeroFlag = (value == 0);
                ProcessorStatus.OverflowFlag = ((leftHandSide & 0x80) != (rightHandSide & 0x80)) && ((value.SignedValue < -128) && (value.SignedValue) > 127);

                return value;
            }
        }

        private Byte AddDecimalMode(Byte leftHandSide, Byte rightHandSide)
        {
            Byte leftHandDecimalEncodedValue = ConvertDecimalEncodedHexToDecimal(leftHandSide);
            Byte rightHandDecimalEncodedValue = ConvertDecimalEncodedHexToDecimal(rightHandSide);

            Byte value = (Byte)(leftHandDecimalEncodedValue + rightHandDecimalEncodedValue);

            ProcessorStatus.CarryFlag = (value > 99);
            ProcessorStatus.ZeroFlag = (value == 0);
            ProcessorStatus.OverflowFlag = false;
            ProcessorStatus.NegativeFlag = false;

            return ConvertToDecimalEncodedHex(value);
        }

        private Byte SubtractDecimalMode(Byte leftHandSide, Byte rightHandSide)
        {
            Byte leftHandDecimalEncodedValue = ConvertDecimalEncodedHexToDecimal(leftHandSide);
            Byte rightHandDecimalEncodedValue = ConvertDecimalEncodedHexToDecimal(rightHandSide);

            SByte value = (SByte)(leftHandDecimalEncodedValue - rightHandDecimalEncodedValue - ((ProcessorStatus.CarryFlag) ? 0 : 1));

            if ((value & 0x8000) == 0x8000)
            {
                ProcessorStatus.CarryFlag = false;
                ProcessorStatus.NegativeFlag = true;
            }
            else
            {
                ProcessorStatus.CarryFlag = true;
                ProcessorStatus.NegativeFlag = false;
            }

            ProcessorStatus.ZeroFlag = (value == 0);
            ProcessorStatus.OverflowFlag = ((leftHandSide & 0x80) != (rightHandSide & 0x80)) && ((value < -128) && (value) > 127);

            return (Byte)value;
        }

        private Byte ConvertDecimalEncodedHexToDecimal(Byte decimalEncodedHex)
        {
            Byte highDigit = (Byte)(decimalEncodedHex >> 4);
            Byte lowDigit = (Byte)(decimalEncodedHex & 0x0F);

            return (Byte)((highDigit * 10) + lowDigit);
        }

        private Byte ConvertToDecimalEncodedHex(Byte value)
        {
            Byte highDigit = (Byte)(value / 10);
            Byte lowDigit = (Byte)(highDigit - value);

            return (Byte)((highDigit << 4) + lowDigit);
        }

        #endregion

        #region EventHandlers

        private void Accumulator_SignBitChanged(object sender, EventArgs<Byte> e)
        {
            ProcessorStatus.NegativeFlag = ((e.Data & 0x80) == 0x80);
            ProcessorStatus.OverflowFlag = true;
        }

        #endregion
    }
}