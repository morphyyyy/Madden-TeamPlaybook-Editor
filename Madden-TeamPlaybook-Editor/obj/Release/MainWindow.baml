   M S B A M L   `   `   `  ���� Q  MMadden-Playbook-Editor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:MaddenTeamPlaybookEditor.User_Controls&MaddenTeamPlaybookEditor.User_Controls  Y1clr-namespace:MaddenTeamPlaybookEditor.ViewModels#MaddenTeamPlaybookEditor.ViewModels  ]3clr-namespace:MaddenCustomPlaybookEditor.ViewModels%MaddenCustomPlaybookEditor.ViewModels  C&clr-namespace:MaddenTeamPlaybookEditorMaddenTeamPlaybookEditor  S.clr-namespace:MaddenTeamPlaybookEditor.Classes MaddenTeamPlaybookEditor.Classes  )    #MaddenTeamPlaybookEditor.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     MUserControls4clr-namespace:MaddenTeamPlaybookEditor.User_Controls     TTeamPlaybookViewModels1clr-namespace:MaddenTeamPlaybookEditor.ViewModels     XCustomPlaybookViewModels3clr-namespace:MaddenCustomPlaybookEditor.ViewModels     8local&clr-namespace:MaddenTeamPlaybookEditor     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Bclasses.clr-namespace:MaddenTeamPlaybookEditor.Classes     -     �Name$  wdwPlaybookEditor�� � Title$! Madden Team Playbook Editor�� � WindowStartupLocation$ CenterScreen=� � Icon$$ /Resources/Playbook_Editor.ico�� � UseLayoutRounding .  � WindowStyle$ ThreeDBorderWindow=�$��Visible=� � WindowState$ Normal=� � SizeToContent$ WidthAndHeight=�����  �� ImageSource$ Resources/chalkboard.jpg��	 j� Viewport$	 0,0,1920,1067�
 j� ViewportUnits$
 Absolute=� j� TileMode$
 Tile=� j� Stretch$ UniformToFill=� j� 
AlignmentX$
 Left=� j� 
AlignmentY$	 Top=� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$ ApplicationCommands.Paste�� � 	Resources &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value��%�  '        '�� 6    �� "  .���  � ��
 ���� ��  � ��
 ���� ��  � ��
 �   ���   �@ HorizontalGridLinesBrush �	#FFD6D6D6��  �@ VerticalGridLinesBrush
 �   ���  �@ GridLinesVisibility$ 
Horizontal=���  �@ CanUserAddRows .  ��  �@ CanUserDeleteRows .  �� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��
 �   ���  � ��$ 5q�.��]� $
��Auto��$
��Auto��$��Stretch=�.+�o� $	��Top=� o� 
IsMainMenu . .@�m� $��_File��.@�m� -   $
��Open���� m� -   $��Load Roster���� m� -   $
��Save��m� -   $��Save All Playart��m� -   $��Save All ARTL��m� $��Options��.@�m� $
��Copy�� m� Command$ ApplicationCommands.Copy��m� $��Paste��$ ApplicationCommands.Paste���� m� $��Delete��$  ApplicationCommands.Delete��m� $��	Functions��.@�m� -   $��Sabo's Gameplan Revamp��m� -	   $��Set Generic Audibles��m� $��Delete��.@�m� -
   $��Remove All Formations��m� -   $��Remove All Sub-Formations��m� -   $��Remove All Plays��m� -   $��PSAL Editor��m� $
��Help��.@��� -   $j��dhttps://docs.google.com/spreadsheets/d/1yGtMkVxY1HZ-Pi_ZuffaI1TUtA6tpNpQPQUELa-INaI/edit?usp=sharing?�.�Table Definitions9� -    9�Name$ xpdTeamPlaybook��
�������$��Team Playbook�� 9� ExpandDirection$
 Down=� 9� 
IsExpanded . .���� .@��� $��Playbook��.��R� -    R�Name$ tvwPlaybook��$��5q�$	��Top=� J� 	AllowDrop . $	��600��$��Hidden=�$��Hidden=���Z� ��Z� ��Z� ���� *
Formations+   -   $ ApplicationCommands.Copy��  -   $ ApplicationCommands.Paste��  -   $  ApplicationCommands.Delete��  �� DataType5   /MaddenTeamPlaybookEditor.ViewModels.FormationVM �� ItemsSource �� ItemContainerStyle   SubFormationTreeViewItem �� BasedOn  �� Mode! �� Triggers" �� Binding# �� Value6   0MaddenTeamPlaybookEditor.User_Controls.Formation$  Name%   	formation8   2MaddenTeamPlaybookEditor.ViewModels.SubFormationVM  PlayTreeViewItem9   3MaddenTeamPlaybookEditor.User_Controls.SubFormation&  Name'   showPackage(   subFormation0   *MaddenTeamPlaybookEditor.ViewModels.PlayVM1   +MaddenTeamPlaybookEditor.User_Controls.Play)  Name*   showAudibles+   play, ��Name- �� Orientation. d�Name/ � ToolTip0 d� 	IsChecked  AudibleQuick  
AudibleRun  AudibleDeep  AudibleFake1 �� TextAlignment�� %�  (��       *, +)7   (�� m    *, +)7  (�� �    *, +)7  7  7  7  �� "   �� *SubFormations+ �� " Q�8   .���  �@Q�
IsExpanded �� *
IsExpanded+$  TwoWay=���  �@Q�
IsSelected �� *
IsSelected+$  TwoWay=�! �� " �� *	IsVisible+# True.a���  � y�$ Visible=��� " �� *	IsVisible+	# False.a���  � y�$ 	Collapsed=�.R�  $$ uclFormation��{�. % �� �� "   �� *Plays+ �� " Q�8   .���  �@Q�
IsExpanded �� *
IsExpanded+$  TwoWay=���  �@Q�
IsSelected �� *
IsSelected+$  TwoWay=�! �� " �� *	IsVisible+# True.a���  � y�$ Visible=��� " �� *	IsVisible+	# False.a���  � y�$ 	Collapsed=�.R�  $& uclSubFormation��' . ( �� �� "  .R�]� .+�  $) uclPlay��* . $	��Top=�+ �� �� $, pnlAudibles��$- 
Horizontal=�$��Center=�$��Bottom=�$	��Top=�.+�d� $. AudibleQuick��$/ Short Pass (X)��$��18��0 �� *isShortAudible+���� " d�8   d� $. 
AudibleRun��$/ Run (Y/Triangle)��$��18��0 �� *isRunAudible+���� " d�8  d� $. AudibleDeep��$/ Deep Pass (LB/L1)��$��18��0 �� *isDeepAudible+���� " d�8  d� $. AudibleFake��$/ Play Action (RB/R1)��$��18��0 �� *isFakeAudible+���� " d�8  �� $	��Top=�$��DK Crayon Crumble)�$��15(�$
��Wrap=�$1 Center=�
������ 
����������� *	PBPL.name+���� " Q�  FormationTreeViewItem# [ .�-   ��  �@Q�
IsExpanded �� *
IsExpanded+$  TwoWay=���  �@Q�
IsSelected �� *
IsSelected+$  TwoWay=�! �� " �� *	IsVisible+# True.a���  � y�$ Visible=��� " �� *	IsVisible+	# False.a���  � y�$ 	Collapsed=��� $��Gameplan��.��]� $	��Top=�$	��5,1q�$	��600��.+�]� $	��Top=�$	��5,1q�.+��� -   2 ��Name$2 cbxPLYT��3 �� SelectedValuePath$	3 Key��4 �� DisplayMemberPath$4 Value��$	��5,1q�
������ $��0q���.  $��Hidden=�$��Hidden=�5 �� 
IsEditable5 . 6 �� Text$6 Select Plays by Type������ �� -   $2 lvwSituations��$	��5,1q���.  $��Hidden=�$��Hidden=�
������ ���� ���� .R�6	   0MaddenTeamPlaybookEditor.User_Controls.Situation	  7 	  	situation7 �� R� -   $ lvwPlaysByRouteDepth��
������ 
������ $��Hidden=�$	��5,1q�$	��600��$	��875��$	��Top=����� ���� .R�]� .+��� $	��Top=�$��DK Crayon Crumble)�$��14(�$
��Wrap=�$1 Center=�
������ 
���������`� 8 �� StringFormat$8 {0}
{1}
{2}��.1��� 9 �� Path$&9  SubFormation.Formation.PBFM.name��� $9 SubFormation.PBST.name��� $9 	PBPL.name�  $	��Top=�+ �� ���� .R�>
   8MaddenTeamPlaybookEditor.Classes.UniformGridOrientations
  $	��Top=�: 
  Orientation$: 
