using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class PlaInstruction : InstructionBase
    {
        public PlaInstruction() : base((byte)OpCodes.Pla, "PLA", Modes.Implied, 1, 4) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.Accumulator.Value = cpu.PopStack();

            // Update flags
            cpu.ProcessorStatus.ZeroFlag = (cpu.Accumulator.Value == 0);
            cpu.ProcessorStatus.NegativeFlag = (cpu.Accumulator.Value & Constants.NEGATIVE_FLAG) != 0;
        }
    }
}
