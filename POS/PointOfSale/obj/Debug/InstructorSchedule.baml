   M S B A M L   `   `   `  ����G  CWpfScheduler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:WpfScheduler;assembly=WpfSchedulerWpfScheduler  5       PointOfSale$   PointOfSale.InstructorSchedule   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?my0clr-namespace:WpfScheduler;assembly=WpfScheduler     5      X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xtck)http://schemas.xceed.com/wpf/xaml/toolkit  5      e	 aSyncfusion.SfSchedule.WPF, Version=14.4451.0.15, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89`
 \Syncfusion.Tools.Wpf, Version=14.4451.0.15, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89g cSyncfusion.SfRadialMenu.Wpf, Version=14.4451.0.15, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89h dSyncfusion.SfSpreadsheet.WPF, Version=14.4451.0.15, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89e aSyncfusion.SfCellGrid.WPF, Version=14.4451.0.15, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.Wpf, Version=14.4451.0.15, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89<
Syncfusion!http://schemas.syncfusion.com/wpf 	 
     5      
�������.��5      �   � ColumnDefinitions  5   
   �� 5      $��3* �6   �� 5      $	��32* �6    � RowDefinitions 5   
   �� 5      $
��1.5* �6   �� 5      $	��20* �6   .+�5   
   , 	 &Syncfusion.UI.Xaml.Schedule.SfSchedule  -   5        $��studentScheduler��5          FocusedMonth
 �����   NonFocusedMonth
 �����   ScheduleType$
 Week=�5          WorkEndHour$ 21��5          WorkStartHour$ 5��5          ShowNonWorkingHours .     TimeMode$ TwentyFourHours=�5       $��1��5       $��1��5       � 5   
   $��0��6   $��1��5        5!      �� 5"      $��* �6    �� 5#      $��* �6    .+�5%      �� $��Instructor Schedule��6   $��Center=�5&      $��Center=�5'      $��0��5(      $
��Bold"�5)      $��14(�5*      � 5+      $	��350��6   $
��Left=�5,      $��1��5-      .+�5.      �� $�� Instructor Name��6   $��Bottom=�5/      �� 50      -   5        $��cmbInstructorName��50      $	��200��51      $��2q�52      	 �� SelectedValuePath$	 ID��53      
 �� DisplayMemberPath$
 FullName��54      55      56      58      � 59   
   $��1��6   $��0,0,10,0q�5:      $��Bottom=�5;       � Orientation$ 
Horizontal=�5<      $��Right=�5=      .+�5>      �� -   5         ��Name$ btnDay��5>      $��0,0,10,0q�5?      $	��Day��5@      $
��Left=�5A      $	��Top=�5B      $��75��5C      
�������5E      �� 5F      -   5        $ btnWeek��5F      $��0,0,10,0q�5G      $
��Week��5H      $
��Left=�5I      $	��Top=�5J      $��75��5K      
�������5M      �� 5N      -   5        $ btnMonth��5N      $��Month��5O      $
��Left=�5P      $	��Top=�5Q      $��75��5R      
�������5T      5V      5!     5"     