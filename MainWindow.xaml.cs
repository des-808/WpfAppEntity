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
        public delegate void MyDelegate(object heroes);
        AppContext db = new AppContext();
        internal static object? heroesLists;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;  
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Heroes.Load();
            DataContext = db.Heroes.Local.ToObservableCollection();
        }
        private void addBtn_Click(object sender, RoutedEventArgs e) 
        {
            add_menu addMenu = new(new MyDelegate(AddHeroDbToHeroesList));
            addMenu.ShowDialog();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            Hero? hero  = heroesList.SelectedItem as Hero; 
            if(hero is null) {return; }
            if (MessageBox.Show("Уверен что хочешь удалить?",
            "DELETE", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.Heroes.Remove(hero);
                db.SaveChanges();
                heroesList.Items.Refresh();
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Hero? hero = heroesList.SelectedItem as Hero;
            if (hero is null) return;

            EditHero editHero = new(new MyDelegate(EditHeroDbToHeroesList));
            editHero.nameTextBox.Text = hero.Name;
            editHero.raceTextBox.Text = hero.Race;
            editHero.ageTextBox.Text = hero.Age.ToString();
            editHero.weaponTextBox.Text = hero.Weapon;
            editHero.ShowDialog();
        }


        private void copyBtn_Click(Object sender, RoutedEventArgs e)
        {
            Hero hero = heroesList.SelectedItem as Hero;
            if (hero is null) return;
            Hero newHero = new Hero();
            newHero.Name = hero.Name;
            newHero.Race = hero.Race;
            newHero.Age = hero.Age;
            newHero.Weapon = hero.Weapon;
            db.Heroes.Add(newHero);
            db.SaveChanges();
            heroesList.Items.Refresh();
        }

        private void fileSave_Click(object sender, RoutedEventArgs e)
        {
            Hero? hero = heroesList.SelectedItem as Hero;
            if (hero is null) return;
            Files.files(@"c:\app\content.txt",hero);
            
        }
        public void fileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            fileManager fileSave = new();
            fileSave.ShowDialog();
            //List<Hero> list = db.Heroes.ToList();
            //Files.files(@"c:\app\content.txt", list);
        }

        public void selectAll_Click(object sender, EventArgs e)
        {
            List<Hero> list = new List<Hero>();
            foreach (var item in list)
            {


            }
        }

        private void heroesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Hero? hero = heroesList.SelectedItem as Hero;
            if (hero is null) return;
        }
        public void AddHeroDbToHeroesList(object heroes)
        {
            Hero heroes2 = heroes as Hero;
            Hero? hero = new Hero();
            hero.Name =   heroes2.Name;
            hero.Race =   heroes2.Race;
            hero.Age =    heroes2.Age;
            hero.Weapon = heroes2.Weapon;
            //Hero? hero2 = new Hero();
            //hero2 = db.Heroes.Find(hero.Id);
            db.Heroes.Add(hero);
            db.SaveChanges();
            heroesList.Items.Refresh();
        }

        public void EditHeroDbToHeroesList(object heroes)
        {
            Hero heroes2 = heroes as Hero;
            Hero? hero = heroesList.SelectedItem as Hero;
            if (hero is null) return;
            hero.Name = heroes2.Name;
            hero.Race = heroes2.Race;
            hero.Age = heroes2.Age;
            hero.Weapon = heroes2.Weapon;
            hero = db.Heroes.Find(hero.Id);
            db.SaveChanges();
            heroesList.Items.Refresh();
        }

        


    }
   
}
