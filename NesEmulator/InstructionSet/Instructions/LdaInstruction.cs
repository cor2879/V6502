using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class LdaInstruction : InstructionBase
    {
        public LdaInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "LDA", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var value = Mode.Fetch(cpu);
            cpu.Accumulator.Value = value;

            cpu.ProcessorStatus.ZeroFlag = (value == 0);
            cpu.ProcessorStatus.NegativeFlag = (value & Constants.NegativeFlag) != 0;
        }
    }
}
