using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iSabaya;

namespace iSabaya.ObjectFileMapper
{
    public delegate void DelegateVoidStringT<T>(string filePath, T t);

    public abstract class ObjectFileMapping<T> : MappingBase
        where T : class, new()
    {
        #region abstract

        public abstract void Export(String filePath, T t);
        public abstract ExtractStatus Import(String filePath, out T t);
        public abstract IList<T> Import(String filePath);
        public abstract void FinalizeOutput(IFileWriter writer);
        public abstract IFileWriter InitializeOutput(String filePath);

        #endregion abstract

        #region persistent

        public virtual String Code { get; set; }
        public virtual String Description { get; set; }
        //public virtual TimeInterval EffectivePeriod { get; set; }
        public virtual String FileNamePattern { get; set; }
        public virtual String FileNameExtension { get; set; }

        private RecordMapperBase<T> recordMapping;
        public virtual RecordMapperBase<T> RecordMapping
        {
            get { return this.recordMapping; }
            set
            {
                this.recordMapping = value;
                if (null != this.recordMapping)
                    this.recordMapping.Initialize(this);
            }
        }
        
        public virtual DelegateVoidStringT<T> OnImportStartHandler { get; set; }
        public virtual DelegateVoidStringT<T> OnImportFinishHandler { get; set; }
        public virtual DelegateVoidStringT<T> OnExportStartHandler { get; set; }
        public virtual DelegateVoidStringT<T> OnExportFinishHandler { get; set; }
        public virtual int LineNoOfFirstDetailRecord { get; set; }
        public virtual String Version { get; set; }

        #endregion persistent

        /// <summary>
        /// Base is 1.  Default is 1.
        /// </summary>
        public virtual int CurrentRowNo { get; set; }

        public virtual bool HasBeenInitialized { get; set; }
    }
}
