using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests.AddressingModeTests
{
    [TestClass]
    public class IndirectIndexedAddressingModeTests
    {
        private Mock<IVirtualConsole> consoleMock = new Mock<IVirtualConsole>();
        private Processor _cpu;

        [TestInitialize]
        public void Setup()
        {
            _cpu = new Processor(consoleMock.Object);
            _cpu.Memory[0xFFFD] = 0x00;
            _cpu.Memory[0xFFFC] = 0x02; // Set reset vector to 0x0200.  Main correct endian-ness
            _cpu.Reset(); // This is necessary in order to force the CPU to acquire the new ProgramCounter
        }

        [TestMethod]
        public void IndirectIndexedAddressingMode_FetchDWord_ReturnsCorrectAddress()
        {
            _cpu.IndexerY.Value = 4;
            _cpu.Memory[0x200] = 0x80;  // Operand in Program Memory
            _cpu.Memory[0x80] = 0x33;   // Low byte
            _cpu.Memory[0x81] = 0x03;   // High byte

            var mode = new IndirectIndexedAddressingMode();
            var result = mode.FetchDWord(_cpu);

            Assert.AreEqual(0x37, result.LowPart, "Low byte should be 0x37.");
            Assert.AreEqual(0x03, result.HighPart, "High byte should be 0x03.");
        }
    }
}
