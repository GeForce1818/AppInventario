   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5      *    $InventarioApp.Views.ProductoListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:InventarioApp.Views     5      -   5        $��18(�5	      
�������.��5      � $��10q�6     � RowDefinitions  5   
   �� 5      $��7* �6   �� 5      $��7* �6   �� 5      $	��79* �6   �� 5      $��7* �6    � ColumnDefinitions 5   
   �� 5      $	��15* �6   �� 5      $	��20* �6   �� 5      $	��13* �6   �� 5      $	��20* �6   �� 5      $	��40* �6   .+�5   
   �� -   5         ��Name$ lblIdproducto��5      $��Id producto��6'   $��0��6=   $��0��6J   $
��Left=�6Z   $��Center=�6u   �� 5   
   -   5         ��Name$ txtProductoId��5       �� 	MaxLength$ 10��6)   $��0��68   $��1��6E   $	��150��6U   $��30��6a   $
��Left=�6m   $��Center=�6�   �� 5   
   -   5        $ 	lblNombre��5      $��Nombre��6#   $��1��64   $��0��6A   $
��Left=�6Q   $��Center=�6l   �� 5   
   -   5        $ 	txtNombre��5      $��1��6%   $��1��62   $	��150��6B   $��30��6N   $
��Left=�6Z   $��Center=�6u   $��2��6�   �� 5   
   -   5        $ lblCategoria��5      $��
Categoría��6&   $��0��6:   $��2��6G   $
��Left=�6W   $��Center=�6r   �� 5   
   -   5         ��Name$ cmbCategoria��5      $��0��6)   $��3��66   $��30��6F   $��Center=�6R   $��0q�6m   �� 5    
   -   5        $ lblInventario��5       $��
Inventario��6'   $��1��6<   $��2��6I   $
��Left=�6Y   $��Center=�6t   �� 5!   
   -	   5        $ cmbInventario��5!      $��1��6*   $��3��67   $��30��6G   $��Center=�6S   $��0q�6n   �� 5#   
   -
   5         ��Name$ 	btnBuscar��5#      $��Buscar��6$   $��0��65   $��4��6B   $��Center=�6R   $
��Left=�6m   $��90��6�   $��40,0,0,0q�6�    � Cursor$
 Hand��6�   �� 5$   
   -   5        $ 
btnLimpiar��5$      $��Limpiar��6%   $��1��67   $��4��6D   $��Center=�6T   $
��Left=�6o   $��90��6�   $��40,0,0,0q�6�   $
 Hand��6�   &   System.Windows.Controls.DataGrid  5&   
   -   5          Name$ gridProductos��5&      $��2��6*   $��5��67   	   AutoGenerateColumns$	 False��6K   
   
IsReadOnly$

 True��6g      SelectionMode$ Single=�6y   $��	0,20,0,20q�6�      RowStyle 5'      �� 5(       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5)      ��  �� Property � �� �� Value$ 30��6/   5*         Columns 5,      0  *System.Windows.Controls.DataGridTextColumn  5-      ,  &System.Windows.Controls.DataGridColumn   Header$ Id producto��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 12* 6:   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   �� *producto_id+  5.      $ Nombre��6%   $	 20* 65    6A   �� *nombre+  5/      $ Descripción��6%   $	 40* 6:    6F   �� *descripcion+  50      $ Cantidad��6%   $	 12* 67    6C   �� *cantidad_total+  51      $ Precio unitario��6%   $	 10* 6>    6J   �� *preciounitario+ �� StringFormat$ C2��  52      $ Idcategoria��6%   $ 0 6:      
Visibility$ Hidden=�6e    6D   �� *categoria_id+6e     53      $ 
Categoría��6%   $	 15* 68    6D   �� *categoria_nombre+  54      $ IdInventario��6%   $ 0 6;   $ Hidden=�6g    6E   �� *inventario_id+6g     55      $ 
Inventario��6%   $	 15* 69    6E   �� *inventario_nombre+57      �� 58   
   $��3��6   $��0��6"   $��5��62   $��Stretch=�6F    �� Orientation$ 
Horizontal=�6d   .+�59      �� -   5        $ btnActualizar��59      $��
Actualizar��6,   $	��100��6A   $��30��6M   $��0,0,10,0q�6Y   $
 Hand��6�   �� 5:      -   5        $ btnEliminar��5:      $��Eliminar��6*   $	��100��6=   $��30��6I   $��0,0,10,0q�6U   $
 Hand��6�   5<      5=      5>      