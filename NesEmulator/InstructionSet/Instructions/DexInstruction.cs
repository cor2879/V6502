using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class DexInstruction : InstructionBase
    {
        public DexInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "DEX", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.IndexerX.Value--;

            cpu.ProcessorStatus.ZeroFlag = cpu.IndexerX.Value == 0;
            cpu.ProcessorStatus.NegativeFlag = (cpu.IndexerX.Value & Constants.NegativeFlag) != 0;

            cpu.ProgramCounter += Length;
        }
    }
}
