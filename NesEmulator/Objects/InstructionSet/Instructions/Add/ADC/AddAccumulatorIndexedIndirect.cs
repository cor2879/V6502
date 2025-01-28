using System;

namespace Emulators.Mso6502
{
    public class AddAccumulatorIndexedIndirect
        : InstructionBase
    {
        #region Fields

        public const Byte OP_CODE = 0x61;
        public const int Size = 2;
        public const int MAX_CYCLES = 6;
        public const String Mnemonic = "ADC";

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
                    _tempValue = Cpu.Memory[ProgramCounter + 0x01];
                    break;
                case 1:
                    _tempValue += Cpu.IndexerX.Value;
                    break;
                case 2:
                    _address.LowPart = Cpu.Memory[_tempValue++];
                    break;
                case 3:
                    _address.HighPart = Cpu.Memory[_tempValue];
                    break;
                case 4:
                    _tempValue = Cpu.Add(Cpu.Accumulator.Value, Cpu.Memory[_address]);
                    break;
                case 5:
                    Cpu.Accumulator.Value = _tempValue.LowPart;
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}