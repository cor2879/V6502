using System;

using PSR = Emulators.Mso6502.ProcessorStatusRegister;

namespace Emulators.Mso6502
{
    public class BranchOnCarrySetInstruction
        : InstructionBase
    {
        #region Fields

        public const Byte OP_CODE = 0xB0;
        public const int Size = 2;
        public const int MAX_CYCLES = 2;
        public const String Mnemonic = "BCS";

        private Byte _tempValue = 0x00;

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
            _tempValue = 0x00;
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
                    if (Cpu.ProcessorStatus.CarryFlag)
                    {
                        // If _tempValue >= 0x80 then it is a negative signed value.
                        // Get its absolute value using 2's complement notation and
                        // subtract from the program counter.
                        if (_tempValue >= 0x80)
                        {
                            Cpu.ProgramCounter -= (Byte)((_tempValue ^ 0xFF) + 0x01);
                        }
                        else
                        {
                            Cpu.ProgramCounter += _tempValue;
                        }
                    }
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}