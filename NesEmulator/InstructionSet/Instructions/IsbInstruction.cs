using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class IsbInstruction : InstructionBase
    {
        public IsbInstruction(byte opCode, AddressingModeBase mode, byte length, byte cycles)
            : base(opCode, "ISB", mode, length, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            // Fetch value from memory, increment it, store it back
            var address = Mode.FetchDWord(cpu);
            Debug.WriteLine($"{nameof(IsbInstruction)}::{nameof(PerformExecution)} | Before INC | Address: {address}, Memory: 0x{cpu.Memory[address]:X2}");

            var value = (byte)(cpu.Memory[address] + 1);
            cpu.Memory[address] = value;

            Debug.WriteLine($"{nameof(IsbInstruction)}::{nameof(PerformExecution)} | After INC | Address: {address}, Memory: 0x{cpu.Memory[address]:X2}");

            // Perform SBC operation with the new incremented value
            byte carry = cpu.ProcessorStatus.CarryFlag ? (byte)0x01 : (byte)0x00;
            byte result = (byte)(cpu.Accumulator.Value - value - (1 - carry));

            // Set flags accordingly
            cpu.ProcessorStatus.CarryFlag = cpu.Accumulator.Value >= (value + (1 - carry));
            cpu.ProcessorStatus.ZeroFlag = result == 0;
            cpu.ProcessorStatus.NegativeFlag = (result & ProcessorStatusRegister.NegativeBit) != 0;
            cpu.ProcessorStatus.OverflowFlag = ((cpu.Accumulator.Value ^ value) & (cpu.Accumulator.Value ^ result) & ProcessorStatusRegister.NegativeBit) != 0;

            // Store the result back in the accumulator
            cpu.Accumulator.Value = result;

            Debug.WriteLine($"{nameof(IsbInstruction)}::{nameof(PerformExecution)} | After SBC  | Accumulator: {cpu.Accumulator.Value:X2}, Carry: {cpu.ProcessorStatus.CarryFlag}");
        }
    }
}
