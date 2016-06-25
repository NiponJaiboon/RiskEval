using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper
{
    public interface IFileWriter
    {
        void Append(object value);
        int CurrentColNo { get; set; }
        int CurrentRowNo { get; set; }
        //void ClearLineBuffer();
        string FilePath { get; set; }
        void Next();
        void Close();
    }
}
