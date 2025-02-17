using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502
{
    public static class Constants
    {
        public const byte NEGATIVE_FLAG = 0x80;
        public const ushort PAGE_BOUNDARY = 0xFF00;

        public static class Processor
        {
            public const UInt16 RAM_ADDRESS = 0x0000;
            public const UInt16 IO_ADDRESS = 0x2000;
            public const UInt16 PPU_CONTROL_REGISTER_1_ADDRESS = 0x2000;
            public const UInt16 PPU_CONTROL_REGISTER_2_ADDRESS = 0x2001;
            public const UInt16 PPU_STATUS_REGISTER_ADDRESS = 0x2002;
            public const UInt16 SPRITE_MEMORY_ADDRESS = 0x2003;
            public const UInt16 SPRITE_MEMORY_DATA_ADDRESS = 0x2004;
            public const UInt16 SPRITE_SCROLL_OFFSETS_ADDRESS = 0x2005;
            public const UInt16 EXPANSION_MODULES_ADDRESS = 0x5000;
            public const UInt16 CARTRIDGE_RAM_ADDRESS = 0x6000;
            public const UInt16 CARTRIDGE_ROM_LOWER_BANK_ADDRESS = 0x8000;
            public const UInt16 CARTRIDGE_ROM_UPPER_BANK_ADDRESS = 0xC000;
            public const UInt32 NTSC_CLOCK_SPEED = 1_790_000;
            public const UInt16 STACK_POINTER_START_ADDRESS = 0x01FF;
        }
    }
}
