using System;
using System.Windows.Media.Media3D;


namespace ObjLoader
{
    class MeshInfo
    {
        Point3DCollection _vertices = null;
        Vector3DCollection _normals = null;
        // texture coords are only 2D, but Point3DCollection is convenient
        // so I just set the 3rd value to 0.0
        Point3DCollection _uvCoords = null;

        public string MeshName { get; set; }
        public int VertexCount { get { return _vertices.Count; } }
        public int NormalCount { get { return _normals.Count; } }
        public int UVCoordCount { get { return _uvCoords.Count; } }

        public MeshInfo()
        {
            _vertices = new Point3DCollection();
            _normals = new Vector3DCollection();
            _uvCoords = new Point3DCollection();
        }

        public MeshInfo(Point3DCollection vert, Vector3DCollection norm, 
            Point3DCollection uvCoord)
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
                        Vector3D vn = new Vector3D(Convert.ToDouble(parts[1]), Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3]));
                        _normals.Add(vn);
                        break;
                    case "vt":
                        Point3D vt = new Point3D(Convert.ToDouble(parts[1]), Convert.ToDouble(parts[2]), 0.0);
                        _vertices.Add(vt);
                        break;
                    case "f":
                        // 2DO
                        //Face f = new Face();
                        //f.LoadFromStringArray(parts);
                        //FaceList.Add(f);
                        break;
                    default:    // ignore everything else
                        break;
                }
            }
            return true;
        }
    }
}
