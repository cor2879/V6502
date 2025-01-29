using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class StaInstruction : InstructionBase
    {
        public StaInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "STA", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            Mode.Store(cpu, cpu.Accumulator.Value);
        }
    }
}
