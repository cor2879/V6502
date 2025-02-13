using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class BrkInstruction : InstructionBase
    {
        public BrkInstruction() : base((byte)OpCodes.Brk, "BRK", Modes.Implied, 1, 7) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var returnAddress = cpu.ProgramCounter + 2;

            Debug.WriteLine($"Computed Return Address: {returnAddress}");

            cpu.PushStack(returnAddress.HighPart);
            cpu.PushStack(returnAddress.LowPart);

            var statusToPush = (byte)(cpu.ProcessorStatus.Value | ProcessorStatusRegister.BrkBit | ProcessorStatusRegister.Bit5);
            
            Debug.WriteLine($"Pushed Processor Status: 0x{statusToPush:X2}");

            cpu.PushStack(statusToPush);

            cpu.ProcessorStatus.IrqDisabledFlag = true;

            cpu.ProgramCounter = AddressingModeBase.Read16(cpu, ProcessorStatusRegister.InterruptVector);

            Debug.WriteLine($"Fetched Interrupt Vector Address: {cpu.ProgramCounter}");
        }
    }

}
