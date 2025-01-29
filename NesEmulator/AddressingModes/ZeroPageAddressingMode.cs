using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class ZeroPageAddressingMode : AddressingModeBase
    {
        public override byte Fetch(IProcessor cpu)
        {
            var address = cpu.Memory[cpu.ProgramCounter++];
            return cpu.Memory[address];
        }

        public override void Store(IProcessor cpu, byte value)
        {
            var address = cpu.Memory[cpu.ProgramCounter++];
            cpu.Memory[address] = value;
        }
    }
}
