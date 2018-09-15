using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileParser.Tests
{
    [TestClass]
    public class DataProcessorTests
    {
        [TestMethod]
        public void ProcessFileOk()
        {
            //prepare test data
            using(var stream = File.CreateText("inputtest.csv"))
            {
                stream.WriteLine("advertiser,ad_spend");
                stream.WriteLine("\"test1\",\"4.6\"");
                stream.WriteLine("\"test1\",\"1\"");
                
            }
            var processor = new DataProcessor("inputtest.csv",new AutoColumnBuilder("advertiser|ad_spend"),
                new SimpleFileWriter(new DataGroupingByAdv(),"testResult.csv"),
                new TextParser());

            processor.ProcessFile();

            //check result
            if (!File.Exists("testResult.csv"))
                Assert.Fail("Result file not found");
            var resultlines = File.ReadLines("testResult.csv").ToArray();
            Assert.IsTrue(resultlines.Length==2,"Wrong number of line in result file. Expected:2, result:{0}",resultlines.Length );
            Assert.IsTrue(resultlines[0]=="advertiser,ad_spend","header is wrong, expected: 'advertiser,ad_spend', got: {0}", resultlines[0]);
            Assert.IsTrue(resultlines[1]=="\"test1\",\"5.6\"","line 1 is wrong, expected: \"test1\",\"5.6\", got: {0}", resultlines[1]);

            //delete temp data
            File.Delete("inputtest.csv");
            File.Delete("testResult.csv");
        }

        [TestMethod]
        public void ProcessFileOk2Lines()
        {
            //prepare test data
            using(var stream = File.CreateText("inputtest.csv"))
            {
                stream.WriteLine("advertiser,ad_spend");
                stream.WriteLine("\"test1\",\"4.6\"");
                stream.WriteLine("\"test2\",\"1,1\"");
                
            }
            var processor = new DataProcessor("inputtest.csv",new AutoColumnBuilder("advertiser|ad_spend"),
                new SimpleFileWriter(new DataGroupingByAdv(),"testResult.csv"),
                new TextParser());

            processor.ProcessFile();

            //check result
            if (!File.Exists("testResult.csv"))
                Assert.Fail("Result file not found");
            var resultlines = File.ReadLines("testResult.csv").ToArray();
            Assert.IsTrue(resultlines.Length==3,"Wrong number of line in result file. Expected:3, result:{0}",resultlines.Length );
            Assert.IsTrue(resultlines[0]=="advertiser,ad_spend","header is wrong, expected: 'advertiser,ad_spend', got: {0}", resultlines[0]);
            Assert.IsTrue(resultlines[1]=="\"test1\",\"4.6\"","line 1 is wrong, expected: \"test1\",\"5.6\", got: {0}", resultlines[1]);
            Assert.IsTrue(resultlines[2]=="\"test2\",\"1.1\"","line 2 is wrong, expected: \"test2\",\"1.1\", got: {0}", resultlines[2]);


            //delete temp data
            File.Delete("inputtest.csv");
            File.Delete("testResult.csv");
        }

                [TestMethod]
        public void ProcessFileEmptyFile()
        {
            //prepare test data
            using(var stream = File.CreateText("inputtest.csv"))
            {
                stream.WriteLine("advertiser,ad_spend");
            }
            var processor = new DataProcessor("inputtest.csv",new AutoColumnBuilder("advertiser|ad_spend"),
                new SimpleFileWriter(new DataGroupingByAdv(),"testResult.csv"),
                new TextParser());

            processor.ProcessFile();

            //check result
            if (File.Exists("testResult.csv"))
                Assert.Fail("Result exists");
            

            //delete temp data
            File.Delete("inputtest.csv");
            
        }

        public void ProcessFileNullFile()
        {
            //prepare test data
            using(var stream = File.CreateText("inputtest.csv"))
            {
                
            }
            var processor = new DataProcessor("inputtest.csv",new AutoColumnBuilder("advertiser|ad_spend"),
                new SimpleFileWriter(new DataGroupingByAdv(),"testResult.csv"),
                new TextParser());

            processor.ProcessFile();

            //check result
            if (File.Exists("testResult.csv"))
                Assert.Fail("Result exists");
            

            //delete temp data
            File.Delete("inputtest.csv");
            
        }

    }
}