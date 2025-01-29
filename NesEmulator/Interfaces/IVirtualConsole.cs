namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces
{
    public interface IVirtualConsole
    {
        IProcessor Cpu { get; }

        void WriteMemory(ushort address, byte value);

        byte ReadMemory(ushort address);

        void RenderFrame();

        void Reset();
    }
}
