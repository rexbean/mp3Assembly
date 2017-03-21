using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MP3disassembly
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] music;
            
            
            string[] fileNames=Directory.GetFiles (@"D:\inspiration\Music\钢琴88键素材\钢琴音色MP3\");
            
            //for (int i = 0; i < fileNames.Length; i++)
            //{
                //disassembly d = new disassembly();
                //music = d.ReadFileIn(fileNames[i]);
                //d.disassemblyIt(music, fileNames[i]);
            const string FILENAME = @"D:\inspiration\Music\钢琴88键素材\钢琴音色MP3\01-A    -大字2组.mp3";
            disassembly d = new disassembly();
            music = d.ReadFileIn(FILENAME);
            d.disassemblyIt(music, FILENAME);
            d.reassemblyIt(music);
            //}


        }
    }
}
