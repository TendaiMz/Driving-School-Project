   M S B A M L   `   `   `  ����  PointOfSale!    PointOfSale.MaintenanceCard   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � RowDefinitions  5	   
   �� 5
      $
��300* �6   �� 5      $
��300* �6   .+�5   
   � $��10q�6   $��0��6   
������� � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��1.5* �6#   �� 5      $��* �6#   �� 5      $
��1.5* �6#   �� 5      $��* �6#   �� 5      $
��1.5* �6#     5      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��0* �6    .+�5      �� $��Maintenance��6   $��Center=�6*   $��Center=�6E   $��0��6b   $��6��6o   $
��Bold"�6�   $��18(�6�   �� 5       $��0��6   $��1��6$   $��Center=�61   $��Car Registration Number��6L   �� 5!      $��0��6   $��2��6$   $��Center=�61   $��
Instructor��6L   �� 5"      $��4��6   $��1��6$   $��Center=�61   $��Maintenance Type��6L   �� 5#      $��2��6   $��3��6$   $��Center=�61   $��Description��6L   �� 5$      $��2��6   $��1��6$   $��Center=�61   $
��Date��6L   �� 5%      $��2��6   $��2��6$   $��Center=�61   $��Amount Paid��6L   �� 5&      $��4��6   $��2��6$   $��Center=�61   $��Invoice Number��6L   �� 5'      $��0��6   $��3��6$   $��Center=�61   $��Notes��6L   �� 5)      -   5        $��	cmbCarReg��5)      $��1��6)   $��1��69   $��25��6F   $��10q�6V    �� 
IsEditable .  �� SelectedValuePath$ ID��6t    �� DisplayMemberPath$ VehicleRegistration��6�   �� 5*      -   5        $��cmbInstructor��5*      $��1��6-   $��2��6=   $��25��6J   $��10q�6Y   $ ID��6e   $ FullName��6|   �� 5+      -   5        $��txtNotes��5+      $��1��6'   $��3��68   $��2��6E   $��75��6V   $��10q�6f   �� 5,      -   5        $��cmbType��5,      $��5��6&   $��1��66   $��25��6C   $��10q�6R   $ ID��6^   $
 Name��6u   (  "System.Windows.Controls.DatePicker  5-      -   5        $��dtpDate��5-      $��3��6(   $��1��68   $��25��6E   $��10q�6T   �� 5.      -   5        $��	txtAmount��5.      $��3��6'   $��2��67   $��25��6D   $��10q�6S   �� 5/      -   5        $��txtDescription��5/      $��3��6,   $��2��6<   $��3��6P   $��25��6]   $��10q�6l   �� 50      -   5        $��
txtInvoice��50      $��5��6(   $��2��68   $��25��6E   $��10q�6U   �� 51      -	   5        $��btnSave��51      $��5��6$   $��5��64   $
��Save��6A   $��Right=�6P   $	��5,5q�6l   $��85��6y   $��25��6�   6�   52      &   System.Windows.Controls.DataGrid  53   
   -
   5        $��dtgMaintenance��53       $��1��6      AutoGenerateColumns .  $��0q�6S      GridLinesVisibility$ 
Horizontal=�6g      
IsReadOnly .     CanUserAddRows .  	   HorizontalGridLinesBrush
	 ��·�
   RowBackground

 �����
�������   Columns 56      4  .System.Windows.Controls.DataGridComboBoxColumn  57      -   5        
cmbCarReg1��,  &System.Windows.Controls.DataGridColumn   Header$ Car Reg��57   =      Width5  /System.Windows.Controls.DataGridLengthConverter$
 .35* 6N      SelectedValuePath$ ID��6�      DisplayMemberPath$ VehicleRegistration��6�      SelectedValueBinding 6[   �� *CarID+ �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�6�     58      -   5        cmbInstructor1��$ 
Instructor��58   A   $
 .45* 6U   $ ID��6�   $ FullName��6�    6b   �� *InstructorID+ . $ PropertyChanged=�6�     59      -   5        cmbType1��$ Maintenance Type��59   ;   $
 .35* 6U   $ ID��6�   $
 Name��6�    6b   �� *TypeID+ . $ PropertyChanged=�6�   0  *System.Windows.Controls.DataGridTextColumn  5:      $ Description��6%   $ * 6:   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *Description+ . $ PropertyChanged=�4  .System.Windows.Controls.DataGridTemplateColumn  5;      $
 Date��6*   $
 .35* 68      CellTemplate 5<      �� 5=      .R�5>           SelectedDate 6)   �� *Date+ . $ PropertyChanged=�5?      5A        5B      $ Amount Paid��6%   $
 .35* 6:    6G   �� *
AmountPaid+ . $ PropertyChanged=�  5C      $ Invoice Number��6%   $	 .5* 6=    6I   �� *InvoiceNumber+ . $ PropertyChanged=�  5D      $ Notes��6%   $ * 64    6>   �� *Notes+ . $ PropertyChanged=�5G      5I      5J      