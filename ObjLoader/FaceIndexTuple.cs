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
            for (int i = 0; i < parts.Length; ++i)
            {
                if (String.IsNullOrWhiteSpace(parts[i]))
                {
                    // 2DO: verify an appropriate value to use here
                    // ... for purposes of the present exercise, this is fine
                    // ... but a real utility might want to handle it differently
                    parts[i] = "0";
                }
            }
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

        // 2DO: verify an appropriate value to use here when one of the
        // ... values supplied is missing (i.e., the empty string "")
        // ... "0" is fine for this exercise but maybe not for a real utility
        private void Initialize(string v, string n, string uv)
        {
            // 2DO: determine if Convert.ToUInt64 would be better here
            // ... in my limited testing this is fine as is, but for
            // ... a real utility, a specification of .obj file formats
            // ... to be supported would supply the answer
            try
            {
                VertexIndex = Convert.ToInt32(v);
            }
            catch (Exception)
            {
                VertexIndex = 0;
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
