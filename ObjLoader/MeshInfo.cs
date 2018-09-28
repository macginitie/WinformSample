using System;
using System.Collections.Generic;
using System.Threading; // only used to test the Cancel/progress update
using System.Windows.Media.Media3D;


namespace ObjLoader
{
    class MeshInfo
    {
        List<Point3D> _vertices = null;
        List<Point3D> _normals = null;
        // texture coords are only 2D, but Point2D doesn't exist
        // so I just set the 3rd value to 0.0
        List<Point3D> _uvCoords = null;

        List<FaceDefinition> _triangles = null;
        List<FaceDefinition> _quadrilaterals = null;

        public string MeshName { get; set; }
        public int VertexCount { get { return _vertices.Count; } }
        public int NormalCount { get { return _normals.Count; } }
        public int UVCoordCount { get { return _uvCoords.Count; } }
        // note: individual values are stored, but convenient access has not
        // yet been implemented
        public int TriangularFaceCount { get { return _triangles.Count; } }
        public int QuadFaceCount { get { return _quadrilaterals.Count; } }

        public MeshInfo()
        {
            _vertices = new List<Point3D>();
            _normals = new List<Point3D>();
            _uvCoords = new List<Point3D>();
            _triangles = new List<FaceDefinition>();
            _quadrilaterals = new List<FaceDefinition>();
        }

        public MeshInfo(List<Point3D> vert, List<Point3D> norm,
            List<Point3D> uvCoord)
        {
            _vertices = vert;
            _normals = norm;
            _uvCoords = uvCoord;
        }

        // sets meshname to "" if no more meshes
        public void LoadFileLines(string[] fileLines, ref int index, out string nextMeshName)
        {
            nextMeshName = "";
            while (index < fileLines.Length)
            {
                if (! ProcessLine(fileLines[index++], ref nextMeshName))
                {
                    break;
                }
                // used for testing Cancel, progress; without this it's too fast
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Parses and loads a line from an OBJ file.
        /// non-geometry info is discarded.
        /// returns false when a new mesh is encountered (i.e., when the line starts with "g ")
        /// Note: adapted from code obtained from https://github.com/stefangordon/ObjParser
        /// </summary>		
        private bool ProcessLine(string line, ref string meshName)
        {
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0)
            {
                switch (parts[0])
                {
                    case "g":
                        meshName = parts[1]; // 2DO: handle error condition if "g" is alone on the line
                        return false;
                    case "v":
                        Point3D v = new Point3D(Convert.ToDouble(parts[1]), Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3]));
                        _vertices.Add(v);
                        break;
                    case "vn":
                        Point3D vn = new Point3D(Convert.ToDouble(parts[1]), Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3]));
                        _normals.Add(vn);
                        break;
                    case "vt":
                        // setting 3rd coord to 0.0 so I can use the Point3DCollection class (just for convenience)
                        Point3D vt = new Point3D(Convert.ToDouble(parts[1]), Convert.ToDouble(parts[2]), 0.0);
                        _uvCoords.Add(vt);
                        break;
                    case "f":
                        FaceDefinition faceDef = new FaceDefinition();
                        if (parts.Length == 4) // 3 vertices plus the "f"
                        {
                            faceDef.AddTriangularFace(parts[1], parts[2], parts[3]);
                            _triangles.Add(faceDef);
                        }
                        else // note: pentagonal, hexagonal, etc. faces are handled as quadrilaterals (not covered in spec)
                        {
                            faceDef.AddQuadrilateralFace(parts[1], parts[2], parts[3], parts[4]);
                            _quadrilaterals.Add(faceDef);
                        }
                        break;
                    default:    // ignore everything else
                        break;
                }
            }
            return true;
        }
    }
}
