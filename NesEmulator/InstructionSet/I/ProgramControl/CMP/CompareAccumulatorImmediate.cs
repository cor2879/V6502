﻿using System;

using PSR = Emulators.Mso6502.ProcessorStatusRegister;

namespace Emulators.Mso6502
{
    public class CompareAccumulatorImmediateInstruction
        : InstructionBase
    {
        #region Fields

        public const Byte OP_CODE = 0xC9;
        public const int Size = 2;
        public const int MAX_CYCLES = 2;
        public const String Mnemonic = "CMP";

        private int _tempValue = 0;

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
            base.Invoke(cpu);
        }

        public override void Cycle()
        {
            switch (CurrentCycle)
            {
                case 0:
                    _tempValue = Cpu.Accumulator.Value.CompareTo(Cpu.Memory[ProgramCounter + 1]);
                    break;
                case 1:
                    Cpu.CompareResult(_tempValue);
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}