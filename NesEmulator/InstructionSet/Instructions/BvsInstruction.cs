using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class BvsInstruction : BranchInstructionBase
    {
        public BvsInstruction() : base((byte)OpCodes.Bvs, "BVS", 2, 2) { }
        protected override bool ShouldBranch(IProcessor cpu) => cpu.ProcessorStatus.OverflowFlag;
    }
}
