using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class JmpInstruction : InstructionBase
    {
        public JmpInstruction(byte opCode, AddressingModeBase mode) : base(opCode, "JMP", mode, 3, mode is IndirectAddressingMode ? (byte)5 : (byte)3) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var address = Mode.FetchDWord(cpu);

            cpu.ProgramCounter = address;

            Debug.WriteLine($"{nameof(JmpInstruction)}::{nameof(PerformExecution)} | Program Counter (After Execution): {cpu.ProgramCounter}");
        }
    }
}
