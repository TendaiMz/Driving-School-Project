   M S B A M L   `   `   `  ����G  CWpfScheduler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:WpfScheduler;assembly=WpfSchedulerWpfScheduler  5       PointOfSale!   PointOfSale.StudentSchedule   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?my0clr-namespace:WpfScheduler;assembly=WpfScheduler     5      X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xtck)http://schemas.xceed.com/wpf/xaml/toolkit  5      e	 aSyncfusion.SfSchedule.WPF, Version=14.4451.0.15, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89`
 \Syncfusion.Tools.Wpf, Version=14.4451.0.15, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89g cSyncfusion.SfRadialMenu.Wpf, Version=14.4451.0.15, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89h dSyncfusion.SfSpreadsheet.WPF, Version=14.4451.0.15, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89e aSyncfusion.SfCellGrid.WPF, Version=14.4451.0.15, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.Wpf, Version=14.4451.0.15, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89<
Syncfusion!http://schemas.syncfusion.com/wpf 	 
     5      -   5        
�������.��5      �   � ColumnDefinitions  5   
   �� 5      $��3* �6   �� 5      $	��32* �6    � RowDefinitions 5   
   �� 5      $
��1.5* �6   �� 5      $	��20* �6   .+�5   
   , 	 &Syncfusion.UI.Xaml.Schedule.SfSchedule  -   5        $��studentScheduler��5          FocusedMonth
 �����   NonFocusedMonth
 �����   ScheduleType$
 Week=�5          WorkEndHour$ 21��5          WorkStartHour$ 5��5          ShowNonWorkingHours .     TimeMode$ TwentyFourHours=�5       $��1��5       $��1��5       � 5    
   $��Bottom=�6   $��0��5!      $��1��5"      .+�5#      �� $��Student Name��6   $��Center=�5$      �� 5%      -   5        	 ��Name$	 cmbStudentName��5%      $	��200��5&      $��2q�5'      
 �� 
IsEditable
 .  �� SelectedValuePath$ ID��5)       �� DisplayMemberPath$ FullName��5*      5+      � 5,      $��Bottom=�6     5.      �� 5/      �� 50      �� 51      �� 52      �� 53      �� 54      .+�56      �� $	��100��6   $��0��57      $��	Completed��58      �� 59      $��80��6   $��2��5:      $��Pending��5;      �� 5<      -   5         ��Name$ txbCompleted��5>      $��20��5<      $��Center=�5=      $��1��5?      
����  �$��SemiBold"�5A      $��	-30,0,0,0q�5B      �� 5C      -   5        $ 
txbPending��5E      $��20��5C      $��Center=�5D      $��3��5F      
����  �$��SemiBold"�5H      $��	-30,0,0,0q�5I      5J      5L      � 5M   
   $��1��5N      $��0,0,10,0q�5O      $��Bottom=�5P       � Orientation$ 
Horizontal=�5Q      $��Right=�5R      .+�5[      �� -   5         ��Name$ btnWeek��5[      $��0,0,10,0q�5\      $
��Week��5]      $
��Left=�5^      $	��Top=�5_      $��75��5`      
�������5b      �� 5c      -   5        $ btnMonth��5c      $��Month��5d      $
��Left=�5e      $	��Top=�5f      $��75��5g      
�������5i      5k      5j     5k     