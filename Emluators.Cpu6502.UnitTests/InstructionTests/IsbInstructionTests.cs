#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests.InstructionTests
{
    [TestClass]
    public class IsbInstructionTests
    {
        private Mock<IVirtualConsole> consoleMock = new Mock<IVirtualConsole>();
        private Processor _cpu;

        [TestInitialize]
        public void Setup()
        {
            _cpu = new Processor(consoleMock.Object);
            _cpu.Memory[0xFFFD] = 0x00;
            _cpu.Memory[0xFFFC] = 0x02; // Set reset vector to 0x0200
            _cpu.Memory[0x0200] = 0x33;
            _cpu.Memory[0x0201] = 0x33;

            _cpu.Reset();

            // Set up necessary memory locations for different addressing modes
            _cpu.Memory[0x20] = 0x10;
            _cpu.Memory[0x3333] = 0x90; // Absolute
            _cpu.Memory[0x3336] = 0x50; // Absolute Y
            _cpu.Memory[0x3338] = 0x50; // Absolute X
            _cpu.Memory[0x0050] = 0x50;
            _cpu.Memory[0x0035] = 0x31;
            _cpu.Memory[0x33] = 0x20;  // Zero Page Mode
            _cpu.Memory[0x31] = 0x30;  // Zero Page X Mode
            _cpu.Memory[0x44] = 0xDD; // Indexed Indirect
            _cpu.Memory[0x45] = 0xBA;
            _cpu.Memory[0x0024] = 0x80;  // Indirect Indexed Mode

            // Set up pointers for Indexed Indirect and Indirect Indexed Modes
            _cpu.Memory[0x70] = 0x10; // Low byte of indirect address
            _cpu.Memory[0x71] = 0x00; // High byte of indirect address

            _cpu.Memory[0x80] = 0x20; // Low byte of indirect indexed address
            _cpu.Memory[0x81] = 0x00; // High byte of indirect indexed address
        }

        [DataTestMethod]
        [DataRow(OpCodes.IsbAbsolute, (ushort)0x3333, (byte)0x90, (byte)0x20)] // Absolute Mode
        [DataRow(OpCodes.IsbZeroPage, (ushort)0x33, (byte)0x20, (byte)0x10)] // ZeroPage Mode
        public void Isb_Executes_Correctly(OpCodes opcode, ushort address, byte initialMemoryValue, byte initialAccumulator)
        {
            _cpu.Memory[address] = initialMemoryValue;
            _cpu.Accumulator.Value = initialAccumulator;

            byte expectedMemoryValue;
            DWord6502 expectedAddress;

            switch (opcode)
            {
                case OpCodes.IsbZeroPage:
                    expectedMemoryValue = (byte)(initialAccumulator + 1);
                    expectedAddress = initialMemoryValue;
                    break;
                default:
                    expectedMemoryValue = (byte)(initialMemoryValue + 1);
                    expectedAddress = address;
                    break;
            }

            var carry = _cpu.ProcessorStatus.CarryFlag ? (byte)0x01 : (byte)0x00;
            var expectedResult = (byte)(initialAccumulator - expectedMemoryValue - (1 - carry));

            InstructionRegistry.Instance.TryGetInstruction(opcode, out var instruction);
            instruction.Execute(_cpu);

            Assert.AreEqual(expectedMemoryValue, _cpu.Memory[expectedAddress], "ISB should increment the memory value.");
            Assert.AreEqual(expectedResult, _cpu.Accumulator.Value, "ISB should store the correct SBC result in the accumulator.");
        }

        [DataTestMethod]
        [DataRow(OpCodes.IsbAbsoluteX, (ushort)0x3333, (byte)0x50, (byte)0x25, (byte)0x05)] // AbsoluteX Mode
        [DataRow(OpCodes.IsbIndexedIndirect, (ushort)0xBADD, (byte)0x44, (byte)0x35, (byte)0x11)] // IndexedIndirect Mode
        [DataRow(OpCodes.IsbZeroPageX, (ushort)0x33, (byte)0x32, (byte)0x15, (byte)0x02)] // ZeroPageX Mode
        public void Isb_X_Executes_Correctly(OpCodes opcode, ushort address, byte initialMemoryValue, byte initialAccumulator, byte indexValue)
        {
            _cpu.Memory[address] = initialMemoryValue;
            _cpu.Accumulator.Value = initialAccumulator;
            _cpu.IndexerX.Value = indexValue;

            var carry = _cpu.ProcessorStatus.CarryFlag ? (byte)0x01 : (byte)0x00;

            InstructionRegistry.Instance.TryGetInstruction(opcode, out var instruction);
            instruction.Execute(_cpu);

            DWord6502 indexedAddress, actualMemoryValue;
            var expectedMemoryValue = 0x00;
            var expectedResult = 0x00;
            var actualOffsetValue = 0x00;

            switch (opcode)
            {
                case OpCodes.IsbAbsoluteX:
                    indexedAddress = (DWord6502)(address + indexValue);
                    expectedMemoryValue = (byte)(initialMemoryValue + 1);
                    expectedResult = (byte)(initialAccumulator - expectedMemoryValue - (1 - carry));
                    actualMemoryValue = _cpu.Memory[indexedAddress];
                    actualOffsetValue = _cpu.Memory[initialMemoryValue];
                    break;
                case OpCodes.IsbIndexedIndirect:
                    indexedAddress = (DWord6502)(initialMemoryValue);
                    expectedMemoryValue = address;
                    actualMemoryValue = (DWord6502)(new DWord6502(_cpu.Memory[address]) - 1);
                    actualOffsetValue = new DWord6502(_cpu.Memory[indexedAddress], _cpu.Memory[indexedAddress + 1]);
                    expectedResult = (byte)(initialAccumulator - _cpu.Memory[actualOffsetValue] - (1 - carry));
                    break;
                case OpCodes.IsbZeroPageX:
                    indexedAddress = (ushort)((address + indexValue) & 0xFF);
                    expectedResult = (byte)(initialAccumulator - initialMemoryValue - (1 - carry));
                    actualMemoryValue = _cpu.Memory[indexedAddress];
                    actualOffsetValue = _cpu.Memory[initialMemoryValue];
                    break;// ZeroPage wrap-around
                default:
                    throw new InvalidOperationException($"Unsupported OpCode {opcode}");
            };

            Assert.AreEqual(initialMemoryValue, actualMemoryValue, "ISB should increment the memory value.");
            Assert.AreEqual(expectedMemoryValue, actualOffsetValue, "Memory at offset location should be incrememnted.");
            Assert.AreEqual(expectedResult, _cpu.Accumulator.Value, "ISB should store the correct SBC result in the accumulator.");
        }

        [DataTestMethod]
        [DataRow(OpCodes.IsbAbsoluteY, (ushort)0x3333, (byte)0x50, (byte)0x25, (byte)0x03)] // AbsoluteY Mode
        [DataRow(OpCodes.IsbIndirectIndexed, (ushort)0x3333, (byte)0x80, (byte)0x40, (byte)0x04)] // IndirectIndexed Mode

        public void Isb_Y_Executes_Correctly(OpCodes opcode, ushort address, byte initialMemoryValue, byte initialAccumulator, byte indexValue)
        {
            _cpu.Memory[address] = initialMemoryValue;
            _cpu.Accumulator.Value = initialAccumulator;
            _cpu.IndexerY.Value = indexValue;

            byte carry = _cpu.ProcessorStatus.CarryFlag ? (byte)0x01 : (byte)0x00;

            InstructionRegistry.Instance.TryGetInstruction(opcode, out var instruction);
            instruction.Execute(_cpu);

            DWord6502 actualMemoryValue, indexedAddress, expectedMemoryValue;
            byte expectedResult, actualResult = 0x00;

            switch (opcode)
            {
                case OpCodes.IsbAbsoluteY:
                    indexedAddress = (DWord6502)(address + indexValue);
                    expectedMemoryValue = (byte)(initialMemoryValue + 1);
                    expectedResult = (byte)(initialAccumulator - expectedMemoryValue - (1 - carry));
                    actualMemoryValue = _cpu.Memory[initialMemoryValue];
                    actualResult = _cpu.Accumulator.Value;
                    break;
                case OpCodes.IsbIndirectIndexed:
                    // The address points to the zero-page location where the base address is stored
                    var zeroPageAddress = (ushort)(address & 0x00FF); // Ensure zero-page wraparound
                    // Read the base address from the zero page (16-bit address formed from two bytes)
                    indexedAddress = new DWord6502(_cpu.Memory[zeroPageAddress], _cpu.Memory[(zeroPageAddress + 1) & 0x00FF]);
                    // Calculate final address using Y register as an offset
                    var actualOffsetValue = indexedAddress + indexValue;
                    // Expected memory value after increment
                    expectedMemoryValue = (byte)(initialMemoryValue + 1);
                    // Read the incremented value from memory
                    actualMemoryValue = _cpu.Memory[actualOffsetValue];
                    // Expected accumulator result after SBC
                    expectedResult = (byte)(initialAccumulator - _cpu.Memory[(ushort)actualOffsetValue] - (1 - carry));
                    actualResult = _cpu.Accumulator.Value;
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported OpCode: {opcode}");
            }

            Assert.AreEqual(expectedMemoryValue, actualMemoryValue, "ISB should increment the memory value.");
            Assert.AreEqual(expectedResult, actualResult, "ISB should store the correct SBC result in the accumulator.");
        }

    }
}
