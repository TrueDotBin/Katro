using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.ConsoleApp
{
    public sealed class SampleHost : IKatroHost
    {
        public void PrintLn(string text)
        {
            Console.WriteLine(text);
        }
    }
}
