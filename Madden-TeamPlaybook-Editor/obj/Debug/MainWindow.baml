   M S B A M L   `   `   `  ����Q  MMadden-Playbook-Editor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:MaddenTeamPlaybookEditor.User_Controls&MaddenTeamPlaybookEditor.User_Controls  5      Y1clr-namespace:MaddenTeamPlaybookEditor.ViewModels#MaddenTeamPlaybookEditor.ViewModels  5      ]3clr-namespace:MaddenCustomPlaybookEditor.ViewModels%MaddenCustomPlaybookEditor.ViewModels  5      C&clr-namespace:MaddenTeamPlaybookEditorMaddenTeamPlaybookEditor  5      S.clr-namespace:MaddenTeamPlaybookEditor.Classes MaddenTeamPlaybookEditor.Classes  5
      )    #MaddenTeamPlaybookEditor.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      MUserControls4clr-namespace:MaddenTeamPlaybookEditor.User_Controls     5      TTeamPlaybookViewModels1clr-namespace:MaddenTeamPlaybookEditor.ViewModels     5      XCustomPlaybookViewModels3clr-namespace:MaddenCustomPlaybookEditor.ViewModels     5      8local&clr-namespace:MaddenTeamPlaybookEditor     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5	      Bclasses.clr-namespace:MaddenTeamPlaybookEditor.Classes     5
      -   5          �Name$  wdwPlaybookEditor��5       � Title$! Madden Team Playbook Editor��5       � WindowStartupLocation$ CenterScreen=�5       � Icon$$ /Resources/Playbook_Editor.ico��5       � UseLayoutRounding .  � WindowStyle$ ThreeDBorderWindow=�5      $��Visible=�5       � WindowState$ Normal=�5       � SizeToContent$ WidthAndHeight=�5      ��5      �� 5   
    �� ImageSource$ Resources/chalkboard.jpg��5      	 j� Viewport$	 0,0,1920,1067�5      
 j� ViewportUnits$
 Absolute=�5       j� TileMode$
 Tile=�5       j� Stretch$ UniformToFill=�5       j� 
AlignmentX$
 Left=�5       j� 
AlignmentY$	 Top=�5        J� CommandBindings 5$      )  #System.Windows.Input.CommandBinding  5%   
   -   5           Command$ ApplicationCommands.Paste��5%      6=    � 	Resources 5(      &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value��%�  '        '�� �    �� 5)   
   "  6   .�5*      ��  � ��
 ���� 6+   �� 5+       � ��
 ���� 6,   �� 5,       � ��
 �   �6+   �� 5-        �@ HorizontalGridLinesBrush �	#FFD6D6D669   �� 5.       �@ VerticalGridLinesBrush
 �   �67   �� 5/       �@ GridLinesVisibility$ 
Horizontal=�64   �� 50       �@ CanUserAddRows .  6/   �� 51       �@ CanUserDeleteRows .  62   52      �� 53   
   " ��6   .�54      ��  � ��
 ���� 6+   �� 55       � ��
 ���� 6,   �� 56       � ��
 �   �6+   �� 57       � ��$ 5q�6'   58      59      .��5;      ]� $
��Auto��6   $
��Auto��6   $��Stretch=�6+   .+�5=   
   o� $	��Top=�6    o� 
IsMainMenu . .@�5>      m� $��_File��6   .@�5?      m� -   5        $
��Open��5?      6)   �� 5@      m� 5A      -   5        $��Load Roster��5A      60   �� 5B      m� 5C      -   5        $
��Save��5C      6)   m� 5D      -   5        $��Save All Playart��5D      65   m� 5E      -   5        $��Save All ARTL��5E      62   5F      m� 5G      $��Options��6   .@�5H      m� $
��Copy��6    m� Command$ ApplicationCommands.Copy��6)   m� 5I      $��Paste��6   $ ApplicationCommands.Paste��6*   �� 5J      m� 5K      $��Delete��6   $  ApplicationCommands.Delete��6+   5L      m� 5M      $��	Functions��6   .@�5N      m� -   5        $��Sabo's Gameplan Revamp��5N      6;   m� 5O      -	   5        $��Set Generic Audibles��5O      69   m� 5P      $��Delete��6   .@�5Q      m� -
   5        $��Remove All Formations��5Q      6>   m� 5R      -   5        $��Remove All Sub-Formations��5R      6B   m� 5S      -   5        $��Remove All Plays��5S      69   5T      m� 5U      -   5        $��PSAL Editor��5U      60   5V      m� 5W      $
��Help��6   .@�5X      �� -   5        $j��dhttps://docs.google.com/spreadsheets/d/1yGtMkVxY1HZ-Pi_ZuffaI1TUtA6tpNpQPQUELa-INaI/edit?usp=sharing?�5X      .�6�   Table Definitions5Z      5[      5\      9� 5^   
   -   5         9�Name$ xpdTeamPlaybook��5^      
�������$��Team Playbook��5`       9� ExpandDirection$
 Down=�5a       9� 
