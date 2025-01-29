#pragma warning disable CS8618
using Microsoft.Testing;
using Microsoft.Testing.Platform;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using System.Runtime.CompilerServices;

namespace Emluators.Cpu6502.UnitTests
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
        }

        [TestMethod]
        public async Task LdaImmediateLoadsCorrectValue()
        {
            var expected = (byte)0x42;

            await _cpu.LoadProgramAsync(
            [
                (byte)OpCodes.LdaImmediate,
                expected
            ]);

            _cpu.Step();
            var actual = _cpu.Accumulator.Value;

            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:XX} | Actual: 0x{actual:XX}");
            Assert.IsFalse(_cpu.ProcessorStatus.ZeroFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.NegativeFlag);
        }

        [TestMethod]
        public async Task LdaZeroPageLoadsCorrectValue()
        {
            var address = (byte)0x10;
            var expected = (byte)0x55;
            _cpu.Memory[address] = expected;

            await _cpu.LoadProgramAsync(new byte[] { (byte)OpCodes.LdaZeroPage, address });
            _cpu.Step();
            var actual = _cpu.Accumulator.Value;

            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:XX} | Actual: 0x{actual:XX}");
        }

        [TestMethod]
        public async Task LdaZeroPageXLoadsCorrectValue()
        {
            var address = (byte)0x10;
            var offset = (byte)0x02;
            var expected = (byte)0xAA;
            _cpu.IndexerX.Value = offset;
            _cpu.Memory[address + offset] = expected;

            await _cpu.LoadProgramAsync(new byte[] { (byte)OpCodes.LdaZeroPageX, address });
            _cpu.Step();
            var actual = _cpu.Accumulator.Value;

            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:XX} | Actual: 0x{actual:XX}");
        }

        [TestMethod]
        public async Task LdaAbsoluteLoadsCorrectValue()
        {
            var address = (ushort)0x2000;
            var expected = (byte)0x77;
            _cpu.Memory[address] = expected;

            await _cpu.LoadProgramAsync(new byte[] { (byte)OpCodes.LdaAbsolute, (byte)(address & 0xFF), (byte)(address >> 8) });
            _cpu.Step();
            var actual = _cpu.Accumulator.Value;

            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:XX} | Actual: 0x{actual:XX}");
        }

        [TestMethod]
        public async Task LdaAbsoluteXLoadsCorrectValue()
        {
            var baseAddress = (ushort)0x3000;
            var offset = (byte)0x05;
            var expected = (byte)0x99;
            _cpu.IndexerX.Value = offset;
            _cpu.Memory[baseAddress + offset] = expected;

            await _cpu.LoadProgramAsync(new byte[] { (byte)OpCodes.LdaAbsoluteX, (byte)(baseAddress & 0xFF), (byte)(baseAddress >> 8) });
            _cpu.Step();
            var actual = _cpu.Accumulator.Value;

            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:XX} | Actual: 0x{actual:XX}");
        }

        [TestMethod]
        public async Task LdaAbsoluteYLoadsCorrectValue()
        {
            var baseAddress = (ushort)0x4000;
            var offset = (byte)0x08;
            var expected = (byte)0x33;
            _cpu.IndexerY.Value = offset;
            _cpu.Memory[baseAddress + offset] = expected;

            await _cpu.LoadProgramAsync(new byte[] { (byte)OpCodes.LdaAbsoluteY, (byte)(baseAddress & 0xFF), (byte)(baseAddress >> 8) });
            _cpu.Step();
            var actual = _cpu.Accumulator.Value;

            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:XX} | Actual: 0x{actual:XX}");
        }

        [TestMethod]
        public async Task LdaIndirectXLoadsCorrectValue()
        {
            var pointer = (byte)0x20;
            var expected = (byte)0xC4;
            var effectiveAddress = (ushort)0x5000;
            _cpu.IndexerX.Value = 0x04;
            _cpu.Memory[(byte)(pointer + _cpu.IndexerX.Value)] = (byte)(effectiveAddress & 0xFF);
            _cpu.Memory[(byte)(pointer + _cpu.IndexerX.Value + 1)] = (byte)(effectiveAddress >> 8);
            _cpu.Memory[effectiveAddress] = expected;

            await _cpu.LoadProgramAsync(new byte[] { (byte)OpCodes.LdaIndexedIndirect, pointer });
            _cpu.Step();
            var actual = _cpu.Accumulator.Value;

            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:XX} | Actual: 0x{actual:XX}");
        }

        [TestMethod]
        public async Task LdaIndirectYLoadsCorrectValue()
        {
            var pointer = (byte)0x30;
            var expected = (byte)0xD8;
            var baseAddress = (ushort)0x6000;
            var offset = (byte)0x07;
            _cpu.IndexerY.Value = offset;
            _cpu.Memory[pointer] = (byte)(baseAddress & 0xFF);
            _cpu.Memory[pointer + 1] = (byte)(baseAddress >> 8);
            _cpu.Memory[baseAddress + offset] = expected;

            await _cpu.LoadProgramAsync(new byte[] { (byte)OpCodes.LdaIndirectIndexed, pointer });
            _cpu.Step();
            var actual = _cpu.Accumulator.Value;

            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:XX} | Actual: 0x{actual:XX}");
        }
    }
}
