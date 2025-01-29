using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class AbsoluteXAddressingMode : AddressingModeBase
    {
        public override byte Fetch(IProcessor cpu)
        {
            var address = Read16(cpu);
            return cpu.Memory[address + cpu.IndexerX.Value];
        }

        public override void Store(IProcessor cpu, byte value)
        {
            var address = Read16(cpu);
            cpu.Memory[address + cpu.IndexerX.Value] = value;
        }
    }
}