IsExpanded . .��5�      �� .@�5�      �� $��Playbook��6   .��5�      R� -   5         R�Name$ tvwPlaybook��5�      $��5q�5�      $	��Top=�5�       J� 	AllowDrop . $	��600��5�      $��Hidden=�5�      $��Hidden=�5�      ��5�      Z� ��5�      Z� ��5�      Z� ��5�      �� *
Formations+ 5�        5�      -   5        $ ApplicationCommands.Copy��5�   -   6P     5�      -   5        $ ApplicationCommands.Paste��5�   -   6Q     5�      -   5        $  ApplicationCommands.Delete��5�   -   6R    5�       �� DataType5   /MaddenTeamPlaybookEditor.ViewModels.FormationVM �� ItemsSource �� ItemContainerStyle   SubFormationTreeViewItem �� BasedOn  �� Mode! �� Triggers" �� Binding# �� Value6   0MaddenTeamPlaybookEditor.User_Controls.Formation$  Name%   	formation8   2MaddenTeamPlaybookEditor.ViewModels.SubFormationVM  PlayTreeViewItem9   3MaddenTeamPlaybookEditor.User_Controls.SubFormation&  Name'   showPackage(   subFormation0   *MaddenTeamPlaybookEditor.ViewModels.PlayVM1   +MaddenTeamPlaybookEditor.User_Controls.Play)  Name*   showAudibles+   play, ��Name- �� Orientation. d�Name/ � ToolTip0 d� 	IsChecked  AudibleQuick  
AudibleRun  AudibleDeep  AudibleFake1 �� TextAlignment�� 5�      %K	  (��       5�   ;   *, +)7   (�� :    5�   ;   *, +)7  (�� o    5�   ;   *, +)7  7  7  7  �� 5�   "   "  6;    6r   �� *SubFormations+ 5�   &   �� 5�   *   " Q�60   8   .�5�   .   ��  �@Q�
IsExpanded 6K   �� *
IsExpanded+$  TwoWay=��� 5�   .    �@Q�
IsSelected 6K   �� *
IsSelected+$  TwoWay=�! 5�   .   �� 5�   2   " 6>   �� *	IsVisible+# True6\   .a�5�   6   ��  � y�$ Visible=�6S   5�   3   �� 5�   2   " 6>   �� *	IsVisible+	# False6\   .a�5�   6   ��  � y�$ 	Collapsed=�6S   5�   3   5�   +   .R�5�   &     $$ uclFormation��6=   {�. % 6S   �� 6i   5�   #   �� 5�   "   "  6;    6u   �� *Plays+ 5�   &   �� 5�   *   " Q�60   8   .�5�   .   ��  �@Q�
IsExpanded 6K   �� *
IsExpanded+$  TwoWay=��� 5�   .    �@Q�
IsSelected 6K   �� *
IsSelected+$  TwoWay=�! 5�   .   �� 5�   2   " 6>   �� *	IsVisible+# True6\   .a�5�   6   ��  � y�$ Visible=�6S   5�   3   �� 5�   2   " 6>   �� *	IsVisible+	# False6\   .a�5�   6   ��  � y�$ 	Collapsed=�6S   5�   3   5�   +   .R�5�   &     $& uclSubFormation��6@   ' . ( 6Y   �� 6r   5�   #   �� 5�   "   "  6;   .R�5�   &   ]� .+�5�   *     $) uclPlay��6<   * . $	��Top=�5�   <   + 5�   <   �� 5�   <   �� 5�   *   $, pnlAudibles��65   $- 
Horizontal=�5�   5   $��Center=�5�   5   $��Bottom=�5�   5   $	��Top=�5�   5   .+�5�   .   d� $. AudibleQuick��6;   $/ Short Pass (X)��6Q   $��18��6j   0 6v   �� *isShortAudible+��5�   2   �� 5�   6   " d�6<   8   6_   5�   /   d� 5�   .   $. 
AudibleRun��6;   $/ Run (Y/Triangle)��6O   $��18��6j   0 6v   �� *isRunAudible+��5�   2   �� 5�   6   " d�6<   8  6_   5�   /   d� 5�   .   $. AudibleDeep��6;   $/ Deep Pass (LB/L1)��6P   $��18��6l   0 6x   �� *isDeepAudible+��5�   2   �� 5�   6   " d�6<   8  6_   5�   /   d� 5�   .   $. AudibleFake��6;   $/ Play Action (RB/R1)��6P   $��18��6n   0 6z   �� *isFakeAudible+��5�   2   �� 5�   6   " d�6<   8  6_   5�   /   5�   +   �� 5   *   $	��Top=�5  -   $��DK Crayon Crumble)�5  -   $��15(�5  -   $
��Wrap=�5  -   $1 Center=�5  -   
������ 
���������5  -   �� *	PBPL.name+5  -   5  '   5  #   5     ��5     �� 5     " Q�6$     FormationTreeViewItem# [ 6G   .�5  "   -   5        �� 5  "    �@Q�
IsExpanded 6?   �� *
IsExpanded+$  TwoWay=��� 5  "    �@Q�
IsSelected 6?   �� *
IsSelected+$  TwoWay=�! 5   "   �� 5!  &   " 62   �� *	IsVisible+# True6P   .a�5"  *   ��  � y�$ Visible=�6G   5#  '   �� 5$  &   " 62   �� *	IsVisible+	# False6P   .a�5%  *   ��  � y�$ 	Collapsed=�6G   5&  '   5)     5,     5.     �� 50     $��Gameplan��6   .��52     ]� $	��Top=�6    $	��5,1q�68   $	��600��6E   .+�54     ]� $	��Top=�6$   $	��5,1q�69   .+�56     �� -   5        2 ��Name$2 cbxPLYT��57  !   3 �� SelectedValuePath$	3 Key��59  !   4 �� DisplayMemberPath$4 Value��5:  !   $	��5,1q�5;  !   
������ $��0q�5=  !   ��.  $��Hidden=�5@  !   $��Hidden=�5A  !   5 �� 
IsEditable5 . 6 �� Text$6 Select Plays by Type��5C  !   ��58  !   �� 5C  !   �� 5E     -   5        $2 lvwSituations��5F  !   $	��5,1q�5H  !   ��.  $��Hidden=�5J  !   $��Hidden=�5K  !   
������ ��5G  !   �� ��5O  "   �� 5P  &   .R�5Q  *   6	   0MaddenTeamPlaybookEditor.User_Controls.Situation	  7 	  	situation7 6A   �� 5R  '   5T     5V     R� 5X     -   5        $ lvwPlaysByRouteDepth��5Y     
������ 
������ $��Hidden=�5]     $	��5,1q�5^     $	��600��5_     $	��875��5`     $	��Top=�5a     ��5Z     �� ��5b     �� 5c  "   .R�5d  &   ]� .+�5f  *   �� $	��Top=�5g  -   $��DK Crayon Crumble)�5h  -   $��14(�5i  -   $
��Wrap=�5j  -   $1 Center=�5k  -   
������ 
���������5n  .   `� 5o  2   8 �� StringFormat$8 {0}
{1}
{2}��6?   .1�5p  6   �� 9 �� Path$&9  SubFormation.Formation.PBFM.name�6>   �� 5q  6   $9 SubFormation.PBST.name�6>   �� 5r  6   $9 	PBPL.name�6>   5s  3   5u  +     5w  *   $	��Top=�6M   + 6<   �� 6M   5y  '   5z  #   ��5|     �� 5}  "   .R�5~  &   >
   8MaddenTeamPlaybookEditor.Classes.UniformGridOrientations
  $	��Top=�6F   : 
  Orientation$: 
