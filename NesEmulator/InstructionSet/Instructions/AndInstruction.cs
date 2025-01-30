using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class AndInstruction : InstructionBase
    {
        public AndInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "AND", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var value = Mode.Fetch(cpu);

            // Perform bitwise AND operation
            cpu.Accumulator.Value &= value;

            // Update processor status flags
            cpu.ProcessorStatus.ZeroFlag = cpu.Accumulator.Value == 0;
            cpu.ProcessorStatus.NegativeFlag = (cpu.Accumulator.Value & Constants.NEGATIVE_FLAG) != 0;

            // Advance Program Counter
            cpu.ProgramCounter += Length;
        }
    }
}