Horizontal=�; 
  VerticalOrientation$	; Top=�$��LeftToRight=�< K� IsItemsHost< . = A� Columns$= 3������ " Q�# [ .���  �@Q�
IsExpanded �� *
IsExpanded+$  TwoWay=���  �@Q�
IsSelected �� *
IsSelected+$  TwoWay=��� $��Tables��.���� -   > ��Name$> 	tclTables��$	��600��.@��� $
��PBFM��.��<   6MaddenTeamPlaybookEditor.User_Controls.FilterableTable  ?   Data? �� *PBFM+$  TwoWay=��� $
��FORM��.��  ? �� *FORM+$  TwoWay=��� $
��PBST��.��  ? �� *PBST+$  TwoWay=��� $
��SETL��.��  ? �� *SETL+$  TwoWay=��� $
��SETP��.��  ? �� *SETP+$  TwoWay=��� $
��SPKF��.��  ? �� *SPKF+$  TwoWay=��� $
��SPKG��.��  ? �� *SPKG+$  TwoWay=��� $
��SGFM��.��  ? �� *SGFM+$  TwoWay=��� $
��SETG��.��  ? �� *SETG+$  TwoWay=��� $
��PBPL��.��  ? �� *PBPL+$  TwoWay=��� $
��PLYL��.��  ? �� *PLYL+$  TwoWay=��� $
��PLPD��.��  ? �� *PLPD+$  TwoWay=��� $
��PLRD��.��  ? �� *PLRD+$  TwoWay=��� $
��PLYS��.��  ? �� *PLYS+$  TwoWay=��� $
��PSAL��.��  -   @  Name$@ uclPSALTable��? �� *PSAL+$  TwoWay=��� $
��ARTL��.��  ? �� *ARTL+$  TwoWay=��� $
��PBAI��.��  -   $@ uclPBAITable��? �� *PBAI+$  TwoWay=��� $
��PBAU��.��  ? �� *PBAU+$  TwoWay=��� $
��PBCC��.��  ? �� *PBCC+$  TwoWay=��� $
��PLCM��.��  ? �� *PLCM+$  TwoWay=��� $
��PPCT��.��  ? �� *PPCT+$  TwoWay=��� $
��SDEF��.��  ? �� *SDEF+$  TwoWay=��� $
��SRFT��.��  ? �� *SRFT+$  TwoWay=�9� -   $ xpdCustomPlaybook��
�������$��Custom Playbook��$
 Down=� . $y�	Collapsed=�.���� -   $> tclCustomPlaybookTables��$	��Top=�.@��� $
