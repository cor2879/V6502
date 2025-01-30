using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class InxInstruction : InstructionBase
    {
        public InxInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "INX", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.IndexerX.Value++;

            cpu.ProcessorStatus.ZeroFlag = cpu.IndexerX.Value == 0;
            cpu.ProcessorStatus.NegativeFlag = (cpu.IndexerX.Value & Constants.NEGATIVE_FLAG) != 0;

            cpu.ProgramCounter += Length;
        }
    }
}
