using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UBIOCClass.Commands;
using UBIOCClass.Services;
using UBIOCClass.Stores;

namespace UBIOCClass.ViewModels
{
    class AlarmViewModel : NotifyPropertyChangedBase
    {
        //public AlarmHistoryViewModel AlarmHistoryView { get; }
        //public AlarmRegisterViewModel AlarmRegisterView { get; }
        private readonly MainNavigationStore _mainNavigationStore;
        private INotifyPropertyChanged _currentViewModel;
        public INotifyPropertyChanged CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private void CurrentViewModelChanged()
        {
            CurrentViewModel = _mainNavigationStore.CurrentViewModel;
        }


        public AlarmViewModel(MainNavigationStore mainNavigationStore, INavigationService navigationService)
        {
            // ViewModel 초기화 코드
            _mainNavigationStore = mainNavigationStore;
            _mainNavigationStore.CurrentViewModelChanged += CurrentViewModelChanged;
        }
    }
}
