using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    /// <summary>
    /// Return from Subroutine
    /// </summary>
    public class RtsInstruction : InstructionBase
    {
        public RtsInstruction() : base((byte)OpCodes.Rts, "RTS", Modes.Implied, 1, 6) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var lowByte = cpu.PopStack();
            var highByte = cpu.PopStack();
            var returnAddress = new DWord6502(lowByte, highByte);

            cpu.ProgramCounter = returnAddress + 1;
        }
    }

}
