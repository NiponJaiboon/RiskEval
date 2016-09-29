using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    public class TextFileWriter : IFileWriter
    {
        public TextFileWriter(string filePath, Encoding encoding)
        {
            this.FilePath = filePath;
            this.Encoding = encoding;
        }

        public Encoding Encoding { get; set; }

        private StreamWriter exportDestination { get; set; }
        public virtual StreamWriter ExportDestination
        {
            get
            {
                if (String.IsNullOrEmpty(this.FilePath))
                    throw new Exception("File path is empty.");

                if (null == this.exportDestination)
                {
                    try
                    {
                        StreamWriter writer;
                        if (null == this.Encoding)
                            writer = new StreamWriter(this.FilePath, false, new UTF8Encoding(false));// Encoding.UTF8);
                        else
                            writer = new StreamWriter(this.FilePath, false, this.Encoding);
                        this.exportDestination = writer;
                    }
                    catch (Exception exc)
                    {
                        throw new Exception("Cannot write file '" + this.FilePath + "'", exc);
                    }
                }
                return this.exportDestination;
            }

            protected set { this.exportDestination = value; }
        }

        public virtual void Append(String text)
        {
            if (String.IsNullOrEmpty(text))
                return;

            this.RecordBuffer.Append(text);
            ++this.CurrentColNo;
        }

        public virtual void AppendCSV(String text)
        {
            if (FieldDelimiterChar == 0)
                throw new Exception("Value separator for variable length record is null.");
            if (this.RecordBuffer.Length > 0)
                this.RecordBuffer.Append(FieldDelimiterChar);
            if (!String.IsNullOrEmpty(text))
                this.RecordBuffer.Append(text);
            ++this.CurrentColNo;
        }

        public StringBuilder RecordBuffer = new StringBuilder();

        public virtual char FieldDelimiterChar { get; set; }

        #region IFileWriter Members

        public virtual void Append(object text)
        {
            Append((string)text);
        }

        public virtual void Close()
        {
            this.ExportDestination.Close();
        }

        public virtual int CurrentColNo { get; set; }

        public virtual int CurrentRowNo { get; set; }

        public virtual string FilePath { get; set; }

        public virtual void Next()
        {
            this.ExportDestination.WriteLine(this.RecordBuffer.ToString());
            ++this.CurrentRowNo;
            this.CurrentColNo = 1;
            this.RecordBuffer.Clear();
        }

        #endregion
    }
}
