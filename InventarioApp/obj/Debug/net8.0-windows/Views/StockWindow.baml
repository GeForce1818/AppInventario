   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5   	   %    InventarioApp.Views.StockWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:InventarioApp.Views     5   	   -   5          � Title$  Stock��5   	   $��18(�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	   $	��450��5   	   $	��500��6   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFA9E309��6   � 5      $ White��6    � Offset$ 1Q�6)   5      .��5      � $��20q�6    � ColumnDefinitions 5   
   �� 5      $	��56* �6   �� 5      $	��79* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5       �� 5!      $��2* �6   .+�5%   
   �� $��ID de Stock��6   $��0��6&   $��0��63   $��Center=�6C   $
��Left=�6^   $��34��6y   $	��120��6�   �� 5&   
   -   5        	 ��Name$	 
txtStockId��5&      $��0��6&   $��1��63   $	��200��6C   $��30��6O   $��Center=�6[   $
��Left=�6v   
 �� 	MaxLength$
 10��6�   �� 5(   
   $��
ID de Lote��6   $��1��6%   $��0��62   $��Center=�6B   $
��Left=�6]   $��34��6x   $	��120��6�   �� 5)   
   -   5         ��Name$ 	cmbLoteId��5)      $��1��6&   $��1��63   $	��200��6C   $��30��6O   $��Center=�6[   $
��Left=�6v   �� 5+   
   $��Cantidad Disponible��6   $��2��6.   $��0��6;   $��Center=�6K   $
��Left=�6f   $��34��6�   $	��190��6�   �� 5,   
   -   5        $	 txtCantidadDisponible��5,      $��2��61   $��1��6>   $	��200��6N   $��30��6Z   $��Center=�6f   $
��Left=�6�   �� 5.   
   $��Estado de Stock��6   $��3��6*   $��0��67   $��Center=�6G   $
��Left=�6b   $��34��6}   $	��190��6�   �� 5/   
   -   5        $ cmbStockEstado��5/      $��3��6+   $��1��68   $	��200��6H   $��30��6T   $��Center=�6`   $
��Left=�6{   �� 53   
   $��5��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $
��Left=�6_   $	��Top=�6z   $��40��6�   $��
135,58,0,0q�6�   $	��190��6�   .+�54      �� -   5         ��Name$ 
btnGuardar��54      $��Guardar��6)   $��80��6;   $��30��6F   $��	0,0,15,10q�6R    � Cursor$
 Hand��6~   �� 55      -   5        $ 	btnCerrar��55      $��Cerrar��6(   $��80��69   $��30��6D   $��	0,0,15,10q�6P   $
 Hand��6{   56      57      58      