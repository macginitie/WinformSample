using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjLoader
{
    class ObjFileInfo
    {
        string _filePath = null;
        string _errorInfo = "";

        // caller should verify existence of file
        public ObjFileInfo(string filePath)
        {
            _filePath = filePath;
        }

        public bool LoadFile()
        {


            return true;
        }

        public string GetErrorInfo()
        {
            return _errorInfo;
        }
    }
}
