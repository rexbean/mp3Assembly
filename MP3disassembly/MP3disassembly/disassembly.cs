using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MP3disassembly
{
    class disassembly
    {
        List<int> start = new List<int>();
        List<byte[]> disMusic = new List<byte[]>();
        public byte[] ReadFileIn(string filePath)
        {
            
            FileInfo fi = new FileInfo(filePath);
            long len = fi.Length;
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] buffer = new byte[len];
            fs.Read(buffer, 0, (int)len);
            for (int i = 0; i < len; i++)
            {
                if (buffer[i] == buffer[0] && buffer[i + 1] == buffer[1])
                {
                    start.Add(i);
                }
            }
            fs.Close();
            return buffer;
        }

        public void disassemblyIt(byte[] music,string name)
        {
            
            int len = music.Length;
            float frameLength=(len - 128)/8;
            int mod=(len-128)%8;
            int frameCount = 0;
            int startFrame=0;
            int nameIndex = 0;
            string[] dirName;
            dirName = name.Split('.');
            DirectoryInfo dir = new DirectoryInfo(dirName[0]);
            dir.Create();//自行判断一下是否存在。
            #region music
            for (int i = 0; i < start.Count; i++)
            {
                int offset = 0;
                if (i == 0)
                {
                    offset = start[0];
                }
                else if (i == start.Count - 1)
                {
                    offset=music.Length-128 - start[i];
                }
                else
                {
                    offset=start[i + 1] - start[i];
                }
                if ((frameCount+offset)< frameLength)
                {
                    frameCount += offset;
                }
                else
                {
                    byte[] buffer = new byte[frameCount + 128];
                    for (int j = 0; j < frameCount; j++)
                    {
                        buffer[j] = music[j + startFrame];
                    }
                    disMusic.Add(buffer);
                    startFrame=start[i-1];
                    for (int k = 0; k < 128; k++)
                    {
                        buffer[frameCount + k] = music[len - 128 + k];

                    }
                    FileStream fs = new FileStream(dirName[0] + "\\"+nameIndex + ".mp3", FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Close();
                    frameCount = 0;
                    nameIndex++;
                }
            }
            #endregion

            byte[] buffer1 = new byte[frameCount + 128];
            for (int j = 0; j < frameCount; j++)
            {
                buffer1[j] = music[j + startFrame];
            }
            disMusic.Add(buffer1);
            for (int k = 0; k < 128; k++)
            {
                buffer1[frameCount + k] = music[len - 128 + k];

            }
            FileStream fs1 = new FileStream(dirName[0] +"\\"+nameIndex + ".mp3", FileMode.OpenOrCreate, FileAccess.Write);
            fs1.Write(buffer1, 0, buffer1.Length);
            fs1.Close();
            frameCount = 0;
            
        }

        public void reassemblyIt(byte[] music)
        {

            
            int count=disMusic[0].Length*6+disMusic[1].Length+disMusic[2].Length+disMusic[3].Length+disMusic[4].Length
                +disMusic[5].Length+disMusic[6].Length+disMusic[7].Length;
            byte[] newMusic = new byte[count+128];
            for (int i = 0; i < disMusic[0].Length; i++)
            {
                newMusic[i] = disMusic[1][i];
            }
            for (int i = 0; i < disMusic[0].Length; i++)
            {
                newMusic[disMusic[1].Length+i] = disMusic[1][i];
            }
            for (int i = 0; i < disMusic[1].Length; i++)
            {
                newMusic[disMusic[1].Length*2 + i] = disMusic[1][i];
            }
            for (int i = 0; i < disMusic[0].Length; i++)
            {
                newMusic[disMusic[1].Length * 3 + i] = disMusic[1][i];
            }
            for (int i = 0; i < disMusic[1].Length; i++)
            {
                newMusic[disMusic[1].Length * 4 + i] = disMusic[1][i];
            }
            for (int i = 0; i < disMusic[1].Length; i++)
            {
                newMusic[disMusic[1].Length * 5 + i] = disMusic[1][i];
            }
            for (int i = 0; i < disMusic[2].Length; i++)
            {
                newMusic[disMusic[1].Length * 6 + i] = disMusic[2][i];
            }
            for (int i = 0; i < disMusic[3].Length; i++)
            {
                newMusic[disMusic[1].Length * 6 + disMusic[2].Length + i] = disMusic[3][i];
            }
            for (int i = 0; i < disMusic[4].Length; i++)
            {
                newMusic[disMusic[1].Length * 6 + disMusic[2].Length + disMusic[3].Length + i] = disMusic[4][i];
            }
            for (int i = 0; i < disMusic[5].Length; i++)
            {
                newMusic[disMusic[1].Length * 6 + disMusic[2].Length + disMusic[3].Length + disMusic[4].Length + i] = disMusic[5][i];
            }
            for (int i = 0; i < disMusic[6].Length; i++)
            {
                newMusic[disMusic[1].Length * 6 + disMusic[2].Length + disMusic[3].Length + disMusic[4].Length + disMusic[5].Length + i] = disMusic[6][i];
            }
            for (int i = 0; i < disMusic[7].Length; i++)
            {
                newMusic[disMusic[1].Length * 6 + disMusic[2].Length + disMusic[3].Length + disMusic[4].Length + disMusic[5].Length + disMusic[6].Length + i] = disMusic[7][i];
            }
           
            for (int i = 0; i < 128; i++)
            {
                newMusic[disMusic[1].Length * 6 + disMusic[2].Length + disMusic[3].Length + disMusic[4].Length + disMusic[5].Length + disMusic[6].Length + disMusic[7].Length  + i] = music[music.Length - 128 + i];
            }
            FileStream fs1 = new FileStream("d:\\1111.mp3", FileMode.OpenOrCreate, FileAccess.Write);
            fs1.Write(newMusic, 0, newMusic.Length);
            fs1.Close();    

 
        }
    }
}
