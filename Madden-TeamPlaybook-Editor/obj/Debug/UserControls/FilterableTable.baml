   M S B A M L   `   `   `  ����  Madden-Playbook-Editor<    6MaddenTeamPlaybookEditor.User_Controls.FilterableTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5          >�Name$  uclFilterableTable��5      .��5      �� .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��5q�6.   .+�5	      �� -   5         ��Name$ 	cbxFilter��5	      $	��5,0q�6*    �� SelectedIndex$ 0��67   $	��100��6I   6X   �� 5
      -   5         ��Name$ tbxValue��5
      $	��5,0q�6(   $	��200��65   6A   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5      $��5q�5      $	��835��5      
���   �   HorizontalGridLinesBrush �	#FFD6D6D6   VerticalGridLinesBrush
 �   �   GridLinesVisibility$ 
Horizontal=�5      	   CanUserAddRows	 . 
   CanUserDeleteRows
 . !   EnableColumnVirtualization .    EnableRowVirtualization . ��5      Z� ��5      Z� ��5      ��  �� ElementName$ uclFilterableTable�� �� Path$
 Data� �� Mode$ TwoWay=�5      5      5      