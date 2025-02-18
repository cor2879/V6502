using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class LaxInstruction : InstructionBase
    {
        public LaxInstruction(AddressingModeBase mode, byte size, byte cycles)
            : base((byte)OpCodes.Lax, "LAX", mode, size, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            byte value = Mode.Fetch(cpu);
            cpu.Accumulator.Value = value;
            cpu.IndexerX.Value = value;
        }
    }
}
