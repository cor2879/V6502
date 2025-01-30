using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes
{
    public class RelativeAddressingMode : AddressingModeBase
    {
        public override byte Fetch(IProcessor cpu)
        {
            // Fetch the signed 8-bit offset from memory
            return cpu.Memory[cpu.ProgramCounter];
        }

        public override void Store(IProcessor cpu, byte value)
        {
            throw new NotImplementedException();
        }

        public DWord6502 GetEffectiveAddress(IProcessor cpu)
        {
            // Fetch the signed offset
            var offset = (sbyte)Fetch(cpu);

            // Calculate target address based on Program Counter
            return (DWord6502)(cpu.ProgramCounter + offset);
        }
    }
}
