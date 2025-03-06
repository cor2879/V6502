using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class IscInstruction : InstructionBase
    {
        public IscInstruction(OpCodes opcode, AddressingModeBase mode, byte length, byte cycles)
            : base((byte)opcode, "ISC", mode, length, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var address = Mode.FetchDWord(cpu);

            Debug.WriteLine($"{nameof(IscInstruction)}::{nameof(PerformExecution)} | Before INC | Address: {address}, Memory: 0x{cpu.Memory[address]:X2}");

            var value = (byte)(cpu.Memory[address] + 1);
            cpu.Memory[address] = value;

            Debug.WriteLine($"{nameof(IscInstruction)}::{nameof(PerformExecution)} | After INC | Address: {address}, Memory: 0x{cpu.Memory[address]:X2}");

            var carry = cpu.ProcessorStatus.CarryFlag ? 1 : 0;
            var result = cpu.Accumulator.Value - value - (1 - carry);

            cpu.Accumulator.Value = (byte)result;
            cpu.ProcessorStatus.CarryFlag = result >= 0;
            cpu.ProcessorStatus.ZeroFlag = cpu.Accumulator.Value == 0;
            cpu.ProcessorStatus.NegativeFlag = (cpu.Accumulator.Value & ProcessorStatusRegister.NegativeBit) != 0;

            Debug.WriteLine($"{nameof(IsbInstruction)}::{nameof(PerformExecution)} | After SBC  | Accumulator: {cpu.Accumulator.Value:X2}, Carry: {cpu.ProcessorStatus.CarryFlag}");
        }
    }
}
