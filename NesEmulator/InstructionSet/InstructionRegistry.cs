using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions.OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions;

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
                // ADD
                { OpCodes.AdcImmediate, new AdcInstruction((byte)OpCodes.AdcImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.AdcZeroPage, new AdcInstruction((byte)OpCodes.AdcZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.AdcZeroPageX, new AdcInstruction((byte)OpCodes.AdcZeroPageX, Modes.ZeroPageX, 2, 4) },
                { OpCodes.AdcAbsolute, new AdcInstruction((byte)OpCodes.AdcAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.AdcAbsoluteX, new AdcInstruction((byte)OpCodes.AdcAbsoluteX, Modes.AbsoluteX, 3, 4) }, // +1 if page crossed
                { OpCodes.AdcAbsoluteY, new AdcInstruction((byte)OpCodes.AdcAbsoluteY, Modes.AbsoluteY, 3, 4) }, // +1 if page crossed
                { OpCodes.AdcIndexedIndirect, new AdcInstruction((byte)OpCodes.AdcIndexedIndirect, Modes.IndexedIndirect, 2, 6) },
                { OpCodes.AdcIndirectIndexed, new AdcInstruction((byte)OpCodes.AdcIndirectIndexed, Modes.IndirectIndexed, 2, 5) }, // +1 if page crossed

                // ALR (AND SHIFT RIGHT)
                { OpCodes.Alr, new AlrInstruction(Modes.Immediate, 2, 3) },

                // ANC
                { OpCodes.Anc, new AncInstruction(Modes.Immediate, 2, 3) },

                // AND
                { OpCodes.AndImmediate, new AndInstruction((byte)OpCodes.AndImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.AndAbsolute, new AndInstruction((byte)OpCodes.AndAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.AndAbsoluteX, new AndInstruction((byte)OpCodes.AndAbsoluteX, Modes.AbsoluteX, 3, 4) }, // +1 if page crossed
                { OpCodes.AndAbsoluteY, new AndInstruction((byte)OpCodes.AndAbsoluteY, Modes.AbsoluteY, 3, 4) }, // +1 if page crossed
                { OpCodes.AndIndexedIndirect, new AndInstruction((byte)OpCodes.AndIndexedIndirect, Modes.IndexedIndirect, 2, 6) },
                { OpCodes.AndIndirectIndexed, new AndInstruction((byte)OpCodes.AndIndirectIndexed, Modes.IndirectIndexed, 2, 5) }, // +1 if page crossed
                { OpCodes.AndZeroPage, new AndInstruction((byte)OpCodes.AndZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.AndZeroPageX, new AndInstruction((byte)OpCodes.AndZeroPageX, Modes.ZeroPageX, 2, 4) },

                // ARR (AND ROTATE RIGHT)
                { OpCodes.Arr, new ArrInstruction(Modes.Immediate, 2, 3) },

                // Arithmetic Shift Left
                { OpCodes.AslAccumulator, new AslInstruction((byte)OpCodes.AslAccumulator, Modes.Accumulator, 1, 2) },
                { OpCodes.AslZeroPage, new AslInstruction((byte)OpCodes.AslZeroPage, Modes.ZeroPage, 2, 5) },
                { OpCodes.AslZeroPageX, new AslInstruction((byte)OpCodes.AslZeroPageX, Modes.ZeroPageX, 2, 6) },
                { OpCodes.AslAbsolute, new AslInstruction((byte)OpCodes.AslAbsolute, Modes.Absolute, 3, 6) },
                { OpCodes.AslAbsoluteX, new AslInstruction((byte)OpCodes.AslAbsoluteX, Modes.AbsoluteX, 3, 7) },

                { OpCodes.Bcc, new BccInstruction() },
                { OpCodes.Bcs, new BcsInstruction() },
                { OpCodes.Beq, new BeqInstruction() },
                { OpCodes.Bmi, new BmiInstruction() },
                { OpCodes.Bne, new BneInstruction() },
                { OpCodes.Bpl, new BplInstruction() },
                { OpCodes.Bvc, new BvcInstruction() },
                { OpCodes.Bvs, new BvsInstruction() },

                // BIT
                { OpCodes.BitZeroPage, new BitInstruction((byte)OpCodes.BitZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.BitAbsolute, new BitInstruction((byte)OpCodes.BitAbsolute, Modes.Absolute, 3, 4) },

                // BRK
                { OpCodes.Brk, new BrkInstruction() },

                // CL (Clear Status)
                { OpCodes.CLC, new ClcInstruction() },
                { OpCodes.CLD, new CldInstruction() },
                { OpCodes.CLI, new CliInstruction() },
                { OpCodes.CLV, new ClvInstruction() },

                // CMP (Compare Accumulator)
                { OpCodes.CmpAbsolute, new CmpInstruction((byte)OpCodes.CmpAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.CmpAbsoluteX, new CmpInstruction((byte)OpCodes.CmpAbsoluteX, Modes.AbsoluteX, 3, 4) }, // +1 if page crossed
                { OpCodes.CmpAbsoluteY, new CmpInstruction((byte)OpCodes.CmpAbsoluteY, Modes.AbsoluteY, 3, 4) }, // +1 if page crossed
                { OpCodes.CmpImmediate, new CmpInstruction((byte)OpCodes.CmpImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.CmpIndexedIndirect, new CmpInstruction((byte)OpCodes.CmpIndexedIndirect, Modes.IndexedIndirect, 2, 6) },
                { OpCodes.CmpIndirectIndexed, new CmpInstruction((byte)OpCodes.CmpIndirectIndexed, Modes.IndirectIndexed, 2, 5) }, // +1 if page crossed
                { OpCodes.CmpZeroPage, new CmpInstruction((byte)OpCodes.CmpZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.CmpZeroPageX, new CmpInstruction((byte)OpCodes.CmpZeroPageX, Modes.ZeroPageX, 2, 4) },

                // CPX (Compare X Register)
                { OpCodes.CpxAbsolute, new CpxInstruction((byte)OpCodes.CpxAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.CpxImmediate, new CpxInstruction((byte)OpCodes.CpxImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.CpxZeroPage, new CpxInstruction((byte)OpCodes.CpxZeroPage, Modes.ZeroPage, 2, 3) },

                // CPY (Compare Y Register)
                { OpCodes.CpyAbsolute, new CpyInstruction((byte)OpCodes.CpyAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.CpyImmediate, new CpyInstruction((byte)OpCodes.CpyImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.CpyZeroPage, new CpyInstruction((byte)OpCodes.CpyZeroPage, Modes.ZeroPage, 2, 3) },

                { OpCodes.DecAbsolute, new DecInstruction((byte)OpCodes.DecAbsolute, Modes.Absolute, 3, 6) },
                { OpCodes.DecAbsoluteX, new DecInstruction((byte)OpCodes.DecAbsoluteX, Modes.AbsoluteX, 3, 7) },
                { OpCodes.DecZeroPage, new DecInstruction((byte)OpCodes.DecZeroPage, Modes.ZeroPage, 2, 5) },
                { OpCodes.DecZeroPageX, new DecInstruction((byte)OpCodes.DecZeroPageX, Modes.ZeroPageX, 2, 6) },
                { OpCodes.Dex, new DexInstruction((byte)OpCodes.Dex, Modes.Implied, 1, 2) },
                { OpCodes.Dey, new DeyInstruction((byte)OpCodes.Dey, Modes.Implied, 1, 2) },

                // Double NOP
                { OpCodes.DOP, new DopInstruction(Modes.Immediate, 2, 3) },

                { OpCodes.EorAbsolute, new EorInstruction((byte)OpCodes.EorAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.EorAbsoluteX, new EorInstruction((byte)OpCodes.EorAbsoluteX, Modes.AbsoluteX, 3, 4) }, // +1 if page crossed
                { OpCodes.EorAbsoluteY, new EorInstruction((byte)OpCodes.EorAbsoluteY, Modes.AbsoluteY, 3, 4) }, // +1 if page crossed
                { OpCodes.EorImmediate, new EorInstruction((byte)OpCodes.EorImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.EorIndexedIndirect, new EorInstruction((byte)OpCodes.EorIndexedIndirect, Modes.IndexedIndirect, 2, 6) },
                { OpCodes.EorIndirectIndexed, new EorInstruction((byte)OpCodes.EorIndirectIndexed, Modes.IndirectIndexed, 2, 5) }, // +1 if page crossed
                { OpCodes.EorZeroPage, new EorInstruction((byte)OpCodes.EorZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.EorZeroPageX, new EorInstruction((byte)OpCodes.EorZeroPageX, Modes.ZeroPageX, 2, 4) },
                { OpCodes.IncAbsolute, new IncInstruction((byte)OpCodes.IncAbsolute, Modes.Absolute, 3, 6) },
                { OpCodes.IncAbsoluteX, new IncInstruction((byte)OpCodes.IncAbsoluteX, Modes.AbsoluteX, 3, 7) },
                { OpCodes.IncZeroPage, new IncInstruction((byte)OpCodes.IncZeroPage, Modes.ZeroPage, 2, 5) },
                { OpCodes.IncZeroPageX, new IncInstruction((byte)OpCodes.IncZeroPageX, Modes.ZeroPageX, 2, 6) },
                { OpCodes.Inx, new InxInstruction((byte)OpCodes.Inx, Modes.Implied, 1, 2) },
                { OpCodes.Iny, new InyInstruction((byte)OpCodes.Iny, Modes.Implied, 1, 2) },

                // JMP
                { OpCodes.JmpA, new JmpInstruction((byte)OpCodes.JmpA, Modes.Absolute) },
                { OpCodes.JmpI, new JmpInstruction((byte)OpCodes.JmpI, Modes.Indirect) },

                // Jump to Subroutine
                { OpCodes.Jsr, new JsrInstruction() },

                // LAX Instructions (Load Accumulator and X)
                { OpCodes.LaxImmediate, new LaxInstruction((byte)OpCodes.LaxImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.LaxZeroPage, new LaxInstruction((byte)OpCodes.LaxZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.LaxZeroPageY, new LaxInstruction((byte)OpCodes.LaxZeroPageY, Modes.ZeroPageY, 2, 4) },
                { OpCodes.LaxAbsolute, new LaxInstruction((byte)OpCodes.LaxAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.LaxAbsoluteY, new LaxInstruction((byte)OpCodes.LaxAbsoluteY, Modes.AbsoluteY, 3, 4) }, // Extra cycle if page boundary crossed
                { OpCodes.LaxIndexedIndirect, new LaxInstruction((byte)OpCodes.LaxIndexedIndirect, Modes.IndexedIndirect, 2, 6) },

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

                // Logical Shift Right
                { OpCodes.LsrAccumulator, new LsrInstruction((byte)OpCodes.LsrAccumulator, Modes.Accumulator, 1, 2) },
                { OpCodes.LsrZeroPage, new LsrInstruction((byte)OpCodes.LsrZeroPage, Modes.ZeroPage, 2, 5) },
                { OpCodes.LsrZeroPageX, new LsrInstruction((byte)OpCodes.LsrZeroPageX, Modes.ZeroPageX, 2, 6) },
                { OpCodes.LsrAbsolute, new LsrInstruction((byte)OpCodes.LsrAbsolute, Modes.Absolute, 3, 6) },
                { OpCodes.LsrAbsoluteX, new LsrInstruction((byte)OpCodes.LsrAbsoluteX, Modes.AbsoluteX, 3, 7) },

                // NO OP
                { OpCodes.NOP, new NopInstruction(Modes.Implied, 1, 2) },

                { OpCodes.OraAbsolute, new OraInstruction((byte)OpCodes.OraAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.OraAbsoluteX, new OraInstruction((byte)OpCodes.OraAbsoluteX, Modes.AbsoluteX, 3, 4) }, // +1 if page crossed
                { OpCodes.OraAbsoluteY, new OraInstruction((byte)OpCodes.OraAbsoluteY, Modes.AbsoluteY, 3, 4) }, // +1 if page crossed
                { OpCodes.OraImmediate, new OraInstruction((byte)OpCodes.OraImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.OraIndexedIndirect, new OraInstruction((byte)OpCodes.OraIndexedIndirect, Modes.IndexedIndirect, 2, 6) },
                { OpCodes.OraIndirectIndexed, new OraInstruction((byte)OpCodes.OraIndirectIndexed, Modes.IndirectIndexed, 2, 5) }, // +1 if page crossed
                { OpCodes.OraZeroPage, new OraInstruction((byte)OpCodes.OraZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.OraZeroPageX, new OraInstruction((byte)OpCodes.OraZeroPageX, Modes.ZeroPageX, 2, 4) },

                // Push and Pull
                { OpCodes.Pha, new PhaInstruction() },
                { OpCodes.Php, new PhpInstruction() },
                { OpCodes.Pla, new PlaInstruction() },
                { OpCodes.Plp, new PlpInstruction() },

                // Rotate Left
                { OpCodes.RolAccumulator, new RolInstruction((byte)OpCodes.RolAccumulator, Modes.Accumulator, 1, 2) },
                { OpCodes.RolZeroPage, new RolInstruction((byte)OpCodes.RolZeroPage, Modes.ZeroPage, 2, 5) },
                { OpCodes.RolZeroPageX, new RolInstruction((byte)OpCodes.RolZeroPageX, Modes.ZeroPageX, 2, 6) },
                { OpCodes.RolAbsolute, new RolInstruction((byte)OpCodes.RolAbsolute, Modes.Absolute, 3, 6) },
                { OpCodes.RolAbsoluteX, new RolInstruction((byte)OpCodes.RolAbsoluteX, Modes.AbsoluteX, 3, 7) },

                // Rotate Right
                { OpCodes.RorAccumulator, new RorInstruction((byte)OpCodes.RorAccumulator, Modes.Accumulator, 1, 2) },
                { OpCodes.RorZeroPage, new RorInstruction((byte)OpCodes.RorZeroPage, Modes.ZeroPage, 2, 5) },
                { OpCodes.RorZeroPageX, new RorInstruction((byte)OpCodes.RorZeroPageX, Modes.ZeroPageX, 2, 6) },
                { OpCodes.RorAbsolute, new RorInstruction((byte)OpCodes.RorAbsolute, Modes.Absolute, 3, 6) },
                { OpCodes.RorAbsoluteX, new RorInstruction((byte)OpCodes.RorAbsoluteX, Modes.AbsoluteX, 3, 7) },

                // Return from Subroutine\Interupt
                { OpCodes.Rts, new RtsInstruction() },
                { OpCodes.Rti, new RtiInstruction() },

                // SB (Subtract)
                { OpCodes.SbcImmediate, new SbcInstruction((byte)OpCodes.SbcImmediate, Modes.Immediate, 2, 2) },
                { OpCodes.SbcZeroPage, new SbcInstruction((byte)OpCodes.SbcZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.SbcZeroPageX, new SbcInstruction((byte)OpCodes.SbcZeroPageX, Modes.ZeroPageX, 2, 4) },
                { OpCodes.SbcAbsolute, new SbcInstruction((byte)OpCodes.SbcAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.SbcAbsoluteX, new SbcInstruction((byte)OpCodes.SbcAbsoluteX, Modes.AbsoluteX, 3, 4) }, // +1 if page crossed
                { OpCodes.SbcAbsoluteY, new SbcInstruction((byte)OpCodes.SbcAbsoluteY, Modes.AbsoluteY, 3, 4) }, // +1 if page crossed
                { OpCodes.SbcIndexedIndirect, new SbcInstruction((byte)OpCodes.SbcIndexedIndirect, Modes.IndexedIndirect, 2, 6) },
                { OpCodes.SbcIndirectIndexed, new SbcInstruction((byte)OpCodes.SbcIndirectIndexed, Modes.IndirectIndexed, 2, 5) }, // +1 if page crossed

                // SAX Instructions (Store A & X)
                { OpCodes.SaxZeroPage, new SaxInstruction((byte)OpCodes.SaxZeroPage, Modes.ZeroPage, 2, 3) },
                { OpCodes.SaxZeroPageY, new SaxInstruction((byte)OpCodes.SaxZeroPageY, Modes.ZeroPageY, 2, 4) },
                { OpCodes.SaxAbsolute, new SaxInstruction((byte)OpCodes.SaxAbsolute, Modes.Absolute, 3, 4) },
                { OpCodes.SaxIndexedIndirect, new SaxInstruction((byte)OpCodes.SaxIndexedIndirect, Modes.IndexedIndirect, 2, 6) },

                // SBX
                { OpCodes.Sbx, new SbxInstruction() },

                // SE (Set Status)
                { OpCodes.SEC, new SecInstruction() },
                { OpCodes.SED, new SedInstruction() },
                { OpCodes.SEI, new SeiInstruction() },

                // ST (Store)
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

                // Triple NOP
                { OpCodes.TOP, new TopInstruction(Modes.Absolute, 3, 4) },
            };

            return instructions;
        }
    }
}
