using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;
using System.Diagnostics;


namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class IndirectAddressingMode : AddressingModeBase
    {
        public override byte Fetch(IProcessor cpu)
        {
            var address = FetchDWord(cpu);
            return cpu.Memory[address];
        }

        public override void Store(IProcessor cpu, byte value)
        {
            var address = FetchDWord(cpu);
            cpu.Memory[address] = value;
        }

        public override DWord6502 FetchDWord(IProcessor cpu)
        {
            var pointer = AddressingModeBase.Read16(cpu);

            Debug.WriteLine($"{nameof(IndirectAddressingMode)}::{nameof(FetchDWord)} | {nameof(pointer)}: {pointer}");

            if ((pointer & 0x00FF) == 0xFF) // Handle 6502 page boundary bug using Constants.PAGE_BOUNDARY
            {
                var lowByte = cpu.Memory[pointer];
                var highByte = cpu.Memory[(ushort)(pointer & Constants.PAGE_BOUNDARY)];
                Debug.WriteLine($"{nameof(IndirectAddressingMode)}::{nameof(FetchDWord)} | Page Boundary Bug Triggered! Low Byte: 0x{lowByte:X2}, High Byte: 0x{highByte:X2}");

                return new DWord6502(lowByte, highByte);
            }

            var resolvedAddress = AddressingModeBase.Read16(cpu, pointer);
            Debug.WriteLine($"{nameof(IndirectAddressingMode)}::{nameof(FetchDWord)} | Normal Read: {resolvedAddress}");
            return resolvedAddress;
        }
    }
}
