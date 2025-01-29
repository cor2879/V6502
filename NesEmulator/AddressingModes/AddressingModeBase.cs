using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public abstract class AddressingModeBase
    {
        public abstract byte FetchOperand(Processor cpu, Memory memory);

        public abstract ushort FetchAddress(Processor cpu, Memory memory);
    }
}
