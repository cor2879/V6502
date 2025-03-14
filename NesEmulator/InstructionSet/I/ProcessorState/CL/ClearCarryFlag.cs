﻿using System;

namespace Emulators.Mso6502
{
    public class ClearCarryFlagInstruction
        : InstructionBase
    {
        #region Fields

        public const Byte OP_CODE = 0x18;
        public const int Size = 1;
        public const int MAX_CYCLES = 2;
        public const String Mnemonic = "CLC";

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
            base.Invoke(cpu);
        }

        public override void Cycle()
        {
            switch (CurrentCycle)
            {
                case 0:
                    break;
                case 1:
                    Cpu.ProcessorStatus.CarryFlag = false;
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}