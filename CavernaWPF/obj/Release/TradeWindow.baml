   M S B A M L   `   `   `  ���� K  GCavernaWPF, Version=1.0.6256.6784, Culture=neutral, PublicKeyToken=null=#clr-namespace:CavernaWPF.ConvertersCavernaWPF.Converters      CavernaWPF.TradeWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :
Converters#clr-namespace:CavernaWPF.Converters       � Title$  
CavernaWPF��$	��300��$	��300�� � 	Resources 3   -CavernaWPF.Converters.ResourceToIconConverter   resourceToIconConverter��%   &	          .���  � RowDefinitions �� $��* ��� $��50 ��� $��30 �.+��� $��0�� �� AlternationCount$	 100������ *	Exchanges+���� .R��� -   .����  �� Orientation$ 
Horizontal=�.+��� ���� *Key+���� .R��� $ 
Horizontal=����� .R��� $ 
Horizontal=�.+��� ���� *Amount+�� $��25��$��25������ *type+ �� 	Converter# [  �� $
�� => ���� ���� *Value+���� .R��� $ 
Horizontal=����� .R��� $ 
Horizontal=�.+��� ���� *Amount+�� $��25��$��25������ *type+# [  �� $��1��$ 
Horizontal=�.+��� -    ��Name$ 
TradeInput������ .R��� $ 
Horizontal=����� .R��� $ 
Horizontal=�.+��� ���� *Amount+�� ���� *type+�� $��=>���� -   $ TradeOutput������ .R��� $ 
Horizontal=����� .R��� $ 
Horizontal=�.+��� ���� *Amount+�� ���� *type+� $��2��$��Right=� � ColumnDefinitions �� $��* ��� $��60 ��� $��60 �.+��� -   $��1��$��OK���� -   $��2��$��Cancel��