using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class AdcInstruction : InstructionBase
    {
        public AdcInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "ADC", addressingMode, length, cycles)
        { }

        protected override void PerformExecution(IProcessor processor)
        {
            var value = Mode.Fetch(processor);
            var carryIn = processor.ProcessorStatus.CarryFlag ? 1 : 0;
            var accumulator = processor.Accumulator.Value;
            DWord6502 result;

            if (processor.ProcessorStatus.DecimalFlag)
            {
                // Perform BCD addition
                var lowNibble = (accumulator & 0x0F) + (value & 0x0F) + carryIn;

                if (lowNibble > 9)
                {
                    lowNibble += 6;
                }

                var highNibble = (accumulator & 0xF0) + (value & 0xF0);

                if (lowNibble > 0x0F)
                {
                    highNibble += 0x10;
                }

                if (highNibble > 0x90)
                {
                    highNibble += 0x60;
                }

                result = (DWord6502)(highNibble + (lowNibble & 0x0F));
            }
            else
            {
                // Standard binary addition
                result = (DWord6502)(accumulator + value + carryIn);
            }

            processor.Accumulator.Value = result.LowPart;
            processor.ProcessorStatus.CarryFlag = result > 0xFF;
            processor.ProcessorStatus.ZeroFlag = processor.Accumulator.Value == 0;
            processor.ProcessorStatus.NegativeFlag = (processor.Accumulator.Value & Constants.NEGATIVE_FLAG) != 0;
            processor.ProcessorStatus.OverflowFlag = ((accumulator ^ value) & Constants.NEGATIVE_FLAG) == 0 &&
                                                     ((accumulator ^ result.LowPart) & Constants.NEGATIVE_FLAG) != 0;

            processor.ProgramCounter += Length;
        }
    }
}
