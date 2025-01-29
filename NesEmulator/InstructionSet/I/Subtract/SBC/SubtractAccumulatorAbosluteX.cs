using System;

namespace Emulators.Mso6502
{
    public class SubtractAccumulatorAbsoluteXInstruction
        : InstructionBase
    {
        #region Fields

        public const Byte OP_CODE = 0xFD;
        public const int Size = 3;
        public const int MAX_CYCLES = 4;
        public const String Mnemonic = "SBC";

        private DWord6502 _tempValue = 0x0000;
        private DWord6502 _address = 0x0000;

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
            _address = 0x0000;
            base.Invoke(cpu);
        }

        public override void Cycle()
        {
            switch (CurrentCycle)
            {
                case 0:
                    _address.LowPart = Cpu.Memory[ProgramCounter + 0x01];
                    break;
                case 1:
                    _address.HighPart = Cpu.Memory[ProgramCounter + 0x02];
                    break;
                case 2:
                    _tempValue = Cpu.Subtract(Cpu.Accumulator.Value, Cpu.Memory[_address + Cpu.IndexerX.Value]);
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