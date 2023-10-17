using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppEntity
{
    internal interface IFileService
    {
        List<Hero> Open(string filename);
        void Save(string filename, List<Hero> heroList);
    }
}
