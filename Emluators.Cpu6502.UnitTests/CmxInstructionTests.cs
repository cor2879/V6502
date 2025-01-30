#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests
{
    [TestClass]
    public class CpxInstructionTests
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
        public async Task CpxImmediateSetsCarryFlag()
        {
            _cpu.IndexerX.Value = 0x40;
            var value = (byte)0x30;

            await _cpu.LoadProgramAsync([(byte)OpCodes.CpxImmediate, value]);
            _cpu.Step();

            Assert.IsTrue(_cpu.ProcessorStatus.CarryFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.ZeroFlag);
        }
    }

}
