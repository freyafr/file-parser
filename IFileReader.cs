using System.Collections.Generic;
using System.Data;
using System.IO;

namespace NsnApp
{
    public interface IFileReader{
            ICollection<DataRow> ReadDataBuffered(DataTable inputTable,StreamReader fileReader, int buffer = 0);
    }

    public interface IColumnBuilder{
        void BuildInputColumns(DataTable source);
        void BuildOutputColumns(DataTable source,string columnsToGroup);
    }

    public interface IFileWriter{
        //void WriteDataRows(ICollection<DataRow> outputRows,string outputFile);
        void PrepareOutputForSaving(ICollection<DataRow> outputRows);
        void WriteOutputFile(string outputFile);
    }
}