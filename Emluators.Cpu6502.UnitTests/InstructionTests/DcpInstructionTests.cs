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
    public class DcpInstructionTests
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
            _cpu.Memory[0x0200] = 0x33;
            _cpu.Memory[0x0201] = 0x33;

            // Memory setup for addressing modes
            _cpu.Memory[0x20] = 0x10;    // ZeroPage
            _cpu.Memory[0x33] = 0x20;
            _cpu.Memory[0x30] = 0x20;    // ZeroPageX
            _cpu.Memory[0x3333] = 0x50; // Absolute
            _cpu.Memory[0x3338] = 0x60; // AbsoluteX
            _cpu.Memory[0x0060] = 0x50;
            _cpu.Memory[0x333A] = 0x70; // AbsoluteY

            _cpu.Memory[0x0024] = 0x80;  // Indirect Indexed Mode

            _cpu.Memory[0x70] = 0x80;    // IndexedIndirect Low Byte
            _cpu.Memory[0x71] = 0x00;    // IndexedIndirect High Byte
            _cpu.Memory[0x80] = 0x20; // Low byte of indirect indexed address
            _cpu.Memory[0x81] = 0x00; // High byte of indirect indexed address
        }

        [DataTestMethod]
        [DataRow(OpCodes.DcpAbsolute, (ushort)0x3333, (byte)0x50, (byte)0x40)]
        [DataRow(OpCodes.DcpZeroPage, (ushort)0x20, (byte)0x10, (byte)0x20)]
        public void Dcp_Executes_Correctly(OpCodes opcode, ushort address, byte initialMemoryValue, byte initialAccumulator)
        {
            _cpu.Memory[address] = initialMemoryValue;
            _cpu.Accumulator.Value = initialAccumulator;

            var expectedMemoryValue = (byte)(initialMemoryValue - 1);
            var expectedResult = (byte)(initialAccumulator - expectedMemoryValue);
            var carry = initialAccumulator >= expectedMemoryValue;
            var zeroFlag = expectedResult == 0;
            var negativeFlag = (expectedResult & ProcessorStatusRegister.NegativeBit) != 0;

            switch (opcode)
            {
                case OpCodes.DcpZeroPage:
                    expectedMemoryValue = (byte)(initialMemoryValue - 1);
                    expectedResult = (byte)(initialAccumulator - expectedMemoryValue);
                    break;
                default:
                    expectedMemoryValue = (byte)(initialMemoryValue - 1);
                    expectedResult = (byte)(initialAccumulator - expectedMemoryValue);
                    break;
            }

            InstructionRegistry.Instance.TryGetInstruction(opcode, out var instruction);
            instruction.Execute(_cpu);

            Assert.AreEqual(expectedMemoryValue, _cpu.Memory[address], "DCP should decrement the memory value.");
            Assert.AreEqual(carry, _cpu.ProcessorStatus.CarryFlag, "DCP should set the Carry flag correctly.");
            Assert.AreEqual(zeroFlag, _cpu.ProcessorStatus.ZeroFlag, "DCP should set the Zero flag correctly.");
            Assert.AreEqual(negativeFlag, _cpu.ProcessorStatus.NegativeFlag, "DCP should set the Negative flag correctly.");
        }

        [DataTestMethod]
        [DataRow(OpCodes.DcpAbsoluteX, (ushort)0x3333, (byte)0x60, (byte)0x50, (byte)0x05)]
        [DataRow(OpCodes.DcpZeroPageX, (ushort)0x30, (byte)0x20, (byte)0x30, (byte)0x02)]
        public void Dcp_X_Executes_Correctly(OpCodes opcode, ushort address, byte initialMemoryValue, byte initialAccumulator, byte indexValue)
        {
            _cpu.Memory[address] = initialMemoryValue;
            _cpu.Accumulator.Value = initialAccumulator;
            _cpu.IndexerX.Value = indexValue;

            InstructionRegistry.Instance.TryGetInstruction(opcode, out var instruction);
            instruction.Execute(_cpu);

            DWord6502 indexedAddress, actualMemoryValue;
            var expectedMemoryValue = 0x00;
            var actualOffsetValue = 0x00;
            var actualCarry = _cpu.ProcessorStatus.CarryFlag;
            var actualZeroFlag = _cpu.ProcessorStatus.ZeroFlag;
            var actualNegativeFlag = _cpu.ProcessorStatus.NegativeFlag;

            switch (opcode)
            {
                case OpCodes.DcpAbsoluteX:
                    indexedAddress = (DWord6502)(address + indexValue);
                    expectedMemoryValue = (byte)(initialAccumulator - 1);
                    actualMemoryValue = _cpu.Memory[initialMemoryValue];
                    break;
                case OpCodes.DcpZeroPageX:
                    indexedAddress = (ushort)((_cpu.Memory[0x0200] + indexValue) & 0xFF);
                    expectedMemoryValue = (byte)0xFF;
                    actualMemoryValue = _cpu.Memory[indexedAddress];
                    actualOffsetValue = _cpu.Memory[initialMemoryValue];
                    break;// ZeroPage wrap-around
                default:
                    throw new InvalidOperationException($"Unsupported OpCode {opcode}");
            };

            var expectedResult = _cpu.Accumulator.Value - expectedMemoryValue;
            var expectedCarry = initialAccumulator >= expectedMemoryValue;
            var expectedZeroFlag = expectedResult == 0;
            var expectedNegativeFlag = (expectedResult & ProcessorStatusRegister.NegativeBit) != 0;

            Assert.AreEqual(expectedMemoryValue, actualMemoryValue, "DCP should decrement the memory value.");
            Assert.AreEqual(expectedCarry, actualCarry, "DCP should set the Carry flag correctly.");
            Assert.AreEqual(expectedZeroFlag, actualZeroFlag, "DCP should set the Zero flag correctly.");
            Assert.AreEqual(expectedNegativeFlag, actualNegativeFlag, "DCP should set the Negative flag correctly.");
        }

        [DataTestMethod]
        [DataRow(OpCodes.DcpAbsoluteY, (ushort)0x3333, (byte)0x80, (byte)0x60, (byte)0x07)]
        [DataRow(OpCodes.DcpIndirectIndexed, (ushort)0x3333, (byte)0x80, (byte)0x40, (byte)0x04)]
        public void Dcp_Y_Executes_Correctly(OpCodes opcode, ushort address, byte initialMemoryValue, byte initialAccumulator, byte indexValue)
        {
            _cpu.Memory[address] = initialMemoryValue;
            _cpu.Accumulator.Value = initialAccumulator;
            _cpu.IndexerY.Value = indexValue;

            InstructionRegistry.Instance.TryGetInstruction(opcode, out var instruction);
            instruction.Execute(_cpu);

            DWord6502 actualMemoryValue, indexedAddress, expectedMemoryValue;
            byte expectedResult, actualResult = 0x00;

            switch (opcode)
            {
                case OpCodes.DcpAbsoluteY:
                    indexedAddress = (DWord6502)(address + indexValue);
                    expectedMemoryValue = (byte)(initialMemoryValue - 1);
                    expectedResult = (byte)(initialAccumulator - expectedMemoryValue);
                    actualMemoryValue = _cpu.Memory[_cpu.Memory[indexedAddress]];
                    actualResult = _cpu.Accumulator.Value;
                    break;
                case OpCodes.DcpIndirectIndexed:
                    // The address points to the zero-page location where the base address is stored
                    var zeroPageAddress = (ushort)(address & 0x00FF); // Ensure zero-page wraparound
                    // Read the base address from the zero page (16-bit address formed from two bytes)
                    indexedAddress = new DWord6502(_cpu.Memory[zeroPageAddress], _cpu.Memory[(zeroPageAddress + 1) & 0x00FF]);
                    // Calculate final address using Y register as an offset
                    var actualOffsetValue = indexedAddress + indexValue;
                    // Expected memory value after increment
                    expectedMemoryValue = (byte)(initialMemoryValue - 1);
                    // Read the incremented value from memory
                    actualMemoryValue = _cpu.Memory[actualOffsetValue];
                    // Expected accumulator result after SBC
                    expectedResult = (byte)(initialAccumulator - _cpu.Memory[(ushort)actualOffsetValue]);
                    actualResult = _cpu.Accumulator.Value;
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported OpCode: {opcode}");
            }

            var carry = initialAccumulator >= expectedMemoryValue;
            var zeroFlag = expectedResult == 0;
            var negativeFlag = (expectedResult & ProcessorStatusRegister.NegativeBit) != 0;

            Assert.AreEqual(expectedMemoryValue, actualMemoryValue, "DCP should decrement the memory value.");
            Assert.AreEqual(carry, _cpu.ProcessorStatus.CarryFlag, "DCP should set the Carry flag correctly.");
            Assert.AreEqual(zeroFlag, _cpu.ProcessorStatus.ZeroFlag, "DCP should set the Zero flag correctly.");
            Assert.AreEqual(negativeFlag, _cpu.ProcessorStatus.NegativeFlag, "DCP should set the Negative flag correctly.");
        }
    }
}
