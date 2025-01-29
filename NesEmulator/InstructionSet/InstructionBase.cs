/***********************************************************************************************
 * 
 *  FileName: InstructionBase.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
#pragma warning disable CS8618
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet
{
    // Base class for all instructions
    public abstract class InstructionBase
    {
        public virtual byte OpCode { get; }

        public virtual string Mnemonic { get; }

        public AddressingModeBase Mode { get; }

        public virtual int Cycles { get; }

        public virtual byte Length { get; }

        public byte CurrentCycle { get; private set; } = 0;

        public InstructionBase(byte opCode, string mnemonic, AddressingModeBase mode, byte length, byte cycles)
        {
            OpCode = opCode;
            Mode = mode;
            Mnemonic = mnemonic; ;
            Length = length;
            Cycles = cycles;
        }

        public void Execute(IProcessor cpu)
        {
            PerformExecution(cpu);
            cpu.AddCycles(Cycles);
        }

        protected abstract void PerformExecution(IProcessor cpu);
    }
}