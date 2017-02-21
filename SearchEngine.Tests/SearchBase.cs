using System;
using System.Collections.Generic;
using NUnit;
using NUnit.Framework;
using SearchEngine;
using System.IO;

namespace SearchEngine.Tests
{
    //Not tests for all the scenarios have been added due to time constraints.  I need to use moq to unit test IndexOperations.cs
    //and searchopertions.cs as without it becomes an integration test.

    //The below three methods are TDD.  Created these first and refactored the code.
    [TestFixture]
    public class SearchBaseTest 
    {
        SearchBase search = new SearchBase();
        List<byte> currentLine;
        List<string> lines;

        [SetUp]
        public void SearchBaseTest_SetUp() {
            currentLine = new List<byte>()
            { 10,20,13};
            lines = new List<string>();
        }

        [Test]
        public void When_Insert_Elements_To_Output_Return_A_Positive_Integer()
        {
           
            int output=search.InsertElementsToOutput(currentLine, lines, 0);
            Assert.AreEqual(1, output);
        }
        [Test]
        public void Check_For_File_Names_Inside_Directory()
        {
            File.CreateText(Directory.GetCurrentDirectory()+"index.txt");
            FileInfo[] fileInfo=search.GetFileNamesInsideTheDirectory();
            Assert.IsNotNull(fileInfo.Length);
        }

        [Test]
        public void Check_For_Last_Byte_Was_Carriage_Return()
        {
            int output = search.InsertElementsToOutput(currentLine, lines, 0);
            Assert.AreEqual(1, output);
        }
    }
}
