using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class TopInstruction : InstructionBase
    {
        public TopInstruction(AddressingModeBase mode, byte size, byte cycles)
            : base((byte)OpCodes.TOP, "TOP", mode, size, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            // TOP behaves like NOP but takes extra cycles.
        }
    }
}
