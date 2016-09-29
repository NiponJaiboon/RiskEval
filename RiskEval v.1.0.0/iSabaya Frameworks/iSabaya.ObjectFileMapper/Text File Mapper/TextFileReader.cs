using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    public class TextFileReader : IFileReader
    {
        public TextFileReader(string filePath)
        {
            this.ImportFilePath = filePath;
        }

        private StreamReader recordSource { get; set; }
        public virtual StreamReader RecordSource
        {
            get
            {
                if (String.IsNullOrEmpty(this.ImportFilePath))
                    throw new Exception("File path is empty.");

                if (null == this.recordSource)
                {
                    try
                    {
                        StreamReader reader = new StreamReader(this.ImportFilePath);
                        this.recordSource = reader;
                        this.LineNo = 0;
                    }
                    catch (Exception exc)
                    {
                        throw new Exception("Cannot read file '" + this.ImportFilePath + "'", exc);
                    }
                }
                return (StreamReader)this.recordSource;
            }

            protected set { this.recordSource = value; }
        }

        public virtual string RecordBuffer { get; protected set; }

        public virtual string Next()
        {
            if (this.notUndo)
            {
                this.RecordBuffer = RecordSource.ReadLine();
                if (null == RecordBuffer)
                {
                    // sawangchai add Close(); due to error file being use by another process.
                    Close();
                    throw new EndOfStreamException();
                }
                else
                    ++this.LineNo;
            }
            
            this.notUndo = true;
            return this.RecordBuffer;
        }

        private bool notUndo = true;

        public virtual char FieldDelimiterChar { get; set; }

        public virtual string[] FieldValues { get; set; }

        public virtual string GetCSVFieldValue(int columnNo)
        {
            if (FieldDelimiterChar == 0)
                throw new Exception("Value separator for variable length record is null.");
            if (null == this.FieldValues)
                this.FieldValues = this.RecordBuffer.Split(FieldDelimiterChar);
            return this.FieldValues[columnNo];
        }

        #region IFileReader Members

        public void Close()
        {
            this.recordSource.Close();
        }

        public string ImportFilePath { get; set; }

        public int LineNo { get; set; }

        object IFileReader.Next()
        {
            return this.Next();
        }

        public void Previous()
        {
            this.notUndo = false;
        }

        #endregion
    }
}
