단축키 propfull 탭탭 전체
단축키 prop 탭탭 선언
단축키 ctor 탭탭 ctor 생성자 

1.NuGet:Microsoft.Extensions.DependencyInjection 8.0 으로 설치
Install-Package Microsoft.Extensions.DependencyInjection
2.MainWindow 삭제
3.기본폴더 추가

WpfDINaviagation/
│
├── ViewModels/
│   ├── MainViewModel.cs
│   ├── HomeViewModel.cs
│   ├── AboutViewModel.cs
│   └── NotifyPropertyChangedBase.cs│
│
├── Stores/
│   └── MainNavigationStore.cs
│
├── Views/
│   ├── MainView.xaml
│   ├── HomeView.xaml
│   └── AboutView.xaml
│
├── Services/
│   └── NavigationService.cs
│
├── Models/
│
├── Commands
│   └── DelegateCommand.cs
│
├── App.xaml
├── App.xaml.cs
└── MainWindow.xaml

4. 앱으로 이동하여 
 4-1. StartupUri="MainWindow.xaml"> 삭제
 4-2. App.xaml.cs 이동. IoC/DI 파일 코딩 수정

5. MvvM 패턴으로 작성 ADD
 5-1.NotifyPropertyChangedBase
 5-2.DelegateCommand

6. Navigation 추가
 6-1.창 생성 
 6-2.viewModel 추가
 6-3.MainView.xaml 수정
   
<!-- ContentControl의 일반적인 사용 사례 -->
<!-- 동적 뷰 로딩: -->
<!-- MVVM 애플리케이션에서 ContentControl은 동적으로 뷰를 전환하는 데 일반적으로 사용됩니다. 'Content' 속성은 일반적으로 표시할 현재 뷰를 보유하는 ViewModel의 속성에 바인딩됩니다. -->
<!-- 단일 콘텐츠 표시: -->
<!-- ContentControl을 사용하여 텍스트, 이미지 또는 기타 모든 유형의 UI 요소를 표시할 수 있습니다. -->
<!-- 데이터 템플릿: ContentControl을 사용하면 복잡한 데이터 개체를 바인딩하고 ContentTemplate을 사용하여 해당 개체를 표시하는 사용자 지정 방법을 정의할 수 있습니다.-->
 6.4 각 xaml 수정
 6.5 CurrentViewModel Navigation하기 CurrentViewModel위하여 class 생성

7. Services 생성
 7-1. NavigationService css 추가
 7-2. INavigationService interface 추가
 7-3. NaviType enum 추가

8. LoginViewModel 
 cs 수정  

9. class App  Stroes 및 Services 추가

10. ViewModels 모델에서 추가
	
11.MainViewModel(생성자 의전성 주입)