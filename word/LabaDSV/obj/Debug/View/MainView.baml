   M S B A M L   `   `   `  ����  LabaDSV    LabaDSV.View.MainView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      -   5        $��
MainWindow��5        � Title$6  0Генератор рандомных чисел��5      $	��750��5      $	��550��5	      $	��700��5
      $	��500��5       � AllowsTransparency . 
������ $��Segou UI Light)�5      $��14(�5      $��Light"�5       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ MainResources.xaml?�6%   %    5      ��5      �� 5   
    �� 
TargetType" �6   .�5      �� '  !System.Windows.Shell.WindowChrome �� Property �@ WindowChrome	 �� Value	 5        5      
   CaptionHeight$
 1Q�5          CornerRadius$ 4��5!         GlassFrameThickness$ 0,0,0,1q�5"         NonClientFrameEdges$
 None=�5#         ResizeBorderThickness$ 7q�5$      5&      5'      ��5*      �� 5+   
   
�������
������ $��0q�5.       �� CornerRadius$ 5��5/         e J� Effect# [  50      ._�51      �  � RowDefinitions 52      �� 53      $��30 �6$   �� 54      $��* �6$   �� 55      $��40 �6$    � ColumnDefinitions 58      �� 59      $��40 �6'   �� 5:      $��* �6'   .+�5=      �� $��0��5>      $��2��5?      ���#60C4EB$ 5 5 0 0��5A      ._�5B      ]� .+�5C      �� $��20��5D      $��20��5E      $��	10,2,10,0q�5F      $
��Left=�5G      $
��Left=�5H      $��../Image/settings.png��5I       � ContextMenu 5J      �� 5K   "   .@�5L   &   m� $��Setting��6/   .@�5M   *   m� $��Longest word��6^    m� Command 63   �� *FindLongestWordCommand+6^   m� 5N   *   $��Shortest word��6_    63   �� *FindShortestWordCommand+6_   m� 5O   *   $��Common word��6]    63   �� *FindCommonWordCommand+6]   m� 5P   *   $��Rarely word��6]    63   �� *FindRarelyWordCommand+6]   m� 5Q   *   $��	Ten times��6Z    63   �� *FindWordTenCommand+6Z   m� 5R   *   $��	Last word��6[    63   �� *FindLastWordCommand+6[   5S   '   5T   #   5V      �� 5X      $��5,8,10,0q�5Y      
�������$)��#Текстовый редактор��5[      ]� 5\      $��20��6$   $��	10,2,10,0q�63   .+�5]      �� $	��180��5^   !   $��20��5_   !   $	��100��5`   !   $	��200��5a   !   
�������$��0q�5c   !   $
��Left=�5d   !   ��5e   !   �� *
SearchText+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5f      $��20��5g   !   $��20��5h   !   $��5,0,15,0q�5i   !   $��Right=�5j   !    � Cursor$
 Hand��5k   !   $��Right=�5l   !   $��../Image/search.png��5m   !    J� InputBindings 5n   "   d� 5o   &    d� MouseAction$ 	LeftClicke�5r   )    �� Command 5p   )   �� *SearchWordCommand+ �� CommandParameter 5q   )   ��  �� ElementName$ MainTextBox��5r   )   5t      5v      �� 5x      $��20��5y      $	��160��5z      $
