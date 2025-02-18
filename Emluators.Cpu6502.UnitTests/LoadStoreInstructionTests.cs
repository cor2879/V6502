#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests
{
    [TestClass]
    public class LoadStoreInstructionTests
    {
        private Mock<IVirtualConsole> consoleMock = new Mock<IVirtualConsole>();
        private Processor _cpu;

        [TestInitialize]
        public void Setup()
        {
            _cpu = new Processor(consoleMock.Object);
            _cpu.Memory[0xFFFD] = 0x00;
            _cpu.Memory[0xFFFC] = 0x02; // Set reset vector to 0x0200
            _cpu.Reset();
        }

        [TestMethod]
        public void Lax_Executes_Correctly()
        {
            _cpu.Memory[0x0200] = 0x42;
            var instruction = new LaxInstruction(Modes.Immediate, 2, 3);
            instruction.Execute(_cpu);

            Assert.AreEqual(0x42, _cpu.Accumulator.Value, "LAX should load Accumulator.");
            Assert.AreEqual(0x42, _cpu.IndexerX.Value, "LAX should also load X register.");
        }

        [TestMethod]
        public void Sax_Executes_Correctly()
        {
            _cpu.Accumulator.Value = 0xAA;
            _cpu.IndexerX.Value = 0x55;
            var instruction = new SaxInstruction(Modes.ZeroPage, 2, 3);
            instruction.Execute(_cpu);

            Assert.AreEqual(0x00, _cpu.Memory[0x200], "SAX should store A & X result.");
        }
    }
}