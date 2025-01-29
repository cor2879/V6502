using System;

using PSR = Emulators.Mso6502.ProcessorStatusRegister;

namespace Emulators.Mso6502
{
    public class SubtractAccumulatorZeroPageXInstruction
        : InstructionBase
    {
        #region Fields

        public const Byte OP_CODE = 0xF5;
        public const int Size = 2;
        public const int MAX_CYCLES = 4;
        public const String Mnemonic = "SBC";

        private DWord6502 _tempValue = 0x0000;

        #endregion

        #region Constructors



        #endregion

        #region Properties

        public override Byte OpCode
        {
            get { return OP_CODE; }
        }

        public override int MaxCycles
        {
            get { return MAX_CYCLES; }
        }

        #endregion

        #region Methods

        public override void Invoke(Cpu6502 cpu)
        {
            _tempValue = 0x0000;
            base.Invoke(cpu);
        }

        public override void Cycle()
        {
            switch (CurrentCycle)
            {
                case 0:
                    _tempValue = Cpu.Memory[ProgramCounter + 1];
                    break;
                case 1:
                    _tempValue += Cpu.IndexerX.Value;
                    break;
                case 2:
                    _tempValue = Cpu.Subtract(Cpu.Accumulator.Value, Cpu.Memory[_tempValue]);
                    break;
                case 3:
                    Cpu.Accumulator.Value = _tempValue.LowPart;
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}