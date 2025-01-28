using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Emulators.Mso6502
{
    public class InstructionSet
    {
        #region Fields

        public static readonly InstructionSet Instance = LoadInstructionSet();

        private Dictionary<Byte, InstructionBase> _instructionTable = new Dictionary<Byte, InstructionBase>(255);

        #endregion

        #region Constructors

        private InstructionSet()
        { 
            Initialize();
        }

        #endregion

        #region Properties



        #endregion

        #region Methods

        public void Invoke(Byte opCode, Cpu6502 cpu)
        {
            _instructionTable[opCode].Invoke(cpu);
        }

        private void Initialize()
        { }

        private static InstructionSet LoadInstructionSet()
        {
            IEnumerable<Type> instructionTypes = Assembly.GetExecutingAssembly().GetTypes().Where(i => i.IsSubclassOf(typeof(InstructionBase)));

            var instructionSet = new InstructionSet();

            foreach (var instructionType in instructionTypes)
            {
                var instruction = (InstructionBase)instructionType.GetConstructor(new Type[] { }).Invoke(null);

                instructionSet.AddInstruction(instruction);
            }

            return instructionSet;
        }

        private void AddInstruction(InstructionBase instruction)
        {
            if (_instructionTable.ContainsKey(instruction.OpCode))
            {
                throw new InstructionSetOverload6502Exception(_instructionTable[instruction.OpCode], instruction);
            }

            _instructionTable[instruction.OpCode] = instruction;
        }

        #endregion
    }
}