   M S B A M L   `   `   `  ����>  :DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null&clr-namespace:DAL;assembly=DALDAL  5   �    PointOfSale   PointOfSale.test   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F   .DALclr-namespace:DAL;assembly=DAL     6�   -   5          � Title$
  test��5   	   $	��500��6   $	��700��6#    � 	Resources 5         customerViewSource  FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background 	 Button.Disabled.Border 
 Button.Disabled.Foreground �� 
TargetType �� 
TargetType	 ��Name
 �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName  DataTemplate1 � RowDefinitions � ColumnDefinitions��%	  &	        &	      &	 �     &	 �     &	     &	 &    &	 I    &	 l    &	 �    &		 �    &	
 �    '�� �    7  7  7  7  7  7  7  7  7 	 7 
 &	 �    �� 5   
   6   �� 5   
   .�5	      ��  � �� 5
      �� 5      .R�5      �� $��2q�6$    . #��� ��6J   $��1��6�   $	 1 2R�6�   5      5      5      �� 5   
   $ 	#FFDDDDDD��6;   �� 5   
   $ 	#FF707070��67   �� 5   
   $ 	#FFBEE6FD��6>   �� 5   
   $ 	#FF3C7FB1��6:   �� 5   
   $ 	#FFC4E5F6��6<   �� 5   
   $ 	#FF2C628B��68   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   " ��6   .�5      ��  �@�FocusVisualStyle8   61   �� 5       � ��8  6+   �� 5       � ��8  6,   �� 5       � ��# � ��6+   �� 5       � ��$ 1q�60   �� 5        � ��$ Center=�6;   �� 5!       � ��$ Center=�69   �� 5"       � ��$ 1q�6(   �� 5#       � �� 5$      �� 5%      " ��6&   .R�5&      �� $	 border��6!   #��z��61   #��z��6]   #��z��6�    . $
 5��6�   ._�5'      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5(       5)      P� 5*       �@��IsDefaulted . . �5+   "   �� $ border��6@   # � ��6T    � ��5,      P� 5-       �@J�IsMouseOver . . �5.   "   �� $ border��6?   8   � ��6S   �� 5/   "   $ border��6@   8   � ��6T   50      P� 51       � �� . . �52   "   �� $ border��6?   8   � ��6S   �� 53   "   $ border��6@   8   � ��6T   54      P� 55       � {� .  . �56   "   �� $ border��6?   8   � ��6S   �� 57   "   $ border��6@   8   � ��6T   �� 58   "   $ contentPresenter��6K   8 	  � ��6i   59      5;      5=      5>      �� 5?   
   .R�5@      � $	��312��6   $	��530��6     5A      �� 5B      $	��51* �6$   �� 5C      $	��53* �6$    5E      �� 5F      $	��75* �6'   �� 5G      $	��76* �6'   �� 5H      $
��114* �6'   .+�5J      �� $
��Wrap=�6   �� 61   
�������
���  ��$��12.6666666666667(�6Z   $��Consolas)�6v   .�6�   �� $�FullName��6�   6�   6�   5K      5L      5M      .��5N      �  � DataContext# [  6    5O   
   �� 5P      $
��152* �6   �� 5Q      $	��82* �6   �� 5R      $	��84* �6   �� 5S      $
��151* �6    5U   
   �� 5V      $	��89* �6   �� 5W      $
��257* �6   .+�5Z   
   &   System.Windows.Controls.DataGrid  -   5          Name$ customerDataGrid��5Z         RowDetailsVisibilityMode$ VisibleWhenSelected=�6-      EnableRowVirtualization .    AutoGenerateColumns .  $��2��6�   ��6\   ��    Columns 5[      0  *System.Windows.Controls.DataGridTextColumn  5\      -   5        addressColumn��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ SizeToHeader 5\   <      Header$ Address��6Q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6b   �� *	Address+  5]      -   5        customerNumberColumn��$ SizeToHeader 5]   C   $ Customer Number��6X    6q   �� *CustomerNumber+4  .System.Windows.Controls.DataGridTemplateColumn  5^      -   5        dateOfBirthColumn��$ SizeToHeader 5^   D   $ Date Of Birth��6Y      CellTemplate 5_      �� 5`      .R�5a      (  "System.Windows.Controls.DatePicker     SelectedDate 6)   �� *DateOfBirth+ �� Mode$ TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions . 5b      5d        5e      -   5        expiryDateColumn��$ SizeToHeader 5e   C   $ Expiry Date��6X    5f      �� 5g      .R�5h         6)   �� *
ExpiryDate+$ TwoWay=� .  . 5i      5k        5l      -   5        firstNameColumn��$ SizeToHeader 5l   >   $ 
First Name��6S    6g   �� *	FirstName+  5m      -   5        fullNameColumn��$ SizeToHeader 5m   =       
IsReadOnly  . $ 	Full Name��6d    6w   �� *
FullName+  5n      -	   5        iDColumn��$ SizeToHeader 5n   7   $ ID��6L    6X   �� *ID+  5o      -
   5        identityNumberColumn��$ SizeToHeader 5o   C   $ Identity Number��6X    6q   �� *IdentityNumber+  5p      -   5        initialsColumn��$ SizeToHeader 5p   =   $ Initials��6R    6d   �� *
Initials+  5q      -   5        nextOfKinColumn��$ SizeToHeader 5q   >   $ Next Of Kin��6S    6h   �� *	NextOfKin+  5r      -   5        nextOfKinPhoneNumberColumn��$ SizeToHeader 5r   I   $ Next Of Kin Phone Number��6^    6�   �� *NextOfKinPhoneNumber+  5s      -   5        pDLNumberColumn��$ SizeToHeader 5s   >   $ 	PDLNumber��6S    6f   �� *	PDLNumber+  5t      -   5        phoneNumber1Column��$ SizeToHeader 5t   A   $ Phone Number 1��6V    6n   �� *PhoneNumber1+  5u      -   5        phoneNumber2Column��$ SizeToHeader 5u   A   $ Phone Number 2��6V    6n   �� *PhoneNumber2+  5v      -   5        surnameColumn��$ SizeToHeader 5v   <   $ Surname��6Q    6b   �� *	Surname+  5w      -   5        validColumn��$ SizeToHeader 5w   >   $ Valid��6S    5x      �� 5y      .R�5z         6)   �� *Valid+$ TwoWay=� .  . 5{      5}      5      � 5�   
   -   5        ! �Name$! grid1��5�      $
