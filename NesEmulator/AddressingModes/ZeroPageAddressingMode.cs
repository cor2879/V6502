using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class ZeroPageAddressingMode : AddressingModeBase
    {
        public override byte FetchOperand(Processor cpu, Memory memory) => memory[memory[cpu.ProgramCounter++]];

        public override ushort FetchAddress(Processor cpu, Memory memory) => memory[cpu.ProgramCounter++];
    }
}
