   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5      -    'InventarioApp.Views.PedidoNuevoListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:InventarioApp.Views     5      -   5        $��18(�5	      
�������.��5      � $��10q�6     � RowDefinitions  5   
   �� 5      $��7* �6   �� 5      $��7* �6   �� 5      $	��79* �6   �� 5      $��7* �6    � ColumnDefinitions 5   
   �� 5      $	��15* �6   �� 5      $	��20* �6   �� 5      $	��13* �6   �� 5      $	��20* �6   �� 5      $	��40* �6   .+�5   
   �� -   5         ��Name$ lblIdPedido��5      $��Id N. Pedido��6%   $��0��6<   $��0��6I   $
��Left=�6Y   $��Center=�6u   �� 5   
   -   5         ��Name$ txtNPedidoId��5       �� 	MaxLength$ 10��6(   $��0��67   $��1��6D   $	��150��6T   $��30��6`   $
��Left=�6l   $��Center=�6�   �� 5   
   -   5        $ lblFechaPedido��5      $��Fecha de Pedido��6(   $��13(�6B   $��1��6P   $��Center=�6`   $��2��6{   $��	163,0,0,0q�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ calNFechaPedido��5      $��0��6.   $��3��6;   $��30��6K   $��Center=�6W   $��Center=�6t   $��0,5,0,0q�6�   $	��124��6�   �� 5!   
   -   5        $ lblProveedorId��5!      $��Proveedor Id��6(   $��1��6?   $��0��6L   $
��Left=�6\   $��Center=�6w   �� 5"   
   -   5         ��Name$ cmbNProveedor��5"      $��1��6*   $��1��67   $	��150��6G   $��30��6S   $
��Left=�6_   $��Center=�6z   �� 5$   
   -   5        $ lblFechaEntrega��5$      $��Fecha de Entrega��6)   $��13(�6D   $��1��6R   $��2��6_   $
��Left=�6o   $��Center=�6�     5%   
   -	   5        $ calNFechaEntrega��5%      $��1��6/   $��3��6<   $��30��6L   $��Center=�6X   $��Center=�6u   $��0,5,0,0q�6�   $	��124��6�   �� 5'   
   -
   5        $ lblTipoPagoId��5'      $��Tipo de Pago Id��6'   $��0��6A   $��4��6N   $
��Left=�6^   $��Center=�6y   $��10,0,0,0q�6�   �� 5(   
   -   5        $ cmbNTipoPago��5(      $��4��6)   $
��Left=�69   $��	165,0,0,0q�6T   $��Center=�6g   $	��120��6�   �� 5*   
   -   5         ��Name$ 	btnBuscar��5*      $��Buscar��6$   $��1��65   $��4��6B   $
��Left=�6R   $��90��6m   $��78,4,0,5q�6x    � Cursor$
 Hand��6�   �� 5+   
   -   5        $ 
btnLimpiar��5+      $��Limpiar��6%   $��1��67   $��4��6D   $��Bottom=�6T   $
��Left=�6o   $��90��6�   $��	195,0,0,4q�6�   $
 Hand��6�   &   System.Windows.Controls.DataGrid  5.   
   -   5        	  Name$	 gridPedidoNuevo��5.      $��2��6,   $��5��69   
   AutoGenerateColumns$
 False��6M      
IsReadOnly$
 True��6i      SelectionMode$ Single=�6{   $��	0,20,0,20q�6�      RowStyle 5/      �� 50       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�51      ��  �� Property � �� �� Value$ 30��6/   52         Columns 54      0  *System.Windows.Controls.DataGridTextColumn  55      ,  &System.Windows.Controls.DataGridColumn   Header$ Id N. Pedido��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 12* 6;   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *pedido_nuevo_id+  56      $ Fecha de Pedido��6%   $	 20* 6>    6J   �� *pedido_nuevo_fecha+ �� StringFormat$ 
dd/MM/yyyy��  57      $ 	Proveedor��6%   $	 15* 68    6D   �� *proveedor_nombre+  58      $ Fecha de Entrega��6%   $	 20* 6?    6K   �� *fecha_entrega+$ 
dd/MM/yyyy��  59      $ Tipo de Pago��6%   $	 15* 6;    6G   �� *descripcion+5;      �� 5>   
   $��3��6   $��0��6"   $��5��62   $��Stretch=�6F    �� Orientation$ 
Horizontal=�6d   .+�5?      �� -   5        $ btnNuevo��5?      $��Nuevo��6'   $	��100��67   $��30��6C   $��0,0,10,0q�6O   $
 Hand��6a   6o   �� 5@      -   5        $ btnActualizar��5@      $��
Actualizar��6,   $	��100��6A   $��30��6M   $��0,0,10,0q�6Y   $
 Hand��6�   �� 5A      -   5        $ btnEliminar��5A      $��Eliminar��6*   $	��100��6=   $��30��6I   $��0,0,10,0q�6U   $
 Hand��6�   5B      5D      5E      