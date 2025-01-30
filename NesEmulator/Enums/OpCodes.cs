namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums
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

        // AND (Logical AND)
        AndAbsolute = 0x2D,
        AndAbsoluteX = 0x3D,
        AndAbsoluteY = 0x39,
        AndImmediate = 0x29,
        AndIndexedIndirect = 0x21,  // (Indirect, X)
        AndIndirectIndexed = 0x31,  // (Indirect), Y
        AndZeroPage = 0x25,
        AndZeroPageX = 0x35,

        // Branch Instructions
        BeqRelative = 0xF0,
        BneRelative = 0xD0,
        BplRelative = 0x10,

        // DEC (Decrement Memory)
        DecAbsolute = 0xCE,
        DecAbsoluteX = 0xDE,
        DecZeroPage = 0xC6,
        DecZeroPageX = 0xD6,

        // DEX (Decrement X Register)
        DexImplied = 0xCA,

        // DEY (Decrement Y Register)
        DeyImplied = 0x88,

        // EOR (Logical XOR)
        EorAbsolute = 0x4D,
        EorAbsoluteX = 0x5D,
        EorAbsoluteY = 0x59,
        EorImmediate = 0x49,
        EorIndexedIndirect = 0x41,  // (Indirect, X)
        EorIndirectIndexed = 0x51,   // (Indirect), Y
        EorZeroPage = 0x45,
        EorZeroPageX = 0x55,

        // INC (Increment Memory)
        IncAbsolute = 0xEE,
        IncAbsoluteX = 0xFE,
        IncZeroPage = 0xE6,
        IncZeroPageX = 0xF6,

        // INX (Increment X Register)
        InxImplied = 0xE8,

        // INY (Increment Y Register)
        InyImplied = 0xC8,

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

        // ORA (Logical OR)
        OraAbsolute = 0x0D,
        OraAbsoluteX = 0x1D,
        OraAbsoluteY = 0x19,
        OraImmediate = 0x09,
        OraIndexedIndirect = 0x01,  // (Indirect, X)
        OraIndirectIndexed = 0x11,  // (Indirect), Y
        OraZeroPage = 0x05,
        OraZeroPageX = 0x15,

        // SBC Instructions (Subtract)
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
