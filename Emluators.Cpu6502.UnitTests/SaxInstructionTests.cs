using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;
using System.Net;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests
{
    [TestClass]
    public class SaxInstructionTests
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

            // Setup Memory
            _cpu.Memory[0x2000] = 0x44;
            _cpu.Memory[0x2001] = 0x44;
            _cpu.Memory[0x0200] = 0x33;
            _cpu.Memory[0x0201] = 0x33;
            _cpu.Memory[0xBB] = 0x34;
            _cpu.Memory[(DWord6502)((0xBB & 0xFF00) | ((0xBB + 1) & 0x00FF))] = 0x12;
        }

        [DataTestMethod]
        [DataRow(OpCodes.SaxZeroPage, (ushort)0x30, (byte)0xAA, (byte)0x55)]  // ZeroPage Mode
        [DataRow(OpCodes.SaxZeroPageY, (ushort)0x31, (byte)0xBB, (byte)0x66)] // ZeroPageY Mode
        [DataRow(OpCodes.SaxAbsolute, (ushort)0x3333, (byte)0xCC, (byte)0x77)] // Absolute Mode
        [DataRow(OpCodes.SaxIndexedIndirect, (ushort)0x1234, (byte)0xDD, (byte)0x88)] // IndexedIndirect Mode
        public void Sax_Executes_Correctly(OpCodes opcode, ushort address, byte aValue, byte xValue)
        {
            _cpu.Accumulator.Value = aValue;
            _cpu.IndexerX.Value = xValue;
            var expectedValue = (byte)(aValue & xValue);

            InstructionRegistry.Instance.TryGetInstruction(opcode, out var instruction);
            instruction.Execute(_cpu);

            if (opcode is OpCodes.SaxZeroPageY)
            {
                address = ZeroPageYAddressingMode.PeekCurrentAddress(_cpu);
            }

            Assert.AreEqual(expectedValue, _cpu.Memory[address],
                $"SAX should store A & X result in memory. Expected: {expectedValue}, Actual: {_cpu.Memory[address]}");
        }
    }
}
