using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class DecInstruction : InstructionBase
    {
        public DecInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "DEC", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var address = Mode.Fetch(cpu);
            cpu.Memory[address]--;

            cpu.ProcessorStatus.ZeroFlag = cpu.Memory[address] == 0;
            cpu.ProcessorStatus.NegativeFlag = (cpu.Memory[address] & Constants.NEGATIVE_FLAG) != 0;

            cpu.ProgramCounter += Length;
        }
    }

}
