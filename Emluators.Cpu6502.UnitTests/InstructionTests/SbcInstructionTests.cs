#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests.InstructionTests
{
    [TestClass]
    public class SbcInstructionTests
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
        public async Task SbcImmediateSubtractsCorrectly()
        {
            _cpu.Accumulator.Value = 0x50;
            _cpu.ProcessorStatus.CarryFlag = true; // Carry must be set for correct subtraction

            await _cpu.LoadProgramAsync([(byte)OpCodes.SbcImmediate, 0x20]);
            _cpu.Step();

            AssertAccumulator(0x30);
            Assert.IsFalse(_cpu.ProcessorStatus.ZeroFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.NegativeFlag);
        }

        [TestMethod]
        public async Task SbcImmediateWithCarryCleared()
        {
            _cpu.Accumulator.Value = 0x50;
            _cpu.ProcessorStatus.CarryFlag = false; // Carry not set, should subtract extra 1

            await _cpu.LoadProgramAsync([(byte)OpCodes.SbcImmediate, 0x20]);
            _cpu.Step();

            AssertAccumulator(0x2F);
        }

        [TestMethod]
        public async Task SbcZeroPageSubtractsCorrectly()
        {
            _cpu.Memory[0x10] = 0x10;
            _cpu.Accumulator.Value = 0x50;
            _cpu.ProcessorStatus.CarryFlag = true;

            await _cpu.LoadProgramAsync([(byte)OpCodes.SbcZeroPage, 0x10]);
            _cpu.Step();

            AssertAccumulator(0x40);
        }

        [TestMethod]
        public async Task SbcZeroPageXSubtractsCorrectly()
        {
            _cpu.Memory[0x15] = 0x10;
            _cpu.IndexerX.Value = 0x05;
            _cpu.Accumulator.Value = 0x50;
            _cpu.ProcessorStatus.CarryFlag = true;

            await _cpu.LoadProgramAsync([(byte)OpCodes.SbcZeroPageX, 0x10]);
            _cpu.Step();

            AssertAccumulator(0x40);
        }

        [TestMethod]
        public async Task SbcAbsoluteSubtractsCorrectly()
        {
            var address = (DWord6502)0x2000;
            _cpu.Memory[address] = 0x15;
            _cpu.Accumulator.Value = 0x60;
            _cpu.ProcessorStatus.CarryFlag = true;

            await _cpu.LoadProgramAsync([(byte)OpCodes.SbcAbsolute, address.LowPart, address.HighPart]);
            _cpu.Step();

            AssertAccumulator(0x4B);
        }

        [TestMethod]
        public async Task SbcAbsoluteXSubtractsCorrectly()
        {
            var baseAddress = (DWord6502)0x3000;
            _cpu.Memory[baseAddress + 0x05] = 0x30;
            _cpu.IndexerX.Value = 0x05;
            _cpu.Accumulator.Value = 0x80;
            _cpu.ProcessorStatus.CarryFlag = true;

            await _cpu.LoadProgramAsync([(byte)OpCodes.SbcAbsoluteX, baseAddress.LowPart, baseAddress.HighPart]);
            _cpu.Step();

            AssertAccumulator(0x50);
        }

        [TestMethod]
        public async Task SbcAbsoluteYSubtractsCorrectly()
        {
            var baseAddress = (DWord6502)0x4000;
            _cpu.Memory[baseAddress + 0x08] = 0x22;
            _cpu.IndexerY.Value = 0x08;
            _cpu.Accumulator.Value = 0x90;
            _cpu.ProcessorStatus.CarryFlag = true;

            await _cpu.LoadProgramAsync([(byte)OpCodes.SbcAbsoluteY, baseAddress.LowPart, baseAddress.HighPart]);
            _cpu.Step();

            AssertAccumulator(0x6E);
        }

        [TestMethod]
        public async Task SbcIndirectXSubtractsCorrectly()
        {
            var pointer = (byte)0x20;
            var effectiveAddress = (DWord6502)0x5000;
            _cpu.IndexerX.Value = 0x04;
            _cpu.Memory[(byte)(pointer + _cpu.IndexerX.Value)] = effectiveAddress.LowPart;
            _cpu.Memory[(byte)(pointer + _cpu.IndexerX.Value + 1)] = effectiveAddress.HighPart;
            _cpu.Memory[effectiveAddress] = 0x12;
            _cpu.Accumulator.Value = 0x42;
            _cpu.ProcessorStatus.CarryFlag = true;

            await _cpu.LoadProgramAsync([(byte)OpCodes.SbcIndexedIndirect, pointer]);
            _cpu.Step();

            AssertAccumulator(0x30);
        }

        [TestMethod]
        public async Task SbcIndirectYSubtractsCorrectly()
        {
            var pointer = (byte)0x30;
            var baseAddress = (DWord6502)0x6000;
            var offset = (byte)0x07;
            _cpu.IndexerY.Value = offset;
            _cpu.Memory[pointer] = baseAddress.LowPart;
            _cpu.Memory[pointer + 1] = baseAddress.HighPart;
            _cpu.Memory[baseAddress + offset] = 0x18;
            _cpu.Accumulator.Value = 0x50;
            _cpu.ProcessorStatus.CarryFlag = true;

            await _cpu.LoadProgramAsync([(byte)OpCodes.SbcIndirectIndexed, pointer]);
            _cpu.Step();

            AssertAccumulator(0x38);
        }

        private void AssertAccumulator(byte expected)
        {
            var actual = _cpu.Accumulator.Value;
            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:X2} | Actual: 0x{actual:X2}");
        }
    }
}