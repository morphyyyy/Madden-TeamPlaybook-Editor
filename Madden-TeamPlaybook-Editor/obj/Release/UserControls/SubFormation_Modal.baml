   M S B A M L   `   `   `  ���� Q  MMadden-Playbook-Editor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:MaddenTeamPlaybookEditor.ViewModels#MaddenTeamPlaybookEditor.ViewModels   MathConverter.XamarinFormsQ?clr-namespace:HexInnovation;assembly=MathConverter.XamarinFormsHexInnovation _4clr-namespace:MaddenTeamPlaybookEditor.User_Controls&MaddenTeamPlaybookEditor.User_Controls  >    8MaddenTeamPlaybookEditor.User_Controls.SubFormationModal   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     TTeamPlaybookViewModels1clr-namespace:MaddenTeamPlaybookEditor.ViewModels     Pmath?clr-namespace:HexInnovation;assembly=MathConverter.XamarinForms     MUserControls4clr-namespace:MaddenTeamPlaybookEditor.User_Controls     -     >�Name$  uclSubFormationModal�� J� 	AllowDrop .  � 	Resources    MenuItemPanelTemplate �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius J� SnapsToDevicePixels&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader:  4System.Windows.Controls.Primitives.DataGridRowHeader	 ��Name
 �� Triggers P� Property P� Value �� 
TargetName  
FieldBrush �� ImageSource �� BitmapScalingMode j� Stretch j� 
AlignmentY��%�  &	        'm�      7   '�� <     7   '  �     '�� �    '�� �    '  C    '  �    '�� �    &	 ?    �� .R��� $��	-20,0,0,0q��� " m�.���  � ��8   �� " ��.���  � ��8   ��  � �� �� " ��.R��� $ 10��$��0q�
���/kU� . ._��� �� "  .���  � ��
 ���� ��  � ��
 ���� ��  �@ RowBackground
 ���� ��  � ��
 ������  �@ GridLinesVisibility$
 None=���  �@ CanUserAddRows . ��  �@ CanUserDeleteRows . �� " ��.���  � ��
 ���� ��  � ��
 ������  � ��$
 Bold"��� " ��.���  � ��
 ���� ��  � ��
 ������  � ��
 ������ "  .���  � ��
 ���� ��  � ��$
 Bold"���  � ��$ 30����  � {� .  �� "  .���  � ��
 ���� ��  � {� .  �� " ��.���  � �� �� " ��.R��� $��Panel��.���� $	 ContentSite��$	��Top=�$��Center=�$��Header��$	��5,0q�
 P�  �@��
IsSelected . . ��� $ Panel�� � ��
 ���� �� $ Panel�� � ��
 ����P�  �@��
IsSelected .  . ��� $ Panel�� � ��
 ���� �� $ Panel�� � ��
 �   ��� $! ../Resources/full_field.jpg��$
 Fant=�$ UniformToFill=�$	 Top=�.��]� .+��� -    ��Name$ cvsField��$
��Left=�$	��950��$	��533��.+��� -   $��bdrField��#��[ $ 10��$	��533��$	��950���� 	 �� X1$ 0��	 �� Y1$ 599.5��	 �� X2$	 533��	 �� Y2$ 599.5��$��5������  �� Color ��  �� A$ 96�� �� R$ 0�� �� G$ 0�� �� B$ 0���� $ 0��$ 499.5��$	 533��$ 499.5��$��5������  �� $	 144��$	 204��$	 128��$ 40���� -    ��Name$ iclIcons��{�.  �� Left$ 266.5��
 �� Top$	 600������ *PlayerPlayartView+ �� Mode$ TwoWay=����� .R��� $ 
cvsPlayers������ .R��� .+�7   1MaddenTeamPlaybookEditor.User_Controls.PlayerIcon     Name$  clnIcon��!   Scale$! 2Q�"   ShowPosition" . #   AbsolutePositioning# . $   	Draggable$ . %   Player% �� ���� .���  �@��Left �� *XY.X+��  �@��Top �� *XY.Y+& �� Triggers& P�  �@J�IsMouseOver . . ���  �@J�Effect 3  -System.Windows.Media.Effects.DropShadowEffect  '   ShadowDepth$' 0Q�(   Color$( White��)   Opacity$) 1Q�*   
BlurRadius$* 20Q���  � z� �� + �� ScaleX$+ 2Q�, �� ScaleY$, 2Q�]� $
��Left=�$	��575��$��5q���.  .+��� -   - ��Name$- 	tabPlayer��
������ 
������ $	��Top=�$	��Top=�$��Stretch=�.@��� $
��PBST��.��� $��Center=�. � ColumnDefinitions. �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �/ � RowDefinitions/ �� �� .+��� $	��5,0q�
������$��0��$��0��$	��rec���� {�.  $��0��$��1������ *
PBST.rec+$ TwoWay=��� $	��5,0q�
������$��1��$��0��$
��SETL���� $��1��$��1������ *	PBST.SETL+$ TwoWay=��� $	��5,0q�
������$��2��$��0��$
��PBFM���� $��2��$��1������ *	PBST.PBFM+$ TwoWay=��� $	��5,0q�
������$��3��$��0��$
��pbst���� $��3��$��1������ *	PBST.pbst+$ TwoWay=��� $	��5,0q�
������$��4��$��0��$
��SPF_���� $��4��$��1������ *	PBST.SPF_+$ TwoWay=��� $	��5,0q�
������$��5��$��0��$
��ord_���� {�.  $��5��$��1������ *	PBST.ord_+$ TwoWay=��� $	��5,0q�
������$��6��$��0��$
��name���� $��6��$��1������ *	PBST.name+$ TwoWay=��� $
��SETL��.��� $��Center=�. �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �/ �� �� .+��� $	��5,0q�
������$��0��$��0��$	��rec���� {�.  $��0��$��1������ *
SETL.rec+$ TwoWay=��� $	��5,0q�
������$��1��$��0��$
��setl���� $��1��$��1������ *	SETL.setl+$ TwoWay=��� $	��5,0q�
������$��2��$��0��$
��FORM���� $��2��$��1������ *	SETL.FORM+$ TwoWay=��� $	��5,0q�
������$��3��$��0��$
��MOTN���� $��3��$��1������ *	SETL.MOTN+$ TwoWay=��� $	��5,0q�
������$��4��$��0��$
��CLAS���� $��4��$��1������ *	SETL.CLAS+$ TwoWay=��� $	��5,0q�
������$��5��$��0��$
��SETT���� $��5��$��1������ *	SETL.SETT+$ TwoWay=��� $	��5,0q�
������$��6��$��0��$
��SITT���� $��6��$��1������ *	SETL.SITT+$ TwoWay=��� $	��5,0q�
������$��7��$��0��$
��SLF_���� $��7��$��1������ *	SETL.SLF_+$ TwoWay=��� $	��5,0q�
������$��8��$��0��$
��name���� $��8��$��1������ *	SETL.name+$ TwoWay=��� $	��5,0q�
������$��9��$��0��$
��poso���� $��9��$��1������ *	SETL.poso+$ TwoWay=��� $��
Alignments��.���� -   $- tabAlignments��
������ 
������ $��Bottom=�$��Center=����� *
Alignments+���� 0 �� DataType8   2MaddenTeamPlaybookEditor.ViewModels.SubFormationVM"0  .R��� 1 ��Name$1 tabAlignment������ *	SGFM.name+���� " ��  FormationTabItem2 �� BasedOn#2 [ �� $��Packages��.���� -   $- tabPackages��
������ 
������ $��Bottom=�$��Center=����� *
Packages+���� "0  .R��� $1 tabAlignment������ *	SPKF.name+���� " ��#2 [ 