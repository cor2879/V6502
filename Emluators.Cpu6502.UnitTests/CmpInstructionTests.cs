#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests
{
    [TestClass]
    public class CmpInstructionTests
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
        public async Task CmpImmediateSetsFlagsCorrectly()
        {
            _cpu.Accumulator.Value = 0x50;
            var value = (byte)0x30;

            await _cpu.LoadProgramAsync([(byte)OpCodes.CmpImmediate, value]);
            _cpu.Step();

            Assert.IsTrue(_cpu.ProcessorStatus.CarryFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.ZeroFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.NegativeFlag);
        }

        [TestMethod]
        public async Task CmpZeroPageSetsZeroFlag()
        {
            var address = (byte)0x20;
            _cpu.Memory[address] = 0x50;
            _cpu.Accumulator.Value = 0x50;

            await _cpu.LoadProgramAsync([(byte)OpCodes.CmpZeroPage, address]);
            _cpu.Step();

            Assert.IsTrue(_cpu.ProcessorStatus.ZeroFlag);
        }
    }
}
