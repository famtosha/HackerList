   M S B A M L   `   `   `  ����C  ?FirstWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:FirstWpfFirstWpf  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L5clr-namespace:System.Diagnostics;assembly=WindowsBaseSystem.Diagnostics 5   	       FirstWpf.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:FirstWpf     5   	   Fdiag5clr-namespace:System.Diagnostics;assembly=WindowsBase     5   	     � Title$  
HackerList��5	   	   $	��546��6   $��947.5��6)    � DataContext 5
        FirstWpf.VM  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   � $��1��6   .+�5      �� $��0q�6   ��6*   �� *Info+1  +System.Diagnostics.PresentationTraceSources   
TraceLevel$
 High=� �� SelectedItem 6u   �� *SelectedItem+��5      �� 5      .R�5      �� $��3q�6%   .+�5      �  � ShowGridLines .   5   "   �� 5   &   $
��auto �67   �� 5   &   $
��auto �67   .+�5    "   �� $��1��6-   .+�5!   &   �� $��15(�60   ��6>   ��  �� Path$
 Name��� 5"   &   $��12(�60   ��6>   �� $ ID��� 5#   &   $��12(�60   
���  ����6O   �� $	 VAC��� 5$   &   $��12(�60   ��6>   �� $ 
GameStatus�5%   #   �� 5'   "   $��4q�6(   $��0��63   $
��Left=�6C   $��50��6}   $��50��6�   ��6^   �� $ Avatar�6�   5(      5)      5*      5,      5-      � 5/   
   $��0��6    � RowDefinitions 51      �� 52      $
��auto �6    �� 53      $
��auto �6    �� 54      $
��auto �6    �� 55      $
��auto �6    �� 56      $
��auto �6    .+�59      �� $��10q�6   $��0��6%   .+�5:      �� $	��200��6   $��Get Info��6$   �� 5;      $��4q�6   $��
Get Online��6$   $	��200��6^   $��25��6j   ��69   �� *GetOnlineCommand+6j   �� 5<      $��4q�6   $��1��6$   $��Get All��61   $	��200��6e   $��25��6q   ��6C   �� *GetAllCommand+6q   �� 5=      ��6   �� *Status+5>      �� 5@      $��10q�6   $��1��6%   .+�5A      �� $	��200��6   $��Add to list��6$   �� 5B      $��4q�6   $	��200��6<   ��6%   �� *AddTB+6<   �� 5C      $��4q�6   $	��Add��6C   $	��200��6Q   $��25��6]   ��6$   �� *
AddCommand+6]   5D      �� 5F      $��10q�6   $��2��6%   .+�5G      �� $��4q�6   $	��200��6#   $��Remove from list��6/   �� 5H      $��4q�6;   $��Remove��6F   $	��200��6W   $��25��6c   ��6   �� *RemoveCommand+6c   5I      �� 5K      $��10q�6   $��3��6%   .+�5L      �� $	��200��6   $��Change list path��6$   �� 5M      $��4q�62   $	��200��6=   ��6   �� *PathTB+6=   �� 5N      $��4q�6?   $	��Set��6J   $	��200��6X   $��25��6d   ��6   �� *ChangePathCommand+6d   5O      �� 5Q      $��4��6   .+�5R      �� $	��200��6   $��Open in browser��6$   �� 5S      $��4q�6   $
��Open��6M   $	��200��6\   $��25��6h   ��6$   �� *OpenInBrowserCommand+6h   5T      5U      5V      5W      