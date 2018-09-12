using System;
using System.Data;
using System.Globalization;

namespace NsnApp{
    public class TextParser : ITextParser
    {
        public DataRow FillDataFromString(DataRow row,string[] values)
        {
            for (int i = 0; i < Math.Min(values.Length, row.Table.Columns.Count); i++)
                {
                    if(row.Table.Columns[i].DataType==typeof(double))
                    {
                        row[i]=double.Parse(values[i],CultureInfo.InvariantCulture);
                    }
                    else
                        row[i]=values[i];
                }
            return row;
        }

        public string[] ParseFileLine(string fileLine)
        {
            if (fileLine==null)
                return new string[0];
            string[] dataRowStringArray =  fileLine.Split(",");//Regex.Matches(fileReader.ReadLine(),"(\"(.*?)\"(,|$))").Select(s=>s.Groups[2].Value).ToArray();
            for(int i=0;i<dataRowStringArray.Length;i++)
                dataRowStringArray[i]=dataRowStringArray[i].Replace("\"","");
            return dataRowStringArray;
        }
    }
}