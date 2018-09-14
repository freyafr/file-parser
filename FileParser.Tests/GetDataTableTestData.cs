using System.Data;

namespace FileParser.Tests
{
    public static class GetDataTableTestData
    {
        public static DataTable GetTestDataTableWitGoodData()
        {
            var testTable = new DataTable();
            testTable.Columns.Add("c1",typeof(string));
            testTable.Columns.Add("c2",typeof(double));
            testTable.Columns.Add("c3",typeof(string));
            return testTable;
        }
    }
}