using System;
using System.IO;
using System.Text;

namespace UNFScoreRecorder
{
    public class UNFDataEntry
    {
        public DateTime timeStamp;
        public string northScore, westScore, eastScore, southScore;
        public UNFDataEntry(DateTime timeStamp, string northScore, string southScore, string eastScore, string westScore)
        {
            this.timeStamp = timeStamp;
            this.northScore = northScore;
            this.southScore = southScore;
            this.eastScore = eastScore;
            this.westScore = westScore;
        }

        public string writeDataEntry(string file)
        {
            try
            {
                if (!File.Exists(file+".csv"))
                {
                    using (FileStream outFile = File.OpenWrite(file+".csv"))
                    {
                        Byte[] header = new UTF8Encoding(true).GetBytes("timeStamp, north_score, east_score, west_score, south_score\n" +
                            String.Format("{0},{1},{2},{3},{4}\n", timeStamp.ToString(), northScore, eastScore, westScore, southScore));
                        outFile.Write(header, 0, header.Length);
                    }
                }
                else
                {
                    using (StreamWriter outfile = File.AppendText(file + ".csv"))
                    {
                        outfile.WriteLine(String.Format("{0},{1},{2},{3},{4}\n", timeStamp.ToString(), northScore, eastScore, westScore, southScore));
                    }
                }
                return "Success";
            }
            catch
            {
                return "Fail";
            }
        }
    }
}
