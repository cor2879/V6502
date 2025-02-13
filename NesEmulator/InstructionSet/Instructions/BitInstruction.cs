using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class BitInstruction : InstructionBase
    {
        public BitInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "BIT", addressingMode, length, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var value = Mode.Fetch(cpu);
            var result = cpu.Accumulator.Value & value;

            cpu.ProcessorStatus.ZeroFlag = (result == 0);
            cpu.ProcessorStatus.OverflowFlag = (value & ProcessorStatusRegister.OverflowBit) != 0;
            cpu.ProcessorStatus.NegativeFlag = (value & ProcessorStatusRegister.NegativeBit) != 0;
        }
    }
}
