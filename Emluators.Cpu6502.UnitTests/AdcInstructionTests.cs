#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace Emulators.Cpu6502.UnitTests
{
    [TestClass]
    public class AdcInstructionTests
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
        public async Task AdcImmediateAddsWithoutCarry()
        {
            var initial = (byte)0x10;
            var value = (byte)0x20;
            var expected = (byte)(initial + value);

            _cpu.Accumulator.Value = initial;

            await _cpu.LoadProgramAsync([(byte)OpCodes.AdcImmediate, value]);
            _cpu.Step();

            AssertAccumulator(expected);
            Assert.IsFalse(_cpu.ProcessorStatus.CarryFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.OverflowFlag);
        }

        [TestMethod]
        public async Task AdcImmediateAddsWithCarry()
        {
            var initial = (byte)0xF0;
            var value = (byte)0x20;
            var expected = (byte)(initial + value);

            _cpu.Accumulator.Value = initial;

            await _cpu.LoadProgramAsync([(byte)OpCodes.AdcImmediate, value]);
            _cpu.Step();

            AssertAccumulator(expected);
            Assert.IsTrue(_cpu.ProcessorStatus.CarryFlag);
        }

        [TestMethod]
        public async Task AdcImmediateSetsZeroFlag()
        {
            var initial = (byte)0x00;
            var value = (byte)0x00;

            _cpu.Accumulator.Value = initial;

            await _cpu.LoadProgramAsync([(byte)OpCodes.AdcImmediate, value]);
            _cpu.Step();

            AssertAccumulator(0x00);
            Assert.IsTrue(_cpu.ProcessorStatus.ZeroFlag);
        }

        [TestMethod]
        public async Task AdcImmediateSetsNegativeFlag()
        {
            var initial = (byte)0x50;
            var value = (byte)0xA0;
            var expected = (byte)(initial + value);

            _cpu.Accumulator.Value = initial;

            await _cpu.LoadProgramAsync([(byte)OpCodes.AdcImmediate, value]);
            _cpu.Step();

            AssertAccumulator(expected);
            Assert.IsTrue(_cpu.ProcessorStatus.NegativeFlag);
        }

        [TestMethod]
        public async Task AdcImmediateDetectsOverflow()
        {
            var initial = (byte)0x7F; // Largest positive signed value
            var value = (byte)0x01;  // Adding 1 should overflow
            _cpu.Accumulator.Value = initial;

            await _cpu.LoadProgramAsync([(byte)OpCodes.AdcImmediate, value]);
            _cpu.Step();

            Assert.IsTrue(_cpu.ProcessorStatus.OverflowFlag);
        }

        [TestMethod]
        public async Task AdcImmediateHandlesDecimalMode()
        {
            var initial = (byte)0x25; // BCD: 25
            var value = (byte)0x38;   // BCD: 38
            var expected = (byte)0x63; // Expected BCD: 63

            _cpu.Accumulator.Value = initial;
            _cpu.ProcessorStatus.DecimalFlag = true;

            await _cpu.LoadProgramAsync([(byte)OpCodes.AdcImmediate, value]);
            _cpu.Step();

            AssertAccumulator(expected);
            Assert.IsFalse(_cpu.ProcessorStatus.CarryFlag);
        }

        [TestMethod]
        public async Task AdcImmediateHandlesDecimalModeCarry()
        {
            var initial = (byte)0x95; // BCD: 95
            var value = (byte)0x15;   // BCD: 15
            var expected = (byte)0x10; // Expected BCD result: 10, with carry

            _cpu.Accumulator.Value = initial;
            _cpu.ProcessorStatus.DecimalFlag = true;

            await _cpu.LoadProgramAsync([(byte)OpCodes.AdcImmediate, value]);
            _cpu.Step();

            AssertAccumulator(expected);
            Assert.IsTrue(_cpu.ProcessorStatus.CarryFlag);
        }

        private void AssertAccumulator(byte expected)
        {
            var actual = _cpu.Accumulator.Value;
            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:X2} | Actual: 0x{actual:X2}");
        }
    }
}
