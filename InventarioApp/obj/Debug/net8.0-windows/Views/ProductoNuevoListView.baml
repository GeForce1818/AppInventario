   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5      /    )InventarioApp.Views.ProductoNuevoListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:InventarioApp.Views     5      -   5        $��18(�5      
�������.��5      � $��10q�6     � RowDefinitions  5   
   �� 5      $��7* �6   �� 5      $��7* �6   �� 5      $	��79* �6   �� 5      $��7* �6    � ColumnDefinitions 5   
   �� 5      $	��19* �6   �� 5      $	��20* �6   �� 5      $	��26* �6   �� 5      $	��20* �6   �� 5      $	��25* �6   �� 5      $	��20* �6   �� 5      $	��15* �6   �� 5      $	��20* �6   .+�5   
   �� -   5         ��Name$ lblProductoId��5      $��Producto ID��6'   $��Center=�6=   $��0��6X   $��0��6e   �� 5   
   -   5         ��Name$ txtProductoId��5      $	��100��6)   $��30��65   $��0��6A   $��1��6N   �� 5   
   -   5        $ lblProducto��5      $��Nombre��6%   $��Center=�66   $��0��6Q   $��2��6^   �� 5    
   -   5        $ 	txtnombre��5       $	��100��6%   $��30��61   $��0��6=   $��3��6J   �� 5!   
   -   5        $ lblCategoriaNombre��5!      $��
Categoría��6,   $��Center=�6@   $��0��6[   $��4��6h   �� 5"   
   -   5         ��Name$ cmbCategoriaNombre��5"      $	��100��6/   $��30��6;   $��0��6G   $��5��6T   �� 5#   
   -   5        $ lblDescripcion��5#      $��Descripción��6(   $��Center=�6>   $��1��6Y   $��0��6f   �� 5$   
   -	   5        $ txtDescripcion��5$      $	��100��6*   $��30��66   $��1��6B   $��1��6O   �� 5%   
   -
   5        $ lblPrecioEstimado��5%      $��Precio Estimado��6+   $��Center=�6E   $��1��6`   $��2��6m   �� 5&   
   -   5        $ txtPrecioEstimado��5&      $	��100��6-   $��30��69   $��1��6E   $��3��6R   �� 5'   
   -   5        $ lblEstadoRegistro��5'      $��Estado Registro��6+   $��Center=�6E   $��1��6`   $��4��6m   �� 5(   
   -   5        $ cmbEstadoRegistro��5(      $	��100��6.   $��30��6:   $��1��6F   $��5��6S   �� 5)   
   -   5         ��Name$ 	btnBuscar��5)      $��Buscar��6$   $��0��65   $��6��6B   $��Center=�6R   $
��Left=�6m   $��70��6�   $��8,0,0,0q�6�    � Cursor$
 Hand��6�   �� 5*   
   -   5        $ 
btnLimpiar��5*      $��Limpiar��6%   $��0��67   $��7��6D   $��Center=�6T   $
��Left=�6o   $��90��6�   $��10,0,0,0q�6�   $
 Hand��6�   &   System.Windows.Controls.DataGrid  5,   
   -   5          Name$ gridProductos��5,      $��2��6*   $��8��67      AutoGenerateColumns$ False��5-      	   
IsReadOnly$
	 True��5.      
   SelectionMode$
 Single=�5/      $��	0,20,0,20q�6*      RowStyle 50      �� 51       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�52      ��  �� Property � �� �� Value$ 30��6/   53         Columns 55      0  *System.Windows.Controls.DataGridTextColumn  56      ,  &System.Windows.Controls.DataGridColumn   Header$ ID Producto��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 10* 6:   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   �� *producto_nuevo_id+  57      $ Nombre��6%   $	 20* 65    6A   �� *nombre+  58      $ Descripción��6%   $	 22* 6:    6F   �� *descripcion+  59      $ Precio Estimado��6%   $	 15* 6>    6J   �� *precio_estimado+  5:      $ ID Categoría��6%   $	 15* 6;      
Visibility$ Hidden=�6h    6G   �� *categoria_id+6h     5;      $ 
Categoría��6%   $	 15* 68    6D   �� *categoria_nombre+  5<      $ Registro��6%   $	 15* 67   $ Hidden=�6g    6C   �� *estado_registro+6g     5=      $ Estado Registro��6%   $	 15* 6>    6J   �� *estado+5?      �� 5@   
   $��3��6   $��0��6"   $��5��62   $��Stretch=�6F    �� Orientation$ 
Horizontal=�6d   .+�5A      �� -   5        $ btnNuevo��5A      $��Nuevo��6'   $	��100��67   $��30��6C   $��0,0,15,0q�6O   $
 Hand��6x   �� 5B      -   5        $ btnActualizar��5B      $��
Actualizar��6,   $	��100��6A   $��30��6M   $��0,0,15,0q�6Y   $
 Hand��6�   �� 5C      -   5        $ btnEliminar��5C      $��Eliminar��6*   $	��100��6=   $��30��6I   $��0,0,15,0q�6U   $
 Hand��6�   5D      5F      5G      