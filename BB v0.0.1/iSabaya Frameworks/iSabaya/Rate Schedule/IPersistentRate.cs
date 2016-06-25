using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
	public interface IPersistentRate<TInput, TOutput>
	{
        TOutput ApplyRate(TInput quantity, Rounding<TOutput> rounding);
        void Persist(Context context);
	}
}
