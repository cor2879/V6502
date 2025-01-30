#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emluators.Cpu6502.UnitTests
{
    [TestClass]
    public class OraEorInstructionTests
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
        public async Task OraImmediatePerformsBitwiseOr()
        {
            var initialValue = (byte)0b10101010;
            var operand = (byte)0b11001100;
            var expected = (byte)(initialValue | operand);

            _cpu.Accumulator.Value = initialValue;
            await _cpu.LoadProgramAsync([(byte)OpCodes.OraImmediate, operand]);

            _cpu.Step();

            Assert.AreEqual(expected, _cpu.Accumulator.Value);
            Assert.AreEqual(expected == 0, _cpu.ProcessorStatus.ZeroFlag);
            Assert.AreEqual((expected & 0x80) != 0, _cpu.ProcessorStatus.NegativeFlag);
        }

        [TestMethod]
        public async Task EorImmediatePerformsBitwiseXor()
        {
            var initialValue = (byte)0b10101010;
            var operand = (byte)0b11001100;
            var expected = (byte)(initialValue ^ operand);

            _cpu.Accumulator.Value = initialValue;
            await _cpu.LoadProgramAsync([(byte)OpCodes.EorImmediate, operand]);

            _cpu.Step();

            Assert.AreEqual(expected, _cpu.Accumulator.Value);
            Assert.AreEqual(expected == 0, _cpu.ProcessorStatus.ZeroFlag);
            Assert.AreEqual((expected & 0x80) != 0, _cpu.ProcessorStatus.NegativeFlag);
        }
    }

}
