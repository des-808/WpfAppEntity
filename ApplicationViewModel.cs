using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppEntity
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        AppContext db = new AppContext();
        public ObservableCollection<Hero> Heroes { get; set; }
        Hero selectedHero;

        IFileService fileService;
        IDialogService dialogService;

        
        //command save file
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (dialogService.SaveFileDialog() == true)
                            {
                                fileService.Save(dialogService.FilePath, Heroes.ToList());
                                dialogService.ShowMessage("Файл сохранен");
                            }
                        }
                        catch (Exception ex)
                        {
                            dialogService.ShowMessage(ex.Message);
                        }
                    }));
            }
        }
        //command open file
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                    (openCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (dialogService.OpenFileDialog() == true)
                            {
                                var phones = fileService.Open(dialogService.FilePath);
                                Heroes.Clear();
                                foreach (var p in phones)
                                    Heroes.Add(p);
                                dialogService.ShowMessage("Файл открыт");
                            }
                        }
                        catch (Exception ex)
                        {
                            dialogService.ShowMessage(ex.Message);
                        }
                    }));
            }
        }

        //command save file
        private RelayCommand replace;
        public RelayCommand Replace
        {
            get
            {
                return replace ??
                    (replace = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (dialogService.SaveFileDialog() == true)
                            {
                                fileService.Save(dialogService.FilePath, Heroes.ToList());
                                dialogService.ShowMessage("Файл сохранен");
                            }
                        }
                        catch (Exception ex)
                        {
                            dialogService.ShowMessage(ex.Message);
                        }
                    }));
            }
        }

        // команда добавления
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      add_menu addMenu = new add_menu(new Hero());
                      if (addMenu.ShowDialog() == true)
                      {
                          Hero hero = addMenu.Hero;
                          db.Heroes.Add(hero);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда копирования
        private RelayCommand copyCommand;
        public RelayCommand CopyCommand
        {
            get
            {
                return copyCommand ??
                  (copyCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      Hero? hero = selectedItem as Hero;
                      if (hero == null) return;
                      Hero newHero = new Hero();
                      newHero.Name = hero.Name;
                      newHero.Race = hero.Race;
                      newHero.Age = hero.Age;
                      newHero.Weapon = hero.Weapon;
                      db.Heroes.Add(newHero);//db.Entry(hero).State = EntityState.Modified;
                      db.SaveChanges();
                  }));
            }
        }

        // команда редактирования
        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      Hero? hero = selectedItem as Hero;
                      if (hero == null) return;
 
                      Hero vm = new Hero
                      {
                          Name = hero.Name,
                          Race = hero.Race,
                          Age = hero.Age,
                          Weapon = hero.Weapon,
                      };
                      EditHero editHero = new EditHero(vm);
 
                      if (editHero.ShowDialog() == true)
                      {
                          hero.Name = editHero.Hero.Name;
                          hero.Race = editHero.Hero.Race;
                          hero.Age = editHero.Hero.Age;
                          hero.Weapon = editHero.Hero.Weapon;
                          db.Entry(hero).State = EntityState.Modified;
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда удаления
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      Hero? hero = selectedItem as Hero;
                      if (hero == null) return;
                      db.Heroes.Remove(hero);
                      db.SaveChanges();
                  }));
            }
        }


        public Hero SelectedHero
        {
            get { return selectedHero; }
            set
            {
                selectedHero = value;
                OnPropertyChanged("SelectedHero");
            }
        }

        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
            db.Heroes.Load();
            Heroes = db.Heroes.Local.ToObservableCollection();

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

