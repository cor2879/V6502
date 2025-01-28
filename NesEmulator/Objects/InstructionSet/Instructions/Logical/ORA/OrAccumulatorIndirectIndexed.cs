using System;

namespace Emulators.Mso6502
{
    public class OrAccumulatorIndirectIndexed
        : InstructionBase
    {
        #region Fields

        public const Byte OP_CODE = 0x11;
        public const int Size = 2;
        public const int MAX_CYCLES = 5;
        public const String Mnemonic = "ORA";

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
                    _address.LowPart = Cpu.Memory[_tempValue];
                    break;
                case 2:
                    _address.HighPart = Cpu.Memory[_tempValue + 1];
                    break;
                case 3:
                    _tempValue = (Byte)(Cpu.Accumulator.Value | Cpu.Memory[_address + Cpu.IndexerY.Value]);
                    break;
                case 4:
                    Cpu.ProcessorStatus.NegativeFlag = ((_tempValue & 0x80) == 0x80);
                    Cpu.ProcessorStatus.ZeroFlag = (_tempValue == 0);
                    Cpu.Accumulator.Value = _tempValue.LowPart;
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}