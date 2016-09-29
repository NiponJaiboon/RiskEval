using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper
{
    static class ClassExtensions
    {
        private const string LessMessage = "Found {0} records of {1} which is less than the specified minimum of {2}.";

        #region IMultiRecordMapper

        public static void Export<T, V>(this IMultiRecordMapper<T, V> recordMapper, IFileWriter writer)
            where V : class, new()
        {
            IEnumerable<V> vEnumerable = recordMapper.EnumerableOfV;
            if (null == vEnumerable)
            {
                recordMapper.Value = recordMapper.GetObject();
                if (null != recordMapper.Value)
                    ExportOneInstance(writer, recordMapper);
            }
            else
            {
                foreach (V v in vEnumerable)
                {
                    recordMapper.Value = v;
                    ExportOneInstance(writer, recordMapper);
                }
            }
        }

        private static void ExportOneInstance<T, V>(IFileWriter writer, IMultiRecordMapper<T, V> recordMapper)
            where V : class, new()
        {
            if (null != recordMapper.SignatureRecordMapper)
            {
                recordMapper.SignatureRecordMapper.Value = recordMapper.Value;
                recordMapper.SignatureRecordMapper.Export(writer);
            }
            foreach (var m in recordMapper.RecordMappers)
            {
                m.Target = recordMapper.Value;
                m.Export(writer);
            }
        }

        public static ExtractStatus Import<T, V>(this IMultiRecordMapper<T, V> recordMapper, IFileReader reader)
            where V : class, new()
        {
            ExtractStatus r = ExtractStatus.Failed;
            int count = 0;
            do
            {
                V v = null;
                if (null == recordMapper.SignatureRecordMapper)
                {
                    v = new V();
                    r = ExtractStatus.Success;
                }
                else
                {
                    recordMapper.SignatureRecordMapper.Target = recordMapper.Target;
                    recordMapper.SignatureRecordMapper.Value = null;
                    r = recordMapper.SignatureRecordMapper.Import(reader);
                    v = recordMapper.SignatureRecordMapper.Value;

                    if (r == ExtractStatus.Success && v == null || r == ExtractStatus.Failed)
                    {
                        if (count >= recordMapper.MinOccurrence
                            && (recordMapper.MaxOccurrence == 0 || count <= recordMapper.MaxOccurrence))
                            r = ExtractStatus.Success;
                        else
                        {
                            r = ExtractStatus.Failed;
                            r.Message = String.Format(LessMessage, count, recordMapper.ToString(), recordMapper.MinOccurrence);
                        }
                        return r;
                    }
                    else if (r == ExtractStatus.SkipTheRest)
                        break;
                }

                if (r == ExtractStatus.Success)
                {
                    ++count;
                    recordMapper.Value = v;
                    recordMapper.SetProperty();
                    if (null != recordMapper.RecordMappers)
                    {
                        foreach (var m in recordMapper.RecordMappers)
                        {
                            m.Target = v;
                            r = m.Import(reader);
                            if (r != ExtractStatus.Success)
                                break;
                        }
                    }
                }

            } while (recordMapper.MaxOccurrence == 0 || count < recordMapper.MaxOccurrence);

            return r;
        }

        #endregion IMultiRecordMapper

        #region ISingleRecordMapper

        public static void Export<T, V>(this ISingleRecordMapper<T, V> recordMapper, IFileWriter writer)
            where V : class, new()
        {
            IEnumerable<V> vEnumerable = recordMapper.EnumerableOfV;
            if (null != vEnumerable)
            {
                foreach (V v in vEnumerable)
                {
                    recordMapper.Value = v;
                    recordMapper.Insert(writer);
                }
            }
            else
            {
                if (null == recordMapper.Value)
                    recordMapper.Value = recordMapper.GetObject();

                recordMapper.Insert(writer);
            }
        }

        public static ExtractStatus Import<T, V>(this ISingleRecordMapper<T, V> recordMapper, IFileReader reader)
            where V : class, new()
        {
            ExtractStatus r;
            int count = 0;
            do
            {
                try
                {
                    reader.Next();

                    recordMapper.Value = null;
                    r = recordMapper.Extract(reader);
                    if (r == ExtractStatus.Success)
                    {
                        ++count;
                        recordMapper.SetProperty();
                    }
                    else
                    {
                        if (r == ExtractStatus.ValueMismatched)
                        {
                            reader.Previous();
                            if (count >= recordMapper.MinOccurrence)
                                r = ExtractStatus.Success;
                            else
                            {
                                r = ExtractStatus.Failed;
                                r.Message = String.Format(LessMessage, count, recordMapper.ToString(), recordMapper.MinOccurrence);
                            }
                        }
                        break;
                    }
                }
                catch (EndOfStreamException)
                {
                    if (count >= recordMapper.MinOccurrence)
                    {
                        r = ExtractStatus.EndOfFile;
                        break;
                    }
                    else
                    {
                        r = ExtractStatus.Failed;
                        r.Message = String.Format(LessMessage, count, recordMapper.ToString(), recordMapper.MinOccurrence);
                    }
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Error at line {0} : {1}", reader.LineNo, exc.ToString()));
                }

            } while (recordMapper.MaxOccurrence == 0 || count < recordMapper.MaxOccurrence);

            return r;
        }

        #endregion ISingleRecordMapper

        #region IMultiFieldValueMapper

        public static ExtractStatus Extract<V>(this IMultiFieldValueMapper<V> mapper, IFileReader reader)
            where V : class, new()
        {
            ExtractStatus r = ExtractStatus.Failed;
            if (mapper.SignatureFieldMapper == null)
            {
                if (mapper.Value == null)
                    mapper.Value = new V();
            }
            else
            {
                mapper.SignatureFieldMapper.Target = mapper.Value;
                r = mapper.SignatureFieldMapper.Extract(reader);
                if (r != ExtractStatus.Success)
                    return r;
                if (mapper.Value == null)
                    mapper.Value = mapper.SignatureFieldMapper.Target;
            }

            if (null == mapper.Value)
            {
                if (mapper.IsMandatory)
                    throw new Exception(mapper.FieldInfo + "Mandatory field is empty.");
            }
            else if (null != mapper.FieldMappers)
            {
                foreach (var f in mapper.FieldMappers)
                {
                    f.Target = mapper.Value;
                    r = f.Extract(reader);
                    if (r != ExtractStatus.Success)
                        break;
                }
            }

            return ExtractStatus.Success;
        }

        private static ExtractStatus ExtractValues<V>(IMultiFieldValueMapper<V> mapper, IFileReader reader)
            where V : class, new()
        {
            ExtractStatus r = ExtractStatus.Success;

            if (null != mapper.FieldMappers)
            {
                foreach (var f in mapper.FieldMappers)
                {
                    f.Target = mapper.Value;
                    r = f.Extract(reader);
                    if (r != ExtractStatus.Success)
                        break;
                }
            }
            return r;
        }

        public static void Insert<V>(this IMultiFieldValueMapper<V> mapper, IFileWriter writer)
            where V : class, new()
        {
            if (mapper.SignatureFieldMapper != null)
            {
                mapper.SignatureFieldMapper.Target = mapper.Value;
                mapper.SignatureFieldMapper.Insert(writer);
            }

            foreach (var f in mapper.FieldMappers)
            {
                f.Target = mapper.Value;
                f.Insert(writer);
            }
        }

        #endregion IMultiFieldValueMapper
    }
}
