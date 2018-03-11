using NFlow.Core;
using NFlow.Core.Global;
using NFlow.Core.Middlewares.Tracing;
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

            Flow<String> flow = new Flow<String>();
            flow.Register(new ConsoleMiddleware<String>());

            // define flow
            //flow.Actions
            //    .WriteLine("Step 1")
            //    .WriteLine("Step 2")
            //    .WriteLine("Step 3");

            flow.Actions
                .If(ctx => 1 == 1, a => a.WriteLine("Do this...")).Else(a => a.WriteLine("Do something else..."))

                .WriteLine("Step 1")
                .StartActionsGroup(
                    a => a
                        .WriteLine("sub-Action1").WriteLine("sub-Action 2")
                        .If(ctx2 => 2 != 1, 
                        t2 => t2.StartActionsGroup(
                            a2 => a2
                                .WriteLine("sub-Action1").WriteLine("sub-Action 2")
                                .If(ctx2 => 2 != 1, t21 => t21.WriteLine("AAA"), f21 => f21.WriteLine("BBB"))

                        ), 
                        f2 => f2.StartActionsGroup(
                            a2 => a2
                                .WriteLine("sub-Action1").WriteLine("sub-Action 2")
                                .If(ctx2 => 2 != 1, t22 => t22.WriteLine("AAA"), f22 => f22.WriteLine("BBB"))

                        ))

                        .StartActionsGroup(
                            a2 => a2
                                .WriteLine("sub-Action1").WriteLine("sub-Action 2")
                                .If(ctx2 => 2 != 1, t2 => t2.WriteLine("AAA"), f2 => f2.WriteLine("BBB"))

                        )

                )
                .WriteLine("Step 2")
                .WriteLine("Step 3")

                .If(ctx => 1 == 1, a => a.WriteLine("Do this..."), "?", "a==1")
                .ElseIf(c => 1 == 2, a => a.WriteLine("Or this..."), "a==2")
                .Else(a => a.WriteLine("Do this instead"), "Else")

                .If(ctx => 1 == 1,
                    t => t.WriteLine("true").If(ctx2 => 2 != 1, t2 => t2.WriteLine("asd"), f2 => f2.WriteLine("aslkdj")),
                    f => f.WriteLine("false"))

                .WriteLine("Step 4")
                .WriteLine("Step 5");



            // execute flow
            var context = new FlowContext<String>(String.Empty);
            flow.Execute(context);

            var dotNotation = flow.ToDotNotation();

            Console.ReadLine();
        }
    }
}
