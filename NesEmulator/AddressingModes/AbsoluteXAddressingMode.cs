using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;
using System.Diagnostics;

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

        public override DWord6502 FetchDWord(IProcessor cpu)
        {
            var baseAddress = Read16(cpu);
            var address = (DWord6502)(baseAddress + cpu.IndexerX.Value);

            Debug.WriteLine($"{nameof(AbsoluteXAddressingMode)}::{nameof(FetchDWord)} | Base Address: {baseAddress}, Indexed Address: {address}");

            return base.FetchDWord(cpu, address);
        }
    }
}
