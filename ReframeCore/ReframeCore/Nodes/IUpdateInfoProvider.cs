﻿using ReframeCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Nodes
{
    public interface IUpdateInfoProvider
    {
        NodeUpdateInfo UpdateInfo { get; }
    }
}