��20,5q�5{      $��Center=�5|      
�������   System.Windows.Media.Fonts   SystemFontFamilies#��Z 5~       �� SelectedItem 5      �� *FileInstance.Font+$ PropertyChanged=�$ TwoWay=� �� TargetNullValue$ Segou UI Light���� 5�      $��13��5�      $��13��5�      $��0,2,15,0q�5�      $��Right=�5�      $
 Hand��5�      $��Right=�5�      $��../Image/exit.png��5�       5�      d� 5�   "   $ 	LeftClicke�5�   %    5�   %   �� *CloseWindowCommand+ 5�   %   �� $ 
MainWindow��5�   %   5�      �� 5�      $��13��5�      $��13��5�      $��	0,13,15,0q�5�      $��Right=�5�      $
 Hand��5�      $��Right=�5�      $��../Image/collapse.png��5�       5�      d� 5�   "   $ 	LeftClicke�5�   %    5�   %   �� *CollapseWindowCommand+ 5�   %   �� $ 
MainWindow��5�   %   5�      5�       5�      d� 5�      $ 	LeftClicke�5�       5�      �� *MoveWindowCommand+ 5�      �� $ 
MainWindow��5�      5�      �� 5�      $��1��5�      $��2��5�      $��0��5�      ���#B4DCF5$��0,0,1,0q�5�      ._�5�      �� .+�5�      �� $��25��5�      $��25��5�      $
��0,10q�5�      $
 Hand��5�      $��../Image/open-folder.png��5�       5�      d� 5�   "   $ 	LeftClicke�6S    6/   �� *OpenFileCommand+6S   5�      �� 5�      $��25��5�      $��25��5�      $
��0,10q�5�      $
 Hand��5�      $"��../Image/download-button.png��5�       5�      d� 5�   "   $ 	LeftClicke�6S    6/   �� *SaveFileCommand+6S   5�      �� 5�      $��25��5�      $��25��5�      $
��0,10q�5�      $
 Hand��5�      $��../Image/folder.png��5�       5�      d� 5�   "   $ 	LeftClicke�6R    6/   �� *AddFileCommand+6R   5�      �� 5�      $��25��5�      $��25��5�      $
��0,10q�5�      $
 Hand��5�      $��../Image/plus-button.png��5�       5�      d� 5�   "   $ 	LeftClicke�6[    6/   �� *IncreaseFontSizeCommand+6[   5�      �� 5�      $��25��5�      $��25��5�      $��25��5�      $��25��5�      $
��10,3q�5�      $��18(�5�      
���  ����5�      �� *FileInstance.Size+�� 5�      $��25��5�      $��25��5�      $
��0,10q�5�      $
 Hand��5�      $&�� ../Image/round-delete-button.png��5�       5�      d� 5�   "   $ 	LeftClicke�6[    6/   �� *DecreaseFontSizeCommand+6[   5�      �� 5�      $��25��5�      $��25��5�      $
��0,10q�5�      $
 Hand��5�      $ ��../Image/color-palette.png��5�       5�      d� 5�   "   $ 	LeftClicke�6V    6/   �� *ChangeColorCommand+6V   5�      �� 5�      $��25��5�      $��25��5�      $
��0,10q�5�      $
 Hand��5�      $��../Image/pie-chart.png��5�       5�      d� 5�   "   $ 	LeftClicke�6T    6/   �� *ShowChartCommand+6T   5�      �� 5      $��25��5     $��25��5     $
��0,10q�5     $
 Hand��5     $��../Image/error.png��5      5     d� 5  "   $ 	LeftClicke�6X    6/   �� *ShowTextErrorCommand+6X   5	     �� 5
     $��25��5     $��25��5     $
��0,10q�5     $
 Hand��5     $��../Image/start.png��5      5     d� 5  "   $ 	LeftClicke�6Y    6/   �� *StartTextErrorCommand+6Y   5     5     5     �� 5     -   5        $��MainTextBox��5     $��1��5     $��1��5     $
��5,10q�5       �� AcceptsReturn  . 
�������$��0q�5     $! �� HorizontalScrollBarVisibility$! Disabled=�5"     " �� TextWrapping$
" Wrap=�5$     "# �� VerticalScrollBarVisibility$
# Auto=�5%     ��5     �� *FileInstance.Font+$ TwoWay=�$ PropertyChanged=���5      �� *FileInstance.Size+$ TwoWay=�$ PropertyChanged=���5!     �� *Brush+$ Black����5#     �� *FileInstance.Text+$ TwoWay=�$ PropertyChanged=�.��5%  8   .  (System.Windows.Interactivity.Interaction$   Triggers$ 5&     /  )System.Windows.Interactivity.EventTrigger  5'     %   	EventName$% TextChanged��6)   .  (System.Windows.Interactivity.TriggerBase&   Actions.& 5(     6  0System.Windows.Interactivity.InvokeCommandAction  '   Command' 64   �� *TextChangedCommand+(   CommandParameter( 6[   �� $ MainListBox��5)     .��5*  .   5+     �� 5,     -   5        $��MainListBox��5-     $��2��5.     $��1��5/     
�������$��0q�51     $y�	Collapsed=�52     $ 53       54     $% SelectionChanged��6)   .& 55       ' 64   �� *SelectionChangedCommand+( 6`   �� $ MainListBox��) �� Path$) SelectedItem�56     58     59     5:     5<     