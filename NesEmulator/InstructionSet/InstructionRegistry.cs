using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet
{
    public class InstructionRegistry
    {
        private static readonly InstructionRegistry _instance = new InstructionRegistry();

        private readonly Dictionary<byte, InstructionBase> _instructions = LoadInstructions();

        private InstructionRegistry() { }

        public static InstructionRegistry Instance => _instance;

        private void RegisterInstruction(InstructionBase instruction)
        {
            _instructions[instruction.OpCode] = instruction;
        }

        public bool TryGetInstruction(byte opCode, out InstructionBase instruction)
        {
            return _instructions.TryGetValue(opCode, out instruction);
        }

        private static Dictionary<byte, InstructionBase> LoadInstructions()
        {
            var instructions = new Dictionary<byte, InstructionBase>();

            // TODO: Load Instructions
            return instructions;
        }
    }
}
