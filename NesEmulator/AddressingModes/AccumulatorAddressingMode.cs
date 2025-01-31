using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class AccumulatorAddressingMode : AddressingModeBase
    {
        public override byte Fetch(IProcessor cpu)
        {
            return cpu.Accumulator.Value;
        }

        public override void Store(IProcessor cpu, byte value)
        {
            cpu.Accumulator.Value = value;
        }
    }
}
