using System;
namespace iSabaya
{
    interface IPersistentTemporalEntity : IPersistentEntity
    {
        void Activate(Context context, TimeInterval effectivePeriod, UserAction approvedAction);
        TimeInterval EffectivePeriod { get; set; }
        bool IsEffective { get; }
        bool IsEffectiveOn(DateTime date);
        void Terminate(Context context, DateTime expiryTS);
        void Terminate(DateTime expiryTS);
    }
}
