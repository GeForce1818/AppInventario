   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5   	   -    'InventarioApp.Views.ProductoNuevoWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:InventarioApp.Views     5   	   -   5          � Title$  Producto Nuevo��5   	   $��18(�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	   $	��450��5   	   $	��500��6   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFA9E309��6   � 5      $ White��6    � Offset$ 1Q�6)   5      .��5      � $��20q�6    � ColumnDefinitions 5   
   �� 5      $	��46* �6   �� 5      $	��79* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       �� 5!      $��2* �6   .+�5&   
   �� $��Producto Nuevo ID��6   $��0��6,   $��0��69   $��Center=�6I   $
��Left=�6d   �� 5'   
   -   5        	 ��Name$	 txtProductoNuevoId��5'      $��0��6.   $��1��6;   $	��200��6K   $��30��6W   $��Center=�6c   $
��Left=�6~   �� 5)   
   $��Nombre��6   $��1��6!   $��0��6.   $��Center=�6>   $
��Left=�6Y   �� 5*   
   -   5        $	 	txtNombre��5*      $��1��6%   $��1��62   $	��200��6B   $��30��6N   $��Center=�6Z   $
��Left=�6u   �� 5,   
   $��Descripción��6   $��2��6&   $��0��63   $��Center=�6C   $
��Left=�6^   �� 5-   
   -   5        $	 txtDescripcion��5-      $��2��6*   $��1��67   $	��200��6G   $��30��6S   $��Center=�6_   $
��Left=�6z   �� 5/   
   $��Precio Estimado��6   $��3��6*   $��0��67   $��Center=�6G   $
��Left=�6b   �� 50   
   -   5        $	 txtPrecioEstimado��50      $��3��6-   $��1��6:   $	��200��6J   $��30��6V   $��Center=�6b   $
��Left=�6}   �� 52   
   $��
Categoría��6   $��4��6$   $��0��61   $��Center=�6A   $
��Left=�6\   �� 53   
   -   5        
 ��Name$
 cmbCategoria��53      $��4��6)   $��1��66   $	��200��6F   $��30��6R   $��Center=�6^   $
��Left=�6y   �� 55   
   $��Estado Registro��6   $��5��6*   $��0��67   $��Center=�6G   $
��Left=�6b   �� 56   
   -   5        $
 cmbRegistro��56      $��5��6(   $��1��65   $	��200��6E   $��30��6Q   $��Center=�6]   $
��Left=�6x   �� 59   
   $��7��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   $	��Top=�6|   $��40��6�   $��0,20,0,0q�6�   .+�5:      �� -   5         ��Name$ 
btnGuardar��5:      $��Guardar��6)   $��80��6;   $��30��6F   $��0,0,15,0q�6R    � Cursor$
 Hand��6}   �� 5;      -	   5        $ 	btnCerrar��5;      $��Cerrar��6(   $��80��69   $��30��6D   $��0,0,15,0q�6P   $
 Hand��6z   5<      5=      5>      