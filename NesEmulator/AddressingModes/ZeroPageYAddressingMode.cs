using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class ZeroPageYAddressingMode : AddressingModeBase
    {
        public override byte Fetch(IProcessor cpu)
        {
            var address = GetNextAddress(cpu);
            return cpu.Memory[address];
        }

        public override void Store(IProcessor cpu, byte value)
        {
            var address = GetNextAddress(cpu);
            cpu.Memory[address] = value;
        }

        private ushort GetNextAddress(IProcessor cpu)
        {
            return (ushort)((cpu.Memory[cpu.ProgramCounter++] + cpu.IndexerY.Value) & 0xFF);
        }
    }
}
