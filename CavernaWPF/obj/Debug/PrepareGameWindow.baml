   M S B A M L   `   `   `  ����K  GCavernaWPF, Version=1.0.6256.6682, Culture=neutral, PublicKeyToken=null=#clr-namespace:CavernaWPF.ConvertersCavernaWPF.Converters  5      "    CavernaWPF.PrepareGameWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      :
Converters#clr-namespace:CavernaWPF.Converters     5        � Title$  
CavernaWPF��5      $	��500��6   $	��450��6%   $	��500��64   $	��450��6A    � WindowStartupLocation$ CenterScreen=�5      .��5      � $��Center=�6    � RowDefinitions 5      �� 5	      $	��225 �6   6"   �� 5
      $��* �6   6    �� 5      $��30 �6   6!   .+�5      �� $	��225��6
   $	��400��6   $��0��6#   $3��-pack://application:,,,/Images/CavernaLogo.png��60   6i   �� 5      $��1��6   ��6   �� *PlayerSlots+ � 	Resources 5      1   +CavernaWPF.Converters.ColorToDwarfConverter   colorconverter  StartingPlayer��%�   &	        &	        5      6'   �� 5      $��25��6#   $��25��6/   $6��0pack://application:,,,/Images/StartingPlayer.png��6:   6v   5      ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6   {�6+   �� *	Enabled+ �� Mode$ TwoWay=�.+�5      ��  d� 	IsChecked 6   �� *
Selected+$ TwoWay=� � LayoutTransform 5   	   �� 5       �� ScaleX$	 1.2Q�6   	 �� ScaleY$		 1.2Q�6,   5   	   �� 5      $	��100��6   $��16(�62   ��6   �� *Name+.��6@   ��5   	   �� 5   
   
 �� 
TargetType"
 ��6   .�5      ��  �� Property � {� �� Value .  6'    �� Triggers 5      �� 5        �� Binding 6   �� *
Selected+$ TwoWay=� �� Value True6B   .a�5!      ��  � {� . 6)   5"      5$      .��5%      5&   	   �� 5'      -   5        $��80��5'      ��6   �� *ColorOptions+ �� SelectedItem 6A   �� *Color+��5(   	   �� 5)   
   .R�5*      �� $��38��6   $��48��6   ��6(   ��  �� Path$ .� �� 	Converter# [  6o   5+      ��5-   	   �� 5.   
   "
 ��6   .�5/      ��  � {� .  6'    50      �� 51       6   �� *
Selected+ True65   .a�52      ��  � {� . 6)   53      55      57   	   	� 58       	� 	GroupName$ default��6    6(   �� *StartingPlayer+ 59   	   �� 5:   
   $	 1.2Q�6   $		 1.2Q�6&   ��5<   	   �� 5=   
   "
 	�6   .�5>      ��  � y�$ Hidden=�6(    5?      P� 5@       P� Property �@d�	IsChecked P� Value$
 TrueY�6)   . �5A      ��  � ��# [ 6'   5B      �� 5C       6   �� *
Selected+ True65   .a�5D      ��  � y�$ Visible=�6*   5E      5G      5I   	   5J      5K      5M      � 5N      $��2��6	   $��Right=�6    � ColumnDefinitions 5O      �� 5P      $��* �6   6#   �� 5Q      $��70 �6   6$   �� 5R      $��40 �6   6$   .+�5T      �� -   5        $��1��5T      $��
Start Game��6   ��5U      �� 5V      "
 ��6    5W      �� 5X   	    6   �� *SelectedPlayers+ 069   .a�5Y   
   ��  � {� .  6&   5Z   
   �� 5[   	    6   �� *SelectedPlayers+ 169   .a�5\   
   ��  � {� . 6&   5]   
   5_      5a      �� 5b      -   5        $��2��5b      $
��Quit��6   6G   5c      5d      5e      