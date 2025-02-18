#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests
{
    [TestClass]
    public class NopInstructionTests
    {
        private Mock<IVirtualConsole> consoleMock = new Mock<IVirtualConsole>();
        private Processor _cpu;

        [TestInitialize]
        public void Setup()
        {
            _cpu = new Processor(consoleMock.Object);
        }

        [TestMethod]
        public void Nop_Executes_Without_Changing_State()
        {
            var initialPC = _cpu.ProgramCounter;
            var instruction = new NopInstruction(Modes.Implied, 1, 2);
            instruction.Execute(_cpu);

            Assert.AreEqual(initialPC, _cpu.ProgramCounter, "NOP should not change Program Counter.");
        }

        [TestMethod]
        public void Dop_Executes_Without_Changing_State()
        {
            var initialPC = _cpu.ProgramCounter;
            var instruction = new DopInstruction(Modes.Immediate, 2, 3);
            instruction.Execute(_cpu);

            Assert.AreEqual(initialPC, _cpu.ProgramCounter, "DOP should not change Program Counter.");
        }

        [TestMethod]
        public void Top_Executes_Without_Changing_State()
        {
            var initialPC = _cpu.ProgramCounter;
            var instruction = new TopInstruction(Modes.Absolute, 3, 4);
            instruction.Execute(_cpu);

            Assert.AreEqual(initialPC, _cpu.ProgramCounter, "TOP should not change Program Counter.");
        }
    }
}
