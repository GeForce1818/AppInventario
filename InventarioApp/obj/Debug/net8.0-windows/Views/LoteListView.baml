   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5      &     InventarioApp.Views.LoteListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:InventarioApp.Views     5      -   5        
�������.��5      � $��20q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��5* �6   �� 5      $	��40* �6   .+�5   
   �� $��0��6    �� CornerRadius$ 10��6   $��1q�60   
����������#fcfbfc._�5      � $��10q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $	��10* �6'   �� 5      $
��Auto �6'   �� 5      $	��10* �6'   �� 5      $
��Auto �6'   �� 5      $	��10* �6'   �� 5      $
��Auto �6'   �� 5      $	��10* �6'   .+�5!      �� $��ID Lote:��6   $��0��6+   $��Center=�6;   $��Right=�6V   �� 5"      -   5         ��Name$ 	txtLoteId��5"      $��1��6-   $	��120��6=   $��25��6I   
�������6U   �� 5$      $��ID Producto:��6   $��2��6/   $��Center=�6?   $��Right=�6Z   �� 5%      -   5         ��Name$ cmbProductoId��5%      $��3��62   $	��120��6B   $��25��6N   
�������6Z   �� 5'      $��Fecha Fabricación:��6   $��4��65   $��Center=�6E   $��Right=�6`   (  "System.Windows.Controls.DatePicker  5(      -   5          Name$ dpFechaFabricacion��5(      $��5��69   $	��130��6I   $��25��6U   �� 5*      $��Fecha Vencimiento:��6   $��6��65   $��Center=�6E   $��Right=�6`     5+      -   5        $ dpFechaVencimiento��5+      $��7��69   $	��130��6I   $��25��6U   5,      5-      �� 50   
   $��1��6   $ 10��6   $��1q�60   
����������#fcfbfc$��0,10,0,0q�6p   ._�51      � $��10q�6    52      �� 53      $
��Auto �6'   �� 54      $��* �6'   �� 55      $
��Auto �6'   �� 56      $��* �6'   .+�59      �� $��Ubicación Lote:��6   $��0��62   $��Center=�6B   $��Right=�6]   �� 5:      -   5        $ txtLoteUbicacion��5:      $��1��64   $	��150��6D   $��25��6P   
�������6\   �� 5<      $��Estado Lote:��6   $��2��6/   $��Center=�6?   $��Right=�6Z   �� 5=      -   5        $ cmbLoteEstado��5=      $��3��62   $	��150��6B   $��25��6N   5>      5?      �� 5B   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��0,10,0,0q�6X   .+�5C      �� -   5         ��Name$ btnNuevo��5C      $��Nuevo��6'   $	��100��67   $��5q�6C   6N   �� 5D      -	   5        $ btnActualizar��5D      $��
Actualizar��6,   $	��100��6A   $��5q�6M   6X   �� 5E      -
   5        $ btnEliminar��5E      $��Eliminar��6*   $	��100��6=   $��5q�6I   6T   �� 5F      -   5        $ 	btnBuscar��5F      $��Buscar��6(   $	��100��69   $��5q�6E   6P   �� 5G      -   5        $ 
btnLimpiar��5G      $��Limpiar��6)   $	��100��6;   $��5q�6G   6R   5H      &   System.Windows.Controls.DataGrid  5K   
   -   5          Name$ 	gridLotes��5K      $��2��6&   $��2��63   	   AutoGenerateColumns$	 False��6D   
   
IsReadOnly$

 True��6`      SelectionMode$ Single=�6r   $��	0,50,0,10q�6�      RowStyle 5L      �� 5M       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5N      ��  �� Property � �� �� Value$ 30��6/   5O         Columns 5Q      0  *System.Windows.Controls.DataGridTextColumn  5R      ,  &System.Windows.Controls.DataGridColumn   Header$ ID Lote��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 66   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *	lote_id+  5S      $ ID Producto��6%   $ 2* 6:      
Visibility$ Hidden=�6e    6E   �� *producto_id+6e     5T      $ Producto��6%   $ 2* 67    6B   �� *nombre+  5U      $ Fecha Fabricación��6%   $
 1.5* 6@    6M   �� *fecha_fabricacion+  5V      $ Fecha Vencimiento��6%   $
 1.5* 6@    6M   �� *fecha_vencimiento+  5W      $ Ubicación Lote��6%   $ 2* 6=    6H   �� *lote_ubicacion+  5X      $ Estado Lote��6%   $ 2* 6:   $ Hidden=�6e    6E   �� *lote_estado+6e     5Y      $ Estado del Lote��6%   $ 2* 6>    6I   �� *estado_nombre+5[      5\      5]      