using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class SaxInstruction : InstructionBase
    {
        public SaxInstruction(AddressingModeBase mode, byte size, byte cycles)
            : base((byte)OpCodes.Sax, "SAX", mode, size, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            byte value = (byte)(cpu.Accumulator.Value & cpu.IndexerX.Value);
            Mode.Store(cpu, value);
        }
    }
}
