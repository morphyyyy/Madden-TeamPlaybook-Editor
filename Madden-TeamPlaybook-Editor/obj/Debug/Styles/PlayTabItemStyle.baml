   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   PlayTabItem  �� 
TargetType �� 	Resources  Key �� ImageSource �� BitmapScalingMode j� Stretch �� Property �� Value �� Path �� RelativeSource	 �� AncestorType
 �� 
TargetType � RowDefinitions  PlayDeSelected �� CornerRadius �� Padding ��Name  PlayHighlighted J� Opacity  PlaySelected �� Triggers P� Property P� Value N� EnterActions �� 
TargetName �� TargetProperty W� From	 W� To g� Duration N� ExitActions�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %~
  &	        7  7  7  �� 5      "  ��6     5   
   ���� 5
      PlaySelected $* $../Resources/field_blank_resized.jpg��5      $
 Fant=�5      $ UniformToFill=�5      5      �� 5      PlayDeSelected $6 0../Resources/field_blank_resized_Desaturated.jpg��5      $
 Fant=�5      $ UniformToFill=�5      5      �� 5      PlayHighlighted $/ )../Resources/field_blank_resized_blue.jpg��5      $
 Fant=�5      $ UniformToFill=�5      5       5"      .�5$   
   ��  � ��$ Tahoma)�6'   �� 5%   
    � ��$ 13(�6%   �� 5&   
    � ��
 ���� 6'   �� 5'   
    � �� 67   �� $  HorizontalContentAlignment� �� "	 ���� 5(   
    � �� 65   �� $ VerticalContentAlignment� �� "	 ���� 5)   
    � ��$ 1,0,0,0q�6$   �� 5*   
    � ��
 �����6'   �� 5,   
    � �� 5-      �� 5.      "
 ��6"   .R�5/      � $��2q�6    50      �� 51      $
��Auto �6,   $��50��6:   �� 52      $
��Auto �6,   �� 53      $��* �6,   .+�55      �� $��BdrPlayDeSelected��56      $��1��57      8��  $ 8��59      $ 5q�5:      ._�5;      �� $ cprPlayDeSelected��6/   $��Center=�5<   !   $��Center=�5=   !   $��Header��5>   !   5?      �� 5@      $��BdrPlayHighlighted��5A      $��1��5B      8�� $ 0Q�5D      $ 8��5E      $ 5q�5F      ._�5G      �� $ cprPlayHighlighted��6/   $��Center=�5H   !   $��Center=�5I   !   $��Header��5J   !   5K      �� 5L      $��BdrPlaySelected��5M      $��1��5N      8�� $ 0Q�5P      $ 8��5Q      $ 5q�5R      ._�5S      �� $ cprPlaySelected��6/   $��Center=�5T   !   $��Center=�5U   !   $��Header��5V   !   5W      5X       5Y      P� 5[       �@��
IsSelected .  5]      �� 5^   "   .��5_   &   �� .��5`   *   W� $ BdrPlaySelected��5a   -   $ Opacity�5b   -   $	 0.0X�5c   -   $	 1.0X�68   $ 0:0:0.2D�6A   5d   '   5e   #    5h      �� 5i   "   .��5j   &   �� .��5k   *   W� $ BdrPlaySelected��5l   -   $ Opacity�5m   -   $	 1.0X�5n   -   $	 0.0X�68   $ 0:0:0.5D�6A   5o   '   5p   #   5s      P� 5u       �@J�IsMouseOver .  5w      �� 5x   "   .��5y   &   �� .��5z   *   W� $ BdrPlayHighlighted��5{   -   $ Opacity�5|   -   $	 0.0X�5}   -   $	 1.0X�68   $ 0:0:0.2D�6A   5~   '   5   #    5�      �� 5�   "   .��5�   &   �� .��5�   *   W� $ BdrPlayHighlighted��5�   -   $ Opacity�5�   -   $	 1.0X�5�   -   $	 0.0X�68   $ 0:0:0.5D�6A   5�   '   5�   #   5�      5�      5�      5�      5�      