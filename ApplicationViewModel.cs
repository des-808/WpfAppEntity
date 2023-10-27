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
        private RelayCommand saveCommand;
        private RelayCommand allSaveCommand;
        private RelayCommand openCommand;
        private RelayCommand addCommand;
        private RelayCommand copyCommand;
        private RelayCommand editCommand;
        private RelayCommand deleteCommand;
        public ObservableCollection<Hero> Heroes { get; set; }
        Hero selectedHero;

        IFileService fileService;
        IDialogService dialogService;
        
        
        //command save file
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand((selectedItem) =>
                    {
                        try
                        {
                            if (dialogService.SaveFileDialog() == true)
                            {
                                Hero? hero = selectedItem as Hero;
                                List<Hero> list = new List<Hero>{hero };
                                fileService.Save(dialogService.FilePath, list);
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
        //command save file
        public RelayCommand AllSaveCommand
        {
            get
            {
                return allSaveCommand ??
                    (allSaveCommand = new RelayCommand(obj =>
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

        // команда добавления
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
            set{ selectedHero = value; OnPropertyChanged("SelectedHero");}
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

