   M S B A M L   `   `   `  ����  PointOfSale    PointOfSale.Fueling   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .��5
      �   � RowDefinitions  5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   � $��10q�6   $��0��5      
������� � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��1.5* �6#   �� 5      $��* �6#   �� 5      $
��1.5* �6#   �� 5      $��* �6#   �� 5      $
��1.5* �6#     5      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5       $��* �6    �� 5!      $��0* �6    .+�5#      �� $��Fueling��6   $��Center=�5$      $��Center=�5%      $��0��5&      $��6��5'      $
��Bold"�5(      $��18(�5)      �� 5*      $��0��6   $��1��5+      $��Center=�5,      $��Car Registration Number��5-      �� 5.      $��0��6   $��2��5/      $��Center=�50      $��
Instructor��51      �� 52      $��4��6   $��1��53      $��Center=�54      $��Fuel Amount (Liters)��55      �� 56      $��0��6   $��3��57      $��Center=�58      $��Milage (KM)��59      �� 5:      $��2��6   $��1��5;      $��Center=�5<      $
��Date��5=      �� 5>      $��2��6   $��2��5?      $��Center=�5@      $��Amount Paid��5A      �� 5B      $��4��6   $��2��5C      $��Center=�5D      $��Invoice Number��5E      �� 5F      $��2��6   $��3��5G      $��Center=�5H      $��Notes��5I      �� 5K      -   5        $��	cmbCarReg��5K      $��1��5L      $��1��5M      $��10q�5N      $��25��5O       �� 
IsEditable .  �� SelectedValuePath$ ID��5Q       �� DisplayMemberPath$ VehicleRegistration��5R      5S      �� 5T      -   5        $��cmbInstructor��5T      $��1��5U      $��2��5V      $��10q�5W      $��25��5X      $ ID��5Y      $ FullName��5Z      {�.  5[      �� 5\      -   5        $��txtNotes��5\      $��3��5]      $��3��5^      $��2��5_      $��10q�5`      %  Xceed.Wpf.Toolkit.DecimalUpDown  5a      -   5        $��txtFuelAmount��5a   !   $��5��5b   !   $��1��5c   !   $��10q�5d   !   $��25��5e   !   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   ShowButtonSpinner .  5f   !   (  "System.Windows.Controls.DatePicker  5g      -   5        $��dtpDate��5g      $��3��5h      $��1��5i      $��10q�5j      $��25��5k        5l      -   5        $��	txtAmount��5l   !   $��3��5m   !   $��2��5n   !   $��10q�5o   !   $��25��5p   !    .  �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$ C2��5r   !      Maximum$ 
9999999.00X�5s   !      Minimum$ .5X�5t   !   	   Value$
	 0.00X�5u   !     5v      -   5        $��	txtMilage��5v   !   $��1��5w   !   $��3��5x   !   $��10q�5y   !   $��25��5z   !   
   	AllowSpin
 .   .  5|   !   �� 5}      -   5        $��
txtInvoice��5}      $��5��5~      $��2��5      $��10q�5�      $��25��5�      �� 5�      -	   5        $��btnSave��5�      $��5��5�      $��5��5�      $
��Save��5�      $��Right=�5�      $
��10,5q�5�      $��85��5�      $��25��5�      5�      5�      &   System.Windows.Controls.DataGrid  5�   
   -
   5        $��
dtgFueling��5�      $��1��5�         AutoGenerateColumns .  $��0q�5�         GridLinesVisibility$ 
Horizontal=�5�         
IsReadOnly .     CanUserAddRows .     HorizontalGridLinesBrush
 ��·�   RowBackground
 �����
�������   Columns 5�      4  .System.Windows.Controls.DataGridComboBoxColumn  5�      -   5        
cmbCarReg1��,  &System.Windows.Controls.DataGridColumn   Header$ Car Reg��5�   )      Width5  /System.Windows.Controls.DataGridLengthConverter$
 .35* 5�   )      SelectedValuePath$ ID��5�   )      DisplayMemberPath$ VehicleRegistration��5�   )      SelectedValueBinding 5�   )   �� *	VehicleID+ �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�5�   )     5�      -   5        cmbInstructor1��$ 
Instructor��5�   )   $
 .45* 5�   )   $ ID��5�   )   $ FullName��5�   )      
IsReadOnly .  5�   )   �� *InstructorID+ . $ PropertyChanged=�5�   )   0	  *System.Windows.Controls.DataGridTextColumn	  5�      $ Fuel Amount��6&   $
 .35* 5�   &   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 5�   &   �� *
FuelAmount+ . $ PropertyChanged=�	  5�      $ Milage��6%   $	 .5* 5�   %    5�   %   �� *Milage+ . $ PropertyChanged=�4  .System.Windows.Controls.DataGridTemplateColumn  5�      $
 Date��6*   $
 .35* 5�   *      CellTemplate 5�      �� 5�      .R�5�           SelectedDate 6)   �� *Date+ . $ PropertyChanged=�5�      5�      	  5�      $ Amount Paid��6%   $
 .35* 5�   %    5�   %   �� *Price+ . $ PropertyChanged=�	  5�      $ Invoice Number��6%   $	 .5* 5�   %    5�   %   �� *InvoiceNumber+ . $ PropertyChanged=�	  5�      $ Notes��6%   $
 1.5* 5�   %    5�   %   �� *Notes+ . $ PropertyChanged=�5�      5�      5�      