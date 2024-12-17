using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBIOCClass.Stores;
using UBIOCClass.ViewModels;
using UBIOCClass.Views;

namespace UBIOCClass.Services
{
    // 네비게이션 서비스를 구현하는 클래스
    public class NavigationService : INavigationService
    {
        // 현재 ViewModel을 관리하는 MainNavigationStore의 인스턴스
        private readonly MainNavigationStore _mainNavigationStore;

        // 현재 활성화된 ViewModel을 설정하는 프로퍼티
        private INotifyPropertyChanged CurrentViewModel
        {
            set => _mainNavigationStore.CurrentViewModel = value;
        }

        // 생성자 - MainNavigationStore를 주입받아 초기화
        public NavigationService(MainNavigationStore mainNavigationStore)
        {
            this._mainNavigationStore = mainNavigationStore;
        }

        // 네비게이션을 수행하는 메서드
        public void Navigate(NaviType naviType)
        {
            //CurrentViewModel = (NotifyPropertyChangedBase)App.Current.Services.GetService(typeof(AlarmViewModel));
            switch (naviType)
            {
                case NaviType.AlarmView:
                    // TestViewModel을 DI 컨테이너에서 가져와 CurrentViewModel로 설정
                    CurrentViewModel = (NotifyPropertyChangedBase)App.Current.Services.GetService(typeof(AlarmViewModel));
                    break;
                default:
                    return;  // 기본적으로 아무것도 하지 않음
            }
        }
    }
}
