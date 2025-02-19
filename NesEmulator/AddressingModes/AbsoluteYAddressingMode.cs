using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class AbsoluteYAddressingMode : AddressingModeBase
    {
        public override byte Fetch(IProcessor cpu)
        {
            var address = Read16(cpu);
            return cpu.Memory[address + cpu.IndexerY.Value];
        }

        public override void Store(IProcessor cpu, byte value)
        {
            var address = Read16(cpu);
            cpu.Memory[address + cpu.IndexerY.Value] = value;
        }

        public override DWord6502 FetchDWord(IProcessor cpu)
        {
            var baseAddress = Read16(cpu);
            var address = (ushort)(baseAddress + cpu.IndexerY.Value);

            Debug.WriteLine($"{nameof(AbsoluteYAddressingMode)}::{nameof(FetchDWord)} | Base Address: 0x{baseAddress:X4}, Indexed Address: 0x{address:X4}");

            return base.FetchDWord(cpu, address);
        }
    }
}
