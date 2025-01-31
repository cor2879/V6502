using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class RtiInstruction : InstructionBase
    {
        public RtiInstruction() : base((byte)OpCodes.Rti, "RTI", Modes.Implied, 1, 6) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            Debug.WriteLine($"Initial Stack Pointer: {cpu.StackPointer.Value:X2}");

            // Restore Processor Status Register
            var status = cpu.PopStack();

            // Clear the Break flag (BrkBit), Ensure Bit 5 is set
            cpu.ProcessorStatus.Value = (byte)((status & ~ProcessorStatusRegister.BrkBit) | ProcessorStatusRegister.Bit5);

            Debug.WriteLine($"Popped Processor Status: {status:X2} -> Restored: {cpu.ProcessorStatus.Value:X2}");

            // Pop return address (Low byte first, then High byte)
            var low = cpu.PopStack();
            var high = cpu.PopStack();

            Debug.WriteLine($"Popped Return Address Low: {low:X2}, High: {high:X2}");

            cpu.ProgramCounter = new DWord6502(low, high);

            Debug.WriteLine($"Restored Program Counter: {cpu.ProgramCounter:X4}");
        }
    }

}
