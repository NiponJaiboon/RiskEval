using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Office.Interop.Excel;


namespace iSabaya.ObjectFileMapper.ExcelFile
{
    public class ExcelFileReader : IFileReader
    {
        public ExcelFileReader(string filePath)
        {
            this.ImportFilePath = filePath;
        }

        /// <summary>
        /// Base is 0.  Default is 0.
        /// </summary>
        public virtual int CurrentRowNo
        {
            get { return this.LineNo; }
            set { this.LineNo = value; }
        }

        private Worksheet recordBuffer;
        public virtual Worksheet RecordBuffer
        {
            get
            {
                if (null == this.recordBuffer)
                {
                    this.recordBuffer = (Worksheet)this.ImportSource.ActiveSheet;
                    if (null == this.recordBuffer)
                        throw new iSabayaException(String.Format("Can't get active sheet of '{0}'" + this.ImportFilePath));
                }
                return this.recordBuffer;
            }
            set { this.recordBuffer = value; }
        }

        private Workbook importSource;
        public virtual Workbook ImportSource
        {
            get
            {
                if (String.IsNullOrEmpty(this.ImportFilePath))
                    throw new iSabayaException(Messages.FileFormatFilePathIsNotDefined);

                if (null == this.importSource)
                {
                    try
                    {
                        Application excel = new Application();
                        Workbook wb = excel.Workbooks.Open(this.ImportFilePath, 3, true, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                                                            Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                        this.importSource = wb;
                    }
                    catch (Exception exc)
                    {
                        throw new iSabayaException(Messages.CantOpenFile(this.ImportFilePath), exc);
                    }
                }
                return this.importSource;
            }

            protected set { this.importSource = value; }
        }

        public virtual object Extract(int columnNo)
        {
            Worksheet ws = (Worksheet)this.importSource.Sheets[1];
            Range cell = (Range)ws.Cells[this.CurrentRowNo, columnNo + 1];
            return cell.Value;
        }

        public Worksheet Next()
        {
            ++this.CurrentRowNo;
            return this.RecordBuffer;
        }

        #region IFileReader Members

        public void Close()
        {
            if (null == this.importSource)
                return;

            this.importSource.Close(false);
            this.importSource = null;
        }

        public string ImportFilePath { get; set; }

        public int LineNo { get; set; }

        object IFileReader.Next()
        {
            return this.Next();
        }

        public void Previous()
        {
            --this.CurrentRowNo;
        }

        #endregion

    }
}
