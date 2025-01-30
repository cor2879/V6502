using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class OraInstruction : InstructionBase
    {
        public OraInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "ORA", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var value = Mode.Fetch(cpu);

            // Perform bitwise OR operation
            cpu.Accumulator.Value |= value;

            // Update processor status flags
            cpu.ProcessorStatus.ZeroFlag = cpu.Accumulator.Value == 0;
            cpu.ProcessorStatus.NegativeFlag = (cpu.Accumulator.Value & Constants.NegativeFlag) != 0;

            // Advance Program Counter
            cpu.ProgramCounter += Length;
        }
    }
}
