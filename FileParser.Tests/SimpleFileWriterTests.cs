using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileParser.Tests
{
    [TestClass]
    public class SimpleFileWriterTests
    {
        [TestMethod]
        public void PrepareOutputForSavingOk()
        {
            var inputTable = new DataTable();
            inputTable.Columns.Add("advertiser");
            inputTable.Columns.Add("ad_spend",typeof(double));

            DataRow row = inputTable.NewRow();
            row["advertiser"] = "test1";
            row["ad_spend"] = 4.6;
            var writer = new SimpleFileWriter(new DataGroupingByAdv(), null);
            writer.PrepareOutputForSaving(new List<DataRow>{row,row});
            Assert.IsTrue(writer.OutputRows.Count==1,"There has to be 1 gruped row in the end");            
            Assert.IsTrue( (double)writer.OutputRows.First()["ad_spend"]==9.2,"Wrong sum, expected 9.2, gor {0}");            
        }

        [TestMethod]
        public void PrepareOutputForSavingOkTwoStrings()
        {
            var inputTable = new DataTable();
            inputTable.Columns.Add("advertiser");
            inputTable.Columns.Add("ad_spend",typeof(double));

            DataRow row = inputTable.NewRow();
            row["advertiser"] = "test1";
            row["ad_spend"] = 4.6;

            DataRow row2 = inputTable.NewRow();
            row2["advertiser"] = "test2";
            row2["ad_spend"] = 1;
            var writer = new SimpleFileWriter(new DataGroupingByAdv(), null);
            writer.PrepareOutputForSaving(new List<DataRow>{row,row2});
            Assert.IsTrue(writer.OutputRows.Count==2,"Expected 2 grouped rows in the end");            
            Assert.IsTrue( (double)writer.OutputRows.First()["ad_spend"]==4.6,"Wrong sum, expected 4.6, gor {0}"); 
            Assert.IsTrue( (double)((List<DataRow>)writer.OutputRows)[1]["ad_spend"]==1,"Wrong sum, expected 1, gor {0}"); 
        } 

        [TestMethod]
        public void WriteOutputFileOk()
        {
           var inputTable = new DataTable();
            inputTable.Columns.Add("advertiser");
            inputTable.Columns.Add("ad_spend",typeof(double));

            DataRow row = inputTable.NewRow();
            row["advertiser"] = "test1";
            row["ad_spend"] = 4.6;

            DataRow row2 = inputTable.NewRow();
            row2["advertiser"] = "test2";
            row2["ad_spend"] = 1;
            var writer = new SimpleFileWriter(new DataGroupingByAdv(), "resultTest.csv");
            writer.PrepareOutputForSaving(new List<DataRow>{row,row2});
            writer.WriteOutputFile();
            if (!File.Exists("resultTest.csv"))
                Assert.Fail("Result file was not created");
            var resultlines = File.ReadLines("resultTest.csv").ToArray();
            Assert.IsTrue(resultlines.Length==3,"Wrong number of line in result file. Expected:3, result:{0}",resultlines.Length );
            Assert.IsTrue(resultlines[0]=="advertiser,ad_spend","header is wrong, expected: 'advertiser,ad_spend', got: {0}", resultlines[0]);
            Assert.IsTrue(resultlines[1]=="\"test1\",\"4.6\"","line 1 is wrong, expected: \"test1\",\"4.6\", got: {0}", resultlines[1]);
            Assert.IsTrue(resultlines[2]=="\"test2\",\"1\"","line 2 is wrong, expected: \"test2\",\"1\", got: {0}", resultlines[2]);
        }       
    }
}