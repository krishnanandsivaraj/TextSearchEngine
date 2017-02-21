                                                <h4>TextSearchEngine</h4>
                                                 <h5>Version Beta.01</h5>
<b>Please Note: The idea of this use case is very large and there are a lot of scenarios to cover.  Say for example the system that reads 1kb file is different from 2gb text file.  In real scenarios the use of Tree/Binary Tree/Binary Search Tree is encouraged with Bredth First Search and Depth First Search. While implementing those will take more time, I am limiting myself with the available classes in .NET.  However, Instead of FileInfo, while reading the text file, I have used MemoryMappedInfo, a new class for reading large files in .NET 4.0.
<br/>Please Note:  I could not cover all the unit testing scenarios and unit test coverage for all the files are absent due to time constraint.  I have followed TDD for writing SearchBaseTest.  The code is completely modular and the unit tests can be written on it at any time.  I will add more unit tests after submission.</b>
<b>Use Cases</b>
![alt tag](https://github.com/krishnanandsivaraj/TextSearchEngine/blob/master/usecase.jpg)
![alt tag](https://github.com/krishnanandsivaraj/TextSearchEngine/blob/master/flowdiagram1.jpg)
![alt tag](https://github.com/krishnanandsivaraj/TextSearchEngine/blob/master/flowdiagram3.jpg)
<h6>Output Screen (Running instructions)</h6>
<b>Step  1: Please compile the code and navigate to the bin/debug.  You can see the binaries</b>
![alt tag](https://github.com/krishnanandsivaraj/TextSearchEngine/blob/master/withoutindex.PNG)

<b>Step 2: Add text  files. Here it is textfile1,textfile2 and textfile3</b>

<b>Step3: type SearchEngine.exe index <words you want to search></b>
![alt tag](https://github.com/krishnanandsivaraj/TextSearchEngine/blob/master/indexcreated.PNG)

You can see the index.txt created along with the result

<b>Step4: type SearchEngine.exe search <words you want to search></b>
![alt tag](https://github.com/krishnanandsivaraj/TextSearchEngine/blob/master/searchresults.PNG)

