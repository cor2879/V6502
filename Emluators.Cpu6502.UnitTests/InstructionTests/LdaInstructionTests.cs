#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests.InstructionTests
{
    [TestClass]
    public class LdaInstructionTests
    {
        private Mock<IVirtualConsole> _mockConsole;
        private Processor _cpu;

        [TestInitialize]
        public void Setup()
        {
            _mockConsole = new Mock<IVirtualConsole>();
            _cpu = new Processor(_mockConsole.Object);
            _cpu.Memory.Clear();
        }

        [TestMethod]
        public async Task LdaImmediateLoadsCorrectValue()
        {
            await TestImmediate(0x42, expectZero: false, expectNegative: false);
        }

        [TestMethod]
        public async Task LdaImmediateSetsZeroFlag()
        {
            await TestImmediate(0x00, expectZero: true, expectNegative: false);
        }

        [TestMethod]
        public async Task LdaImmediateSetsNegativeFlag()
        {
            await TestImmediate(0x80, expectZero: false, expectNegative: true);
        }

        [TestMethod]
        public async Task LdaZeroPageLoadsCorrectValue()
        {
            await TestZeroPage(0x10, 0x55);
        }

        [TestMethod]
        public async Task LdaZeroPageXLoadsCorrectValue()
        {
            await TestZeroPageX(0x10, 0x02, 0xAA, expectZero: false, expectNegative: true);
        }

        [TestMethod]
        public async Task LdaAbsoluteLoadsCorrectValue()
        {
            await TestAbsolute(0x2000, 0x77);
        }

        [TestMethod]
        public async Task LdaAbsoluteXLoadsCorrectValue()
        {
            await TestAbsoluteX(0x3000, 0x05, 0x99, expectZero: false, expectNegative: true);
        }

        [TestMethod]
        public async Task LdaAbsoluteYLoadsCorrectValue()
        {
            await TestAbsoluteY(0x4000, 0x08, 0x33);
        }

        [TestMethod]
        public async Task LdaIndirectXLoadsCorrectValue()
        {
            await TestIndirectX(0x20, 0x04, 0x5000, 0xC4, expectZero: false, expectNegative: true);
        }

        [TestMethod]
        public async Task LdaIndirectYLoadsCorrectValue()
        {
            await TestIndirectY(0x30, 0x07, 0x6000, 0xD8, expectZero: false, expectNegative: true);
        }

        // ---- HELPER METHODS ----
        private async Task TestImmediate(byte value, bool expectZero = false, bool expectNegative = false)
        {
            await _cpu.LoadProgramAsync([(byte)OpCodes.LdaImmediate, value]);
            _cpu.Step();
            AssertState(value, expectZero, expectNegative);
        }

        private async Task TestZeroPage(byte address, byte value, bool expectZero = false, bool expectNegative = false)
        {
            _cpu.Memory[address] = value;
            await _cpu.LoadProgramAsync([(byte)OpCodes.LdaZeroPage, address]);
            _cpu.Step();
            AssertState(value, expectZero, expectNegative); ;
        }

        private async Task TestZeroPageX(byte address, byte offset, byte value, bool expectZero = false, bool expectNegative = false)
        {
            _cpu.IndexerX.Value = offset;
            _cpu.Memory[address + offset] = value;
            await _cpu.LoadProgramAsync([(byte)OpCodes.LdaZeroPageX, address]);
            _cpu.Step();
            AssertState(value, expectZero, expectNegative);
        }

        private async Task TestAbsolute(DWord6502 address, byte value, bool expectZero = false, bool expectNegative = false)
        {
            _cpu.Memory[address] = value;
            await _cpu.LoadProgramAsync([(byte)OpCodes.LdaAbsolute, address.LowPart, address.HighPart]);
            _cpu.Step();
            AssertState(value, expectZero, expectNegative); ;
        }

        private async Task TestAbsoluteX(DWord6502 baseAddress, byte offset, byte value, bool expectZero = false, bool expectNegative = false)
        {
            _cpu.IndexerX.Value = offset;
            _cpu.Memory[baseAddress + offset] = value;
            await _cpu.LoadProgramAsync([(byte)OpCodes.LdaAbsoluteX, baseAddress.LowPart, baseAddress.HighPart]);
            _cpu.Step();
            AssertState(value, expectZero, expectNegative);
        }

        private async Task TestAbsoluteY(DWord6502 baseAddress, byte offset, byte value, bool expectZero = false, bool expectNegative = false)
        {
            _cpu.IndexerY.Value = offset;
            _cpu.Memory[baseAddress + offset] = value;
            await _cpu.LoadProgramAsync([(byte)OpCodes.LdaAbsoluteY, baseAddress.LowPart, baseAddress.HighPart]);
            _cpu.Step();
            AssertState(value, expectZero, expectNegative);
        }

        private async Task TestIndirectX(byte pointer, byte indexOffset, DWord6502 effectiveAddress, byte value, bool expectZero = false, bool expectNegative = false)
        {
            _cpu.IndexerX.Value = indexOffset;
            _cpu.Memory[(byte)(pointer + _cpu.IndexerX.Value)] = effectiveAddress.LowPart;
            _cpu.Memory[(byte)(pointer + _cpu.IndexerX.Value + 1)] = effectiveAddress.HighPart;
            _cpu.Memory[effectiveAddress] = value;

            await _cpu.LoadProgramAsync([(byte)OpCodes.LdaIndexedIndirect, pointer]);
            _cpu.Step();
            AssertState(value, expectZero, expectNegative);
        }

        private async Task TestIndirectY(byte pointer, byte offset, DWord6502 baseAddress, byte value, bool expectZero = false, bool expectNegative = false)
        {
            _cpu.IndexerY.Value = offset;
            _cpu.Memory[pointer] = baseAddress.LowPart;
            _cpu.Memory[pointer + 1] = baseAddress.HighPart;
            _cpu.Memory[baseAddress + offset] = value;

            await _cpu.LoadProgramAsync([(byte)OpCodes.LdaIndirectIndexed, pointer]);
            _cpu.Step();
            AssertState(value, expectZero, expectNegative);
        }

        private void AssertState(byte expected, bool expectZero = false, bool expectNegative = false)
        {
            var actual = _cpu.Accumulator.Value;
            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:X2} | Actual: 0x{actual:X2}");
            Assert.AreEqual(expectZero, _cpu.ProcessorStatus.ZeroFlag, "Zero Flag mismatch");
            Assert.AreEqual(expectNegative, _cpu.ProcessorStatus.NegativeFlag, "Negative Flag mismatch");
        }
    }
}
