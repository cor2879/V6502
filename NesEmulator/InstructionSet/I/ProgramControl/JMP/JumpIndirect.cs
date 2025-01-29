using System;

using PSR = Emulators.Mso6502.ProcessorStatusRegister;

namespace Emulators.Mso6502
{
    public class JumpIndirectInstruction
        : InstructionBase
    {
        #region Fields

        public const Byte OP_CODE = 0x6C;
        public const int Size = 3;
        public const int MAX_CYCLES = 5;
        public const String Mnemonic = "JMP";

        private DWord6502 _absoluteAddress = 0x0000;
        private DWord6502 _indirectAddress = 0x0000;

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
            _absoluteAddress = 0x0000;
            _indirectAddress = 0x0000;
            base.Invoke(cpu);
        }

        public override void Cycle()
        {
            switch (CurrentCycle)
            {
                case 0:
                    _absoluteAddress.LowPart = Cpu.Memory[ProgramCounter + 1];
                    break;
                case 1:
                    _absoluteAddress.HighPart = Cpu.Memory[ProgramCounter + 2];
                    break;
                case 2:
                    _indirectAddress.LowPart = Cpu.Memory[_absoluteAddress];
                    break;
                case 3:
                    _indirectAddress.HighPart = Cpu.Memory[_absoluteAddress + 1];
                    break;
                case 4:
                    Cpu.ProgramCounter = _indirectAddress;
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}