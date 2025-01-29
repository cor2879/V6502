/***********************************************************************************************
 * 
 *  FileName: Processor.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces
{
    public interface IProcessor
    {
        Accumulator Accumulator { get; }

        long CycleCount { get; }

        ushort[] AddressBus { get; }

        CpuRegister<byte> IndexerX { get; }

        CpuRegister<byte> IndexerY { get; }

        Memory Memory { get; }

        Pipeline Pipeline { get; }

        ProcessorStatusRegister ProcessorStatus { get; }

        DWord6502 ProgramCounter { get; set; }

        CpuRegister<ushort> StackPointer { get; }

        IVirtualConsole VirtualConsole { get; }

        void Branch(byte offset);

        void AddCycles(int cycles);

        byte[] GetStack();

        void Interrupt();

        void Jump(ushort address);

        void JumpIndirect(ushort address);

        void Load(CpuRegister<byte> register, byte value);

        void Load(CpuRegister<byte> register, ushort Address);

        Task LoadProgramAsync(byte[] program, ushort startAddress = 0x0600, bool autoRun = false);

        void NonMaskableInterrupt();

        byte PeekStack();

        byte PopStack();

        void PushStack(byte value);

        void Reset();

        void ReturnFromInterrupt();

        /// <summary>
        /// Begins executing instructions continuously until stopped.
        /// </summary>
        Task RunAsync(CancellationToken cancellation);

        /// <summary>
        /// Executes a single instruction
        /// </summary>
        InstructionBase Step();

        /// <summary>
        /// Stops continuous execution
        /// </summary>
        Task StopAsync();

        void Store(CpuRegister<byte> register, ushort Address);
    }
}