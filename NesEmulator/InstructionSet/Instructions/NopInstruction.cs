﻿using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class NopInstruction : InstructionBase
    {
        public NopInstruction(AddressingModeBase mode, byte length, byte cycles)
            : base((byte)OpCodes.NOP, "NOP", mode, length, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            // No operation
        }
    }
}
