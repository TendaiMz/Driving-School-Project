   M S B A M L   `   `   `  ����  PointOfSale#    PointOfSale.InstructorDetails   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5	      �   � RowDefinitions  5   
   �� 5      $
��300* �6   �� 5      $
��200* �6   .+�5   
   � $��10q�6   $��0��5      
������� � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��1.5* �6#   �� 5      $��* �6#   �� 5      $
��1.5* �6#   �� 5      $��* �6#   �� 5      $
��1.5* �6#     5      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5       $��* �6    �� 5!      $��0* �6    .+�5#      �� $��0��6   $��0��5$      $��6��5%      $��Center=�5&      $��Center=�5'      $��Instructor Details��5(      $��18(�5)      $
��Bold"�5*      �� 5+      $��0��6   $��1��5,      $��Center=�5-      $
��Name��5.      �� 5/      $��0��6   $��2��50      $��Center=�51      $��Surname��52      �� 53      $��4��6   $��1��54      $��Center=�55      $��	ID Number��56      �� 57      $��4��6   $��2��58      $��Center=�59      $��License Number��5:      �� 5;      $��4��6   $��3��5<      $��Center=�5=      $��Defensive Expiry��5>      �� 5?      $��2��6   $��1��5@      $��Center=�5A      $��Phone Number��5B      �� 5C      $��2��6   $��2��5D      $��Center=�5E      $��Vehicle��5F      �� 5G      $��2��6   $��3��5H      $��Center=�5I      $��	Defensive��5J      �� 5K      $��0��6   $��4��5L      $��Center=�5M      $��Address��5N      �� 5O      $��0��6   $��3��5P      $��Center=�5Q      $��Email��5R      �� 5T      -   5        $��txtName��5T      $��1��5U      $��1��5V      $��25��5W      $��10q�5X      �� 5Y      -   5        $��
txtSurname��5Y      $��1��5Z      $��2��5[      $��25��5\      $��10q�5]      �� 5^      -   5        $��txtEmail��5^      $��1��5_      $��3��5`      $��25��5a      $��10q�5b      �� 5c      -   5        $��
txtAddress��5c      $��1��5d      $��4��5e      $��2��5f      $��75��5g      $��10q�5h      �� 5i      -   5        $��
txtLicence��5i      $��5��5j      $��2��5k      $��10q�5l      $��25��5m      �� 5n      -   5        $��	txtPhone1��5n      $��3��5o      $��1��5p      $��10q�5q      $��25��5r      �� 5s      -   5        $��
cmbVehicle��5s       �� SelectedValuePath$ ID��5t       �� DisplayMemberPath$ VehicleRegistration��5u      $��3��5v      $��2��5w      $��10q�5x      $��25��5y      �� 5z      -   5        $��txtDefensive��5z      $��3��5{      $��3��5|      $��10q�5}      $��25��5~      �� 5      -	   5        $��txtIDNumber��5      $��5��5�      $��1��5�      $��10q�5�      $��25��5�      (  "System.Windows.Controls.DatePicker  5�      -
   5        $��dtpDefensiveExp��5�      $��5��5�      $��3��5�      $��10q�5�      $��25��5�      �� 5�      -   5        $��btnSave��5�      $��5��5�      $��5��5�      $
��Save��5�      $��Right=�5�      $
��10,5q�5�      $��85��5�      $��35��5�      5�      5�      &   System.Windows.Controls.DataGrid  5�   
   -   5        $��dtgInstructors��5�      $��1��5�         AutoGenerateColumns .  $��0q�5�         GridLinesVisibility$ 
Horizontal=�5�         
IsReadOnly .     CanUserAddRows .     HorizontalGridLinesBrush
 ��·�	   RowBackground
	 �����
�������
   Columns
 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5�   %   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5�   %   �� *Name+ �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�  5�      $ Surname��6%   $ * 5�   %    5�   %   �� *	Surname+ . $ PropertyChanged=�  5�      $ Identity Number��6%   $ * 5�   %    5�   %   �� *
IDNumber+ . $ PropertyChanged=�  5�      $ Email��6%   $
 .75* 5�   %    5�   %   �� *Email+ . $ PropertyChanged=�4  .System.Windows.Controls.DataGridComboBoxColumn  5�      -   5        cmbVehicle1��$ Vehicle��5�   %   $ * 5�   %      SelectedValuePath$ ID��5�   %      DisplayMemberPath$ VehicleRegistration��5�   %      SelectedValueBinding 5�   %   �� *ID+5�   %     5�      $ Contact Number2��6%   $
 .75* 5�   %    5�   %   �� *ContactNumber+ . $ PropertyChanged=�  5�      $ Licence��6%   $
 .75* 5�   %    5�   %   �� *LicenceNumber+ . $ PropertyChanged=�  5�      $ Defensive Number��6%   $
 .75* 5�   %    5�   %   �� *DefensiveNo+ . $ PropertyChanged=�  5�      $ Defensive Expiry��6%   $
 .75* 5�   %    5�   %   �� *DefensiveExpiry+ �� StringFormat$ {0:d}�� . $ PropertyChanged=�  5�      $ Address��6%   $ * 5�   %    5�   %   �� *	Address+ . $ PropertyChanged=�4  .System.Windows.Controls.DataGridCheckBoxColumn  5�      $ Resign��6)   $	 .5* 5�   )    5�   )   �� *
Resigned+ . $ PropertyChanged=�5�      5�      5�      