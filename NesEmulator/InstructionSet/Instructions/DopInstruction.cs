using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class DopInstruction : InstructionBase
    {
        public DopInstruction(AddressingModeBase mode, byte size, byte cycles)
            : base((byte)OpCodes.DOP, "DOP", mode, size, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            // DOP behaves like NOP but sometimes has memory side effects.
        }
    }
}
