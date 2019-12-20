﻿using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public interface IScheduler
    {
        IList<INode> GetNodesForUpdate();
        IList<INode> GetNodesForUpdate(INode initialNode, bool omitInitialNode);

        Dictionary<int, IList<INode>> GetLayersOfNodesForUpdate(IList<INode> nodes);

        ISorter Sorter { get; }
        IDependencyGraph Graph { get; }
    }
}
