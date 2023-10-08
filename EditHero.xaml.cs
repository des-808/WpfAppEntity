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
    /// <summary>
    /// Логика взаимодействия для EditHero.xaml
    /// </summary>
    public partial class EditHero : Window
    {
        MyDelegate d;
        public EditHero(MyDelegate sender)
        {
            InitializeComponent();
            d = sender;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Hero? hero = new Hero();

            hero.Name = nameTextBox.Text;
            hero.Race = raceTextBox.Text;
            hero.Age = Convert.ToInt32(ageTextBox.Text);
            hero.Weapon = weaponTextBox.Text;
            d(hero);
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
