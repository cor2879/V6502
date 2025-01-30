using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class InyInstruction : InstructionBase
    {
        public InyInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "INY", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.IndexerY.Value++;

            cpu.ProcessorStatus.ZeroFlag = cpu.IndexerY.Value == 0;
            cpu.ProcessorStatus.NegativeFlag = (cpu.IndexerY.Value & Constants.NEGATIVE_FLAG) != 0;

            cpu.ProgramCounter += Length;
        }
    }

}
