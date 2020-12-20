using System.Collections.Generic;

namespace HackAssembler
{
    public class Code {
         static readonly Dictionary<string,string> dests = new Dictionary<string, string>(){
             {"A","100"},
             {"D","010"},
             {"M","001"},
             {"","000"},
             {"AMD","111"},
             {"MD","011"},
             {"AM","101"},
             {"AD","110"}
        };
        static readonly Dictionary<string, string> comps = new Dictionary<string, string>()
        {
            {"0",  "101010"},
            {"1",  "111111"},
            {"-1", "111010"},
            {"D",  "001100"},
            {"G",  "110000"},
            {"!D", "001101"},
            {"!G", "110001"},
            {"-D", "001111"},
            {"-G", "110011"},
            {"D+1","011111"},
            {"G+1","110111"},
            {"D-1","001110"},
            {"G-1","110010"},
            {"D+G","000010"},
            {"G+D","000010"},
            {"D-G","010011"},
            {"G-D","000111"},
            {"D&G","000000"},
            {"G&D","000000"},
            {"D|G","010101"},
            {"G|D","010101"}
        };
        static readonly Dictionary<string, string> jmps = new Dictionary<string, string>() { 
            {"","000"},
            {"JGT","001"},
            {"JEQ","010"},
            {"JGE","011"},
            {"JLT","100"},
            {"JNE","101"},
            {"JLE","110"},
            {"JMP","111"}
        };

        public static string dest(string mn) => dests[mn];
        public static string comp(string nm){
            int b12 = 0;
            if(nm.Contains("M")){
                b12 = 1;
            }
            return b12.ToString() + comps[nm.Replace("M","G").Replace("A","G")];
        }
        public static string jmp(string nm) => jmps[nm];

    }
}
