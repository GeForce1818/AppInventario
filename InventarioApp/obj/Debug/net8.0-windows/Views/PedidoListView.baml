   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5      (    "InventarioApp.Views.PedidoListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:InventarioApp.Views     5      -   5        $��18(�5	      
�������.��5      � $��10q�6     � RowDefinitions  5   
   �� 5      $��7* �6   �� 5      $��7* �6   �� 5      $	��79* �6   �� 5      $��7* �6    � ColumnDefinitions 5   
   �� 5      $	��15* �6   �� 5      $	��20* �6   �� 5      $	��13* �6   �� 5      $	��20* �6   �� 5      $	��40* �6   .+�5   
   �� -   5         ��Name$ lblIdPedido��5      $��	Id Pedido��6%   $��0��69   $��0��6F   $
��Left=�6V   $��Center=�6q   �� 5   
   -   5         ��Name$ txtPedidoId��5       �� 	MaxLength$ 10��6'   $��0��66   $��1��6C   $	��150��6S   $��30��6_   $
��Left=�6k   $��Center=�6�   �� 5   
   -   5        $ lblFechaPedido��5      $��Fecha de Pedido��6(   $��13(�6B   $��0��6P   $��2��6]   $
��Left=�6m   $��Center=�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ calFechaPedido��5      $��0��6-   $��3��6:   $��30��6J   $��Center=�6V   $��Center=�6s   $��0,5,0,0q�6�   $	��124��6�   �� 5!   
   -   5        $ lblProveedorId��5!      $��Proveedor Id��6(   $��1��6?   $��0��6L   $
��Left=�6\   $��Center=�6w   �� 5"   
   -   5         ��Name$ cmbProveedor��5"      $��1��6)   $��1��66   $	��150��6F   $��30��6R   $
��Left=�6^   $��Center=�6y   �� 5$   
   -   5        $ lblFechaEntrega��5$      $��Fecha de Entrega��6)   $��13(�6D   $��1��6R   $��2��6_   $
��Left=�6o   $��Center=�6�     5%   
   -	   5        $ calFechaEntrega��5%      $��1��6.   $��3��6;   $��30��6K   $��Center=�6W   $��Center=�6t   $��0,5,0,0q�6�   $	��124��6�   �� 5'   
   -
   5        $ lblTipoPagoId��5'      $��Tipo de Pago Id��6'   $��0��6A   $��4��6N   $
��Left=�6^   $��Center=�6y   $��10,0,0,0q�6�   �� 5(   
   -   5        $ cmbTipoPago��5(      $��4��6(   $
��Left=�68   $��	165,0,0,0q�6S   $��Center=�6f   $	��120��6�   �� 5*   
   -   5         ��Name$ 	btnBuscar��5*      $��Buscar��6$   $��1��65   $��4��6B   $
��Left=�6R   $��90��6m   $��78,4,0,5q�6x    � Cursor$
 Hand��6�   �� 5+   
   -   5        $ 
btnLimpiar��5+      $��Limpiar��6%   $��1��67   $��4��6D   $��Bottom=�6T   $
��Left=�6o   $��90��6�   $��	195,0,0,4q�6�   $
 Hand��6�   &   System.Windows.Controls.DataGrid  5.   
   -   5        	  Name$	 gridPedidos��5.      $��2��6(   $��5��65   
   AutoGenerateColumns$
 False��6I      
IsReadOnly$
 True��6e      SelectionMode$ Single=�6w   $��	0,20,0,20q�6�      RowStyle 5/      �� 50       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�51      ��  �� Property � �� �� Value$ 30��6/   52         Columns 54      0  *System.Windows.Controls.DataGridTextColumn  55      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Id Pedido��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 12* 68   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *	pedido_id+  56      $ Fecha de Pedido��6%   $	 20* 6>    6J   �� *pedido_fecha+ �� StringFormat$ 
dd/MM/yyyy��  57      $ 	Proveedor��6%   $	 15* 68    6D   �� *proveedor_nombre+  58      $ Fecha de Entrega��6%   $	 20* 6?    6K   �� *fecha_entrega+$ 
dd/MM/yyyy��  59      $ Tipo de Pago��6%   $	 15* 6;    6G   �� *descripcion+5;      �� 5>   
   $��3��6   $��0��6"   $��5��62   $��Stretch=�6F    �� Orientation$ 
Horizontal=�6d   .+�5?      �� -   5        $ btnNuevo��5?      $��Nuevo��6'   $	��100��67   $��30��6C   $��0,0,10,0q�6O   $
 Hand��6a   6o   �� 5@      -   5        $ btnActualizar��5@      $��
Actualizar��6,   $	��100��6A   $��30��6M   $��0,0,10,0q�6Y   $
 Hand��6�   �� 5A      -   5        $ btnEliminar��5A      $��Eliminar��6*   $	��100��6=   $��30��6I   $��0,0,10,0q�6U   $
 Hand��6�   5B      5C      5D      