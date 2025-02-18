using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions
{
    public class SbxInstruction : InstructionBase
    {
        public SbxInstruction()
            : base((byte)OpCodes.Sbx, "SBX", Modes.Immediate, 2, 3) { }

        protected override void PerformExecution(IProcessor cpu)
        {
            Debug.WriteLine($"{nameof(SbxInstruction)} | A: {cpu.Accumulator.Value:X2}, X: {cpu.IndexerX.Value:X2}, Memory: {cpu.Memory[0x200]:X2}, Carry: {cpu.ProcessorStatus.CarryFlag}");

            var operand = Mode.Fetch(cpu);
            var andResult = (byte)(cpu.Accumulator.Value & cpu.IndexerX.Value);
            var result = (byte)(andResult - operand);

            cpu.IndexerX.Value = result;
            cpu.ProcessorStatus.ZeroFlag = result == 0;
            cpu.ProcessorStatus.NegativeFlag = (result & ProcessorStatusRegister.NegativeBit) != 0;
            cpu.ProcessorStatus.CarryFlag = andResult >= operand;

            Debug.WriteLine($"{nameof(SbxInstruction)} | A: {cpu.Accumulator.Value:X2}, X: {cpu.IndexerX.Value:X2}, Memory: {cpu.Memory[0x200]:X2}, Carry: {cpu.ProcessorStatus.CarryFlag}");
        }
    }

}
