#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests
{
    [TestClass]
    public class BitwiseArithmeticInstructionTests
    {
        private Mock<IVirtualConsole> consoleMock = new Mock<IVirtualConsole>();
        private Processor _cpu;

        [TestInitialize]
        public void Setup()
        {
            _cpu = new Processor(consoleMock.Object);
            _cpu.Memory[0xFFFD] = 0x00;
            _cpu.Memory[0xFFFC] = 0x02; // Set reset vector to 0x0200.  Main correct endian-ness
            _cpu.Reset(); // This is necessary in order to force the CPU to acquire the new ProgramCounter
        }

        [TestMethod]
        public void Anc_Executes_Correctly()
        {
            var expectedStartingAccumulatorValue = (byte)0x85;
            var expectedStartingMemoryValue = (byte)0xC3;
            var expectedFinalAccumulatorValue = (byte)(expectedStartingAccumulatorValue & expectedStartingMemoryValue);

            _cpu.Accumulator.Value = expectedStartingAccumulatorValue;
            _cpu.Memory[0x200] = expectedStartingMemoryValue;
            var instruction = new AncInstruction(Modes.Immediate, 2, 3);
            instruction.Execute(_cpu);

            Assert.AreEqual(expectedFinalAccumulatorValue, _cpu.Accumulator.Value, "ANC should perform AND operation and store result in Accumulator.");
            Assert.AreEqual((_cpu.Accumulator.Value & ProcessorStatusRegister.NegativeBit) != 0, _cpu.ProcessorStatus.NegativeFlag, "ANC should set Negative flag if bit 7 is set.");
            Assert.AreEqual((_cpu.Accumulator.Value & ProcessorStatusRegister.CarryBit) != 0, _cpu.ProcessorStatus.CarryFlag, "ANC should set Carry flag if bit 7 is set.");
        }

        [TestMethod]
        public void Alr_Executes_Correctly()
        {
            var expectedStartingAccumulatorValue = (byte)0x86;
            var expectedMemoryValue = (byte)0x40;
            var expectedAndResult = (byte)(expectedStartingAccumulatorValue & expectedMemoryValue);
            var expectedEndingAccumulatorValue = (byte)((expectedAndResult >> 1) | (_cpu.ProcessorStatus.CarryFlag ? ProcessorStatusRegister.NegativeBit : 0));
            _cpu.Accumulator.Value = expectedStartingAccumulatorValue;
            _cpu.Memory[0x200] = expectedMemoryValue;
            var instruction = new AlrInstruction(Modes.Immediate, 2, 3);
            instruction.Execute(_cpu);

            Assert.AreEqual(expectedEndingAccumulatorValue, _cpu.Accumulator.Value, "ALR should perform AND operation and logical shift right.");
            Assert.AreEqual((_cpu.Accumulator.Value & ProcessorStatusRegister.NegativeBit) != 0, _cpu.ProcessorStatus.NegativeFlag, "ALR should set Negative flag if bit 7 is set.");
            Assert.AreEqual((_cpu.Accumulator.Value & 0x01) != 0, _cpu.ProcessorStatus.CarryFlag, "ALR should set Carry flag based on LSB.");
        }

        [TestMethod]
        public void Arr_Executes_Correctly()
        {
            var expectedStartingAccumulatorValue = (byte)0xC3;
            var expectedMemoryValue = (byte)0x99;
            var expectedAndResult = (byte)(expectedStartingAccumulatorValue & expectedMemoryValue);
            var expectedCarry = (expectedAndResult & ProcessorStatusRegister.CarryBit) != 0;
            var expectedEndingAccumulatorValue = (byte)((expectedAndResult >> 1) | (_cpu.ProcessorStatus.CarryFlag ? ProcessorStatusRegister.NegativeBit : 0));
            var expectedOverflow = ((expectedEndingAccumulatorValue >> 6) & 1) != ((expectedEndingAccumulatorValue >> 5) & 1);

            _cpu.Accumulator.Value = expectedStartingAccumulatorValue;
            _cpu.Memory[0x200] = expectedMemoryValue;
            var instruction = new ArrInstruction(Modes.Immediate, 2, 3);
            instruction.Execute(_cpu);

            Debug.WriteLine($"{nameof(BitwiseArithmeticInstructionTests)}::{nameof(Arr_Executes_Correctly)} | {nameof(_cpu.Accumulator)}: 0x{_cpu.Accumulator.Value:X2}");
            Assert.AreEqual(expectedEndingAccumulatorValue, _cpu.Accumulator.Value, "ARR should perform AND operation and rotate right.");
            Assert.AreEqual((_cpu.Accumulator.Value & ProcessorStatusRegister.NegativeBit) != 0, _cpu.ProcessorStatus.NegativeFlag, "ARR should set Negative flag if bit 7 is set.");
            Assert.AreEqual(expectedOverflow, _cpu.ProcessorStatus.OverflowFlag, "ARR should set Overflow flag based on bit 6 XOR bit 5.");
            Assert.AreEqual(expectedCarry, _cpu.ProcessorStatus.CarryFlag, "ARR should set Carry flag based on LSB BEFORE rotation.");
        }
    }
}
