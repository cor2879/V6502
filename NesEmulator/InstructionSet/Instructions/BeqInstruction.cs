using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class BeqInstruction : BranchInstructionBase
    {
        public BeqInstruction() : base((byte)OpCodes.Beq, "BEQ", 2, 2) { }
        protected override bool ShouldBranch(IProcessor cpu) => cpu.ProcessorStatus.ZeroFlag;
    }
}