��ARTO��.��  ? �� *ARTO+$  TwoWay=��� $
��ARTD��.��  ? �� *ARTD+$  TwoWay=��� $
��CPFM��.��  ? �� *CPFM+$  TwoWay=��� $
��PBFI��.��  ? �� *PBFI+$  TwoWay=��� $
��PBFM��.��  ? �� *PBFM+$  TwoWay=��� $
��PBPL��.��  ? �� *PBPL+$  TwoWay=��� $
��PBPN��.��  ? �� *PBPN+$  TwoWay=��� $
��PIDX��.��  ? �� *PIDX+$  TwoWay=��� $
��PLCM��.��  ? �� *PLCM+$  TwoWay=��� $
��PLPD��.��  ? �� *PLPD+$  TwoWay=��� $
��PLRD��.��  ? �� *PLRD+$  TwoWay=��� $
��PLYS��.��  ? �� *PLYS+$  TwoWay=��� $
��PPCT��.��  ? �� *PPCT+$  TwoWay=��� $
��PSLO��.��  ? �� *PSLO+$  TwoWay=��� $
��PSLD��.��  ? �� *PSLD+$  TwoWay=��� $
��SDEF��.��  ? �� *SDEF+$  TwoWay=��� $
��SETG��.��  ? �� *SETG+$  TwoWay=��� $
��SETL��.��  ? �� *SETL+$  TwoWay=��� $
��SETP��.��  ? �� *SETP+$  TwoWay=��� $
��SGFF��.��  ? �� *SGFF+$  TwoWay=��� $
��SPKF��.��  ? �� *SPKF+$  TwoWay=��� $
��SPKG��.��  ? �� *SPKG+$  TwoWay=��� $
��SRFT��.��  ? �� *SRFT+$  TwoWay=��� $
��STID��.��  ? �� *STID+$  TwoWay=�9� -   $ xpdSubFormationModal��$y�Visible=�
�������$��Sub Formation��$
 Down=� . .��>   8MaddenTeamPlaybookEditor.User_Controls.SubFormationModal  -   A  Name$A uclSubFormationModal��$	��Top=�9� -   $ xpdPlayModal��$y�	Collapsed=�
�������$
��Play��$
 Down=� . .���� $
��Auto��$
��Auto��$��Stretch=�$- 
Horizontal=�.+�6   0MaddenTeamPlaybookEditor.User_Controls.PlayModal  -    B  Name$B uclPlayModal��$	��Top=�R� -!   $ tvwPSALs��$	��Top=� . $	��600��$��Hidden=�$��Hidden=���Z� ��Z� ��Z� ����  �� %(  (��       *, +)7   (�� f    *, +)7  (�� �    *, +)�� "   �� *SubFormations+ �� " Q�8   .���  �@Q�
IsExpanded �� *
IsExpanded+$  TwoWay=���  �@Q�
IsSelected �� *
IsSelected+$  TwoWay=�! �� " �� *	IsVisible+# True.a���  � y�$ Visible=��� " �� *	IsVisible+	# False.a���  � y�$ 	Collapsed=�.R�  $$ uclFormation��% �� �� "   �� *Plays+ �� " Q�8   .���  �@Q�
IsExpanded �� *
IsExpanded+$  TwoWay=���  �@Q�
IsSelected �� *
IsSelected+$  TwoWay=�! �� " �� *	IsVisible+# True.a���  � y�$ Visible=��� " �� *	IsVisible+	# False.a���  � y�$ 	Collapsed=�.R�  $& uclSubFormation��( �� �� "  .R�  $) uclPlay��+ �� ���� " Q�# [ .�-"   ��  �@Q�
IsExpanded �� *
IsExpanded+$  TwoWay=���  �@Q�
IsSelected �� *
IsSelected+$  TwoWay=�! �� " �� *	IsVisible+# True.a���  � y�$ Visible=��� " �� *	IsVisible+	# False.a���  � y�$ 	Collapsed=