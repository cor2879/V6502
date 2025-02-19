#pragma warning disable CS8618
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.InstructionSet.Instructions;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.UnitTests.ObjectTests
{
    [TestClass]
    public class StatusRegisterInstructionTests
    {
        private Processor _cpu;

        [TestInitialize]
        public void Setup()
        {
            _cpu = new Processor(null);
        }

        [TestMethod]
        public void SEC_Sets_CarryFlag()
        {
            var instruction = new SecInstruction();
            instruction.Execute(_cpu);
            Assert.IsTrue(_cpu.ProcessorStatus.CarryFlag);
        }

        [TestMethod]
        public void CLC_Clears_CarryFlag()
        {
            _cpu.ProcessorStatus.CarryFlag = true;
            var instruction = new ClcInstruction();
            instruction.Execute(_cpu);
            Assert.IsFalse(_cpu.ProcessorStatus.CarryFlag);
        }

        [TestMethod]
        public void SEI_Sets_IrqDisabledFlag()
        {
            var instruction = new SeiInstruction();
            instruction.Execute(_cpu);
            Assert.IsTrue(_cpu.ProcessorStatus.IrqDisabledFlag);
        }

        [TestMethod]
        public void CLI_Clears_IrqDisabledFlag()
        {
            _cpu.ProcessorStatus.IrqDisabledFlag = true;
            var instruction = new CliInstruction();
            instruction.Execute(_cpu);
            Assert.IsFalse(_cpu.ProcessorStatus.IrqDisabledFlag);
        }

        [TestMethod]
        public void SED_Sets_DecimalFlag()
        {
            var instruction = new SedInstruction();
            instruction.Execute(_cpu);
            Assert.IsTrue(_cpu.ProcessorStatus.DecimalFlag);
        }

        [TestMethod]
        public void CLD_Clears_DecimalFlag()
        {
            _cpu.ProcessorStatus.DecimalFlag = true;
            var instruction = new CldInstruction();
            instruction.Execute(_cpu);
            Assert.IsFalse(_cpu.ProcessorStatus.DecimalFlag);
        }

        [TestMethod]
        public void CLV_Clears_OverflowFlag()
        {
            _cpu.ProcessorStatus.OverflowFlag = true;
            var instruction = new ClvInstruction();
            instruction.Execute(_cpu);
            Assert.IsFalse(_cpu.ProcessorStatus.OverflowFlag);
        }
    }
}