using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class IndirectIndexedAddressingMode : AddressingModeBase
    {
        public override byte Fetch(IProcessor cpu)
        {
            var zeroPageAddr = cpu.Memory[cpu.ProgramCounter++];
            var baseAddr = Read16(cpu, zeroPageAddr);
            return cpu.Memory[baseAddr + cpu.IndexerY.Value];
        }

        public override void Store(IProcessor cpu, byte value)
        {
            byte zeroPageAddr = cpu.Memory[cpu.ProgramCounter++];
            ushort baseAddr = Read16(cpu, zeroPageAddr);
            cpu.Memory[baseAddr + cpu.IndexerY.Value] = value;
        }
    }
}
