using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class SaxInstruction : InstructionBase
    {
        public SaxInstruction(byte opCode, AddressingModeBase mode, byte size, byte cycles)
            : base(opCode, "SAX", mode, size, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            byte value = (byte)(cpu.Accumulator.Value & cpu.IndexerX.Value);
            Debug.WriteLine($"{nameof(SaxInstruction)}::{nameof(PerformExecution)} | Mode: {Mode.GetType().Name}, A: 0x{cpu.Accumulator.Value:X2}, X: 0x{cpu.IndexerX.Value:X2}, Storing: 0x{value:X2}");
            Mode.Store(cpu, value);
        }
    }
}
