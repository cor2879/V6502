using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class DcpInstruction : InstructionBase
    {
        public DcpInstruction(byte opCode, AddressingModeBase mode, byte length, byte cycles)
            : base(opCode, "DCP", mode, length, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            // Fetch address and decrement value
            var address = Mode.FetchDWord(cpu);
            Debug.WriteLine($"{nameof(DcpInstruction)}::{nameof(PerformExecution)} | Before DEC | Address: {address}, Memory: 0x{cpu.Memory[address]:X2}");

            var value = (byte)(cpu.Memory[address] - 1);
            cpu.Memory[address] = value;

            Debug.WriteLine($"{nameof(DcpInstruction)}::{nameof(PerformExecution)} | After DEC | Address: {address}, Memory: 0x{cpu.Memory[address]:X2}");

            // Compare with Accumulator
            byte result = (byte)(cpu.Accumulator.Value - value);

            // Set flags
            cpu.ProcessorStatus.CarryFlag = cpu.Accumulator.Value >= result;
            cpu.ProcessorStatus.ZeroFlag = result == 0;
            cpu.ProcessorStatus.NegativeFlag = (result & Constants.NEGATIVE_FLAG) != 0;

            Debug.WriteLine($"{nameof(DcpInstruction)}::{nameof(PerformExecution)} | After SBC  | Accumulator: {cpu.Accumulator.Value:X2}, Carry: {cpu.ProcessorStatus.CarryFlag}");
        }
    }
}
