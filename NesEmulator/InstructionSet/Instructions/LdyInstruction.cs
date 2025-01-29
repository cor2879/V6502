using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class LdyInstruction : InstructionBase
    {
        public LdyInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "LDY", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var value = Mode.Fetch(cpu);
            cpu.IndexerY.Value = value;

            cpu.ProcessorStatus.ZeroFlag = (value == 0);
            cpu.ProcessorStatus.NegativeFlag = (value & 0x80) != 0;
        }
    }
}
