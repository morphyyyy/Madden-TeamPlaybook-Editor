   M S B A M L   `   `   `  ����Q  MMadden-Playbook-Editor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:MaddenTeamPlaybookEditor.User_Controls&MaddenTeamPlaybookEditor.User_Controls  5      1    +MaddenTeamPlaybookEditor.User_Controls.Play   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      MUserControls4clr-namespace:MaddenTeamPlaybookEditor.User_Controls     5      H DMathConverter, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null(mathhttp://hexinnovation.com/math  5      -   5          >�Name$  uclPlay��5       J� 	AllowDrop .  � 	Resources 5      !  HexInnovation.MathConverter   math  MenuItemPanelTemplate �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius J� SnapsToDevicePixels��%�  &	        &	      'm� P     7  '�� �     7    5   
   6   �� 5   
   .R�5      �� $��	-20,0,0,0q�6   5      �� 5   
   " m�6   .�5      ��  � ��8   6+   5      �� 5   
   " ��6   .�5      ��  � ��8   6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ 10��6!   $��0q�63   
���/kU� . ._�5      �� 5      5      5!      5"      5#      .��5%      ]� -   5        	 ]�Name$	 dckPlay��5%      $��Center=�5&   	   {�5'   	   �� *
IsSelected+
 � ContextMenu
 5)   
   �� 5*      .@�5+      �� 
������ 
��������5.      �� *PBPL+�� 50      
������ 
��������53      �� *PLYL+�� 55      
������ 
��������58      �� *PLPD+�� 5:      
������ 
��������5=      �� *PLRD+�� 5?      
������ 
��������5B      �� *
Situations+�� 5D      
������ 
��������5G      �� *PBCC+�� 5I      
������ 
��������5L      �� *PLCM+�� 5N      
������ 
��������5Q      �� *PPCT+�� 5S      
������ 
��������5V      �� *SDEF+�� 5X      
������ 
��������5[      �� *SRFT+�� 5]      
������ 
��������5`      �� *PLYS+5b      .+�5e   
   �� -   5         ��Name$ cvsPlay��5e      $	��Top=�5f      $	��180��5g      $	��120��5h      
 5k      �� 5l      .@�5m      �� -   5        
������ 
������$��Save Artview��5p      5q      5s      .+�5v      �� -   5         ��Name$ iclPlayarts��5w      ��5x      �� *PlayerPlayartView+ �� Mode$ TwoWay=���5y      �� 5z      .R�5{      �� $ 
cvsPlayers��5|      $	��180��5}      $	��120��5~      5�      ��5�      �� 5�      .R�5�      4   .MaddenTeamPlaybookEditor.User_Controls.Playart    Name$ clnARTL��5�         PSALView .     Scale$ 1Q�5�         AbsolutePositioning .    Player 5�      �� 5�      5�      ��5�      �� 5�      .�5�      ��  �@��Left 5�      �� *	SETP.artx+$ TwoWay=��� 5�       �@��Top 5�      �� *	SETP.arty+$ TwoWay=�5�      5�      �� 5�      -   5        $ iclIcons��5�      ��5�      �� *PlayerPlayartView+$ TwoWay=���5�      �� 5�      .R�5�      �� $ 
cvsPlayers��5�      $	��180��5�      $	��120��5�      5�      ��5�      �� 5�      .R�5�      7   1MaddenTeamPlaybookEditor.User_Controls.PlayerIcon    Name$ clnIcon��5�         Scale$ 1Q�5�         ShowPosition .     AbsolutePositioning .    Player 5�      �� 5�      5�      ��5�      �� 5�      .�5�      ��  �@��Left 5�      �� *	SETP.artx+$ TwoWay=��� 5�       �@��Top 5�      �� *	SETP.arty+$ TwoWay=�5�      5�      5�      5�      5�      