using System;
namespace iSabaya
{
    interface IPersistentEntity
    {
        void Activate(Context context, UserAction approvedAction);
        UserAction ApproveAction { get; set; }
        UserAction CreateAction { get; set; }
        void Delete(Context context);
        long ID { get; }
        bool IsNotFinalized { get; set; }
        string LanguageCode { get; set; }
        void Persist(Context context);
        string Reference { get; set; }
        string Remark { get; set; }
        object Tag { get; set; }
        long TempID { get; set; }
        void Terminate();
        string ToString(string languageCode);
        UserAction UpdateAction { get; set; }
    }
}
