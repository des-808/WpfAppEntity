using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Documents;

namespace WpfAppEntity
{
    public static class Files
    {

        //public void files(string path) { 

        public static async void files(string path, object obj)
        {
            //string path = @"c:\app\content.txt";
            Hero hero = obj as Hero;

            string originalText = $"{hero.Id.ToString()} , {hero.Name} , {hero.Race} , {hero.Age.ToString()} , {hero.Weapon}";

            // запись строки
            //await File.WriteAllTextAsync(path, originalText);
            // дозапись в конец файла
            try { await File.AppendAllTextAsync(path, $"{originalText}\n"); } catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            // чтение файла
            //string fileText = await File.ReadAllTextAsync(path);
            //MessageBox.Show(fileText);
        }
        internal static async void files(string path,List<Hero> lst ) {
            //string path = @"c:\app\content.txt";
            Hero hero = new Hero();
            string originalText = "";
            foreach (var item in lst)
            {
                hero = item as Hero;
                originalText += $"{hero.Id} , {hero.Name} , {hero.Race} , {hero.Age} , {hero.Weapon}\n"; 
            }
            //await File.WriteAllTextAsync(path, originalText);// запись строки
            // дозапись в конец файла
            try 
            { 
                await File.AppendAllTextAsync(path,originalText);
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.ToString()); 
            }
            

            // чтение файла
            //string fileText = await File.ReadAllTextAsync(path);
            //MessageBox.Show(fileText);
        }
    }
}
