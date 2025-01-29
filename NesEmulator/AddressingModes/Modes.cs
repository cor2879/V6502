namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public static class Modes
    {
        public static readonly ImmediateAddressingMode Immediate = new ImmediateAddressingMode();
        public static readonly ZeroPageAddressingMode ZeroPage = new ZeroPageAddressingMode();
        public static readonly ZeroPageXAddressingMode ZeroPageX = new ZeroPageXAddressingMode();
        public static readonly ZeroPageYAddressingMode ZeroPageY = new ZeroPageYAddressingMode();
        public static readonly AbsoluteAddressingMode Absolute = new AbsoluteAddressingMode();
        public static readonly AbsoluteXAddressingMode AbsoluteX = new AbsoluteXAddressingMode();
        public static readonly AbsoluteYAddressingMode AbsoluteY = new AbsoluteYAddressingMode();
        public static readonly IndexedIndirectAddressingMode IndexedIndirect = new IndexedIndirectAddressingMode();
        public static readonly IndirectIndexedAddressingMode IndirectIndexed = new IndirectIndexedAddressingMode();
    }
}
