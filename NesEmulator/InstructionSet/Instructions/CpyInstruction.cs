﻿using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class CpyInstruction : InstructionBase
    {
        public CpyInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "CPY", addressingMode, length, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var value = Mode.Fetch(cpu);
            var result = cpu.IndexerY.Value - value;

            cpu.ProcessorStatus.CarryFlag = cpu.IndexerY.Value >= value;
            cpu.ProcessorStatus.ZeroFlag = result == 0;
            cpu.ProcessorStatus.NegativeFlag = (result & Constants.NEGATIVE_FLAG) != 0;
        }
    }
}
