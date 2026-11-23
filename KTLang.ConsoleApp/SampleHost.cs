using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.ConsoleApp
{
    public sealed class SampleHost : IHost
    {
        public void PrintLn(string text)
        {
            Console.WriteLine(text);
        }

        public void Print(string text)
        {
            Console.Write(text);
        }
    }
}
