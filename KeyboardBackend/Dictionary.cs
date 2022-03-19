using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace KeyboardBackend
{
    public class Dictionary
    {
        public List<string> wordList { get; set; }
        public Dictionary()
        {
            var filepath = Directory.GetCurrentDirectory().Replace(@"bin\Debug\net5.0", "") + @"\dictionary2.txt";
            var logFile = File.ReadAllLines(filepath);
            wordList = new List<string>(logFile);

        }
       
    }
}
