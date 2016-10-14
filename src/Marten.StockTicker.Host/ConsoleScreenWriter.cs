using System;
using System.Collections.Generic;

namespace Marten.StockTicker.Host
{
    public class ConsoleScreenWriter : IWriter
    {
        private readonly List<string> _lines = new List<string>();

        public void Write(string write)
        {
            _lines.Add(write);
        }

        public void WriteLine(string line)
        {
            _lines.Add($"{line}\n");
        }

        public void Render()
        {
            Console.Clear();
            foreach (var line in _lines)
            {
                Console.Write(line);
            }

            _lines.Clear();
        }

        public void SetColor(ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
        }

        public void ResetColor()
        {
            Console.ResetColor();
        }
    }
}