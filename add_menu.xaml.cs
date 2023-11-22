using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;
using static WpfAppEntity.MainWindow;

namespace WpfAppEntity
{
    public partial class add_menu : Window
    {
        internal Hero Hero { get; set; }
        internal add_menu(Hero hero) 
        {
            InitializeComponent();
            Hero = hero;
            DataContext = Hero;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)=>Close();
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Hero.Name = nameTextBox.Text;
            Hero.Race = raceTextBox.Text;
            Hero.Age = Convert.ToInt32(ageTextBox.Text);
            Hero.Weapon = weaponTextBox.Text;
            DialogResult = true;
        }
    }
}
