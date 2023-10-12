using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Quicker.Models
{
    public class CsvFile
    {
        public string CsvPath;

        public CsvFile()
        {
            this.CsvPath = "";
        }
        public CsvFile(string path)
        {
            this.CsvPath = path;
        }
        public void SetCsvPath(string path)
        {
            this.CsvPath = path;
        }

        public ObservableCollection<Match> ReadCsv()
        {
            var MatchList = new ObservableCollection<Match>();
            using (var reader = new StreamReader(CsvPath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var keyword = values[0].Trim();
                    var snippet = values[1].Trim();
                    Match match = new Match(keyword, snippet);
                    MatchList.Add(match);
                }
            }
            return MatchList;
        }
    }
}
