#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests.ObjectTests
{
    [TestClass]
    public class ProcessorTests
    {
        private Mock<IVirtualConsole> consoleMock = new Mock<IVirtualConsole>();
        private Processor _cpu;

        [TestInitialize]
        public void Setup()
        {
            _cpu = new Processor(consoleMock.Object);
        }

        [TestMethod]
        public void InterruptPushesReturnAddressAndJumpsToIrqVector()
        {
            // Setup Test Data
            var expectedReturnAddress = (DWord6502)0x0603;  // Expected return address (PC + 2)
            var irqVectorAddress = (DWord6502)0xFFFE;       // IRQ Vector Location
            var targetAddress = (DWord6502)0x3000;          // Where execution should jump

            // Set up the Program Counter and IRQ Vector
            _cpu.ProgramCounter = expectedReturnAddress;
            _cpu.Memory[irqVectorAddress] = targetAddress.LowPart;
            _cpu.Memory[irqVectorAddress + 1] = targetAddress.HighPart;

            // Set some Processor Status Flags before IRQ
            _cpu.ProcessorStatus.CarryFlag = true;
            _cpu.ProcessorStatus.ZeroFlag = false;
            _cpu.ProcessorStatus.NegativeFlag = true;

            var expectedProcessorStatus = (byte)(_cpu.ProcessorStatus.Value & ~ProcessorStatusRegister.BrkBit | ProcessorStatusRegister.Bit5);

            // Simulate an IRQ event
            _cpu.Interrupt();

            // Fetch expected values from stack
            var actualStatus = _cpu.PopStack();
            var actualLowPart = _cpu.PopStack();
            var actualHighPart = _cpu.PopStack();

            var actualReturnAddress = new DWord6502(actualLowPart, actualHighPart);

            // Debug Trace Output
            Debug.WriteLine($"Test Initial Program Counter: {_cpu.ProgramCounter}");
            Debug.WriteLine($"Stack Pointer Before IRQ: {_cpu.StackPointer.Value:X4}");
            Debug.WriteLine($"Interrupt Vector [0xFFFE]: {_cpu.Memory[irqVectorAddress]:X2}{_cpu.Memory[irqVectorAddress + 1]:X2}");
            Debug.WriteLine($"Fetched Interrupt Vector Address: {targetAddress}");
            Debug.WriteLine($"Computed Return Address: {expectedReturnAddress}");
            Debug.WriteLine($"Pushed Processor Status: 0x{actualStatus:X2}");
            Debug.WriteLine($"Actual Return Address from Stack: {actualReturnAddress}");

            // Assertions
            Assert.AreEqual(targetAddress, _cpu.ProgramCounter, $"Expected jump to {targetAddress}, but got {_cpu.ProgramCounter}");
            Assert.AreEqual(expectedReturnAddress, actualReturnAddress, $"Expected Return Address: {expectedReturnAddress}, but got {actualReturnAddress}");

            // Expected Processor Status: Break bit cleared, Bit5 set
            Assert.AreEqual(expectedProcessorStatus, actualStatus, $"Processor Status should match expected after IRQ.");
        }

        [TestMethod]
        public void NonMaskableInterruptPushesReturnAddressAndJumpsToNmiVector()
        {
            // Setup Test Data
            var expectedReturnAddress = (DWord6502)0x0603;  // Expected return address (PC + 2)
            var nmiVectorAddress = (DWord6502)0xFFFA;       // NMI Vector Location
            var targetAddress = (DWord6502)0x4000;          // Where execution should jump

            // Set up the Program Counter and NMI Vector
            _cpu.ProgramCounter = expectedReturnAddress;
            _cpu.Memory[nmiVectorAddress] = targetAddress.LowPart;
            _cpu.Memory[nmiVectorAddress + 1] = targetAddress.HighPart;

            // Set some Processor Status Flags before NMI
            _cpu.ProcessorStatus.CarryFlag = true;
            _cpu.ProcessorStatus.ZeroFlag = false;
            _cpu.ProcessorStatus.NegativeFlag = true;

            // Expected Processor Status after NMI: Break bit cleared, Bit 5 set
            var expectedProcessorStatus = (byte)(_cpu.ProcessorStatus.Value & ~ProcessorStatusRegister.BrkBit | ProcessorStatusRegister.Bit5);

            // Simulate an NMI event
            _cpu.NonMaskableInterrupt();

            // Fetch expected values from stack
            var actualStatus = _cpu.PopStack();
            var actualLowPart = _cpu.PopStack();
            var actualHighPart = _cpu.PopStack();

            var actualReturnAddress = new DWord6502(actualLowPart, actualHighPart);

            // Debug Trace Output
            Debug.WriteLine($"Test Initial Program Counter: {_cpu.ProgramCounter}");
            Debug.WriteLine($"Stack Pointer Before NMI: {_cpu.StackPointer.Value:X4}");
            Debug.WriteLine($"NMI Vector [0xFFFA]: {_cpu.Memory[nmiVectorAddress]:X2}{_cpu.Memory[nmiVectorAddress + 1]:X2}");
            Debug.WriteLine($"Fetched NMI Vector Address: {targetAddress}");
            Debug.WriteLine($"Computed Return Address: {expectedReturnAddress}");
            Debug.WriteLine($"Pushed Processor Status: 0x{actualStatus:X2}");
            Debug.WriteLine($"Actual Return Address from Stack: {actualReturnAddress}");

            // Assertions
            Assert.AreEqual(targetAddress, _cpu.ProgramCounter, $"Expected jump to {targetAddress}, but got {_cpu.ProgramCounter}");
            Assert.AreEqual(expectedReturnAddress, actualReturnAddress, $"Expected Return Address: {expectedReturnAddress}, but got {actualReturnAddress}");
            Assert.AreEqual(expectedProcessorStatus, actualStatus, $"Processor Status should match expected after NMI.");
        }

    }
}
