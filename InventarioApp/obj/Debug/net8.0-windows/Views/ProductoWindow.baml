   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5   	   (    "InventarioApp.Views.ProductoWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:InventarioApp.Views     5   	   -   5          � Title$  Producto��5      $��18(�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	   $	��450��5   	   $	��500��6   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFA9E309��6   � 5      $ White��6    � Offset$ 1Q�6)   5      .��5      � $��20q�6    � ColumnDefinitions 5   
   �� 5      $	��30* �6   �� 5      $	��70* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      $��2* �6   .+�5!   
   �� -   5        	 ��Name$	 lblProductoId��5!      $��Id Producto��6'   $��0��6=   $��0��6J   $��Center=�6Z   $
��Left=�6u   �� 5"   
   -   5        
 ��Name$
 txtProductoId��5"       �� 	MaxLength$ 6��6)   $��0��67   $��1��6D   $	��200��6T   $��30��6`   $��Center=�6l   $
��Left=�6�   �� 5#   
   -   5        $	 	lblNombre��5#      $��Nombre��6#   $��1��64   $��0��6A   $��Center=�6Q   $
��Left=�6l   �� 5$   
   -   5        $
 	txtNombre��5$      $	��200��6%   $��30��61   $��1��6=   $��1��6J   $��Center=�6Z   $
��Left=�6u   �� 5%   
   -   5        $	 lblDescripcion��5%      $��Descripción��6(   $��2��6>   $��0��6K   $��Center=�6[   $
��Left=�6v   �� 5&   
   -   5        $
 txtDescripcion��5&      $	��300��6*   $��30��66   $��2��6B   $��1��6O   $��Center=�6_   $
��Left=�6z   �� 5'   
   -   5        $	 lblPreciouni��5'      $��Precio unitario��6&   $��3��6@   $��0��6M   $��Center=�6]   $
��Left=�6x   �� 5(   
   -	   5        $
 txtPreciouni��5(      $	��200��6(   $��30��64   $��3��6@   $��1��6M   $��Center=�6]   $
��Left=�6x   �� 5)   
   -
   5        $	 lblCategoria��5)      $��
Categoría��6&   $��4��6:   $��0��6G   $��Center=�6W   $
��Left=�6r   �� 5*   
   -   5         ��Name$ cmbCategoria��5*      $��4��6)   $��1��66   $��30��6F   $	��200��6R   $��Center=�6^   $
��Left=�6y   �� 5+   
   -   5        $	 lblInventario��5+      $��
Inventario��6'   $��5��6<   $��0��6I   $
��Left=�6Y   $��Center=�6t   �� 5,   
   -   5        $ cmbInventario��5,      $��5��6*   $��3��67   $��30��6G   $	��200��6S   $��Center=�6_   $
��Left=�6z   �� 5-   
   $��7��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   $��Bottom=�6|   .+�5.      �� -   5         ��Name$ 
btnGuardar��5.      $��Guardar��6)   $��80��6;   $��30��6F   $��	0,0,15,10q�6R    � Cursor$
 Hand��6~   �� 5/      -   5        $ 	btnCerrar��5/      $��Cerrar��6(   $��80��69   $��30��6D   $��	0,0,15,10q�6P   $
 Hand��6|   50      52      53      