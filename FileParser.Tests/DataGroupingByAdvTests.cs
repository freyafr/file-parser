using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileParser.Tests
{
    [TestClass]
    public class DataGroupingByAdvTests
    {
        [TestMethod]
        public void ProperStringFoundOk()
        {
            var testTable = GetDataTableTestData.GetTestDataTableWitGoodData();
            testTable.Columns.Add("advertiser");
            var grouper = new DataGroupingByAdv();
            DataRow row = testTable.NewRow();
            row["advertiser"] = "test";
            bool result = grouper.ProperStringFound(row,row);
            Assert.IsTrue(result,"two idential rows are different");
        }
        [TestMethod]
        public void ProperStringFoundNotFound()
        {
            var testTable = new DataTable();            
            var grouper = new DataGroupingByAdv();
            DataRow row = testTable.NewRow();            
            bool result = grouper.ProperStringFound(row,row);
            Assert.IsFalse(result,"Method has to return false if there is no column advertiser");
        }

        [TestMethod]
        public void GroupFieldsTestOk()
        {
            var testTable = new DataTable();
            testTable.Columns.Add("ad_spend",typeof(double));
            var grouper = new DataGroupingByAdv();
            DataRow row = testTable.NewRow();
            row["ad_spend"] = 4.55;
            grouper.GroupFields(row,row);
            Assert.IsTrue((double)row["ad_spend"]==9.1,"two idential rows are different");
        }

        [TestMethod]
        public void GroupFieldsTestOkColumnNotFound()
        {
            var testTable = new DataTable();            
            var grouper = new DataGroupingByAdv();
            DataRow row = testTable.NewRow();            
            grouper.GroupFields(row,row);            
        }
    }
}