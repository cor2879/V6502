#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests.InstructionTests
{
    [TestClass]
    public class JsrInstructionTests
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
        public async Task JsrPushesReturnAddressAndJumps()
        {
            var returnAddress = (DWord6502)0x0602;
            var targetAddress = (DWord6502)0x2000;

            _cpu.ProgramCounter = returnAddress;
            await _cpu.LoadProgramAsync([(byte)OpCodes.Jsr, targetAddress.LowPart, targetAddress.HighPart]);

            _cpu.Step();

            var actualLowPart = _cpu.PopStack();
            var actualHighPart = _cpu.PopStack();

            Assert.AreEqual(targetAddress, _cpu.ProgramCounter);
            Assert.AreEqual(returnAddress.LowPart, actualLowPart, $"Expected LowPart: 0x{returnAddress.LowPart:X2} | Actual LowPart: 0x{actualLowPart:X2}");
            Assert.AreEqual(returnAddress.HighPart, actualHighPart, $"Expected HighPart: 0x{returnAddress.HighPart:X2} | Actual HighPart: 0x{actualHighPart:X2}");
        }
    }

}
