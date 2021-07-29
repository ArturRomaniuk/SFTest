using System;
using System.IO;
using System.Collections.Generic;

namespace SalesForceTest
{
    interface ISorter
    {
        void SorterMethod(List<string> dataToSort);
    }
    interface IDataProcessor
    {
        void ProcessData(IDataProvider dataProvider, string path);
    }
    interface IDataProvider
    {
        void ReadDataMethod(string path);

    }
    interface IExecuter
    {
        void Execute(List<string> dataToExecute);
    }
    class Choice
    {
        protected void ExeptionCatcher()
        {
            Console.WriteLine("Incorect data pasted,if you like to repeat type 'Yes'");
            string chooser = Console.ReadLine();
            if (chooser.ToLower() != "yes") { return; }
            ChooseMethod();
        }
        public string ChooseMethod()
        {
            // Create list to contain existing file paths and work with them
            List<string> indexForExistingFiles = new List<string>() { };
            indexForExistingFiles.Add("Z:\\SF\\source.txt");
            indexForExistingFiles.Add("Z:\\SF\\source1.txt");
            Console.WriteLine("If you want to see existing exampls of file type 'file',if you want to add new one type 'new' ");
            string deciderForFileType = Console.ReadLine();
            int chooserForExistingFiles;
            string chooserForNewFiles;
            if (deciderForFileType == "file")
            {
                //showing the information about file that exist now
                int i = 1;
                foreach (var item in indexForExistingFiles)
                {
                    Console.Write("file " + i);
                    Console.WriteLine("\t" + item);
                    i++;
                }
                Console.WriteLine("type number of file which you want to chose");
                try
                {
                    chooserForExistingFiles = Int32.Parse(Console.ReadLine());
                    return indexForExistingFiles[chooserForExistingFiles - 1];
                }
                catch (Exception)
                {
                    ExeptionCatcher();
                }
            }
            else if (string.Equals(deciderForFileType, "new", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Please type the way to the new file similar to this exampl 'C:\\SF\\source.txt'");
                chooserForNewFiles = Console.ReadLine();
                return chooserForNewFiles;
            }
            else
            {
                ExeptionCatcher();
            }
            return deciderForFileType;
        }
    }
    class ReadData : IDataProcessor
    {
        public void ProcessData(IDataProvider dataProvider, string path)
        {
            dataProvider.ReadDataMethod(path);
        }
    }
    class Reader : IDataProvider
    {
        protected List<string> txtData = new List<string>();
        ISorter sort = new Sorter();
        Choice choice = new Choice();
        public void ReadDataMethod(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        txtData.Add(line);
                    }
                    sort.SorterMethod(txtData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read or the way to file is incorect");
                Console.WriteLine(e.Message);
                Console.WriteLine("Sry incorect command please try again,Type yes if you want to try again.");
                string chooseForRepeat = Console.ReadLine();
                if (chooseForRepeat != "Yes") { return; }
                choice.ChooseMethod();
            }
        }
    }
    class Sorter : ISorter
    {
        protected void ExeptionCatcher(List<string> txtData)
        {
            Console.WriteLine("Incorect data pasted,if you like to repeat type 'Yes'");
            string chooser = Console.ReadLine();
            if (chooser.ToLower() != "yes") { return; }
            SorterMethod(txtData);
        }
        public void SorterMethod(List<string> txtData)
        {
            Console.WriteLine("How much repeats do you need for Fibonacci sequence?Enter the number");
            int lengthForFibonacci = 0;
            List<string> sortedDataFibonacci = new List<string>();
            try
            {
                lengthForFibonacci = Int32.Parse(Console.ReadLine());
                int firstValue = 0, secondValue = 1, temp = 0;
                for (int i = 0; i < lengthForFibonacci; i++)
                {
                    sortedDataFibonacci.Add(txtData[firstValue + secondValue - 1]);
                    temp = firstValue;
                    firstValue = secondValue;
                    secondValue = secondValue + temp;
                }
            }
            catch (Exception)
            {
                ExeptionCatcher(txtData);
            }
            Console.WriteLine("Do you want to reverse numerics?Paste 'Yes'/'No'");
            string chooserForRevers = Console.ReadLine();
            if (chooserForRevers.ToLower() == "yes")
            {
                FibonacciSortMethod(sortedDataFibonacci, lengthForFibonacci);
            }
            else if (chooserForRevers.ToLower() == "no")
            {
                FibonacciSortMethodWithoutNumerics(sortedDataFibonacci, lengthForFibonacci);
            }
            else
            {
                ExeptionCatcher(txtData);
            }
        }
        IExecuter execute = new Executer();
        protected void FibonacciSortMethod(List<string> txtData, int lengthForFibonacci)
        {
            List<string> resultTxtData = new List<string>();
            foreach (var item in txtData)
            {
                resultTxtData.Add(Reverser.Reverse(item));
            }
            execute.Execute(resultTxtData);
        }
        protected void FibonacciSortMethodWithoutNumerics(List<string> txtData, int lengthForFibonacci)
        {
            List<string> resultTxtData = new List<string>();
            foreach (var item in txtData)
            {
                resultTxtData.Add(Reverser.ReverseWithoutNumbersRevers(item));
            }
            execute.Execute(resultTxtData);
        }
    }
    class Reverser
    {
        public static string Reverse(string stringInput)
        {
            char[] charArray = stringInput.ToCharArray();
            string reverse = null;
            for (int i = charArray.Length - 1; i >= 0; i--)
            {
                reverse += charArray[i];
            }
            return reverse;
        }
        public static string ReverseWithoutNumbersRevers(string stringInput)
        {
            char[] charArray = stringInput.ToCharArray();
            char[] numericsArray = new char[charArray.Length];
            string reverse = String.Empty;
            for (int i = charArray.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(charArray[i]))
                {
                    reverse += charArray[i];
                }
                else
                {
                    numericsArray[i] += charArray[i];
                }
            }
            for (int i = 0; i < numericsArray.Length; i++)
            {
                reverse += numericsArray[i];
            }
            return reverse;
        }
    }
    class Executer : IExecuter
    {
        public void Execute(List<string> finalExecuter)
        {
            Console.WriteLine("Plase enter the name for file");
            var filepath = "Z:\\SF\\" + Console.ReadLine() + ".txt";
            File.AppendAllLines(filepath, finalExecuter);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Choice chooser = new Choice();
            string path = chooser.ChooseMethod();
            IDataProcessor dataProcessor = new ReadData();
            dataProcessor.ProcessData(new Reader(), path);
            Console.ReadKey();
        }
    }
}
