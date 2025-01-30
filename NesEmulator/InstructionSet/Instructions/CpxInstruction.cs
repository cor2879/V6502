using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class CpxInstruction : InstructionBase
    {
        public CpxInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "CPX", addressingMode, length, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var value = Mode.Fetch(cpu);
            var result = cpu.IndexerX.Value - value;

            cpu.ProcessorStatus.CarryFlag = cpu.IndexerX.Value >= value;
            cpu.ProcessorStatus.ZeroFlag = result == 0;
            cpu.ProcessorStatus.NegativeFlag = (result & Constants.NegativeFlag) != 0;
        }
    }
}
