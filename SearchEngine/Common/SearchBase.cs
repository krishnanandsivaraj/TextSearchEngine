using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace SearchEngine
{
    public class SearchBase
    {
        List<long> badPositions = new List<long>();
        List<byte> currentLine = new List<byte>();
        List<string> lines = new List<string>();
        bool lastReadByteWasLF = false;
        int linesToRead = 20, linesRead = 0;
        long lastBytePos;

        Dictionary<string, string> input = new Dictionary<string, string>();
        Dictionary<string,List<string>> output = new Dictionary<string, List<string>>();
        Dictionary<string, string> outs = new Dictionary<string, string>();

        public Dictionary<string,List<string>> BaseSearch(string[] searchKeyWords)
        {
            FileInfo[] Files = GetFileNamesInsideTheDirectory();
            
                string[] files = new string[Files.Length];
            for (int t = 0; t < Files.Length; t++)
                {
                    files[t] = Files[t].Name;
                    long fileLen = new FileInfo(files[t]).Length;
                    lastBytePos = fileLen;
                    using (MemoryMappedFile mapFile = MemoryMappedFile.CreateFromFile(files[t], FileMode.OpenOrCreate))
                    {
                        var view = mapFile.CreateViewAccessor();
                        for (long i = fileLen - 1; i >= 0; i--)
                        {
                            try
                            {
                                byte b = view.ReadByte(i);
                                lastBytePos = i;
                                switch (b)
                                {
                                    default:
                                        lastReadByteWasLF = false;
                                        currentLine.Insert(0, b);
                                        break;
                                    case 13://check for carriage return
                                        if (lastReadByteWasLF)
                                        {
                                            linesRead = AddLastByteWasCReturn(currentLine, lines, linesRead);
                                        }
                                        lastReadByteWasLF = false;
                                        break;
                                    case 10://check for new line
                                        lastReadByteWasLF = true;
                                        currentLine.Insert(0, b);
                                        break;
                                }
                                if (linesToRead == linesRead)
                                {
                                    break;
                                }
                            }
                            catch
                            {
                                lastReadByteWasLF = false;
                                currentLine.Insert(0, (byte)'?');
                                badPositions.Insert(0, i);
                            }
                        }

                    }

                    if (linesToRead > linesRead)
                    {
                        linesRead = InsertElementsToOutput(currentLine, lines, linesRead);
                    }
                    foreach (var s in lines)
                    {
                    if (files[t] != null) { input[files[t]]= Convert.ToString(s); }
                        
                    }
                    lines.Clear();
                    currentLine.Clear();
                    output.Clear();
            }
            
                foreach (var s in searchKeyWords)
                {
                output.Add(s, null);
                List<string> temp = new List<string>();
                for (int u = 0; u < Files.Length; u++)
                {
                    if (input[files[u]].Contains(s)) {
                        temp.Add(files[u]);
                    }
                }
                output[s] = temp;
                }

                return output;

        }

        public int InsertElementsToOutput(List<byte> currentLine, List<string> lines, int linesRead)
        {
            var bArray = currentLine.ToArray();
            if (bArray.LongLength > 1)
            {
                lines.Insert(0, Encoding.UTF8.GetString(bArray));
                linesRead++;
            }

            return linesRead;
        }

        public FileInfo[] GetFileNamesInsideTheDirectory()
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory());
                FileInfo[] Files = d.GetFiles("*.txt");
                return Files;
            }
            catch (Exception ex)
            {

                throw new FileNotFoundException(ex.Message);
            }
        }

        public int AddLastByteWasCReturn(List<byte> currentLine, List<string> lines, int linesRead)
        {
            var bArray = currentLine.ToArray();
            if (bArray.LongLength > 1)
            {
                lines.Insert(0, Encoding.UTF8.GetString(bArray, 1, bArray.Length - 1));

                currentLine.Clear();

                currentLine.Add(13);
                currentLine.Add(10);

                linesRead++;
            }

            return linesRead;
        }

    }
    }
