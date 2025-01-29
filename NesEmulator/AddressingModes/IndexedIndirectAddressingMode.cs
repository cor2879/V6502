using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class IndexedIndirectAddressingMode : AddressingModeBase
    {
        public override byte Fetch(IProcessor cpu)
        {
            var zeroPageAddr = (byte)(cpu.Memory[cpu.ProgramCounter++] + cpu.IndexerX.Value);
            var effectiveAddr = Read16(cpu, zeroPageAddr);
            return cpu.Memory[effectiveAddr];
        }

        public override void Store(IProcessor cpu, byte value)
        {
            byte zeroPageAddr = (byte)(cpu.Memory[cpu.ProgramCounter++] + cpu.IndexerX.Value);
            ushort effectiveAddr = Read16(cpu, zeroPageAddr);
            cpu.Memory[effectiveAddr] = value;
        }

        private DWord6502 Read16(IProcessor cpu, byte address)
        {
            return new DWord6502(cpu.Memory[address], cpu.Memory[(address + 1) & 0xFF]);
        }
    }
}
