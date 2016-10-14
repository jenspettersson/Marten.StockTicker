using System;
using System.Text;

namespace Marten.StockTicker.Host
{
    public class ConsoleScreenWriter : IWriter
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public void Write(string write)
        {
            _stringBuilder.Append(write);
        }

        public void WriteLine(string line)
        {
            _stringBuilder.AppendLine(line);
        }

        public void Render()
        {
            Console.Clear();
            
            Console.Write(_stringBuilder.ToString());

            _stringBuilder.Clear();
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