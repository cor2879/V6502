﻿namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums
{
    public enum OpCodes : byte
    {
        // Arithmetic Instructions
        AdcAbsolute = 0x6D,
        AdcAbsoluteX = 0x7D,
        AdcAbsoluteY = 0x79,
        AdcImmediate = 0x69,
        AdcIndexedIndirect = 0x61,
        AdcIndirectIndexed = 0x71,
        AdcZeroPage = 0x65,
        AdcZeroPageX = 0x75,

        SBC_Immediate = 0xE9,
        SBC_ZeroPage = 0xE5,

        // Branch Instructions
        BeqRelative = 0xF0,
        BneRelative = 0xD0,
        BplRelative = 0x10,

        // Load Instructions
        LdaAbsolute = 0xAD,
        LdaAbsoluteX = 0xBD,
        LdaAbsoluteY = 0xB9,
        LdaImmediate = 0xA9,
        LdaIndexedIndirect = 0xA1,
        LdaIndirectIndexed = 0xB1,
        LdaZeroPage = 0xA5,
        LdaZeroPageX = 0xB5,
        LdxAbsolute = 0xAE,
        LdxAbsoluteY = 0xBE,
        LdxImmediate = 0xA2,
        LdxZeroPage = 0xA6,
        LdxZeroPageY = 0xB6,
        LdyAbsolute = 0xAC,
        LdyAbsoluteX = 0xBC,
        LdyImmediate = 0xA0,
        LdyZeroPage = 0xA4,
        LdyZeroPageX = 0xB4,

        // Other
        NOP = 0xEA,

        SbcAbsolute = 0xED,
        SbcAbsoluteX = 0xFD,
        SbcAbsoluteY = 0xF9,
        SbcImmediate = 0xE9,
        SbcIndexedIndirect = 0xE1,  // (Indirect, X)
        SbcIndirectIndexed = 0xF1,   // (Indirect), Y
        SbcZeroPage = 0xE5,
        SbcZeroPageX = 0xF5,

        // Store Instructions
        StaAbsolute = 0x8D,
        StaAbsoluteX = 0x9D,
        StaAbsoluteY = 0x99,
        StaIndexedIndirect = 0x81,
        StaIndirectIndexed = 0x91,
        StaZeroPage = 0x85,
        StaZeroPageX = 0x95,
        StxAbsolute = 0x8E,
        StxZeroPage = 0x86,
        StxZeroPageY = 0x96,
        StyAbsolute = 0x8C,
        StyZeroPage = 0x84,
        StyZeroPageX = 0x94,
    };
}
