﻿using ReframeAnalyzer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeAnalyzer.Filters
{
    public class ObjectAnalysisFilter : AnalysisFilter
    {
        public ObjectAnalysisFilter(List<IAnalysisNode> originalNodes) : base(originalNodes)
        {
            Query = new Predicate<IAnalysisNode>(n => IsSelected(n.Parent.Parent2) && IsSelected(n.Parent.Parent) && IsSelected(n.Parent) && IsSelected(n));
        }
        public override List<IAnalysisNode> GetAvailableAssemblyNodes()
        {
            List<IAnalysisNode> assemblyNodes = new List<IAnalysisNode>();

            foreach (var objectNode in OriginalNodes)
            {
                if (assemblyNodes.Exists(n => n.Identifier == objectNode.Parent.Parent2.Identifier) == false)
                {
                    assemblyNodes.Add(objectNode.Parent.Parent2);
                }
            }

            return assemblyNodes;
        }

        public override List<IAnalysisNode> GetAvailableNamespaceNodes()
        {
            List<IAnalysisNode> namespaceNodes = new List<IAnalysisNode>();

            foreach (var objectNode in OriginalNodes)
            {
                if (namespaceNodes.Exists(n => n.Identifier == objectNode.Parent.Parent.Identifier) == false)
                {
                    namespaceNodes.Add(objectNode.Parent.Parent);
                }
            }

            return namespaceNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes()
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (var objectNode in OriginalNodes)
            {
                if (classNodes.Exists(n => n.Identifier == objectNode.Parent.Identifier) == false)
                {
                    classNodes.Add(objectNode.Parent);
                }
            }

            return classNodes;
        }

        public override List<IAnalysisNode> GetAvailableClassNodes(IAnalysisNode namespaceNode)
        {
            List<IAnalysisNode> classNodes = new List<IAnalysisNode>();

            foreach (var objectNode in OriginalNodes)
            {
                if (objectNode.Parent.Parent.Identifier == namespaceNode.Identifier && classNodes.Exists(n => n.Identifier == objectNode.Parent.Identifier) == false)
                {
                    classNodes.Add(objectNode.Parent);
                }
            }

            return classNodes;
        }

        public override List<IAnalysisNode> GetAvailableObjectNodes()
        {
            List<IAnalysisNode> objectNodes = new List<IAnalysisNode>();

            foreach (var objectNode in OriginalNodes)
            {
                objectNodes.Add(objectNode);
            }

            return objectNodes;
        }

        public override List<IAnalysisNode> GetAvailableObjectNodes(IAnalysisNode classNode)
        {
            List<IAnalysisNode> objectNodes = new List<IAnalysisNode>();

            foreach (var objectNode in OriginalNodes)
            {
                if (objectNode.Parent.Identifier == classNode.Identifier && objectNodes.Exists(n => n.Identifier == objectNode.Identifier) == false)
                {
                    objectNodes.Add(objectNode);
                }
            }

            return objectNodes;
        }
    }
}
