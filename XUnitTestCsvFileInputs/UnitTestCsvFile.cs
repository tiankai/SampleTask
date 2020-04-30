using System;
using Xunit;
//
using CsvContentHelper;
using CsvContentHelper.Models;

namespace XUnitTestCsvFileInputs
{
    public class UnitTestCsvFile
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Test_CheckCsvFileLength()
        {
            var csvHelper = new CsvHelper();
            // positive test empty
            const string fileContentEmpty = "";
            var actualForEmpty = csvHelper.CheckCsvFileLength(fileContentEmpty);
            Assert.True(actualForEmpty);
            // positive test 1
            const string fileContentTest_Positive_1 = "Name, Class, Dorm Address, Room, GPA\r\nSally Whittaker,2018,McCarren House,312,3.75\r\nBelinda Jameson,2017,Cushing House,148,3.52\r\nJeff Smith,2018,Prescott House,17-D,3.20";
            var actualForPositiveTest_1 = csvHelper.CheckCsvFileLength(fileContentTest_Positive_1);
            Assert.True(actualForPositiveTest_1);
            // positive test 2
            const string fileContentTest_Positive_2 = "Name, Class, Dorm Address, Room, GPA\r\nSally Whittaker,2018,McCarren House,312,3.75\r\nBelinda Jameson,2017,Cushing House,148,3.52\r\nJeff Smith,2018,Prescott House,17-D,3.20";
            var actualForPositiveTest_2 = csvHelper.CheckCsvFileLength(fileContentTest_Positive_2);
            Assert.True(actualForPositiveTest_2);
            // negative test case
            const int maxCharsInput = 60;
            const string fileContentTest_Negative_1 = "Name, Class, Dorm Address, Room, GPA\r\nSally Whittaker,2018,McCarren House,312,3.75\r\nBelinda Jameson,2017,Cushing House,148,3.52\r\nJeff Smith,2018,Prescott House,17-D,3.20";
            var actualForNegativeTest_1 = csvHelper.CheckCsvFileLength(fileContentTest_Negative_1, maxCharsInput);
            Assert.False(actualForNegativeTest_1);
        }
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Test_CheckCsvFileContent()
        {
            var csvHelper = new CsvHelper();
            // negative test empty
            const string fileContentEmpty = "";
            var actualForEmpty = csvHelper.CheckCsvFileContent(fileContentEmpty);
            Assert.False(actualForEmpty);
            // positive test 1
            const string fileContentTest_Positive_1 = "Name, Class, Dorm Address, Room, GPA\r\nSally Whittaker,2018,McCarren House,312,3.75\r\nBelinda Jameson,2017,Cushing House,148,3.52\r\nJeff Smith,2018,Prescott House,17-D,3.20";
            var actualForPositiveTest_1 = csvHelper.CheckCsvFileContent(fileContentTest_Positive_1);
            Assert.True(actualForPositiveTest_1);
            // positive test 2
            const string fileContentTest_Positive_2 = "Name, Class, Dorm Address, Room, GPA\nSally Whittaker,2018,McCarren House,312,3.75\nBelinda Jameson,2017,Cushing House,148,3.52\nJeff Smith,2018,Prescott House,17-D,3.20";
            var actualForPositiveTest_2 = csvHelper.CheckCsvFileContent(fileContentTest_Positive_2);
            Assert.True(actualForPositiveTest_2);
            // negative test case 1
            const string fileContentTest_Negative_1 = "Name, Class, Dorm Address, Room, GPA, Sally Whittaker,2018,McCarren House,312,3.75, Belinda Jameson,2017,Cushing House,148,3.52, Jeff Smith,2018,Prescott House,17-D,3.20";
            var actualForNegativeTest_1 = csvHelper.CheckCsvFileContent(fileContentTest_Negative_1);
            Assert.False(actualForNegativeTest_1);
        }
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Test_CheckCsvRowData()
        {
            var csvHelper = new CsvHelper();
            // positive test 1
            const string fileContentTest_Positive_1 = "Name, Class, Dorm Address, Room, GPA\r\nSally Whittaker,2018,McCarren House,312,3.75\r\nBelinda Jameson,2017,Cushing House,148,3.52\r\nJeff Smith,2018,Prescott House,17-D,3.20";
            var actualForPositiveTest_1 = csvHelper.CheckCsvRowData(fileContentTest_Positive_1);
            Assert.True(actualForPositiveTest_1);
            // positive test 2
            const string fileContentTest_Positive_2 = "Name, Class, Dorm Address, Room, GPA\nSally Whittaker,2018,McCarren House,312,3.75\nBelinda Jameson,2017,Cushing House,148,3.52\nJeff Smith,2018,Prescott House,17-D,3.20";
            var actualForPositiveTest_2 = csvHelper.CheckCsvRowData(fileContentTest_Positive_2);
            Assert.True(actualForPositiveTest_2);
            // negative test case 1
            const string fileContentTest_Negative_1 = "Name\t Dorm Address Whittaker\t2018";
            var actualForNegativeTest_1 = csvHelper.CheckCsvRowData(fileContentTest_Negative_1);
            Assert.False(actualForNegativeTest_1);
        }
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Test_GetCsvAllRows()
        {
            var csvHelper = new CsvHelper();
            // positive test 1
            const string fileContentTest_Positive_1 = "Name, Class, Dorm Address, Room, GPA\r\nSally Whittaker,2018,McCarren House,312,3.75\r\nBelinda Jameson,2017,Cushing House,148,3.52\r\nJeff Smith,2018,Prescott House,17-D,3.20";
            const int expectedRowCount_Positive_1 = 7;
            var actualForPositiveTest_1 = csvHelper.GetCsvAllRows(fileContentTest_Positive_1);
            Assert.Equal(expectedRowCount_Positive_1, actualForPositiveTest_1.Length);
            // positive test 2
            const string fileContentTest_Positive_2 = "Name, Class, Dorm Address, Room, GPA\nSally Whittaker,2018,McCarren House,312,3.75\nBelinda Jameson,2017,Cushing House,148,3.52\nJeff Smith,2018,Prescott House,17-D,3.20";
            const int expectedRowCount_Positive_2 = 4;
            var actualForPositiveTest_2 = csvHelper.GetCsvAllRows(fileContentTest_Positive_2);
            Assert.Equal(expectedRowCount_Positive_2, actualForPositiveTest_2.Length);
            // negative test case 1
            const string fileContentTest_Negative_1 = "Name, Class, Dorm Address, Room, GPA\nSally Whittaker,2018,McCarren House,312,3.75";
            const int expectedRowCount_Negative_1 = 1;
            var actualForNegativeTest_1 = csvHelper.GetCsvAllRows(fileContentTest_Negative_1);
            Assert.NotEqual(expectedRowCount_Negative_1, actualForNegativeTest_1.Length);
        }
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Test_GetCsvDataRow()
        {
            var csvHelper = new CsvHelper();
            // positive test 1
            const string dataRowTest_Positive_1 = "Name, Class, Dorm Address, Room, GPA";
            const int expectedColumnCount_Positive_1 = 5;
            var actualForPositiveTest_1 = csvHelper.GetCsvDataRow(dataRowTest_Positive_1);
            Assert.Equal(expectedColumnCount_Positive_1, actualForPositiveTest_1.Length);
            // positive test 2
            const string dataRowTest_Positive_2 = "Sally Whittaker;2018-N;McCarren House;312;0.00075";
            const int expectedColumnCount_Positive_2 = 5;
            var actualForPositiveTest_2 = csvHelper.GetCsvDataRow(dataRowTest_Positive_2);
            Assert.Equal(expectedColumnCount_Positive_2, actualForPositiveTest_2.Length);
            // negative test case 1
            const string dataRowTest_Negative_1 = "Name, Class, Dorm Address, Room, GPA";
            const int expectedColumnCount_Negative_1 = 1;
            var actualForNegativeTest_1 = csvHelper.GetCsvDataRow(dataRowTest_Negative_1);
            Assert.NotEqual(expectedColumnCount_Negative_1, actualForNegativeTest_1.Length);
        }
    
    }
}
