using System;
using System.Collections.Generic;

using System.Text;

namespace iSabaya
{
    public interface IDotNetRule
    {
        RuleResult Run();

        RuleResult Run(object owner, ParameterList parameters);
    }
}
