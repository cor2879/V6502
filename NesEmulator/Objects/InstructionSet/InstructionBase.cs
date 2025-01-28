/***********************************************************************************************
 * 
 *  FileName: InstructionBase.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
#pragma warning disable CS8618
using System;

namespace Emulators.Mso6502
{
    public abstract class InstructionBase
    {
        #region Fields

        protected Processor _cpu;
        private Int32 _currentCycle;
        private DWord6502 _programCounter = 0x0000;

        #endregion

        #region Contructors

        protected InstructionBase()
        { }

        #endregion

        #region Properties

        public abstract int MaxCycles { get; }

        public abstract Byte OpCode { get; } 

        protected Int32 CurrentCycle
        {
            get { return _currentCycle; }
        }

        protected Processor Cpu
        {
            get { return _cpu; }
        }

        /// <summary>
        /// Reflects the state of the Program Counter at the time the instruction was first invoked.
        /// </summary>
        protected DWord6502 ProgramCounter
        {
            get { return _programCounter; }
        }

        #endregion

        #region Methods

        public virtual void Invoke(Processor cpu)
        {
            _cpu = cpu;
            _currentCycle = 0;
            _cpu.Pipeline.Add(this);
            _programCounter = _cpu.ProgramCounter;
        }

        public virtual void Cycle()
        {
            if (++_currentCycle == MaxCycles)
            {
                _cpu.Pipeline.Remove(this);
            }
        }

        #endregion
    }
}