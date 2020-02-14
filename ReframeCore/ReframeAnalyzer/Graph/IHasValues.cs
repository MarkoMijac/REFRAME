﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Graph
{
    public interface IHasValues
    {
        string CurrentValue { get; }
        string PreviousValue { get; }
    }
}
