using System;
using System.Collections.Generic;

namespace playGround
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sets up  a worker unit and apllies it to the testLab
            Worker W = new Worker();
            var tests = new TestLab(W);
        }
    }
}
