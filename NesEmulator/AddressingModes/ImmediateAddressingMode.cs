﻿using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class ImmediateAddressingMode : AddressingModeBase
    {
        public override byte Fetch(IProcessor cpu)
        {
            return cpu.Memory[cpu.ProgramCounter++];
        }

        public override void Store(IProcessor cpu, byte value)
        {
            throw new InvalidOperationException("Immediate mode is Read Only");
        }
    }
}
