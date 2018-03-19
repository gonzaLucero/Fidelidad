namespace Hexacta.Core.Tools.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    public class TextFileReader : IEnumerable<string[]>, IEnumerable
    {
        private bool checkNumberOfColumns;
        private string delimiter;
        private bool excludeLine;
        private string fileName;
        private int numberOfColumns;

        public TextFileReader(string fileName, string delimiter)
        {
            this.fileName = string.Empty;
            this.delimiter = string.Empty;
            this.excludeLine = false;
            this.checkNumberOfColumns = false;
            this.numberOfColumns = -1;
            this.fileName = fileName;
            this.delimiter = delimiter;
        }

        public TextFileReader(string fileName, string delimiter, bool excludeFirstLine)
        {
            this.fileName = string.Empty;
            this.delimiter = string.Empty;
            this.excludeLine = false;
            this.checkNumberOfColumns = false;
            this.numberOfColumns = -1;
            this.fileName = fileName;
            this.delimiter = delimiter;
            this.excludeLine = excludeFirstLine;
    /*
                LoggerUtility.DebugFormat("{0} - TextFileReader - File: {1}\n", DateTime.Now.ToLongTimeString(), this.fileName);
                LoggerUtility.DebugFormat("{0} - TextFileReader - Delimiter: {1}\n", DateTime.Now.ToLongTimeString(), this.delimiter);
                LoggerUtility.DebugFormat("{0} - TextFileReader - ExcludeLine: {1}\n", DateTime.Now.ToLongTimeString(), this.excludeLine);
      */
        }

        public TextFileReader(string fileName, string delimiter, bool excludeFirstLine, bool checkNumberOfColumns, int numberOfColumns)
        {
            this.fileName = string.Empty;
            this.delimiter = string.Empty;
            this.excludeLine = false;
            this.checkNumberOfColumns = false;
            this.numberOfColumns = -1;
            if (numberOfColumns < 1)
            {
                throw new ApplicationException("The number of Columns should be greater than 0");
            }
            this.fileName = fileName;
            this.delimiter = delimiter;
            this.excludeLine = excludeFirstLine;
            this.checkNumberOfColumns = checkNumberOfColumns;
            this.numberOfColumns = numberOfColumns;
        }

        IEnumerator<string[]> IEnumerable<string[]>.GetEnumerator()
        {
            using (StreamReader iteratorVariable0 = new StreamReader(this.fileName))
            {
                int iteratorVariable1 = 0;
                while (!iteratorVariable0.EndOfStream)
                {
                    if (this.excludeLine && (iteratorVariable1 == 0))
                    {
                        this.excludeLine = false;
                        iteratorVariable0.ReadLine();
                        continue;
                    }
                    iteratorVariable1++;
                    string iteratorVariable2 = iteratorVariable0.ReadLine() + string.Format("{0}{1}", this.delimiter, iteratorVariable1);
                    string[] iteratorVariable3 = iteratorVariable2.Split(this.delimiter.ToCharArray());
                    if (this.checkNumberOfColumns && (iteratorVariable3.Length != (this.numberOfColumns + 1)))
                    {
                        throw new ApplicationException("The number of fields does not match with the file format specification");
                    }
                    //LoggerUtility.DebugFormat("{0} - TextFileReader - Reading line {1} - {2}\n", DateTime.Now.ToLongTimeString(), iteratorVariable1, iteratorVariable2);
                    yield return iteratorVariable3;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)((IEnumerator<string[]>)this)).GetEnumerator();
        }
    }
}
