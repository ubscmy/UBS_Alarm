using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using UBIOCClass.Commands;
using UBIOCClass.Services;
using UBIOCClass.Stores;

namespace UBIOCClass.ViewModels
{
    public class MainViewModel : NotifyPropertyChangedBase
    {
		private readonly MainNavigationStore _mainNavigationStore;

        private INotifyPropertyChanged _currentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; set; }

        private void CurrentViewModelChanged()
		{
			CurrentViewModel = _mainNavigationStore.CurrentViewModel;

        }

        //private void CurrentViewModelChange(object obj)
        //{
        //    //eNaviType naviType = (eNaviType)obj;
        //    //_navigationService.Navigate(naviType);
        //}

        #region NavigationBar Dock
        // 20040906 ubs psy : 네비게이션 바의 방향(수평 또는 수직)을 나타내는 속성
        private Orientation _NavBarOrientation;
        public Orientation NavBarOrientation
        {
            get => _NavBarOrientation;
            // 20040906 ubs psy : SetProperty 메서드를 통해 값이 변경되면 UI에 변경 사항 알림
            set => SetProperty(ref _NavBarOrientation, value);
        }

        // 20040906 ubs psy : 네비게이션 바의 위치를 나타내는 속성 (상단, 하단, 좌측, 우측)
        private Dock _NavBarDock;
        public Dock NavBarDock
        {
            get => _NavBarDock;
            set
            {
                // 20040906 ubs psy : Dock 속성 값을 설정하고 변경 사항 알림
                _NavBarDock = value;
                NotifyPropertyChanged("NavBarDock");

                // 20040906 ubs psy : 네비게이션 바의 위치에 따라 방향을 자동으로 설정
                if (_NavBarDock == Dock.Top || _NavBarDock == Dock.Bottom)
                {
                    NavBarOrientation = Orientation.Horizontal; // 20040906 ubs psy : 상단 또는 하단일 경우 수평
                }
                else if (_NavBarDock == Dock.Left || _NavBarDock == Dock.Right)
                {
                    NavBarOrientation = Orientation.Vertical; // 20040906 ubs psy : 좌측 또는 우측일 경우 수직
                }
            }
        }
        #endregion

        private INavigationService _navigationService;
        public MainViewModel(MainNavigationStore mainNavigationStore, INavigationService navigationService)
		{
			_mainNavigationStore = mainNavigationStore;
			_mainNavigationStore.CurrentViewModelChanged += CurrentViewModelChanged;

            //_navigationService = navigationService;
            navigationService.Navigate(NaviType.AlarmView);

            //UpdateCurrentViewModelCommand = new DelegateCommand(CurrentViewModelChange);
        }

		public INotifyPropertyChanged CurrentViewModel
		{
			get { return _currentViewModel; }
			set {
				if (_currentViewModel != value)
				{
                    _currentViewModel = value;
					NotifyPropertyChanged();
                }
			}
		}
	}
}
