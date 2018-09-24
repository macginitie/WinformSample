using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;


namespace ObjLoader
{
    class MeshInfo
    {
        Point3DCollection _vertices = null;

        public int VertexCount()
        {
            return _vertices.Count;
        }
    }
}
