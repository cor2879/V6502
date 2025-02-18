#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests
{
    [TestClass]
    public class LaxInstructionTests
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

            // Absolute Mode setup
            _cpu.Memory[0x0200] = 0x11;
            _cpu.Memory[0x0201] = 0x01;
            // _cpu.Memory[0x0100] = 0x33;
            _cpu.Memory[0x0111] = 0x33;
            _cpu.Memory[0x11] = 0xEE;

            // IndexedIndirect Zero Page setup
            _cpu.Memory[0x66] = 0x33;
            _cpu.Memory[0x67] = 0x33;
            _cpu.Memory[0x3333] = 0xBA;
        }

        [DataTestMethod]
        [DataRow(OpCodes.LaxImmediate, (ushort)0x200, (byte)0x99)]
        [DataRow(OpCodes.LaxZeroPage, (ushort)0x30, (byte)0xEE)]
        [DataRow(OpCodes.LaxZeroPageY, (ushort)0x31, (byte)0xEE)]
        [DataRow(OpCodes.LaxAbsolute, (ushort)0x400, (byte)0x33)]
        [DataRow(OpCodes.LaxAbsoluteY, (ushort)0x500, (byte)0x33)]
        [DataRow(OpCodes.LaxIndexedIndirect, (ushort)0x600, (byte)0xBA, (byte)0x55)]
        public void Lax_Executes_Correctly(OpCodes opcode, ushort address, byte expectedValue, byte xRegister = 0x00)
        {
            _cpu.IndexerX.Value = xRegister;
            _cpu.Memory[address] = expectedValue;

            InstructionRegistry.Instance.TryGetInstruction(opcode, out var instruction);
            instruction.Execute(_cpu);

            Assert.AreEqual(expectedValue, _cpu.Accumulator.Value, $"LAX should load value {expectedValue} into Accumulator.");
            Assert.AreEqual(expectedValue, _cpu.IndexerX.Value, $"LAX should load value {expectedValue} into IndexerX.");
        }
    }
}