using NFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.CLI
{
    class Program
    {
        static void Main(string[] args)
        {

            Flow<String> flow = new Flow<string>();

            // define flow
            flow.Actions
                .WriteLine("Starting...")
                .If(("1=1?", "Yes", "No"), c => 1 == 1, 
                    t => t.WriteLine("It's true").WriteLine("Do somthing"), 
                    f => f.WriteLine("It's false").WriteLine("Do something else")
                );

            // execute flow
            flow.Execute();

            // export to Dot Notation
            var dotNotation = flow.ToDotNotation();

            Console.ReadLine();
        }
    }
}
