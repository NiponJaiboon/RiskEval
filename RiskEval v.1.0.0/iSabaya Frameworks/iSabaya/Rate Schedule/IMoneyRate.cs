using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public interface IRate<T>
    {
        object Apply(T amount, MoneyRateRounding rounding);
        IRate<T> Clone();
    }
}
