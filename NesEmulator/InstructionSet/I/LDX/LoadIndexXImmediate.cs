﻿using System;

namespace Emulators.Mso6502
{
    public class LoadIndexXImmediateInstruction
        : InstructionBase
    {
        #region Fields

        public const Byte OpCode = 0xA2;
        public const int Size = 2;
        public const int MaxCycles = 2;
        public const String Mnemonic = "LDX";

        private Byte _tempValue = 0x00;

        #endregion

        #region Constructors



        #endregion

        #region Properties


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
                    Cpu.ProcessorStatus.IsNegative = ((_tempValue & 0x80) == 0x80);
                    Cpu.ProcessorStatus.IsZero = (_tempValue == 0);
                    Cpu.IndexerX.Value = _tempValue;
                    break;
            }

            base.Cycle();
        }

        #endregion


    }
}