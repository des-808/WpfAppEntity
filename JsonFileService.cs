using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace WpfAppEntity
{
    internal class JsonFileService : IFileService
    {
        public List<Hero> Open(string filename)
        {
            List<Hero> heroes = new List<Hero>();
            DataContractJsonSerializer jsonFarmatter = new DataContractJsonSerializer(typeof(List<Hero>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                heroes = jsonFarmatter.ReadObject(fs) as List<Hero>;
            }
            return heroes;
        }

        public void Save(string filename, List<Hero> heroList)
        {
            DataContractJsonSerializer jsonFarmatter = new DataContractJsonSerializer(typeof(List<Hero>));
            using (FileStream fs = new FileStream(filename, FileMode.Create)) 
            {
                jsonFarmatter.WriteObject(fs, heroList);


            }
        }
    }
}
