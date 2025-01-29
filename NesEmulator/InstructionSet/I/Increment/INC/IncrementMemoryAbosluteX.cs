using System;

namespace Emulators.Mso6502
{
    public class IncrementMemoryAbsoluteXInstruction
        : InstructionBase
    {
        #region Fields

        public const Byte OP_CODE = 0xFE;
        public const int Size = 3;
        public const int MAX_CYCLES = 7;
        public const String Mnemonic = "INC";

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
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    _address += Cpu.IndexerX.Value;
                    Cpu.Memory[_address]++;
                    Cpu.ProcessorStatus.NegativeFlag = (Cpu.Memory[_address] & 0x80) == 0x80;
                    Cpu.ProcessorStatus.ZeroFlag = (Cpu.Memory[_address] == 0);
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}