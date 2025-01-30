using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class BmiInstruction : BranchInstructionBase
    {
        public BmiInstruction() : base((byte)OpCodes.Bmi, "BMI", 2, 2) { }
        protected override bool ShouldBranch(IProcessor cpu) => cpu.ProcessorStatus.NegativeFlag;
    }
}
