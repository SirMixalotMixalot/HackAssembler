using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
namespace HackAssembler
{
    class Program
    {
        static void Main(string[] args) //Apparently first argument is not file name lol
        {
             if(args.Length == 0) {
                Console.WriteLine("Must supply path to .asm file to assemble...");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("FATAL ERROR : NO INPUT FILE");
                Console.ResetColor();
                Console.WriteLine("USAGE : HackAssembler.exe <INPUT-FILE> (The output (.hack) file will be have the same name as the input file.)");
                return;
            }  
            
            string file = args[0];
            var data = File.ReadAllLines(file).Where(x => x != "" && x[0] !='/' ).Select(x => Regex.Replace(x,@"\s+","")).ToArray();
            //var lstats = data.Where(x => x.IndexOf("(") == 0 && x.IndexOf(")") != -1).Select(x => x[(x.IndexOf("(")+1)..(x.IndexOf(")"))]);
            List<string> assembly = new List<string>();
            Parser p = new Parser(data);
            
            while(p.hasMoreCommands()){
                
                    if(p.currentCommand == Command.ACommand){
                        int val = Int16.Parse(p.symbol());
                        string bin = Convert.ToString(val, 2);
                        while(bin.Length < 15){
                            bin = "0" + bin;
                        }
                        assembly.Add("0" + bin);
                    }else if(p.currentCommand == Command.CCommand) {
                        string dest = p.dest();
                        string comp = p.comp();
                        string jmp = p.jmp();
                        assembly.Add($"111{Code.comp(comp)}{Code.dest(dest)}{Code.jmp(jmp)}");
                    }
                    p.Next();
            }
            string ofile = file.Replace(".asm", ".hack");
            File.WriteAllLines(ofile, assembly);
            Console.WriteLine($"Wrote {assembly.Count} lines to {ofile}");

        }
    }
    
}
