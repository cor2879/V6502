using System;

namespace Emulators.Mso6502
{
    public class LoadAccumulatorIndirectIndexed
        : InstructionBase
    {
        #region Fields

        public const Byte OpCode = 0xB1;
        public const int Size = 2;
        public const int MaxCycles = 5;
        public const String Mnemonic = "LDA";

        private Byte _tempValue = 0x00;
        private DWord6502 _address = 0x0000;

        #endregion

        #region Constructors



        #endregion

        #region Properties


        #endregion

        #region Methods

        public override void Invoke(Cpu6502 cpu)
        {
            _tempValue = 0x00;
            _address = 0x0000;
            base.Invoke(cpu);
        }

        public override void Cycle()
        {
            switch (CurrentCycle)
            {
                case 0:
                    _tempValue = Cpu.Memory[ProgramCounter + 0x01];
                    break;
                case 1:
                    _address.LowPart = Cpu.Memory[_tempValue];
                    break;
                case 2:
                    _address.HighPart = Cpu.Memory[_tempValue + 1];
                    break;
                case 3:
                    _tempValue = Cpu.Memory[_address + Cpu.IndexerY.Value];
                    break;
                case 4:
                    Cpu.ProcessorStatus.IsNegative = ((_tempValue & 0x80) == 0x80);
                    Cpu.ProcessorStatus.IsZero = (_tempValue == 0);
                    Cpu.Accumulator.Value = _tempValue;
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}