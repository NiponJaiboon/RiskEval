using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper
{
    public interface IFileReader
    {
        void Close();

        String ImportFilePath { get; set; }

        int LineNo { get; }

        /// <summary>
        /// Advance to the next line/record
        /// </summary>
        /// <returns></returns>
        object Next();

        /// <summary>
        /// Backtrack to the previous line/record
        /// </summary>
        /// <returns></returns>
        void Previous();
    }
}
