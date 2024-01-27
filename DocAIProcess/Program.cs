using System;

namespace DocAIProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            AIDOcumentProcessor proc = new AIDOcumentProcessor();
            proc.Process().Wait();
        }
    }
}
