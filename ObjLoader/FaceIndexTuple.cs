using System;

namespace ObjLoader
{
    class FaceIndexTuple
    {
        int VertexIndex { get; set; }
        int NormalIndex { get; set; }
        int UVCoordIndex { get; set; }

        public FaceIndexTuple(int v, int n, int uv)
        {
            VertexIndex = v;
            NormalIndex = n;
            UVCoordIndex = uv;
        }

        public FaceIndexTuple(string tuple)
        {
            string[] parts = tuple.Split(new char[] { '/' });
            switch (parts.Length)
            {
                case 1:
                    Initialize(parts[0], "0", "0");
                    break;
                case 2:
                    Initialize(parts[0], parts[1], "0");
                    break;
                case 3:
                    Initialize(parts[0], parts[1], parts[2]);
                    break;
                default:
                    Initialize("0", "0", "0");
                    break;
            }
        }

        public FaceIndexTuple(string v, string n, string uv)
        {
            Initialize(v, n, uv);
        }

        private void Initialize(string v, string n, string uv)
        { 
            // 2DO: determine if ToUInt64 would be better here
            try
            {
                VertexIndex = Convert.ToInt32(v);
            }
            catch (Exception)
            {
                VertexIndex = 0;    // 2DO: should this be -1 ?
            }

            try
            {
                NormalIndex = Convert.ToInt32(n);
            }
            catch (Exception)
            {
                NormalIndex = 0;
            }

            try
            {
                UVCoordIndex = Convert.ToInt32(uv);
            }
            catch (Exception)
            {
                UVCoordIndex = 0;
            }
        }
    }
}
