using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class DeyInstruction : InstructionBase
    {
        public DeyInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "DEY", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.IndexerY.Value--;

            cpu.ProcessorStatus.ZeroFlag = cpu.IndexerY.Value == 0;
            cpu.ProcessorStatus.NegativeFlag = (cpu.IndexerY.Value & Constants.NegativeFlag) != 0;

            cpu.ProgramCounter += Length;
        }
    }

}
