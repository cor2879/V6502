using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions;
using System.Security.Cryptography;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet
{
    public class InstructionRegistry
    {
        private static readonly InstructionRegistry _instance = new InstructionRegistry();

        private readonly Dictionary<OpCodes, InstructionBase> _instructions = LoadInstructions();

        private InstructionRegistry() { }

        public static InstructionRegistry Instance => _instance;

        public bool TryGetInstruction(OpCodes opCode, out InstructionBase instruction)
        {
            return _instructions.TryGetValue(opCode, out instruction);
        }

        public bool TryGetInstruction(byte opCode, out InstructionBase instruction)
        {
            return _instructions.TryGetValue((OpCodes)opCode, out instruction);
        }

        private static Dictionary<OpCodes, InstructionBase> LoadInstructions()
        {
            var instructions = new Dictionary<OpCodes, InstructionBase>()
            {
                { OpCodes.AdcImmediate, new AdcInstruction((byte)OpCodes.AdcImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.AdcZeroPage, new AdcInstruction((byte)OpCodes.AdcZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.AdcZeroPageX, new AdcInstruction((byte)OpCodes.AdcZeroPageX, Modes.ZeroPageX, 2, 4) },
                { OpCodes.AdcAbsolute, new AdcInstruction((byte)OpCodes.AdcAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.AdcAbsoluteX, new AdcInstruction((byte)OpCodes.AdcAbsoluteX, Modes.AbsoluteX, 3, 4) }, // +1 if page crossed
                { OpCodes.AdcAbsoluteY, new AdcInstruction((byte)OpCodes.AdcAbsoluteY, Modes.AbsoluteY, 3, 4) }, // +1 if page crossed
                { OpCodes.AdcIndexedIndirect, new AdcInstruction((byte)OpCodes.AdcIndexedIndirect, Modes.IndexedIndirect, 2, 6) },
                { OpCodes.AdcIndirectIndexed, new AdcInstruction((byte)OpCodes.AdcIndirectIndexed, Modes.IndirectIndexed, 2, 5) }, // +1 if page crossed
                { OpCodes.LdaAbsolute, new LdaInstruction((byte)OpCodes.LdaAbsolute, Modes.Absolute, 3, 4 ) },
                { OpCodes.LdaAbsoluteX, new LdaInstruction((byte)OpCodes.LdaAbsoluteX, Modes.AbsoluteX, 3, 4) },
                { OpCodes.LdaAbsoluteY, new LdaInstruction((byte)OpCodes.LdaAbsoluteY, Modes.AbsoluteY, 3, 4) },
                { OpCodes.LdaImmediate, new LdaInstruction((byte)OpCodes.LdaImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.LdaIndexedIndirect, new LdaInstruction((byte)OpCodes.LdaIndexedIndirect, Modes.IndexedIndirect, 2, 6) },
                { OpCodes.LdaIndirectIndexed, new LdaInstruction((byte)OpCodes.LdaIndirectIndexed, Modes.IndirectIndexed, 2, 5) },
                { OpCodes.LdaZeroPage, new LdaInstruction((byte)OpCodes.LdaZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.LdaZeroPageX, new LdaInstruction((byte)OpCodes.LdaZeroPageX, Modes.ZeroPageX, 2, 4) },
                { OpCodes.LdxAbsolute, new LdxInstruction((byte)OpCodes.LdxAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.LdxAbsoluteY, new LdxInstruction((byte)OpCodes.LdxAbsoluteY, Modes.AbsoluteY, 3, 4) },
                { OpCodes.LdxImmediate, new LdxInstruction((byte)OpCodes.LdxImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.LdxZeroPage, new LdxInstruction((byte)OpCodes.LdxZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.LdxZeroPageY, new LdxInstruction((byte)OpCodes.LdxZeroPageY, Modes.ZeroPageY, 2, 4) },
                { OpCodes.LdyAbsolute, new LdyInstruction((byte)OpCodes.LdyAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.LdyAbsoluteX, new LdyInstruction((byte)OpCodes.LdyAbsoluteX, Modes.AbsoluteX, 3, 4) },
                { OpCodes.LdyImmediate, new LdyInstruction((byte)OpCodes.LdyImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.LdyZeroPage, new LdyInstruction((byte)OpCodes.LdyZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.LdyZeroPageX, new LdyInstruction((byte)OpCodes.LdyZeroPageX, Modes.ZeroPageX, 2, 4) },
                { OpCodes.SbcImmediate, new SbcInstruction((byte)OpCodes.SbcImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.SbcZeroPage, new SbcInstruction((byte)OpCodes.SbcZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.SbcZeroPageX, new SbcInstruction((byte)OpCodes.SbcZeroPageX, Modes.ZeroPageX, 2, 4) },
                { OpCodes.SbcAbsolute, new SbcInstruction((byte)OpCodes.SbcAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.SbcAbsoluteX, new SbcInstruction((byte)OpCodes.SbcAbsoluteX, Modes.AbsoluteX, 3, 4) }, // +1 if page crossed
                { OpCodes.SbcAbsoluteY, new SbcInstruction((byte)OpCodes.SbcAbsoluteY, Modes.AbsoluteY, 3, 4) }, // +1 if page crossed
                { OpCodes.SbcIndexedIndirect, new SbcInstruction((byte)OpCodes.SbcIndexedIndirect, Modes.IndexedIndirect, 2, 6) },
                { OpCodes.SbcIndirectIndexed, new SbcInstruction((byte)OpCodes.SbcIndirectIndexed, Modes.IndirectIndexed, 2, 5) }, // +1 if page crossed
                { OpCodes.StaAbsolute, new StaInstruction((byte)OpCodes.StaAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.StaAbsoluteX, new StaInstruction((byte)OpCodes.StaAbsoluteX, Modes.AbsoluteX, 3, 5) },
                { OpCodes.StaAbsoluteY, new StaInstruction((byte)OpCodes.StaAbsoluteY, Modes.AbsoluteY, 3, 5) },
                { OpCodes.StaIndexedIndirect, new StaInstruction((byte)OpCodes.StaIndexedIndirect, Modes.IndexedIndirect, 2, 6) },
                { OpCodes.StaIndirectIndexed, new StaInstruction((byte)OpCodes.StaIndirectIndexed, Modes.IndirectIndexed, 2, 6) },
                { OpCodes.StaZeroPage, new StaInstruction((byte)OpCodes.StaZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.StaZeroPageX, new StaInstruction((byte)OpCodes.StaZeroPageX, Modes.ZeroPageX, 2, 4) },
                { OpCodes.StxAbsolute, new StxInstruction((byte)OpCodes.StxAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.StxZeroPage, new StxInstruction((byte)OpCodes.StxZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.StxZeroPageY, new StxInstruction((byte)OpCodes.StxZeroPageY, Modes.ZeroPageY, 2, 4) },
                { OpCodes.StyAbsolute, new StyInstruction((byte)OpCodes.StyAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.StyZeroPage, new StyInstruction((byte)OpCodes.StyZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.StyZeroPageX, new StyInstruction((byte)OpCodes.StyZeroPageX, Modes.ZeroPageX, 2, 4) },
            };

            return instructions;
        }
    }
}
