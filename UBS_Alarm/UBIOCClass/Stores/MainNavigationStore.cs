using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBIOCClass.ViewModels;
using System.ComponentModel;

namespace UBIOCClass.Stores
{
    public class MainNavigationStore : NotifyPropertyChangedBase
    {
        private INotifyPropertyChanged _currentViewModel; // 현재 활성화된 ViewModel을 저장하는 필드

        public INotifyPropertyChanged CurrentViewModel
        {
            get { return _currentViewModel; } // 현재 ViewModel 반환
            set
            {
                // 새로운 ViewModel이 현재 ViewModel과 다를 경우에만 변경
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    CurrentViewModelChanged.Invoke(); // ViewModel이 변경되었음을 알리기 위해 이벤트 호출
                    _currentViewModel = null;
                }
            }
        }
        /// <summary>
        /// ViewModel 변경 시 호출되는 델리게이트(Action)
        /// </summary>
        public Action CurrentViewModelChanged { get; set; }
    }
}
