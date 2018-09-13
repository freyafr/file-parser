using System.Collections.Generic;
using System.Data;
using System.IO;

namespace NsnApp
{  

    public interface ITextParser
    {
        string[] ParseFileLine(string fileLine);
        DataRow FillDataFromString(DataRow row,string[] values);
        
    }

    public interface IDataGrouping
    {
        bool ProperStringFound(DataRow source, DataRow dest);
        void GroupFields(DataRow source, DataRow dest);
    }

    public interface IColumnBuilder
    {
        void BuildInputColumns(DataTable source, string headerLine, string[] testValue);
        void BuildOutputColumns(DataTable source,string columnsToGroup);
    }

    public interface IFileWriter
    {
        void PrepareOutputForSaving(ICollection<DataRow> outputRows);
        void WriteOutputFile(string outputFile);
    }
}