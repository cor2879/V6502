using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class BcsInstruction : BranchInstructionBase
    {
        public BcsInstruction() : base((byte)OpCodes.Bcs, "BCS", 2, 2) { }
        protected override bool ShouldBranch(IProcessor cpu) => cpu.ProcessorStatus.CarryFlag;
    }
}
