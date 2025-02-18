#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests
{
    [TestClass]
    public class SbxInstructionTests
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
        public void Sbx_Executes_Correctly()
        {
            var seedValue = (byte)0x10;
            _cpu.Accumulator.Value = 0xAA;
            _cpu.IndexerX.Value = 0x55;
            _cpu.Memory[0x200] = seedValue;
            var expectedResult = (byte)((_cpu.Accumulator.Value & _cpu.IndexerX.Value) - _cpu.Memory[0x200]);
            var expectedAndResult = (byte)(_cpu.Accumulator.Value & _cpu.IndexerX.Value);
            var expectedCarry = expectedAndResult >= seedValue;

            InstructionRegistry.Instance.TryGetInstruction(Enums.OpCodes.Sbx, out var instruction);
            instruction.Execute(_cpu);

            Assert.AreEqual(expectedResult, _cpu.IndexerX.Value, "SBX should store (A & X) - Memory result in X.");
            Assert.AreEqual(expectedResult == 0, _cpu.ProcessorStatus.ZeroFlag, "SBX should set Zero flag if result is zero.");
            Assert.AreEqual((expectedResult & ProcessorStatusRegister.NegativeBit) != 0, _cpu.ProcessorStatus.NegativeFlag, "SBX should set Negative flag if bit 7 is set.");
            Assert.AreEqual(expectedCarry, _cpu.ProcessorStatus.CarryFlag,
                $"SBX should set Carry flag if (A & X) >= Memory. Expected: {expectedCarry}, Actual: {_cpu.ProcessorStatus.CarryFlag}");
        }
    }
}
