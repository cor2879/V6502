using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class LdaImmediateInstruction : InstructionBase
    {
        public enum OpCodes
        {

        };

        public LdaImmediateInstruction(IProcessor cpu, byte length)
            : base(cpu, 0xA9, "LDA", new ImmediateAddressingMode(), length, 2)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.Accumulator.Value = cpu.Memory[cpu.ProgramCounter++];
        }
    }
}
