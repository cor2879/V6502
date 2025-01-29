/***********************************************************************************************
 * 
 *  FileName: InstructionSet.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using System.Reflection;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Exceptions;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet
{
    public class InstructionSet
    {
        #region Fields

        public static readonly InstructionSet Instance = LoadInstructionSet();

        private Dictionary<byte, InstructionBase> _instructionTable = new Dictionary<byte, InstructionBase>(255);

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

        public void Invoke(byte opCode, Processor cpu, Memory memory)
        {
            _instructionTable[opCode].Execute(cpu);
        }

        private void Initialize()
        { }

#pragma warning disable CS8602
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
#pragma warning restore CS8602

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