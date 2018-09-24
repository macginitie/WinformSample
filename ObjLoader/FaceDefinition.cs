using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjLoader
{
    // class only supports triangular or quadrilateral faces, per spec
    class FaceDefinition
    {
        List<FaceIndexTuple> _vertices;

        public FaceDefinition()
        {
            _vertices = new List<FaceIndexTuple>();
        }

        public void AddTriangularFace(string v1, string v2, string v3)
        {
            _vertices.Add(new FaceIndexTuple(v1));
            _vertices.Add(new FaceIndexTuple(v2));
            _vertices.Add(new FaceIndexTuple(v3));
        }

        public void AddQuadrilateralFace(string v1, string v2, string v3, string v4)
        {
            _vertices.Add(new FaceIndexTuple(v1));
            _vertices.Add(new FaceIndexTuple(v2));
            _vertices.Add(new FaceIndexTuple(v3));
            _vertices.Add(new FaceIndexTuple(v4));
        }
    }
}
