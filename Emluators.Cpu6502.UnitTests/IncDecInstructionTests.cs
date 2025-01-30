#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests
{
    [TestClass]
    public class IncDecInstructionTests
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
        public async Task InxIncrementsXRegister()
        {
            _cpu.IndexerX.Value = 0x10;
            await _cpu.LoadProgramAsync([(byte)OpCodes.InxImplied]);

            _cpu.Step();

            Assert.AreEqual(0x11, _cpu.IndexerX.Value);
            Assert.IsFalse(_cpu.ProcessorStatus.ZeroFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.NegativeFlag);
        }

        [TestMethod]
        public async Task DexDecrementsXRegister()
        {
            _cpu.IndexerX.Value = 0x01;
            await _cpu.LoadProgramAsync([(byte)OpCodes.DexImplied]);

            _cpu.Step();

            Assert.AreEqual(0x00, _cpu.IndexerX.Value);
            Assert.IsTrue(_cpu.ProcessorStatus.ZeroFlag);
            Assert.IsFalse(_cpu.ProcessorStatus.NegativeFlag);
        }
    }

}
