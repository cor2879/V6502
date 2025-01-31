#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests
{
    [TestClass]
    public class RtsInstructionTests
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
        public async Task RtsRestoresProgramCounter()
        {
            var returnAddress = (DWord6502)0x4000;
            var expectedAddress = returnAddress + 1;

            _cpu.PushStack(returnAddress.HighPart);
            _cpu.PushStack(returnAddress.LowPart);

            await _cpu.LoadProgramAsync([(byte)OpCodes.Rts]);
            _cpu.Step();

            Assert.AreEqual(expectedAddress, _cpu.ProgramCounter);
        }
    }
}
