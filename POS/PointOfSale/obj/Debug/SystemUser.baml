   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       PointOfSale   PointOfSale.SystemUser   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4s&clr-namespace:System;assembly=mscorlib     5      X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xtck)http://schemas.xceed.com/wpf/xaml/toolkit  5      .��5
      �   � RowDefinitions  5   
   �� 5      $	��53* �6   �� 5      $	��15* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dtgUsers��5       $��0��6      AutoGenerateColumns .  $��0q�6L      GridLinesVisibility$ 
Horizontal=�6`      
IsReadOnly .     CanUserAddRows .     HorizontalGridLinesBrush
 ��·�   RowBackground
 �����
�������$��2��5   D      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 63   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6=   �� *Name+ �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�  5      $ Surname��6%   $	 * 66   
 6@   �� *	Surname+ . $ PropertyChanged=�  5      $ Contact Number��6%   $
	 .75* 6=   
 6J   �� *ContactNumber+ . $ PropertyChanged=�  5      $ Email��6%   $
	 .75* 64   
 6A   �� *Email+ . $ PropertyChanged=�  5      $ 	User Name��6%   $	 * 68   
 6B   �� *
UserName+ . $ PropertyChanged=�4  .System.Windows.Controls.DataGridComboBoxColumn  5      -   5        cmbUserType��$ 	User Type��5   >   $
	 .75* 6Q      SelectedValuePath$ ID��6�      DisplayMemberPath$
 Name��6�      SelectedValueBinding 6_   �� *
UserType+ . $ PropertyChanged=�6�     5      $ Password��6%   $
	 1.5* 67   
 6D   �� *PasswordHash+ . $ PropertyChanged=�4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $ 	Activated��6)   $		 .5* 6=   
 6I   �� *	Activated+ . $ PropertyChanged=�5      �� 5   
   -   5        $��	btnUpdate��5      $��1��6"   $��Reset Password��6/   $��Right=�6H   $��10q�6d   $	��100��6p   $��35��6|   6�   2  ,Xceed.Wpf.Toolkit.Primitives.WindowContainer  5    
   .+�5!      #	  Xceed.Wpf.Toolkit.ChildWindow	  -   5        $��wndUpdateUser��5!   9   0
  *Xceed.Wpf.Toolkit.Primitives.WindowControl 
  Caption$  Reset Password��6    	  IsModal .  
  WindowBackground
 ��  � 
  Left$	 500Q�5"      $��5q�6    
 
  Top$	 100Q�5#      $	��300��5$      $	��200��5%      $��10q�5&      .��5'      �  � ColumnDefinitions 5(      �� 5)      �� 5*        5,      �� 5-      $
��.25* �6(   �� 5.      $��* �6(   �� 5/      $��* �6(   �� 50      $��* �6(   .+�53      �� $��0��6   $��1��6,   $
��Name��69   $��Center=�6H   �� 54      $��0��6   $��2��6,   $��Supervisor Password��69   $��Center=�6W   �� 56      -   5        $��cmbUserName��56      $��1��63   $��1��6C   $��10,15q�6P    �� DisplayMemberPath$ FullName��6_    �� SelectedValuePath$ ID��6|   G� 57      -   5        $��txtPassword��57   "   $��1��65   $��2��6E   $��10,15q�6R   �� 58      -   5        $��btnReset��58      $��1��6-   $��3��6=   $��30��6J   $��75��6V   $��RESET��6a   $��10q�6q   6}   59      5;      5=      5B      5C      