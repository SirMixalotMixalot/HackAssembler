using System.Collections.Generic;
namespace HackAssembler {
    public class SymbolTable {
        public Dictionary<string, int> table;
        int current_reg = 15;
        public SymbolTable(){
            table = new Dictionary<string, int>(){
               {"SP",0},
               {"LCL",1},
               {"ARG",2},
               {"THIS",3},
               {"THAT",4},
               {"SCREEN",0x4000},
               {"KBD",0x6000}
               };
            for (int i = 0; i < 16; i++)
                table.Add($"R{i}", i);
        
        }
        public void addEntry(string k) { table.Add(k, ++current_reg); }
        public void addEntry(string k,int v) { table.Add(k, v); }
        public bool contains(string k) => table.ContainsKey(k);
        public int getAddress(string k) => table[k];

    }
}