using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class BneInstruction : BranchInstructionBase
    {
        public BneInstruction() : base((byte)OpCodes.Bne, "BNE", 2, 2) { }
        protected override bool ShouldBranch(IProcessor cpu) => !cpu.ProcessorStatus.ZeroFlag;
    }
}
