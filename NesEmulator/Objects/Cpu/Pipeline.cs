/***********************************************************************************************
 * 
 *  FileName: Pipeline.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/

using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu
{
    public class Pipeline
    {
        #region Fields

        private List<InstructionBase> _innerList = new List<InstructionBase>(8);

        #endregion

        #region Constructors

        public Pipeline(Processor processor)
        { 
            this.Processor = processor;
        }

        #endregion

        #region Properties

        public Processor Processor { get; private set; }

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
                // instruction.AdvanceCycle(this.Processor);
            }
        }

        #endregion
    }
}