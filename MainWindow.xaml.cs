using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfAppEntity
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel(new DefaulDialogService(), new JsonFileService()); 
        }

        private void fileSave_Click(object sender, RoutedEventArgs e)
        {
            //Hero? hero = heroesList.SelectedItem as Hero;
            //if (hero is null) return;
            //Files.files(@"c:\app\content.txt",hero);
            
        }
        public void fileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            //fileManager fileSave = new();
            //fileSave.ShowDialog();
            //List<Hero> list = db.Heroes.ToList();
            //Files.files(@"c:\app\content.txt", list);
        }

        
    }
   
}
