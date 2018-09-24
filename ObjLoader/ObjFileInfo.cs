using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ObjLoader
{
    class ObjFileInfo
    {
        Point3DCollection _vertices = null;
        bool _validFile = false;

        public ObjFileInfo(string filePath)
        {
            if (File.Exists(filePath))
            {

            }
        }

        public bool ValidFile()
        {
            return _validFile;
        }

        public int VertexCount()
        {
            return _vertices.Count;
        }
    }
}
