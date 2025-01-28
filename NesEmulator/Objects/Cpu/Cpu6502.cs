/***********************************************************************************************
 * 
 *  FileName: Cpu6502.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
#pragma warning disable CS8618, CS8622
using System;

using Emulators.Nes;
using PSR = Emulators.Mso6502.ProcessorStatusRegister;

namespace Emulators.Mso6502
{
    public class Cpu6502
    {
        #region Fields

        public const UInt16 RAM_ADDRESS = 0x0000;
        public const UInt16 IO_ADDRESS = 0x2000;
        public const UInt16 PPU_CONTROL_REGISTER_1_ADDRESS = 0x2000;
        public const UInt16 PPU_CONTROL_REGISTER_2_ADDRESS = 0x2001;
        public const UInt16 PPU_STATUS_REGISTER_ADDRESS = 0x2002;
        public const UInt16 SPRITE_MEMORY_ADDRESS = 0x2003;
        public const UInt16 SPRITE_MEMORY_DATA_ADDRESS = 0x2004;
        public const UInt16 SPRITE_SCROLL_OFFSETS_ADDRESS = 0x2005;
        public const UInt16 EXPANSION_MODULES_ADDRESS = 0x5000;
        public const UInt16 CARTRIDGE_RAM_ADDRESS = 0x6000;
        public const UInt16 CARTRIDGE_ROM_LOWER_BANK_ADDRESS = 0x8000;
        public const UInt16 CARTRIDGE_ROM_UPPER_BANK_ADDRESS = 0xC000;

        private Accumulator _accumulator;
        private CpuRegister<Byte> _indexerX;
        private CpuRegister<Byte> _indexerY;
        private CpuRegister<UInt16> _stackPointer;
        private ProcessorStatusRegister _processorStatus;
        private DWord6502 _instructionPointer;
        private VirtualConsole _virtualConsole;
        private Memory _memory;
        private UInt16[] _addressBus = new UInt16[16];
        private Pipeline _pipeline;

        #endregion

        #region Constructors

        public Cpu6502(VirtualConsole virtualConsole)
        {
            Initialize();
            _virtualConsole = virtualConsole;
            Reset();
        }

        #endregion

        #region Properties

        public Accumulator Accumulator
        {
            get { return _accumulator; }
        }

        public UInt16[] AddressBus
        {
            get { return _addressBus; }
        }

        public CpuRegister<Byte> IndexerX
        {
            get { return _indexerX; }
        }

        public CpuRegister<Byte> IndexerY
        {
            get { return _indexerY; }
        }

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

        public VirtualConsole VirtualConsole
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
            internal set { _instructionPointer = value; }
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
            _pipeline = new Pipeline();
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

        public void Load(CpuRegister<Byte> register, Byte value)
        {
            register.Value = value;
        }

        public void Load(CpuRegister<Byte> register, UInt16 Address)
        {
            register.Value = Memory[Address];
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