using System;

using PSR = Emulators.Mso6502.ProcessorStatusRegister;

namespace Emulators.Mso6502
{
    public class CompareIndexXAbsoluteInstruction
        : InstructionBase
    {
        #region Fields

        public const Byte OP_CODE = 0xEC;
        public const int Size = 3;
        public const int MAX_CYCLES = 4;
        public const String Mnemonic = "CPX";

        private int _tempValue = 0;
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
            _tempValue = 0;
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
                    _tempValue = Cpu.IndexerX.Value.CompareTo(Cpu.Memory[_address]);
                    break;
                case 3:
                    Cpu.CompareResult(_tempValue);
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}