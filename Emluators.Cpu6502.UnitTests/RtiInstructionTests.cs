#pragma warning disable CS8618
using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives;
using System.Diagnostics;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests
{
    [TestClass]
    public class RtiInstructionTests
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
        public async Task RtiRestoresProcessorStatusAndProgramCounter()
        {
            var initialStatus = (byte)(ProcessorStatusRegister.CarryBit | ProcessorStatusRegister.OverflowBit);
            var expectedStatus = (byte)((initialStatus & ~ProcessorStatusRegister.BrkBit) | ProcessorStatusRegister.Bit5);
            var returnAddress = (DWord6502)0x3000;

            // Manually push values onto the stack to simulate an interrupt return
            _cpu.PushStack(returnAddress.HighPart);
            _cpu.PushStack(returnAddress.LowPart);
            _cpu.PushStack(initialStatus);

            Debug.WriteLine($"Initial Stack Pointer: {_cpu.StackPointer.Value:X2}");

            // Execute RTI
            await _cpu.LoadProgramAsync([(byte)OpCodes.Rti]);
            _cpu.Step();

            // Assert that Processor Status was restored correctly
            Assert.AreEqual(expectedStatus, _cpu.ProcessorStatus.Value,
                $"Expected Processor Status: 0x{initialStatus:X2}, Actual: 0x{_cpu.ProcessorStatus.Value:X2}");

            // Assert that Program Counter was restored correctly
            Assert.AreEqual(returnAddress, _cpu.ProgramCounter,
                $"Expected Program Counter: 0x{returnAddress:X4}, Actual: 0x{_cpu.ProgramCounter:X4}");
        }
    }

}
