using Microsoft.Testing;
using Microsoft.Testing.Platform;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Enums;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using System.Runtime.CompilerServices;

namespace Emluators.Cpu6502.UnitTests
{
    [TestClass]
    public class ProcessorTests
    {
        [TestMethod]
        public async Task LdaImmediateLoadsCorrectValue()
        {
            var expected = (byte)0x42;
            var virtualConsole = new Mock<IVirtualConsole>().Object;
            var cpu = new Processor(virtualConsole);

            await cpu.LoadProgramAsync(
            [
                (byte)OpCodes.LdaImmediate,
                expected
            ]);

            cpu.Step();
            var actual = cpu.Accumulator.Value;

            Assert.AreEqual(expected, actual, $"Expected: 0x{expected:XX} | Actual: 0x{actual:XX}");
            Assert.IsFalse(cpu.ProcessorStatus.ZeroFlag);
            Assert.IsFalse(cpu.ProcessorStatus.NegativeFlag);
        }
    }
}
