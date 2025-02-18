using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class AncInstruction : InstructionBase
    {
        public AncInstruction(AddressingModeBase mode, byte size, byte cycles)
            : base((byte)OpCodes.Anc, "ANC", mode, size, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var value = Mode.Fetch(cpu);
            cpu.Accumulator.Value &= value;
            cpu.ProcessorStatus.NegativeFlag = (cpu.Accumulator.Value & ProcessorStatusRegister.NegativeBit) != 0;
            cpu.ProcessorStatus.CarryFlag = (cpu.Accumulator.Value & ProcessorStatusRegister.CarryBit) != 0;
        }
    }
}
