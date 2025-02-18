using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class AlrInstruction : InstructionBase
    {
        public AlrInstruction(AddressingModeBase mode, byte size, byte cycles)
            : base((byte)OpCodes.Alr, "ALR", mode, size, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.Accumulator.Value &= Mode.Fetch(cpu);
            cpu.ProcessorStatus.CarryFlag = (cpu.Accumulator.Value & ProcessorStatusRegister.CarryBit) != 0;
            cpu.Accumulator.Value >>= 1;
            cpu.ProcessorStatus.NegativeFlag = (cpu.Accumulator.Value & ProcessorStatusRegister.NegativeBit) != 0;
            cpu.ProcessorStatus.ZeroFlag = cpu.Accumulator.Value == 0;
        }
    }
}
