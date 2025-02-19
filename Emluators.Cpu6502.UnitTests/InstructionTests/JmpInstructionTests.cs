#pragma warning disable CS8618
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;
using Moq;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests.InstructionTests
{
    [TestClass]
    public class JmpInstructionTests
    {
        private Mock<IVirtualConsole> mockConsole = new Mock<IVirtualConsole>();
        private Processor _cpu;

        [TestInitialize]
        public void Setup()
        {
            _cpu = new Processor(mockConsole.Object);
        }

        [TestMethod]
        public void Jmp_Absolute_Sets_ProgramCounter_Correctly()
        {
            var targetAddress = (DWord6502)0x1234;
            _cpu.Memory[0x200] = (byte)(targetAddress & 0xFF);
            _cpu.Memory[0x201] = (byte)(targetAddress >> 8 & 0xFF);
            _cpu.ProgramCounter = 0x200;

            var instruction = new JmpInstruction((byte)OpCodes.JmpA, Modes.Absolute);
            instruction.Execute(_cpu);

            Assert.AreEqual(targetAddress, _cpu.ProgramCounter,
                $"expected {nameof(targetAddress)}: {targetAddress} | actual {nameof(_cpu.ProgramCounter)}: {_cpu.ProgramCounter}");
        }

        [TestMethod]
        public void Jmp_Indirect_Sets_ProgramCounter_Correctly()
        {
            var pointer = (DWord6502)0x300;
            var targetAddress = (DWord6502)0x5678;
            _cpu.Memory[pointer] = (byte)(targetAddress & 0xFF);
            _cpu.Memory[pointer + 1] = (byte)(targetAddress >> 8 & 0xFF);
            _cpu.Memory[0x200] = (byte)(pointer & 0xFF);
            _cpu.Memory[0x201] = (byte)(pointer >> 8 & 0xFF);
            _cpu.ProgramCounter = 0x200;

            var instruction = new JmpInstruction((byte)OpCodes.JmpI, Modes.Indirect);
            instruction.Execute(_cpu);

            Assert.AreEqual(targetAddress, _cpu.ProgramCounter,
                $"expected {nameof(targetAddress)}: {targetAddress} | actual {nameof(_cpu.ProgramCounter)}: {_cpu.ProgramCounter}");
        }

        [TestMethod]
        public void Jmp_Indirect_Handles_Page_Boundary_Bug()
        {
            var pointer = (DWord6502)0x30FF;
            var incorrectTarget = (DWord6502)0x1278;

            // Store incorrect target address bytes at the boundary
            _cpu.Memory[pointer] = (byte)(incorrectTarget & 0xFF);
            _cpu.Memory[pointer & Constants.PAGE_BOUNDARY | pointer + 1 & 0xFF] = (byte)(incorrectTarget >> 8 & 0xFF);

            _cpu.Memory[0x200] = (byte)(pointer & 0xFF);
            _cpu.Memory[0x201] = (byte)(pointer >> 8 & 0xFF);
            _cpu.ProgramCounter = 0x200;

            InstructionRegistry.Instance.TryGetInstruction(OpCodes.JmpI, out var instruction);
            instruction.Execute(_cpu);

            Assert.AreEqual(incorrectTarget, _cpu.ProgramCounter,
                $"expected {nameof(incorrectTarget)}: {incorrectTarget} | actual {nameof(_cpu.ProgramCounter)}: {_cpu.ProgramCounter}");
        }
    }
}
