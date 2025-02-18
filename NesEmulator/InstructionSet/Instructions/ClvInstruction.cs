using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class ClvInstruction : InstructionBase
    {
        public ClvInstruction() : base((byte)OpCodes.CLV, "CLV", Modes.Implied, 1, 2) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.ProcessorStatus.OverflowFlag = false;
        }
    }
}
