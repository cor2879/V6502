using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class LaxInstruction : InstructionBase
    {
        public LaxInstruction(byte opCode, AddressingModeBase mode, byte size, byte cycles)
            : base(opCode, "LAX", mode, size, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            byte value = Mode.Fetch(cpu);
            Debug.WriteLine($"{nameof(LaxInstruction)}::{nameof(PerformExecution)} | Mode: {Mode.GetType().Name}, Fetched Value: 0x{value:X2}");
            cpu.Accumulator.Value = value;
            cpu.IndexerX.Value = value;
        }
    }
}
