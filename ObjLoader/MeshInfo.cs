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

        public bool LoadFromFile(string path)
        {
            string[] fileLines = File.ReadAllLines(path);

            return true;
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
                    case "usemtl": // discard non-geometry data
                        break;
                    case "mtllib": // discard non-geometry data
                        break;
                    case "v":
                        Point3D v = new Point3D(Convert.ToDouble(parts[1]), Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3]));
                        _vertices.Add(v);
                        break;
                    case "f":
                        //Face f = new Face();
                        //f.LoadFromStringArray(parts);
                        //FaceList.Add(f);
                        break;
                    case "vt":
                        //TextureVertex vt = new TextureVertex();
                        //vt.LoadFromStringArray(parts);
                        //TextureList.Add(vt);
                        //vt.Index = TextureList.Count();
                        break;

                }
            }
            return true;
        }

    }
}
