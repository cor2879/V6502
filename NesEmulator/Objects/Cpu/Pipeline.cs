/***********************************************************************************************
 * 
 *  FileName: Pipeline.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using System;
using System.Collections.Generic;

namespace Emulators.Mso6502
{
    public class Pipeline
    {
        #region Fields

        private List<InstructionBase> _innerList = new List<InstructionBase>(8);

        #endregion

        #region Constructors

        public Pipeline()
        { }

        #endregion

        #region Methods

        public void Add(InstructionBase instruction)
        {
            _innerList.Add(instruction);
        }

        public void Remove(InstructionBase instruction)
        {
            _innerList.Remove(instruction);
        }

        public void Cycle()
        {
            foreach (var instruction in _innerList)
            {
                instruction.Cycle();
            }
        }

        #endregion
    }
}