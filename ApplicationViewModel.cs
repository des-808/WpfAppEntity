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
        internal static object? heroesLists;
       
        Hero selectedPhone;

        IFileService fileService;
        IDialogService dialogService;

        public ObservableCollection<Hero> Heroes { get; set; }

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





        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
            //Heroes = new ObservableCollection<Hero>
            //{
            //    new Hero("IPhone 14", "Apple", 125000 ),
            //    new Hero("Galaxy S22 Ultra", "Samsung", 90000),
            //    new Hero("12 Lite", "Xiaomi", 35000)
            //};
            db.Heroes.Load();
            //DataContext = db.Heroes.Local.ToObservableCollection();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

