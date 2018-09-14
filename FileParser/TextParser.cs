using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FileParser
{
    public class TextParser : ITextParser
    {
        public DataRow FillDataFromString(DataRow row,string[] values)
        {
            for (int i = 0; i < Math.Min(values.Length, row.Table.Columns.Count); i++)
                {
                    if(row.Table.Columns[i].DataType==typeof(double))
                    {
                        if (!string.IsNullOrEmpty(values[i]))
                        {
                            double result;
                            if(double.TryParse(values[i],NumberStyles.Any,CultureInfo.InvariantCulture,out result))
                                row[i]=result;  
                        }                      
                    }
                    else
                        row[i]=values[i];
                }
            return row;
        }

        public string[] ParseFileLine(string fileLine)
        {
            if (fileLine == null)
                return new string[0];
            string[] dataRowStringArray =  Regex.Split(fileLine,",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            for(int i=0;i<dataRowStringArray.Length;i++)
                dataRowStringArray[i]=dataRowStringArray[i].Trim('\"');
            return dataRowStringArray;
        }
    }
}