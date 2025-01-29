/***********************************************************************************************
 * 
 *  FileName: InstructionSetOverload6502Exception.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using System;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.Exceptions
{
    public class InstructionSetOverload6502Exception
        : Exception
    {
        #region Fields

        private InstructionBase _first;
        private InstructionBase _second;

        #endregion

        #region Constructors

        internal InstructionSetOverload6502Exception(InstructionBase first, InstructionBase second)
            : base(String.Format("An attempt was made to overwrite and existing instruction in the Instruction Set with a new instruction.  This indicates an error in the Instruction Set implementation.  Please check the implementations for Instruction types {0} and {1}", 
                   first, second))
        {
            _first = first;
            _second = second;
        }

        #endregion

        #region Properties

        public InstructionBase First { get { return _first; } }

        public InstructionBase Second { get { return _second; } }

        #endregion
    }
}