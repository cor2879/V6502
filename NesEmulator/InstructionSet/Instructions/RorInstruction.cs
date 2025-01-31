using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class RorInstruction : InstructionBase
    {
        public RorInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "ROR", addressingMode, length, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var value = Mode.Fetch(cpu);

            // Capture bit 0 before shifting
            var newCarry = (value & 0x01) << 8;

            // Apply the current Carry Flag into the high bit (bit 7)
            var carryIn = cpu.ProcessorStatus.CarryFlag ? 0x80 : 0;
            var result = (byte)((value >> 1) | carryIn);

            // Store the shifted value
            Mode.Store(cpu, result);

            // Correct order of flag updates
            cpu.ProcessorStatus.ZeroFlag = result == 0;
            cpu.ProcessorStatus.NegativeFlag = (result & ProcessorStatusRegister.NegativeBit) != 0;
            cpu.ProcessorStatus.CarryFlag = (newCarry & 0x80) == 0x80; // Carry should reflect old bit 0
        }

    }
}
