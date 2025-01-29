using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class StxInstruction : InstructionBase
    {
        public StxInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "STX", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            Mode.Store(cpu, cpu.IndexerX.Value);
        }
    }
}
