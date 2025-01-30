using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class CmpInstruction : InstructionBase
    {
        public CmpInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "CMP", addressingMode, length, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var value = Mode.Fetch(cpu);
            var result = cpu.Accumulator.Value - value;

            cpu.ProcessorStatus.CarryFlag = cpu.Accumulator.Value >= value;
            cpu.ProcessorStatus.ZeroFlag = result == 0;
            cpu.ProcessorStatus.NegativeFlag = (result & Constants.NegativeFlag) != 0;
        }
    }
}
