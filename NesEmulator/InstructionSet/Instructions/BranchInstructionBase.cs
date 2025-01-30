using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public abstract class BranchInstructionBase : InstructionBase
    {
        protected abstract bool ShouldBranch(IProcessor cpu);

        public BranchInstructionBase(byte opCode, string mnemonic, byte length, byte cycles)
            : base(opCode, mnemonic, Modes.Relative, length, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var offset = (sbyte)Mode.Fetch(cpu);
            if (ShouldBranch(cpu))
            {
                var oldPc = cpu.ProgramCounter;
                cpu.ProgramCounter += offset;

                // If page boundary crossed, add extra cycle
                if ((oldPc & Constants.PAGE_BOUNDARY) != (cpu.ProgramCounter & Constants.PAGE_BOUNDARY))
                    cpu.AddCycles(1);
            }
        }
    }
}
