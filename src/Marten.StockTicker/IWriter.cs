using System;

namespace Marten.StockTicker
{
    public interface IWriter
    {
        void Write(string write);
        void WriteLine(string line);

        void Render();
        void SetColor(ConsoleColor consoleColor);
        void ResetColor();
    }
}