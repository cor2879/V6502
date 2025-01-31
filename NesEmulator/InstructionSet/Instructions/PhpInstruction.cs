using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class PhpInstruction : InstructionBase
    {
        public PhpInstruction() : base((byte)OpCodes.Php, "PHP", Modes.Implied, 1, 3) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.PushStack(cpu.ProcessorStatus.Value);
        }
    }
}
