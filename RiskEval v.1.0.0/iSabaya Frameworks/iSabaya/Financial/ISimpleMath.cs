using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public interface ISimpleMath<T>
    {
        T Add(T b);
        T Subtract(T b);
        T Multiply(double rate);
    }
}
