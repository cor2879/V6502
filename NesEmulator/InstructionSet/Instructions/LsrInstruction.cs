using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
    {
        public class LsrInstruction : InstructionBase
        {
            public LsrInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
                : base(opCode, "LSR", addressingMode, length, cycles) { }

            protected override void PerformExecution(IProcessor cpu)
            {
                var value = Mode.Fetch(cpu);
                cpu.ProcessorStatus.CarryFlag = (value & 0x01) != 0;  // Carry gets bit 0
                var result = (byte)(value >> 1);

                Mode.Store(cpu, result);
                cpu.ProcessorStatus.ZeroFlag = result == 0;
                cpu.ProcessorStatus.NegativeFlag = false;  // LSR always clears Negative flag
            }
        }
    }

}
