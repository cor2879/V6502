using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class BccInstruction : BranchInstructionBase
    {
        public BccInstruction() : base((byte)OpCodes.Bcc, "BCC", 2, 2) { }

        protected override bool ShouldBranch(IProcessor cpu) => !cpu.ProcessorStatus.CarryFlag;
    }
}
