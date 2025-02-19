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

        public override DWord6502 FetchDWord(IProcessor cpu)
        {
            // Fetch Zero Page address and add X register
            byte zeroPageAddress = (byte)(cpu.Memory[cpu.ProgramCounter++] + cpu.IndexerX.Value);

            // Read 16-bit address from Zero Page
            byte lowByte = cpu.Memory[zeroPageAddress];
            byte highByte = cpu.Memory[(byte)(zeroPageAddress + 1)];

            // Construct and return DWord6502
            return new DWord6502(lowByte, highByte);
        }

    }
}
