using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class BvcInstruction : BranchInstructionBase
    {
        public BvcInstruction() : base((byte)OpCodes.Bvc, "BVC", 2, 2) { }
        protected override bool ShouldBranch(IProcessor cpu) => !cpu.ProcessorStatus.OverflowFlag;
    }
}
