   M S B A M L   `   `   `  ���� K  GCavernaWPF, Version=1.0.6256.6784, Culture=neutral, PublicKeyToken=null=#clr-namespace:CavernaWPF.ConvertersCavernaWPF.Converters  "    CavernaWPF.PrepareGameWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :
Converters#clr-namespace:CavernaWPF.Converters       � Title$  
CavernaWPF��$	��500��$	��450��$	��500��$	��450�� � WindowStartupLocation$ CenterScreen=�.��� $��Center=� � RowDefinitions �� $	��225 ��� $��* ��� $��30 �.+��� $	��225��$	��400��$��0��$3��-pack://application:,,,/Images/CavernaLogo.png���� $��1������ *PlayerSlots+ � 	Resources 1   +CavernaWPF.Converters.ColorToDwarfConverter   colorconverter  StartingPlayer��%g   &	        &	        �� $��25��$��25��$6��0pack://application:,,,/Images/StartingPlayer.png������ .R���  �� Orientation$ 
Horizontal=�{��� *	Enabled+ �� Mode$ TwoWay=�.+���  d� 	IsChecked �� *
Selected+$ TwoWay=� � LayoutTransform ��  �� ScaleX$	 1.2Q�	 �� ScaleY$		 1.2Q��� $	��100��$��16(����� *Name+.������ 
 �� 
TargetType"
 ��.���  �� Property � {� �� Value .   �� Triggers ��  �� Binding �� *
Selected+$ TwoWay=� �� Value True.a���  � {� . .���� -   $��80������ *ColorOptions+ �� SelectedItem �� *Color+���� .R��� $��38��$��48������  �� Path$ .� �� 	Converter# [  ���� "
 ��.���  � {� .   ��  �� *
Selected+ True.a���  � {� . 	�  	� 	GroupName$ default�� �� *StartingPlayer+ �� $	 1.2Q�$		 1.2Q����� "
 	�.���  � y�$ Hidden=� P�  P� Property �@d�	IsChecked P� Value$
 TrueY�. ���  � ��# [ ��  �� *
Selected+ True.a���  � y�$ Visible=�� $��2��$��Right=� � ColumnDefinitions �� $��* ��� $��70 ��� $��40 �.+��� -   $��1��$��
Start Game������ "
 �� ��  �� *SelectedPlayers+ 0.a���  � {� .  ��  �� *SelectedPlayers+ 1.a���  � {� . �� -   $��2��$
��Quit��