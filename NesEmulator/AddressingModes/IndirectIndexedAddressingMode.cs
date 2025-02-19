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
            var zeroPageAddr = cpu.Memory[cpu.ProgramCounter++];
            var baseAddr = Read16(cpu, zeroPageAddr);
            cpu.Memory[baseAddr + cpu.IndexerY.Value] = value;
        }

        public override DWord6502 FetchDWord(IProcessor cpu)
        {
            // Fetch Zero-Page base address
            var zeroPageAddress = cpu.Memory[cpu.ProgramCounter++];

            // Read the low and high bytes from Zero-Page
            var lowByte = cpu.Memory[zeroPageAddress];
            var highByte = cpu.Memory[(byte)(zeroPageAddress + 1)];

            // Construct DWord6502 from low and high bytes
            var baseAddress = new DWord6502(lowByte, highByte);

            // Add the Y register value to the address
            baseAddress.Value += cpu.IndexerY.Value;

            return baseAddress;
        }

    }
}
