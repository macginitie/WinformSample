using System;
using System.IO;
using System.Windows.Media.Media3D;


namespace ObjLoader
{
    class MeshInfo
    {
        Point3DCollection _vertices = null;

        public string MeshName { get; set; }

        public MeshInfo()
        {
            _vertices = new Point3DCollection();
        }

        public int VertexCount()
        {
            return _vertices.Count;
        }

        // sets meshname to "" if no more meshes; kludgy (I underestimated time required for this task)
        public void LoadFileLines(string[] fileLines, ref int index, out string nextMeshName)
        {
            nextMeshName = "";
            while (index < fileLines.Length)
            {
                if (ProcessLine(fileLines[index], ref nextMeshName))
                {
                    ++index;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Parses and loads a line from an OBJ file.
        /// non-geometry info is discarded.
        /// returns false when a new mesh is encountered ("g")
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
                        meshName = parts[1];
                        return false;
                    case "v":
                        Point3D v = new Point3D(Convert.ToDouble(parts[1]), Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3]));
                        _vertices.Add(v);
                        break;
                    case "vn":
                        // 2DO
                        break;
                    case "vt":
                        // 2DO
                        //TextureVertex vt = new TextureVertex();
                        //vt.LoadFromStringArray(parts);
                        //TextureList.Add(vt);
                        //vt.Index = TextureList.Count();
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
