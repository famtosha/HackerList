   M S B A M L   `   `   `  ���� C  ?FirstWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:FirstWpfFirstWpf  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L5clr-namespace:System.Diagnostics;assembly=WindowsBaseSystem.Diagnostics     FirstWpf.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:FirstWpf     Fdiag5clr-namespace:System.Diagnostics;assembly=WindowsBase       � Title$  
HackerList��$	��546��$��947.5�� � DataContext   FirstWpf.VM  .���  � ColumnDefinitions �� $
��auto ��� $��* �.+�� $��1��.+��� $��0q����� *Info+1  +System.Diagnostics.PresentationTraceSources   
TraceLevel$
 High=� �� SelectedItem �� *SelectedItem+���� .R��� $��3q�.+��  � ShowGridLines .   �� $
��auto ��� $
��auto �.+��� $��1��.+��� $��15(�����  �� Path$
 Name��� $��12(����� $ ID��� $��12(�
���  ������ $	 VAC��� $��12(����� $ 
GameStatus��� $��4q�$��0��$
��Left=�$��50��$��50������ $ Avatar�� $��0�� � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��10q�$��0��.+��� $	��200��$��Get Info���� $��4q�$��
Get Online��$	��200��$��25������ *GetOnlineCommand+�� $��4q�$��1��$��Get All��$	��200��$��25������ *GetAllCommand+�� ���� *Status+�� $��10q�$��1��.+��� $	��200��$��Add to list���� $��4q�$	��200������ *AddTB+�� $��4q�$	��Add��$	��200��$��25������ *
AddCommand+�� $��10q�$��2��.+��� $��4q�$	��200��$��Remove from list���� $��4q�$��Remove��$	��200��$��25������ *RemoveCommand+�� $��10q�$��3��.+��� $	��200��$��Change list path���� $��4q�$	��200������ *PathTB+�� $��4q�$	��Set��$	��200��$��25������ *ChangePathCommand+�� $��4��.+��� $	��200��$��Open in browser���� $��4q�$
��Open��$	��200��$��25������ *OpenInBrowserCommand+