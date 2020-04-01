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
using System.Diagnostics;

namespace FirstWpf
{
    class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {    
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Stopwatch Stopwatch = new Stopwatch();

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

        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value; OnPropertyChanged("Status");
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
                  (_getAllCommand = new RelayCommand(async (x) =>
                  {
                      Stopwatch.Start();
                      IsWorking = true;
                      Status = "InProgress...";
                      var result = await MainClasses.GetAllAsync();
                      Application.Current.Dispatcher.Invoke(() =>
                      {
                          Info = result; IsWorking = false;
                          Stopwatch.Stop();
                          Status = "Done in:" + Stopwatch.Elapsed.ToString();
                          Stopwatch.Reset();
                      });
                  },
                  (x) =>
                  {
                      return IsWorking == false;
                  }));
            }
        }

        private RelayCommand _getOnlineCommand;
        public RelayCommand GetOnlineCommand
        {
            get
            {
                return _getOnlineCommand ??
                  (_getOnlineCommand = new RelayCommand(async (x) =>
                  {
                      Stopwatch.Start();
                      IsWorking = true;
                      Status = "InProgress...";
                      var result = await MainClasses.GetOnlineAsync();
                      Application.Current.Dispatcher.Invoke(() =>
                      {
                          Info = result;
                          IsWorking = false;
                          Stopwatch.Stop();
                          Status = "Done in:" + Stopwatch.Elapsed.ToString();
                          Stopwatch.Reset();
                      });
                  },
                  (x) =>
                  {
                      return IsWorking == false;
                  }));
            }
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
                  },
                  (x) =>
                  {
                      return IsWorking == false;
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
                      return _selectedItem != null && IsWorking == false;
                  }));
            }
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand(async (x) =>
                  {
                      MainClasses.AddToList(AddTB);
                      Stopwatch.Start();
                      IsWorking = true;
                      Status = "InProgress...";
                      Application.Current.Dispatcher.Invoke(() =>
                      {
                          IsWorking = false;
                          Stopwatch.Stop();
                          Status = "Done in:" + Stopwatch.Elapsed.ToString();
                          Stopwatch.Reset();
                          AddTB = "";
                      });
                  },
                  (x) =>
                  {
                      return IsWorking == false;
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
