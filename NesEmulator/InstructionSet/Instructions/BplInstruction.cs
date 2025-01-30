using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class BplInstruction : BranchInstructionBase
    {
        public BplInstruction() : base((byte)OpCodes.Bpl, "BPL", 2, 2) { }
        protected override bool ShouldBranch(IProcessor cpu) => !cpu.ProcessorStatus.NegativeFlag;
    }
}
