using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer
{
    public enum AnalysisLevel
    {
        AssemblyLevel,
        NamespaceLevel,
        ClassLevel,
        ClassMemberLevel,
        ObjectLevel,
        ObjectMemberLevel,
        UpdateAnalysisLevel
    };
}
