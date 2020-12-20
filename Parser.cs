using System.Collections.Generic;

namespace HackAssembler
{
     public enum Command {
        ACommand,
        CCommand,
        LCommand
    }
    public class Parser {
        SymbolTable table;
        string[] data;
        int _index;
        string _current;
        public Command currentCommand;
        public Parser(string[] file){

            data = file;
            _current = data[_index++];
            table = new SymbolTable();
            buildTable();
           

        }
        public void Next()
        {
            if (_index == data.Length) {
                _current =  "\0";
                return;
            }
            
            _current = data[_index++];
        
            currentCommand = CommandType();
        }
        public bool hasMoreCommands(){
            return _current != "\0";
        
            
        }
         Command CommandType() {
            
            if(_current.Contains('@')){
                return Command.ACommand;
            }
            if(_current.Contains("(") && _current.Contains(")")){
                return Command.LCommand;
            }
            return Command.CCommand;
        }
        public string symbol() =>
             (currentCommand) switch
             {
                 Command.ACommand => desymbolize(_current[1..]),
                 Command.LCommand => table.getAddress(_current[(_current.IndexOf("(")+1)..(_current.IndexOf(")"))]).ToString(),
                 _ => ""

            };
        
        public string dest(){
            if(currentCommand == Command.CCommand){
                var end = _current.IndexOf('=');
                if(end == -1){
                    return "";
                }
                return _current[..end];
            }
            return "";
        }
        public string comp() {
            if(currentCommand == Command.CCommand){
                var start = _current.IndexOf("=")+1;
                var end = _current.IndexOf(";");
                if(end == -1)
                    end = _current.Length;
                return _current[start..end];
            }
            return "";
        }

        public string jmp(){
            if(currentCommand == Command.CCommand){
                var start = _current.IndexOf(";")+1;
                if(start == 0)
                    return "";
                return _current[start..];
            }
            return "";
        }
        string desymbolize(string sym) => table.contains(sym)? table.getAddress(sym).ToString() : sym;
    
        void buildTable(){
            
            int lcoms = 0;
            var unk = new List<string>();
            
            for (int i = 0; i < data.Length; i++)
            {
                string line = data[i];
                var end = line.IndexOf('/');
                if(end == -1){end = line.Length;}
                data[i] = line[..end];
                line = data[i];
                if(line[0] == '@'){
                    var sym = line[1..];
                    if(int.TryParse(sym,out var x) || table.contains(sym))
                        continue;
                    unk.Add(sym);
                }
                else if (line[0] == '(' && line.IndexOf(')') != -1){
                    var entry = line[1..line.IndexOf(')')];
                    table.addEntry(entry,i-lcoms);
                    lcoms++;
                   
                }
            }
            foreach(string u in unk)
                if(!table.contains(u))
                    table.addEntry(u);
            

        }
        

    }
}
