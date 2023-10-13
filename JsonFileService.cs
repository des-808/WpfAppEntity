using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppEntity
{
    internal class JsonFileService : IFileService
    {
        public List<Hero> Open(string filename)
        {
            List<Hero> phones = new List<Hero>();
            DataContractJsonSerializer jsonFarmatter = new DataContractJsonSerializer(typeof(List<Hero>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                phones = jsonFarmatter.ReadObject(fs) as List<Hero>;
            }
            return phones;
        }

        public void Save(string filename, List<Hero> phonesList)
        {
            DataContractJsonSerializer jsonFarmatter = new DataContractJsonSerializer(typeof(List<Hero>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFarmatter.WriteObject(fs, phonesList);
            }
        }
    }
}
