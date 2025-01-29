using System;

using PSR = Emulators.Mso6502.ProcessorStatusRegister;

namespace Emulators.Mso6502
{
    public class DecrementMemoryZeroPageInstruction
        : InstructionBase
    {
        #region Fields

        public const Byte OP_CODE = 0xC6;
        public const int Size = 2;
        public const int MAX_CYCLES = 5;
        public const String Mnemonic = "DEC";

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
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    Cpu.Memory[_tempValue]--;
                    Cpu.ProcessorStatus.NegativeFlag = (Cpu.Memory[_tempValue] & 0x80) == 0x80;
                    Cpu.ProcessorStatus.ZeroFlag = (Cpu.Memory[_tempValue] == 0);
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}