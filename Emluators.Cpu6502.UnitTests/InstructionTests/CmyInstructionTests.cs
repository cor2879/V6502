#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests.InstructionTests
{
    [TestClass]
    public class CpyInstructionTests
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
        public async Task CpyImmediateSetsNegativeFlag()
        {
            _cpu.IndexerY.Value = 0x10;
            var value = (byte)0x20;

            await _cpu.LoadProgramAsync([(byte)OpCodes.CpyImmediate, value]);
            _cpu.Step();

            Assert.IsFalse(_cpu.ProcessorStatus.CarryFlag);
            Assert.IsTrue(_cpu.ProcessorStatus.NegativeFlag);
        }
    }
}
