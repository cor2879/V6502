using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class RolInstruction : InstructionBase
    {
        public RolInstruction(byte opCode, AddressingModeBase addressingMode, byte length, byte cycles)
            : base(opCode, "ROL", addressingMode, length, cycles) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            var value = Mode.Fetch(cpu);
            var carryIn = cpu.ProcessorStatus.CarryFlag ? 1 : 0;
            cpu.ProcessorStatus.CarryFlag = (value & 0x80) != 0; // Bit 7 to Carry
            var result = (byte)((value << 1) | carryIn);

            Mode.Store(cpu, result);
            cpu.ProcessorStatus.ZeroFlag = result == 0;
            cpu.ProcessorStatus.NegativeFlag = (result & ProcessorStatusRegister.NegativeBit) != 0;
        }
    }
}
