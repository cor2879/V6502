using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class PlpInstruction : InstructionBase
    {
        public PlpInstruction() : base((byte)OpCodes.Plp, "PLP", Modes.Implied, 1, 4) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.ProcessorStatus.Value = cpu.PopStack();
        }
    }

}
