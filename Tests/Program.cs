﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Tests
{
    class Program
    {
        static Program()
        {
#if NET5_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
        }

        static int Main(string[] args)
        {
            var errors = 0;
            var types = typeof(Program).Assembly.GetTypes();
            foreach (var type in types.OrderByDescending(t => t.Name))
            {
                if (!type.GetCustomAttributes(typeof(TestFixtureAttribute), false).Any())
                {
                    continue;
                }

                var instance = Activator.CreateInstance(type);
                foreach (var method in type.GetMethods())
                {
                    if (!method.GetCustomAttributes(typeof(TestAttribute), false).Any())
                    {
                        continue;
                    }

                    GC.Collect(999, GCCollectionMode.Default);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{method.DeclaringType.Name}.cs: info TI0001: Start {method.Name}");
                    Console.ResetColor();
                    try
                    {
                        method.Invoke(instance, null);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{method.DeclaringType.Name}.cs: info TI0002: Success {method.Name}");
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        if (ex is AggregateException) ex = ex.InnerException;

                        Debug.WriteLine(ex);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{method.DeclaringType.Name}.cs: error TE0001: {ex.Message}");
                        Console.WriteLine(ex);
                        Console.ResetColor();
                        errors++;
                    }
                    Console.WriteLine("---");
                }
            }
            if (errors == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"---: info TI9999: All tests successfully completed.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"---: error TE9999: {errors} tests failed!");
            }
            Console.ResetColor();
            if (Debugger.IsAttached)
            {
                WaitExit();
            }

            return errors;
        }

        static void WaitExit()
        {
            Console.Write("--- press enter to exit ---");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                ;
            }
        }
    }
}
