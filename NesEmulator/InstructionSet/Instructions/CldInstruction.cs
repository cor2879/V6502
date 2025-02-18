using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class CldInstruction : InstructionBase
    {
        public CldInstruction() : base((byte)OpCodes.CLD, "CLD", Modes.Implied, 1, 2) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.ProcessorStatus.DecimalFlag = false;
        }
    }
}
