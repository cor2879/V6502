using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class SbcInstruction : InstructionBase
    {
        public SbcInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "SBC", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor cpu)
        {
            // Fetch operand from memory
            var value = Mode.Fetch(cpu);

            // Invert value for subtraction (6502 uses two’s complement)
            value = (byte)~value;

            // Perform the addition (which is effectively subtraction with carry)
            var result = (DWord6502)(cpu.Accumulator.Value + value + (cpu.ProcessorStatus.CarryFlag ? 1 : 0));

            // Store the result in Accumulator
            cpu.Accumulator.Value = result.LowPart;

            // Update Flags
            cpu.ProcessorStatus.CarryFlag = result > 0xFF;
            cpu.ProcessorStatus.ZeroFlag = cpu.Accumulator.Value == 0;
            cpu.ProcessorStatus.NegativeFlag = (cpu.Accumulator.Value & Constants.NegativeFlag) != 0;
            cpu.ProcessorStatus.OverflowFlag = ((cpu.Accumulator.Value ^ value) & Constants.NegativeFlag) == 0 &&
                                               ((cpu.Accumulator.Value ^ result) & Constants.NegativeFlag) != 0;

            // Increment the program counter
            cpu.ProgramCounter += Length;
        }
    }
}
