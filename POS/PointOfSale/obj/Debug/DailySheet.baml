   M S B A M L   `   `   `  ����g  cMicrosoft.ReportViewer.WinForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3atSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinFormsMicrosoft.Reporting.WinForms  5       PointOfSale   PointOfSale.DailySheet   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      \wfSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinForms  5      
�������.��5
      �   � RowDefinitions  5   
   �� 5      $��3* �6   �� 5      $	��17* �6   �� 5      $��3* �6   .+�5   
   �   5      �� 5      $��* �6    �� 5      $��* �6    .+�5      �� $#��Daily Schedule Reconciliation��6   $��Center=�6<   $��Center=�6W   $��0��6t   $
��Bold"�6�   $��18(�6�   � 5      $	��350��6   $��150,0q�6%   $��1��64   .+�5      �� $�� Instructor Name��6   �� 5      -   5        $��cmbInstructorName��5      $	��200��64   $��2q�6@    �� SelectedValuePath$ ID��6K    �� DisplayMemberPath$ FullName��6b   6   5      � 5      $��30,0,0,0q�6   $��1��6*   .+�5      �� $
��Date��6   $��Center=�6#   $��50��6>   (  "System.Windows.Controls.DatePicker  5      -   5        $��dtpDate��5      $��1��6(   $	��120��65   6A   5      5      &   System.Windows.Controls.DataGrid  5!   
   -   5        $��dtgData��5!   6   $��1��6   $��20,10,10,10q�6!      AutoGenerateColumns .     CanUserAddRows .  
�������$ �� IsSynchronizedWithCurrentItem$ FalseY�6�      RowStyle 5"      �� 5#       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5$      ��  �� Property � ��	 �� Value
	 �  ��63   �� 5%       � {�	 . 62   
 �� Triggers
 5&      �� 5'       �� Binding 6&   �� *ScheduleInstructorID+ �� Value 06O   .a�5(      ��  � ��
	 �����6;   �� 5)       � {�	 .  6:   5*      �� 5+       6&   �� *
Complete+ �� NotifyOnSourceUpdated .  �� Mode$ TwoWay=� True6l   .a�5,      ��  � ��
	 � �|�6O   5-      �� 5.       6&   �� *Cancel+ .  True6]   .a�5/      ��  � ��
	 � �|�6O   50      �� 51       6&   �� *NoShow+ .  True6]   .a�52      ��  � ��
	 � �|�6O   53      55         Columns 57      0  *System.Windows.Controls.DataGridTextColumn  58      ,  &System.Windows.Controls.DataGridColumn   Header$
 Time��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *Time+  59      $ Student Name��6%   $ 2* 6;    6G   �� *StudentName+  5:      $ Activity��6%   $
 1.5* 67    6E   �� *BookingName+  5;      $ ID��6%   $ 3* 5<   %      
Visibility$ 	Collapsed=�5>   %    5=   %   �� *ScheduleInstructorID+5>   %   4  .System.Windows.Controls.DataGridTemplateColumn  5H      $ 	Completed��6)   $	 .5* 6<      CellTemplate 5I      �� 5J      .R�5K      �� $��chkComplete��6'   -   5         d� 	IsChecked 5K   ;   �� *
Complete+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   5L      5N        5P      $ 	Cancelled��6)   $	 .5* 6<    5Q      �� 5R      .R�5S      �� $��chkCancelled��6'   -   5         5S   <   �� *Cancel+$ TwoWay=�$ PropertyChanged=�6�   5T      5V        5X      $ Noshow��6)   $	 .5* 69    5Y      �� 5Z      .R�5[      �� $��	chkNoShow��6'   -   5         5[   9   �� *NoShow+$ TwoWay=�$ PropertyChanged=�6�   5\      5^        5`      $ Comment��6)   $ 5* 6:    5a      �� 5b      .R�5c      �� $��
txtComment��6&   -   5        5c   8   5d      5f      5j      � 5k   
   $��2��6    � ColumnDefinitions 5l      �� 5m      $	��52* �6#   �� 5n      $	��56* �6#   �� 5o      $	��45* �6#   �� 5p      $	��52* �6#   �� 5q      $	��95* �6#   .+�5t      � $��4��6   .+�5u      �� -   5        $��btnPrint��5u      $��3��6)   $��Print Schedule��69   $��Right=�6R   $��
0,10,100,5q�6n   $��85��6�   $��35��6�   6�   �� 5v      -	   5        $��btnSave��5v      $
��Save��6)   $��Right=�68   $��	0,10,10,5q�6T   $��85��6g   $��35��6r   6~   5w      5x      2	  ,Xceed.Wpf.Toolkit.Primitives.WindowContainer	  5z   
   $��3��6!   $��2��65   .+�5{      #
  Xceed.Wpf.Toolkit.ChildWindow
  -
   5        $��	wndFilter��5{      $	��Top=�60    
  WindowStartupLocation$ Center=�6J    
  IsModal . 0  *Xceed.Wpf.Toolkit.Primitives.WindowControl
   Top$ 50Q�6y   $	��850��6�   $	��450��6�      Caption$ SELECT DATES��6�   .��5|      � .+�5�      7  1System.Windows.Forms.Integration.WindowsFormsHost  -   5        $��wfhMain��5�   (      Padding$ 10q�67      Child. 5�      /   )Microsoft.Reporting.WinForms.ReportViewer  -   5        	rptViewer��5�   *   5�      5�      5�      5�      5�      5�      