��Left=�6   $��26,5,0,0q�69   $��1��6K   $	��Top=�6X    5�      �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#    5�      �� 5�      $
��Auto �6    .+�5�      �� -   5        " ��Name$" addressLabel��5�      $��1��6F   $
��Left=�6V   $��3q�6q   $��0��6|   $��Center=�6�   ��6*   �� *	Address+6�   5�      � 5�   
   -   5        $! grid2��5�      $
��Left=�6   $��	43,18,0,0q�69   $��1��6L   $	��Top=�6Y   $��1��6q    5�      �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#    5�      �� 5�      $
��Auto �6    5�      � 5�   
   -   5        $! grid3��5�      $
��Left=�6   $��
127,34,0,0q�69   $��1��6M   $	��Top=�6Z    5�      �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#    5�      �� 5�      $
��Auto �6    .+�5�      �� $��Valid:��6   $��0��6%   $
��Left=�65   $��3q�6P   $��0��6[   $��Center=�6h   5�      � 5�   
   -   5        $! grid4��5�      $
��Left=�6   $��
160,27,0,0q�69   $��1��6M   $	��Top=�6Z   $��1��6r    5�      �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#    5�      �� 5�      $
��Auto �6    .+�5�      �� $��Customer Number:��6   $��0��6/   $
��Left=�6?   $��3q�6Z   $��0��6e   $��Center=�6r   �� 5�      -   5        # ��Name$# customerNumberTextBox��5�      $��1��65   $
��Left=�6E   $��23��6`   $��3q�6l   $��0��6w   $��Center=�6�   $	��120��6  ��6�   �� *CustomerNumber+$ TwoWay=� .  . 6  5�      �� 5�   
   $��Button��6   $
��Left=�6"   $��	43,27,0,0q�6=   $��2��6P   $	��Top=�6]   $��75��6u   $��1��6�   �� 5�   
   -   5        $��lstBx��5�      $��3��6   #���  6,   5�      5�      