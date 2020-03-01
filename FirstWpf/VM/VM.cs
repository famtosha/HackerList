using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System.Windows.Media.Imaging;

namespace FirstWpf
{
    class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<PlayerInfo> _info = new List<PlayerInfo>();
        public List<PlayerInfo> Info
        {
            get
            {
                return _info;
                
            }
            set
            {
                _info = value; OnPropertyChanged("Info");
            }
        }

        private bool _isWorking = false;
        public bool IsWorking
        {
            get
            {
                return _isWorking;
            }
            set
            {
                _isWorking = value; OnPropertyChanged("IsWorking");
            }
        }

        private string _listPathTB;
        public string PathTB
        {
            get
            {
                return _listPathTB;
            }
            set
            {
                _listPathTB = value; OnPropertyChanged("PathTB");
            }
        }

        private string _addTB;
        public string AddTB
        {
            get
            {
                return _addTB;
            }
            set
            {
                _addTB = value; OnPropertyChanged("AddTB");
            }
        }

        private PlayerInfo _selectedItem;
        public PlayerInfo SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value; OnPropertyChanged("SelectedItem");
            }
        }

        private RelayCommand _getAllCommand;
        public RelayCommand GetAllCommand
        {
            get
            {
                return _getAllCommand ??
                  (_getAllCommand = new RelayCommand(x =>
                  {
                      IsWorking = true;
                      GetAllAsync();
                  },
                  (x) =>
                  {
                      return IsWorking == false;
                  }));
            }
        }
        private async void GetAllAsync()
        {           
            var result = await MainClasses.GetAllAsync();
            Application.Current.Dispatcher.Invoke(() => { Info = result; IsWorking = false; });            
        }

        private RelayCommand _getOnlineCommand;
        public RelayCommand GetOnlineCommand
        {
            get
            {
                return _getOnlineCommand ??
                  (_getOnlineCommand = new RelayCommand(x =>
                  {
                      IsWorking = true;
                      GetOnlineAsync();
                  },
                  (x) =>
                  {
                      return IsWorking == false;
                  }));
            }
        }
        private async void GetOnlineAsync()
        {
            var result = await MainClasses.GetOnlineAsync();
            Application.Current.Dispatcher.Invoke(() => { Info = result; IsWorking = false; });
        }

        private RelayCommand _changePathCommand;
        public RelayCommand ChangePathCommand
        {
            get
            {
                return _changePathCommand ??
                  (_changePathCommand = new RelayCommand(x =>
                  {
                      InfoPath.SetHackerListPath(PathTB);
                  }));
            }
        }

        private RelayCommand _removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??
                  (_removeCommand = new RelayCommand(x =>
                  {
                      MainClasses.RemoveFromList(SelectedItem.ID);
                      Info = MainClasses.GetOnline();
                  },
                  (x) =>
                  {
                      return _selectedItem != null;
                  }));
            }
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand(x =>
                  {
                      MainClasses.AddToList(AddTB);
                  }));
            }
        }

        private RelayCommand _openInBrowserCommand;
        public RelayCommand OpenInBrowserCommand
        {
            get
            {
                return _openInBrowserCommand ??
                  (_openInBrowserCommand = new RelayCommand((x) =>
                  {
                      System.Diagnostics.Process.Start("https://steamcommunity.com/profiles/" + _selectedItem.ID);
                  },
                  (x) =>
                  {
                      return _selectedItem != null;
                  }));
            }
        }
    }
}
