using System;

using PSR = Emulators.Mso6502.ProcessorStatusRegister;

namespace Emulators.Mso6502
{
    public class JumpAbsoluteInstruction
        : InstructionBase
    {
        #region Fields

        public const Byte OP_CODE = 0x4C;
        public const int Size = 3;
        public const int MAX_CYCLES = 3;
        public const String Mnemonic = "JMP";

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
                    Cpu.ProgramCounter = _address;
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}