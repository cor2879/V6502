using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class IncInstruction : InstructionBase
    {
        public IncInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "INC", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var address = Mode.Fetch(cpu);
            cpu.Memory[address]++;

            // Update processor status flags
            cpu.ProcessorStatus.ZeroFlag = cpu.Memory[address] == 0;
            cpu.ProcessorStatus.NegativeFlag = (cpu.Memory[address] & Constants.NegativeFlag) != 0;

            // Advance Program Counter
            cpu.ProgramCounter += Length;
        }
    }
}
