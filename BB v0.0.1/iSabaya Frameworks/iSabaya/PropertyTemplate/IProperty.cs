using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public interface IProperty
    {
        String ToValueString(Object instance);
        byte[] ToBytes(Object instance);
        Object FromBytes(byte[] bytes);
    }
}
