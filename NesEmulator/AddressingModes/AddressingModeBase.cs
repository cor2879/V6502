using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public abstract class AddressingModeBase
    {
        public abstract byte Fetch(IProcessor cpu);

        public abstract void Store(IProcessor cpu, byte value);

        protected static DWord6502 Read16(IProcessor cpu)
        {
            return new DWord6502(cpu.Memory[cpu.ProgramCounter++], cpu.Memory[cpu.ProgramCounter++]);
        }

        protected static DWord6502 Read16(IProcessor cpu, DWord6502 address)
        {
            byte low = cpu.Memory[address];
            byte high = cpu.Memory[(DWord6502)((address & 0xFF00) | ((address + 1) & 0x00FF))];
            return new DWord6502(low, high);
        }
    }
}
