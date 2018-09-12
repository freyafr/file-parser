using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NsnApp
{
    public class SingleStringReader : IFileReader
    {
        public string[] ParseString(string fileLine)
        {
            string[] dataRowStringArray =  fileLine.Split(",");//Regex.Matches(fileReader.ReadLine(),"(\"(.*?)\"(,|$))").Select(s=>s.Groups[2].Value).ToArray();
            for(int i=0;i<dataRowStringArray.Length;i++)
                dataRowStringArray[i]=dataRowStringArray[i].Replace("\"","");
            return dataRowStringArray;
        }

        public ICollection<DataRow> ReadDataBuffered(DataTable inputTable,StreamReader fileReader, int buffer = 0)
        {
            var resultRows = new List<DataRow>();
            string[] dataRowStringArray = ParseString(fileReader.ReadLine());
            DataRow row = inputTable.NewRow();
            resultRows.Add(row);
            for (int i = 0; i < dataRowStringArray.Length; i++)
            {
                if(row.Table.Columns[i].DataType==typeof(double))
                {
                    row[i]=dataRowStringArray[i].Replace(".",",");
                }
                else
                    row[i]=dataRowStringArray[i];
            }
            return resultRows;
        }
    }
}