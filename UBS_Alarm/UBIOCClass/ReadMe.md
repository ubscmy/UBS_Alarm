����Ű propfull ���� ��ü
����Ű prop ���� ����
����Ű ctor ���� ctor ������ 

1.NuGet:Microsoft.Extensions.DependencyInjection 8.0 ���� ��ġ
Install-Package Microsoft.Extensions.DependencyInjection
2.MainWindow ����
3.�⺻���� �߰�

WpfDINaviagation/
��
������ ViewModels/
��   ������ MainViewModel.cs
��   ������ HomeViewModel.cs
��   ������ AboutViewModel.cs
��   ������ NotifyPropertyChangedBase.cs��
��
������ Stores/
��   ������ MainNavigationStore.cs
��
������ Views/
��   ������ MainView.xaml
��   ������ HomeView.xaml
��   ������ AboutView.xaml
��
������ Services/
��   ������ NavigationService.cs
��
������ Models/
��
������ Commands
��   ������ DelegateCommand.cs
��
������ App.xaml
������ App.xaml.cs
������ MainWindow.xaml

4. ������ �̵��Ͽ� 
 4-1. StartupUri="MainWindow.xaml"> ����
 4-2. App.xaml.cs �̵�. IoC/DI ���� �ڵ� ����

5. MvvM �������� �ۼ� ADD
 5-1.NotifyPropertyChangedBase
 5-2.DelegateCommand

6. Navigation �߰�
 6-1.â ���� 
 6-2.viewModel �߰�
 6-3.MainView.xaml ����
   
<!-- ContentControl�� �Ϲ����� ��� ��� -->
<!-- ���� �� �ε�: -->
<!-- MVVM ���ø����̼ǿ��� ContentControl�� �������� �並 ��ȯ�ϴ� �� �Ϲ������� ���˴ϴ�. 'Content' �Ӽ��� �Ϲ������� ǥ���� ���� �並 �����ϴ� ViewModel�� �Ӽ��� ���ε��˴ϴ�. -->
<!-- ���� ������ ǥ��: -->
<!-- ContentControl�� ����Ͽ� �ؽ�Ʈ, �̹��� �Ǵ� ��Ÿ ��� ������ UI ��Ҹ� ǥ���� �� �ֽ��ϴ�. -->
<!-- ������ ���ø�: ContentControl�� ����ϸ� ������ ������ ��ü�� ���ε��ϰ� ContentTemplate�� ����Ͽ� �ش� ��ü�� ǥ���ϴ� ����� ���� ����� ������ �� �ֽ��ϴ�.-->
 6.4 �� xaml ����
 6.5 CurrentViewModel Navigation�ϱ� CurrentViewModel���Ͽ� class ����

7. Services ����
 7-1. NavigationService css �߰�
 7-2. INavigationService interface �߰�
 7-3. NaviType enum �߰�

8. LoginViewModel 
 cs ����  

9. class App  Stroes �� Services �߰�

10. ViewModels �𵨿��� �߰�
	
11.MainViewModel(������ ������ ����)