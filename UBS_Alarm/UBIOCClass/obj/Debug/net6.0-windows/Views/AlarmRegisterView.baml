   M S B A M L   `   `   `  ����E  AUBIOCClass, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:UBIOCClass.ViewsUBIOCClass.Views  5      =#clr-namespace:UBIOCClass.ViewModelsUBIOCClass.ViewModels  5      (    "UBIOCClass.Views.AlarmRegisterView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:UBIOCClass.Views     5      :
viewmodels#clr-namespace:UBIOCClass.ViewModels     5      .��5      �   � 	Resources  5   
    �� 
TargetType �� Property �� Value��%$  '��       �� 5      " ��6   .�5      ��  � ��$ Center=�68   �� 5       � ��$ Center=�66   �� 5       �@��TextAlignment$ Center=�62   �� 5       � ��$ 20(�6-   �� 5       � ��$
 Bold"�6/   �� 5       � ��
 � ���6/   5      5       � RowDefinitions 5   
   �� 5      $��4* �6   �� 5      $��3* �6   .+�5   
   � $��0��6   .+�5      �  5      �� 5      $��0.06* �6$   �� 5      $��1* �6$   .+�5       �� $��0��6   $��알람 리스트��6)   &   System.Windows.Controls.DataGrid  5!      $��1��6   $��5q�6(      SelectionUnit$ FullRow=�6c      HeadersVisibility$ Column=�6{      	RowHeight$ 40Q�5#         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5$      $��Center=�5%      	   AutoGenerateColumns$	 False��6,   ��5!   3   �� *RegisterModel.AlarmData+
 �� SelectedItem
 5"      �� *!RegisterModel.SelectedAlarmData+   Columns 5&      4  .System.Windows.Controls.DataGridTemplateColumn  5'      ,  &System.Windows.Controls.DataGridColumn   Header$ 등급��61      Width$ 1* 6=      CellTemplate 5(      �� 5)   "   .R�5*   &   �� ��60   �� *
AlarmLevel+��6L   �� *AlarmLevelColor+��6s   �� *AlarmLevelForeColor+5+   #   5-      0  *System.Windows.Controls.DataGridTextColumn  5/         
IsReadOnly$
 True��6.   $ 알람코드��6A   $ 2* 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6U   �� *	AlarmCode+6�     50      $
 True��6.   $ 알람구분��6A   $ 2* 6�    6U   �� *	AlarmType+6�     51      $
 True��6.   $ 	알람명��6A   $ 3* 6�    6V   �� *	AlarmName+6�     52      $
 True��6.   $ 알람설명��6A   $ 4* 6�    6U   �� *AlarmDescription+6�     53      $
 True��6.   $ 알람해결방안��6A   $ 4* 6~    6S   �� *AlarmSolveDescription+6~     54      $
 True��6.   $ 비고��6A   $ 2* 6�    6W   �� *	AlarmNote+6�   56      57      58      � 5:   
   $��1��6    � ColumnDefinitions 5;      �� 5<      $
��1.2* �6#   �� 5=      $��9* �6#   .+�5@      � $��0��6    5A      �� 5B      $��1* �6$   �� 5C      $��1* �6$   �� 5D      $��1* �6$   �� 5E      $��1* �6$   .+�5H      � $��0��6    5I      �� 5J      $
��0.8* �6(   �� 5K      $��1* �6(   .+�5M      �� $��0��6    $��알람 등급��6-   �� 5N      $��1��6   $
��auto��6U   $
��auto��6b   ��6+   �� *RegisterModel.AlarmLevel+6b   5O      � 5P      $��1��6    5Q      �� 5R      $
��0.8* �6(   �� 5S      $��1* �6(   .+�5U      �� $��0��6    $��알람 코드��6-   �� 5V      $��1��6   $
��auto��6T   $
��auto��6a   ��6+   �� *RegisterModel.AlarmCode+6a   5W      � 5X      $��2��6    5Y      �� 5Z      $
��0.8* �6(   �� 5[      $��1* �6(   .+�5]      �� $��0��6    $��
알람 명��6-   �� 5^      $��1��6   $
��auto��6T   $
��auto��6a   ��6+   �� *RegisterModel.AlarmName+6a   5_      � 5`      $��3��6    5a      �� 5b      $
��0.8* �6(   �� 5c      $��1* �6(   .+�5e      �� $��0��6    $��알람 구분��6-   �� 5f      $��1��6   $
��auto��6T   $
��auto��6a   ��6+   �� *RegisterModel.AlarmType+6a   5g      5h      � 5i      $��1��6    5j      �� 5k      $��1* �6$   �� 5l      $��6* �6$   �� 5m      $
��1.3* �6$   .+�5p      � $��0��6    5q      �� 5r      $
��1.2* �6+   �� 5s      $��3* �6+   �� 5t      $��3* �6+   �� 5u      $
��0.1* �6+   .+�5x      �� $��0��6    $��알람 설명��60   �� 5y      $��1��6    $��알람 해결 방안��60   �� 5z      $��2��6    $��비고��60   5{      � 5}      $��1��6    5~      �� 5      $
��1.2* �6+   �� 5�      $��3* �6+   �� 5�      $��3* �6+   �� 5�      $
��0.1* �6+   .+�5�      �� $��0��6   $
��Bold"�6_   $��10,10,10,10q�6q   ��6/   �� * RegisterModel.AlarmDescription+6q   �� 5�      $��1��6   $
��Bold"�6c   $��10,10,10,10q�6u   ��6.   �� *%#RegisterModel.AlarmSolveDescription+6u   �� 5�      $��2��6   $
��Bold"�6W   $��10,10,10,10q�6i   ��6.   �� *RegisterModel.AlarmNote+6i   5�      � 5�      $��2��6    5�      �� 5�      $��6* �6+   �� 5�      $��1* �6+   �� 5�      $��1* �6+   �� 5�      $��1* �6+   �� 5�      $��1* �6+   .+�5�      �� $��1��6   $��5q�6-   $��추가��68   
�������$��20(�6X   $
��Bold"�6f   ��6x   �� *ICommandModel.DB_Insert+�� 5�      $��2��6   $��5q�6-   $��수정��68   
�������$��20(�6X   $
��Bold"�6f   ��6x   �� *ICommandModel.DB_Update+�� 5�      $��3��6   $��5q�6-   $��새로고침��68   
�������$��20(�6Z   $
��Bold"�6h   ��6z   �� *ICommandModel.DB_Select+�� 5�      $��4��6   $��5q�6-   $��삭제��68   
�������$��20(�6X   $
��Bold"�6f   ��6x   �� *ICommandModel.DB_Delete+5�      5�      5�      5�      5�      