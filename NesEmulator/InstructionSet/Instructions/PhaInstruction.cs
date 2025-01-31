using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class PhaInstruction : InstructionBase
    {
        public PhaInstruction() : base((byte)OpCodes.Pha, "PHA", Modes.Implied, 1, 3) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.PushStack(cpu.Accumulator.Value);
        }
    }
}
