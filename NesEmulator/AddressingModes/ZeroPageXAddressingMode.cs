using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class ZeroPageXAddressingMode : AddressingModeBase
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
            return (ushort)((cpu.Memory[cpu.ProgramCounter++] + cpu.IndexerX.Value) & 0xFF);
        }
    }
}