Horizontal=�6^   ; 
  VerticalOrientation$	; Top=�6w   $��LeftToRight=�6�   < K� IsItemsHost< . = A� Columns$= 3��6�   5  #   ��5�     �� 5�  "   " Q�6(   # [ 6K   .�5�  &   ��  �@Q�
IsExpanded 6C   �� *
IsExpanded+$  TwoWay=��� 5�  &    �@Q�
IsSelected 6C   �� *
IsSelected+$  TwoWay=�5�  #   5�     5�     5�     �� 5�     $��Tables��6   .��5�     �� -   5        > ��Name$> 	tclTables��5�     $	��600��5�     .@�5�     �� $
��PBFM��6"   .��5�     <   6MaddenTeamPlaybookEditor.User_Controls.FilterableTable  ?   Data? 6;   �� *PBFM+$  TwoWay=�5�     �� 5�     $
��FORM��6"   .��5�       ? 6;   �� *FORM+$  TwoWay=�5�     �� 5�     $
��PBST��6"   .��5�       ? 6;   �� *PBST+$  TwoWay=�5�     �� 5�     $
��SETL��6"   .��5�       ? 6;   �� *SETL+$  TwoWay=�5�     �� 5�     $
��SETP��6"   .��5�       ? 6;   �� *SETP+$  TwoWay=�5�     �� 5�     $
��SPKF��6"   .��5�       ? 6;   �� *SPKF+$  TwoWay=�5�     �� 5�     $
��SPKG��6"   .��5�       ? 6;   �� *SPKG+$  TwoWay=�5�     �� 5�     $
��SGFM��6"   .��5�       ? 6;   �� *SGFM+$  TwoWay=�5�     �� 5�     $
��SETG��6"   .��5�       ? 6;   �� *SETG+$  TwoWay=�5�     �� 5�     $
��PBPL��6"   .��5�       ? 6;   �� *PBPL+$  TwoWay=�5�     �� 5�     $
��PLYL��6"   .��5�       ? 6;   �� *PLYL+$  TwoWay=�5�     �� 5�     $
��PLPD��6"   .��5�       ? 6;   �� *PLPD+$  TwoWay=�5�     �� 5�     $
��PLRD��6"   .��5�       ? 6;   �� *PLRD+$  TwoWay=�5�     �� 5�     $
��PLYS��6"   .��5�       ? 6;   �� *PLYS+$  TwoWay=�5�     �� 5�     $
��PSAL��6"   .��5�       -   5        @  Name$@ uclPSALTable��5�  ;   ? 6Q   �� *PSAL+$  TwoWay=�5�     �� 5�     $
��ARTL��6"   .��5�       ? 6;   �� *ARTL+$  TwoWay=�5�     �� 5     $
��PBAI��6"   .��5       -   5        $@ uclPBAITable��5  ;   ? 6Q   �� *PBAI+$  TwoWay=�5     �� 5     $
��PBAU��6"   .��5       ? 6;   �� *PBAU+$  TwoWay=�5     �� 5	     $
��PBCC��6"   .��5
       ? 6;   �� *PBCC+$  TwoWay=�5     �� 5     $
