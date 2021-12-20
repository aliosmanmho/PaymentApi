using Payment.Bussinies.StaticData.FileRead.Mapping;
using Payment.Bussinies.StaticData.FileRead.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace Payment.Bussinies.StaticData.FileRead
{
    public class FileReadParser
    {
        public static readonly string App = Directory.GetCurrentDirectory();

        public static readonly string Templates = Path.Combine(App, "Files");
        public static Task<List<BinNumberFr>> ReadBinNumbers(string filepath)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            BinNumberMapping csvMapper = new BinNumberMapping();
            CsvParser<BinNumberFr> csvParser = new CsvParser<BinNumberFr>(csvParserOptions, csvMapper);
            string file = $"{filepath}/BinNumbers.csv";
            if (!System.IO.File.Exists(file))
                return Task.FromResult(new List<BinNumberFr>());
            var result = csvParser
                .ReadFromFile(file, Encoding.ASCII)
                .ToList();
            return  Task.FromResult(result.Select(x => x.Result).ToList());
        }
    }
}
