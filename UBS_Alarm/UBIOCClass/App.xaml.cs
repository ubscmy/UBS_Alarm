using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Windows;
using UBIOCClass.Services;
using UBIOCClass.Stores;
using UBIOCClass.ViewModels;
using UBIOCClass.Views;

namespace UBIOCClass
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 현재 실행 중인 Application 인스턴스를 App 타입으로 캐스팅하여 반환
        /// </summary>
        /// <remarks>
        /// Application.Current는 현재 실행 중인 WPF 애플리케이션의 기본 인스턴스를 가져오는 속성입니다.
        /// 이 속성은 기본적으로 Application 타입을 반환하지만, 이 코드를 통해 현재 애플리케이션 인스턴스를
        /// App 타입으로 캐스팅할 수 있습니다.
        /// 'new' 키워드는 부모 클래스(Application)의 동일한 이름의 멤버를 숨기기 위해 사용됩니다.
        /// 이를 통해 App 클래스 내에서 Current를 App 타입으로 더 쉽게 접근할 수 있게 합니다.
        /// </remarks>
        public new static App Current => (App)Application.Current;
        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection(); // ServiceCollection 객체 생성

            // Stores
            services.AddSingleton<MainNavigationStore>();

            //Services
            services.AddSingleton<INavigationService, NavigationService>();

            // ViewModels
            services.AddSingleton<MainViewModel>(); // MainViewModel을 싱글톤으로 등록
            services.AddSingleton<AlarmViewModel>();
            //services.AddSingleton<LoginViewModel>();
            //services.AddSingleton<SignupViewModel>();
            //services.AddSingleton<TestViewModel>();

            // Views
            // MainView의 DataContext로 MainViewModel을 설정하여 MainView를 싱글톤으로 등록
            services.AddSingleton(s => new MainView()
            {
                DataContext = s.GetRequiredService<MainViewModel>() // MainViewModel을 의존성 주입으로 가져옴
            });

            // 서비스 제공자(ServiceProvider) 생성 후 반환
            return services.BuildServiceProvider();
        }

        /// <summary>
        /// App 클래스 생성자
        /// </summary>
        public App()
        {
            Services = ConfigureServices(); // ConfigureServices 메서드를 호출하여 DI 설정 및 서비스 등록
            var mainView = Services.GetRequiredService<MainView>(); // MainView를 DI 컨테이너에서 가져옴
            mainView.Show();  // MainView를 화면에 표시
        }
        // 서비스 제공자(ServiceProvider)를 외부에서 접근할 수 있도록 하는 프로퍼티
        public IServiceProvider Services { get; }
    }
}

///
/// IServiceProvider는 C#에서 의존성 주입(Dependency Injection, DI) 패턴을 구현할 때 사용되는 서비스 제공자 인터페이스입니다.
/// 의존성 주입 컨테이너에서 객체 인스턴스를 관리하고, 필요한 곳에 해당 인스턴스를 주입해주는 역할을 합니다.
/// 싱글톤(Singleton): 애플리케이션이 실행되는 동안 단 하나의 인스턴스만 유지.
///

///
/// BuildServiceProvider란 무엇인가요?
/// 종속성 주입에서 ServiceCollection은 애플리케이션의 서비스(예: 클래스, 인터페이스 및 해당 종속성)를 등록하는 곳입니다.
/// 모든 서비스를 등록한 후에는 BuildServiceProvider 메서드를 사용하여 IServiceProvider 인터페이스의 인스턴스를 생성합니다. 그러면 이러한 종속성을 해결할 수 있습니다.
/// ServiceCollection: 모든 서비스, 서비스 수명(일시적, 범위 지정, 싱글톤), 인터페이스와 구현 간의 관계를 정의하고 등록하는 곳입니다.
/// ServiceProvider: ServiceCollection에서 BuildServiceProvider()를 호출한 결과입니다. 필요할 때 등록된 서비스의 인스턴스를 제공하는 방법을 아는 컨테이너입니다.
///