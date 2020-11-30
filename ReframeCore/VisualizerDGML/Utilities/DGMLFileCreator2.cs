using ReframeVisualizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizerDGML.Utilities
{
    public class DGMLFileCreator2 : IGraphFileCreator
    {
        const string extension = "dgml";

        public void CreateNewFile(IVisualGraph visualGraph)
        {
            string fileName = GenerateName(visualGraph);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fullName = path + @"\" + fileName + "." + extension;
            string fileContent = visualGraph.SerializeGraph();

            File.WriteAllText(fullName, fileContent);

            System.Diagnostics.Process.Start(fullName);
        }

        private string GenerateName(IVisualGraph visualGraph)
        {
            return new Random().Next().ToString() + "_" + visualGraph.ReactorIdentifier;
        }
    }
}
