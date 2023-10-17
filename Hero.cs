using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppEntity
{
    internal class Hero : INotifyPropertyChanged
    {
        private string? _name;
        private string? _race;
        private int _age;
        private string? _weapon;
        
        public Hero() {}

        public int Id { get; set; }
        public string? Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string? Race
        {
            get { return (string?)_race; }
            set { _race = value; OnPropertyChanged("Race"); }
        }
        public int Age
        {
            get { return _age; }
            set { _age = value; OnPropertyChanged("Age"); }
        }

        public string? Weapon
        {
            get { return _weapon; }
            set { _weapon = value; OnPropertyChanged("Weapon"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
//internal class Hero
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }
//    public string? Race { get; set; }
//    public int Age { get; set; }
//    public string? Weapon { get; set; }
//}