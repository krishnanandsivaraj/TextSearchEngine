using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace SearchEngine
{
    public class SearchBase
    {
        public Dictionary<string,string> BaseSearch(string[] searchKeyWords)
        {

            List<long> badPositions = new List<long>();
            List<byte> currentLine = new List<byte>();
            List<string> lines = new List<string>();
            bool lastReadByteWasLF = false;
            int linesToRead = 20, linesRead = 0;
            long lastBytePos;

            Dictionary<string, string> input = new Dictionary<string, string>();
            List<string> output = new List<string>();

            string stringToCheck = null;

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
                                    case 13:
                                        if (lastReadByteWasLF)
                                        {
                                            linesRead = SayLastByteWasCReturn(currentLine, lines, linesRead);
                                        }
                                        lastReadByteWasLF = false;
                                        break;
                                    case 10:
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
                        input.Add(files[t], Convert.ToString(s));
                    }
                    lines.Clear();
                    currentLine.Clear();
                    foreach (string search in searchKeyWords)
                    {
                        stringToCheck = search;
                        if (input[files[t]].Contains(stringToCheck) && !output.Contains(files[t]))
                        {
                            output.Add(files[t]);
                        }
                    }
                }
            string showResult = stringToCheck + " is contained in :  ";
            foreach (var outputs in output)
            {
                showResult += outputs;
            }
            Dictionary<string, string> outs = new Dictionary<string, string>();
            outs.Add(stringToCheck,showResult);
            return outs;

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

                throw new NotImplementedException(ex.Message);
            }
        }

        public int SayLastByteWasCReturn(List<byte> currentLine, List<string> lines, int linesRead)
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
