﻿namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public static class Modes
    {
        public static readonly AbsoluteAddressingMode Absolute = new AbsoluteAddressingMode();
        public static readonly AbsoluteXAddressingMode AbsoluteX = new AbsoluteXAddressingMode();
        public static readonly AbsoluteYAddressingMode AbsoluteY = new AbsoluteYAddressingMode();
        public static readonly AccumulatorAddressingMode Accumulator = new AccumulatorAddressingMode();
        public static readonly ImmediateAddressingMode Immediate = new ImmediateAddressingMode();
        public static readonly ImpliedAddressingMode Implied = new ImpliedAddressingMode();
        public static readonly IndexedIndirectAddressingMode IndexedIndirect = new IndexedIndirectAddressingMode();
        public static readonly IndirectIndexedAddressingMode IndirectIndexed = new IndirectIndexedAddressingMode();
        public static readonly IndirectAddressingMode Indirect = new IndirectAddressingMode();
        public static readonly RelativeAddressingMode Relative = new RelativeAddressingMode();
        public static readonly ZeroPageAddressingMode ZeroPage = new ZeroPageAddressingMode();
        public static readonly ZeroPageXAddressingMode ZeroPageX = new ZeroPageXAddressingMode();
        public static readonly ZeroPageYAddressingMode ZeroPageY = new ZeroPageYAddressingMode();
    }
}
