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

        Bcc = 0x90, // Branch on Carry Clear
        Bcs = 0xB0, // Branch on Carry Set
        Beq = 0xF0, // Branch on Equal (Zero Flag Set)
        Bmi = 0x30, // Branch on Minus (Negative Flag Set)
        Bne = 0xD0, // Branch on Not Equal (Zero Flag Clear)
        Bpl = 0x10, // Branch on Plus (Negative Flag Clear)
        Bvc = 0x50, // Branch on Overflow Clear
        Bvs = 0x70,  // Branch on Overflow Set

        // CMP (Compare Accumulator)
        CmpAbsolute = 0xCD,
        CmpAbsoluteX = 0xDD,
        CmpAbsoluteY = 0xD9,
        CmpImmediate = 0xC9,
        CmpIndexedIndirect = 0xC1,
        CmpIndirectIndexed = 0xD1,
        CmpZeroPage = 0xC5,
        CmpZeroPageX = 0xD5,

        // CPX (Compare X Register)
        CpxAbsolute = 0xEC,
        CpxImmediate = 0xE0,
        CpxZeroPage = 0xE4,

        // CPY (Compare Y Register)
        CpyAbsolute = 0xCC,
        CpyImmediate = 0xC0,
        CpyZeroPage = 0xC4,

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

        // JUMP (to Subroutine)
        Jsr = 0x20,

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

        // Push and Pull
        Pha = 0x48,
        Php = 0x08,
        Pla = 0x68,
        Plp = 0x28,

        // Return (from Subroutine or Interrupt)
        Rts = 0x60,
        Rti = 0x40,

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
