   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5   	   $    InventarioApp.Views.LoteWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:InventarioApp.Views     5   	   -   5          � Title$
  Lote��5   	   $��18(�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	   $	��450��5   	   $	��500��6   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFA9E309��6   � 5      $ White��6    � Offset$ 1Q�6)   5      .��5      � $��20q�6    � ColumnDefinitions 5   
   �� 5      $	��36* �6   �� 5      $	��79* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      $��2* �6   .+�5#   
   �� -   5        	 ��Name$	 	lblLoteId��5#      $��Id Lote��6#   $��0��65   $��Center=�6E   $
��Left=�6`   $��34��6{   $��65��6�   �� 5$   
   -   5        
 ��Name$
 	txtLoteId��5$       �� 	MaxLength$ 8��6%   $��1��63   $	��200��6C   $��30��6O   $��Center=�6[   $
��Left=�6v   �� 5&   
   -   5        $	 lblProductoId��5&      $��Producto Id��6'   $��1��6=   $��0��6J   $��Center=�6Z   $
��Left=�6u   $��34��6�   $	��103��6�   �� 5'   
   -   5         ��Name$ cmbProductoId��5'      $��1��6*   $��1��67   $	��200��6G   $��30��6S   $��Center=�6_   $
��Left=�6z   �� 5)   
   -   5        $	 lblFechaFabricacion��5)      $��Fch Fabricación��6-   $��2��6G   $��0��6T   $��Center=�6d   $
��Left=�6   $��34��6�   $	��132��6�   (  "System.Windows.Controls.DatePicker  5*   
   -   5          Name$ dpFechaFabricacion��5*      $��2��61   $��1��6>   $	��200��6N   $��30��6Z   $��Center=�6f   $
��Left=�6�   $��10,0,0,0q�6�   �� 5,   
   -   5        $	 lblFechaVencimiento��5,      $��Fch Vencimiento��6-   $��3��6G   $��0��6T   $��Center=�6d   $
��Left=�6   $��34��6�   $	��141��6�     5-   
   -	   5        $ dpFechaVencimiento��5-      $��3��61   $��1��6>   $	��200��6N   $��30��6Z   $��Center=�6f   $
��Left=�6�   $��10,0,0,0q�6�   �� 5/   
   -
   5        $	 lblLoteUbicacion��5/      $��Ubicación Lote��6*   $��4��6C   $��0��6P   $��Center=�6`   $
��Left=�6{   $��34��6�   $	��128��6�   �� 50   
   -   5        $
 txtLoteUbicacion��50      $	��300��6,   $��30��68   $��4��6D   $��1��6Q   $��Center=�6a   $
��Left=�6|   �� 52   
   -   5        $	 lblLoteEstado��52      $��Estado Lote��6'   $��5��6=   $��0��6J   $��Center=�6Z   $
��Left=�6u   $��34��6�   $	��103��6�   �� 53   
   -   5        $ cmbLoteEstado��53      $��5��6*   $��1��67   $��30��6G   $	��200��6S   $��Center=�6_   $
��Left=�6z   �� 56   
   $��6��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $
��Left=�6_   $	��Top=�6z   $��40��6�   $��
135,58,0,0q�6�   $	��190��6�   .+�57      �� -   5         ��Name$ 
btnGuardar��57      $��Guardar��6)   $��80��6;   $��30��6F   $��	0,0,15,10q�6R    � Cursor$
 Hand��6~   �� 58      -   5        $ 	btnCerrar��58      $��Cerrar��6(   $��80��69   $��30��6D   $��	0,0,15,10q�6P   $
 Hand��6|   59      5:      5;      