using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class ArrInstruction : InstructionBase
    {
        public ArrInstruction(AddressingModeBase mode, byte size, byte cycles)
            : base((byte)OpCodes.Arr, "ARR", mode, size, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            cpu.Accumulator.Value &= Mode.Fetch(cpu);

            bool oldCarry = cpu.ProcessorStatus.CarryFlag; // Save old carry state
            cpu.ProcessorStatus.CarryFlag = (cpu.Accumulator.Value & ProcessorStatusRegister.CarryBit) != 0; // Carry is set based on bit 0 before shifting

            // Perform Rotate Right with Carry
            cpu.Accumulator.Value = (byte)((cpu.Accumulator.Value >> 1) | (oldCarry ? ProcessorStatusRegister.NegativeBit : 0));

            cpu.ProcessorStatus.OverflowFlag = ((cpu.Accumulator.Value >> 6) & 1) != ((cpu.Accumulator.Value >> 5) & 1);
            cpu.ProcessorStatus.NegativeFlag = (cpu.Accumulator.Value & ProcessorStatusRegister.NegativeBit) != 0;
            cpu.ProcessorStatus.ZeroFlag = cpu.Accumulator.Value == 0;
        }
    }
}
