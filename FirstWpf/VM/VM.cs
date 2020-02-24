using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FirstWpf
{
    class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<PlayerInfo> _info = new ObservableCollection<PlayerInfo>();
        public ObservableCollection<PlayerInfo> Info
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
                _selectedItem = value;
            }
        }

        private RelayCommand _getAll;
        public RelayCommand GetAll
        {
            get
            {
                return _getAll ??
                  (_getAll = new RelayCommand(x =>
                  {
                      Info = MainClasses.GetAll();
                  }));
            }
        }

        private RelayCommand _getOnline;
        public RelayCommand GetOnline
        {
            get
            {
                return _getOnline ??
                  (_getOnline = new RelayCommand(x =>
                  {
                      Info = MainClasses.GetOnline();
                  }));
            }
        }

        private RelayCommand _changePath;
        public RelayCommand ChangePath
        {
            get
            {
                return _changePath ??
                  (_changePath = new RelayCommand(x =>
                  {
                      InfoPath.SetHackerListPath(PathTB);
                  }));
            }
        }

        private RelayCommand _remove;
        public RelayCommand Remove
        {
            get
            {
                return _remove ??
                  (_remove = new RelayCommand(x =>
                  {
                      MainClasses.RemoveFromList(SelectedItem.ID);
                      MessageBox.Show(SelectedItem.Name);
                      Info = MainClasses.GetOnline();
                  }));
            }
        }

        private RelayCommand _add;
        public RelayCommand Add
        {
            get
            {
                return _add ??
                  (_add = new RelayCommand(x =>
                  {
                      MainClasses.AddToList(AddTB);
                  }));
            }
        }
    }   
}
