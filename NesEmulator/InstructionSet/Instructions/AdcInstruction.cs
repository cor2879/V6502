using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class AdcInstruction : InstructionBase
    {
        public AdcInstruction(AddressingModeBase addressingMode, byte length)
            : base(0x69, "ADC", addressingMode, length, 2)
        { }

        protected override void PerformExecution(IProcessor processor)
        {
            // Read the immediate value from memory using the ProgramCounter
            ushort address = processor.ProgramCounter;
            byte value = processor.Memory[address];

            // Perform the addition with carry
            ushort result = (ushort)(processor.Accumulator.Value + value + (processor.ProcessorStatus.CarryFlag ? 1 : 0));

            // Update the accumulator with the lower byte of the result
            processor.Accumulator.Value = (byte)(result & 0xFF);

            // Update processor status flags
            processor.ProcessorStatus.CarryFlag = result > 0xFF;
            processor.ProcessorStatus.ZeroFlag = processor.Accumulator.Value == 0;
            processor.ProcessorStatus.NegativeFlag = (processor.Accumulator.Value & 0x80) != 0;
            processor.ProcessorStatus.OverflowFlag = ((processor.Accumulator.Value ^ value) & 0x80) == 0 &&
                                                     ((processor.Accumulator.Value ^ result) & 0x80) != 0;

            // Increment the program counter
            processor.ProgramCounter += Length;
        }
    }
}
