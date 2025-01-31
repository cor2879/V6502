using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    /// <summary>
    /// Jump to Subroutine
    /// </summary>
    public class JsrInstruction : InstructionBase
    {
        public JsrInstruction() : base((byte)OpCodes.Jsr, "JSR", Modes.Absolute, 3, 6) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            Debug.WriteLine($"Initial ProgramCounter: {cpu.ProgramCounter}");

            // The return address should be PC - 1 (last byte of JSR instruction)
            var returnAddress = new DWord6502((byte)(cpu.ProgramCounter.LowPart + 1), cpu.ProgramCounter.HighPart);

            // Push return address onto the stack (high byte first, then low byte)
            cpu.PushStack(returnAddress.HighPart);
            cpu.PushStack(returnAddress.LowPart);

            Debug.WriteLine($"ProgramCounter after pushing Return Address to Stack: {cpu.ProgramCounter}");

            // Fetch the target address (two bytes) and jump
            var targetAddress = Mode.FetchDWord(cpu);
            cpu.ProgramCounter = targetAddress;

            Debug.WriteLine($"Program Counter at end: {cpu.ProgramCounter}");
        }
    }

}
