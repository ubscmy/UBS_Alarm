   M S B A M L   `   `   `  ����E  AUBIOCClass, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:UBIOCClass.ViewsUBIOCClass.Views  5   	   =#clr-namespace:UBIOCClass.ViewModelsUBIOCClass.ViewModels  66        UBIOCClass.Views.AlarmView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:UBIOCClass.Views     5   	   :
viewmodels#clr-namespace:UBIOCClass.ViewModels     66   $
��1880��5   
   $	��990��6   .��5	      � .+�5
   
   �� 
������ $��0q�6.     �� SelectedIndex$  0��5       � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers	 P� Property
 P� Value �� 
TargetName��%,  '��       �� 5      " ��6   .�5      ��  � ��$ 16(�61   �� 5       � �� �#524E4F63   �� 5       � �� �#d18c2663   �� 5       � �� 5      �� 5      " ��6.   .R�5   "   �� $��Border��6)   $��30��67   $��1,1,1,0q�6C   ���#8D8688$ 10,3,0,0��6s   $	��2,0q�6�   ._�5   &   �� $ ContentSite��67   $��Center=�5      $��Center=�5      $��Header��5      $
��10,2q�5      5   #    5   "   P� 5   &   	 �@��
IsSelected$

 True��6D   . �5   *   �� $ Border��61    � �� �#d18c266[   �� 5   *   $ Border��61    � �� �#0A4D686z   5   '   P� 5    &   	 �@��
IsSelected$
 False��6D   . �5!   *   �� $ Border��61    � �� �#524E4F6[   5"   '   5$      5&      5'      5(      .@�5)      �� $��알람 발생 이력��6   .��5*      '   !UBIOCClass.Views.AlarmHistoryView   � DataContext 6)   �� *AlarmHistoryViewmodel+5+      �� 5,      $��알람 등록��6   .��5-      (   "UBIOCClass.Views.AlarmRegisterView   6*   �� *AlarmRegisterViewmodel+5.      54      56      57      