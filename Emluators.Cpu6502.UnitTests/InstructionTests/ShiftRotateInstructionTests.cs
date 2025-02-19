#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests.InstructionTests
{
    [TestClass]
    public class ShiftRotateInstructionTests
    {
        private Mock<IVirtualConsole> _mockConsole;
        private Processor _cpu;

        [TestInitialize]
        public void Setup()
        {
            _mockConsole = new Mock<IVirtualConsole>();
            _cpu = new Processor(_mockConsole.Object);
        }

        [TestMethod]
        public async Task AslAccumulatorShiftsLeftAndSetsCarry()
        {
            var initialValue = (byte)0b1010_1010; // 0xAA
            var expectedValue = (byte)0b0101_0100; // 0x54 (shifted left)

            _cpu.Accumulator.Value = initialValue;
            await _cpu.LoadProgramAsync([(byte)OpCodes.AslAccumulator]);

            _cpu.Step();

            AssertAccumulator(expectedValue);
            Assert.IsTrue(_cpu.ProcessorStatus.CarryFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.ZeroFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.NegativeFlag);
        }

        [TestMethod]
        public async Task LsrAccumulatorShiftsRightAndSetsCarry()
        {
            var initialValue = (byte)0b0000_0001; // 0x01
            var expectedValue = (byte)0b0000_0000; // 0x00

            _cpu.Accumulator.Value = initialValue;
            await _cpu.LoadProgramAsync([(byte)OpCodes.LsrAccumulator]);

            _cpu.Step();

            AssertAccumulator(expectedValue);
            Assert.IsTrue(_cpu.ProcessorStatus.CarryFlag);
            Assert.IsTrue(_cpu.ProcessorStatus.ZeroFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.NegativeFlag);
        }

        [TestMethod]
        public async Task RolAccumulatorRotatesLeftWithCarry()
        {
            var initialValue = (byte)0b1000_0001; // 0x81
            var expectedValue = (byte)0b0000_0011; // 0x03 (if Carry was set)

            _cpu.Accumulator.Value = initialValue;
            _cpu.ProcessorStatus.CarryFlag = true;
            await _cpu.LoadProgramAsync([(byte)OpCodes.RolAccumulator]);

            _cpu.Step();

            AssertAccumulator(expectedValue);
            Assert.IsTrue(_cpu.ProcessorStatus.CarryFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.ZeroFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.NegativeFlag);
        }

        [TestMethod]
        public async Task RorAccumulatorRotatesRightWithCarry()
        {
            var initialValue = (byte)0b0000_0011; // 0x03
            var expectedValue = (byte)0b1000_0001; // 0x81 (if Carry was set)

            _cpu.Accumulator.Value = initialValue;
            _cpu.ProcessorStatus.CarryFlag = true;
            await _cpu.LoadProgramAsync([(byte)OpCodes.RorAccumulator]);

            _cpu.Step();

            AssertAccumulator(expectedValue);
            Assert.IsFalse(_cpu.ProcessorStatus.CarryFlag, "CarryFlag was not false");
            Assert.IsFalse(_cpu.ProcessorStatus.ZeroFlag, "ZeroFlag was not false");
            Assert.IsTrue(_cpu.ProcessorStatus.NegativeFlag);
        }

        private void AssertAccumulator(byte expected)
        {
            var actual = _cpu.Accumulator.Value;
            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:X2} | Actual: 0x{actual:X2}");
        }
    }
}