��PLCM��6"   .��5       ? 6;   �� *PLCM+$  TwoWay=�5     �� 5     $
��PPCT��6"   .��5       ? 6;   �� *PPCT+$  TwoWay=�5     �� 5     $
��SDEF��6"   .��5       ? 6;   �� *SDEF+$  TwoWay=�5     �� 5     $
��SRFT��6"   .��5       ? 6;   �� *SRFT+$  TwoWay=�5     5     5     5      5"     9� 5$  
   -   5        $ xpdCustomPlaybook��5$     
�������$��Custom Playbook��5&     $
 Down=�5'      . $y�	Collapsed=�5)     .��5+     �� -   5        $> tclCustomPlaybookTables��5,     $	��Top=�5-     .@�5/     �� $
��ARTO��6   .��50       ? 63   �� *ARTO+$  TwoWay=�51     �� 53     $
��ARTD��6   .��54       ? 63   �� *ARTD+$  TwoWay=�55     �� 57     $
��CPFM��6   .��58       ? 63   �� *CPFM+$  TwoWay=�59     �� 5;     $
��PBFI��6   .��5<       ? 63   �� *PBFI+$  TwoWay=�5=     �� 5?     $
��PBFM��6   .��5@       ? 63   �� *PBFM+$  TwoWay=�5A     �� 5C     $
��PBPL��6   .��5D       ? 63   �� *PBPL+$  TwoWay=�5E     �� 5G     $
��PBPN��6   .��5H       ? 63   �� *PBPN+$  TwoWay=�5I     �� 5K     $
��PIDX��6   .��5L       ? 63   �� *PIDX+$  TwoWay=�5M     �� 5O     $
��PLCM��6   .��5P       ? 63   �� *PLCM+$  TwoWay=�5Q     �� 5S     $
��PLPD��6   .��5T       ? 63   �� *PLPD+$  TwoWay=�5U     �� 5W     $
��PLRD��6   .��5X       ? 63   �� *PLRD+$  TwoWay=�5Y     �� 5[     $
��PLYS��6   .��5\       ? 63   �� *PLYS+$  TwoWay=�5]     �� 5_     $
��PPCT��6   .��5`       ? 63   �� *PPCT+$  TwoWay=�5a     �� 5c     $
��PSLO��6   .��5d       ? 63   �� *PSLO+$  TwoWay=�5e     �� 5g     $
��PSLD��6   .��5h       ? 63   �� *PSLD+$  TwoWay=�5i     �� 5k     $
��SDEF��6   .��5l       ? 63   �� *SDEF+$  TwoWay=�5m     �� 5o     $
��SETG��6   .��5p       ? 63   �� *SETG+$  TwoWay=�5q     �� 5s     $
��SETL��6   .��5t       ? 63   �� *SETL+$  TwoWay=�5u     �� 5w     $
��SETP��6   .��5x       ? 63   �� *SETP+$  TwoWay=�5y     �� 5{     $
��SGFF��6   .��5|       ? 63   �� *SGFF+$  TwoWay=�5}     �� 5     $
��SPKF��6   .��5�       ? 63   �� *SPKF+$  TwoWay=�5�     �� 5�     $
��SPKG��6   .��5�       ? 63   �� *SPKG+$  TwoWay=�5�     �� 5�     $
��SRFT��6   .��5�       ? 63   �� *SRFT+$  TwoWay=�5�     �� 5�     $
��STID��6   .��5�       ? 63   �� *STID+$  TwoWay=�5�     5�     5�     9� 5�  
   -   5        $ xpdSubFormationModal��5�     $y�Visible=�5�     
�������$��Sub Formation��5�     $
 Down=�5�      . .��5�     >   8MaddenTeamPlaybookEditor.User_Controls.SubFormationModal  -   5        A  Name$A uclSubFormationModal��5�  -   $	��Top=�6K   5�     9� 5�  
   -   5        $ xpdPlayModal��5�     $y�	Collapsed=�5�     
�������$
��Play��5�     $
 Down=�5�      . .��5�     �� $
��Auto��6   $
��Auto��6&   $��Stretch=�64   $- 
Horizontal=�6R   .+�5�     6   0MaddenTeamPlaybookEditor.User_Controls.PlayModal  -    5        B  Name$B uclPlayModal��5�  )   $	��Top=�6?   R� 5�     -!   5        $ tvwPSALs��5�     $	��Top=�5�      . $	��600��5�     $��Hidden=�5�     $��Hidden=�5�     ��5�     Z� ��5�     Z� ��5�     Z� ��5�     ��  5�     �� 5�     %�  (��       5�  7   *, +)7   (�� .    5�  7   *, +)7  (�� W    5�  7   *, +)�� 5�     "  67    6n   �� *SubFormations+ 5�  "   �� 5�  &   " Q�6,   8   .�5�  *   ��  �@Q�
IsExpanded 6G   �� *
IsExpanded+$  TwoWay=��� 5�  *    �@Q�
IsSelected 6G   �� *
IsSelected+$  TwoWay=�! 5�  *   �� 5�  .   " 6:   �� *	IsVisible+# True6X   .a�5�  2   ��  � y�$ Visible=�6O   5�  /   �� 5�  .   " 6:   �� *	IsVisible+	# False6X   .a�5�  2   ��  � y�$ 	Collapsed=�6O   5�  /   5�  '   .R�5�  "     $$ uclFormation��69   % 6O   �� 5�     �� 5�     "  67    6q   �� *Plays+ 5�  "   �� 5�  &   " Q�6,   8   .�5�  *   ��  �@Q�
IsExpanded 6G   �� *
IsExpanded+$  TwoWay=��� 5�  *    �@Q�
IsSelected 6G   �� *
IsSelected+$  TwoWay=�! 5�  *   �� 5�  .   " 6:   �� *	IsVisible+# True6X   .a�5�  2   ��  � y�$ Visible=�6O   5�  /   �� 5�  .   " 6:   �� *	IsVisible+	# False6X   .a�5�  2   ��  � y�$ 	Collapsed=�6O   5�  /   5�  '   .R�5�  "     $& uclSubFormation��6<   ( 6U   �� 5�     �� 5�     "  67   .R�5�  "     $) uclPlay��64   + 6E   �� 5�     5�     ��5�     �� 5�     " Q�6    # [ 6C   .�5�     -"   5        �� 5�      �@Q�
IsExpanded 6;   �� *
IsExpanded+$  TwoWay=��� 5�      �@Q�
IsSelected 6;   �� *
IsSelected+$  TwoWay=�! 5�     �� 5�  "   " 6.   �� *	IsVisible+# True6L   .a�5�  &   ��  � y�$ Visible=�6C   5�  #   �� 5�  "   " 6.   �� *	IsVisible+	# False6L   .a�5�  &   ��  � y�$ 	Collapsed=�6C   5�  #   5�     5�     5      5     5     5     