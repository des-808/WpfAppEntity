using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppEntity
{
    public class HeroViewModel : INotifyPropertyChanged
    {
        private Hero hero;
        internal HeroViewModel(Hero h){this.hero = h;}
        public string Name
        {
            get { return hero.Name; }
            set { hero.Name = value;OnPropertyChanged("Name"); }
        }
        public string Race
        {
            get { return hero.Race; }
            set { hero.Race = value; OnPropertyChanged("Race"); }
        }
        public int Age
        {
            get { return hero.Age; }
            set { hero.Age = value; OnPropertyChanged("Age"); }
        }
        public string Weapon
        {
            get { return hero.Weapon; }
            set { hero.Weapon = value; OnPropertyChanged("Weapon"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
