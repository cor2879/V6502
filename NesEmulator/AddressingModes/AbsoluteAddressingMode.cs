using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class AbsoluteAddressingMode : AddressingModeBase
    {
        public override byte Fetch(IProcessor cpu)
        {
            var address = Read16(cpu);
            return cpu.Memory[address];
        }

        public override void Store(IProcessor cpu, byte value)
        {
            var address = Read16(cpu);
            cpu.Memory[address] = value;
        }
    }
}
