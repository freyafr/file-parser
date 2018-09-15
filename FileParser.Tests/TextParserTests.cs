using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileParser.Tests
{
    [TestClass]
    public class TextParserTests
    {
        private readonly TextParser _parser = new TextParser();

        [TestMethod]
        public void ParseFileLineNull()
        {            
            string[] result = _parser.ParseFileLine(null);
            Assert.IsFalse(result==null||result.Length>0, "if null on input should return zero array");
        }
        [TestMethod]
        public void ParseFileLineOk()
        {            
            string[] result = _parser.ParseFileLine("Test1\",01/01/0001,\"87,6\"");
            Assert.IsTrue(result!=null&&result.Length==3,string.Format("Wrong number of params returned.Must be 3 - returned {0}",
            result==null?0:result.Length));
        }

        [TestMethod]
        public void FillDataFromStringOk()
        {
            var testTable = GetDataTableTestData.GetTestDataTableWitGoodData();
            var values = new string[3]{"v1","1,2","v\"2\""};
            DataRow row = testTable.NewRow();
            _parser.FillDataFromString(row,values);
            Assert.IsTrue(row[0].ToString()=="v1","0 column is set wrong");
            Assert.IsTrue((double)row[1]==1.2,"1 column is set wrong: has to be 1.2 got {0}",row[1]);
            Assert.IsTrue(row[2].ToString()=="v\"2\"","2 column is set wrong");
        }

        [TestMethod]
        public void FillDataFromStringOkWithDot()
        {
            var testTable = GetDataTableTestData.GetTestDataTableWitGoodData();
            var values = new string[3]{"v1","1.2","v2\""};
            DataRow row = testTable.NewRow();
            _parser.FillDataFromString(row,values);
            Assert.IsTrue(row[0].ToString()=="v1","0 column is set wrong");
            Assert.IsTrue((double)row[1]==1.2,"1 column is set wrong: has to be 1.2 got {0}",row[1]);
            Assert.IsTrue(row[2].ToString()=="v2\"","2 column is set wrong");
        }
    }
}
