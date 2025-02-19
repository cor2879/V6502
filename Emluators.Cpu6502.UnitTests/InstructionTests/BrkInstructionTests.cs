#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.AddressingModes;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests.InstructionTests
{
    [TestClass]
    public class BrkInstructionTests
    {
        private Mock<IVirtualConsole> _mockConsole;
        private Processor _cpu;

        [TestInitialize]
        public void Setup()
        {
            _mockConsole = new Mock<IVirtualConsole>();
            _cpu = new Processor(_mockConsole.Object);
        }

        [TestMethod]
        public async Task BrkPushesReturnAddressAndJumpsToInterruptVector()
        {
            var expectedReturnAddress = (DWord6502)0x0603;
            var interruptVectorAddress = (DWord6502)0xFFFE;
            var targetAddress = (DWord6502)0x2000;

            _cpu.ProgramCounter = expectedReturnAddress;
            _cpu.Memory[interruptVectorAddress] = targetAddress.LowPart;
            _cpu.Memory[interruptVectorAddress + 1] = targetAddress.HighPart;

            _cpu.ProcessorStatus.CarryFlag = true;
            _cpu.ProcessorStatus.ZeroFlag = false;
            _cpu.ProcessorStatus.NegativeFlag = true;

            await _cpu.LoadProgramAsync(new byte[] { (byte)OpCodes.Brk });

            _cpu.Memory[targetAddress] = (byte)OpCodes.Rti;

            Debug.WriteLine($"Test Initial Program Counter: {_cpu.ProgramCounter}");
            Debug.WriteLine($"Stack Pointer Before BRK: {_cpu.StackPointer.Value:X4}");
            Debug.WriteLine($"Interrupt Vector [0xFFFE]: {_cpu.Memory[interruptVectorAddress]:X2}{_cpu.Memory[interruptVectorAddress + 1]:X2}");

            var expectedProcessorStatus = (byte)(_cpu.ProcessorStatus.Value | ProcessorStatusRegister.Bit5);

            _cpu.Step(); // Execute BRK

            Debug.WriteLine($"Fetched Interrupt Vector Address: {targetAddress}");
            Debug.WriteLine($"Computed Return Address: {expectedReturnAddress}");

            _cpu.Step(); // Execute RTI

            Debug.WriteLine($"Stack Pointer After RTI: {_cpu.StackPointer.Value:X4}");
            Debug.WriteLine($"Restored Processor Status: {_cpu.ProcessorStatus.Value:X2}");
            Debug.WriteLine($"Restored Program Counter: {_cpu.ProgramCounter}");

            // Assertions
            Assert.AreEqual(expectedReturnAddress, _cpu.ProgramCounter, $"Expected Return Address: {expectedReturnAddress}, but got {_cpu.ProgramCounter}");
            Assert.AreEqual(expectedProcessorStatus, _cpu.ProcessorStatus.Value, "Processor Status should match expected after BRK.");
        }

    }

}
