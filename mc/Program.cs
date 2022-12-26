using System;
using System.Linq;
using Compiler.CodeAnalysis;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showTree = false;

            while (true)
            {
                var color = Console.ForegroundColor;

                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                if (line == "#showTree")
                {
                    showTree = !showTree;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(showTree ? "Showing parsing trees." : "Hiding parsing trees.");
                    Console.ForegroundColor = color;
                    continue;
                }
                else if (line == "#clear")
                {
                    Console.Clear();
                    continue;
                }
                
                var syntaxTree = SyntaxTree.Parse(line);
                
               
                if (showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ForegroundColor = color;
                }  

                if (!syntaxTree.Diagnostics.Any())
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;

                    var e = new Evaluator(syntaxTree.Root);
                    var result = e.Evaluate();
                    Console.Write("Result: ");
                    Console.WriteLine(line + " = " + result);
                    Console.ForegroundColor = color;
                }
                else
                {            
                    Console.ForegroundColor = ConsoleColor.Red;

                    foreach (var diagnostic in syntaxTree.Diagnostics)
                        Console.WriteLine(diagnostic);

                    Console.ForegroundColor = color;
                }
            }
        }

        static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            /* Tree part
                ├──
                │
                └──      */

            var marker = isLast ? "└──  " : "├──  ";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);
            
            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "    " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                PrettyPrint(child, indent, child == lastChild);
        }
    }
}
