   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5      '    !InventarioApp.Views.StockListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:InventarioApp.Views     5      -   5        $��18(�5      
�������.��5      � $��10q�6     � RowDefinitions  5   
   �� 5      $��7* �6   �� 5      $��7* �6   �� 5      $	��79* �6   �� 5      $��7* �6    � ColumnDefinitions 5   
   �� 5      $	��24* �6   �� 5      $	��20* �6   �� 5      $	��20* �6   �� 5      $	��20* �6   �� 5      $	��40* �6   .+�5   
   �� -   5         ��Name$ 
lblStockId��5      $��Stock ID��6$   $��Center=�67   $��0��6R   $��0��6_   �� 5   
   -   5         ��Name$ 
txtStockId��5      $	��140��6&   $��30��62   $��0��6>   $��1��6K   �� 5   
   -   5        $ 	lblLoteId��5      $��Lote ID��6#   $��Center=�65   $��0��6P   $��2��6]   �� 5   
   -   5         ��Name$ 	cmbLoteId��5      $	��140��6&   $��30��62   $��0��6>   $��3��6K   �� 5   
   -   5        $ lblCantidadDisponible��5      $��Cantidad Disponible��6/   $��Center=�6M   $��1��6h   $��0��6u   �� 5   
   -   5        $ txtCantidadDisponible��5      $	��140��61   $��30��6=   $��1��6I   $��1��6V   �� 5    
   -   5        $ lblStockEstado��5       $��Estado del Stock��6(   $��Center=�6C   $��1��6^   $��2��6k   �� 5!   
   -	   5        $ cmbStockEstado��5!      $	��140��6+   $��30��67   $��1��6C   $��3��6P   �� 5"   
   -
   5         ��Name$ 	btnBuscar��5"      $��Buscar��6$   $��0��65   $��4��6B   $��Center=�6R   $
��Left=�6m   $��90��6�   $��40,0,0,0q�6�    � Cursor$
 Hand��6�   �� 5#   
   -   5        $ 
btnLimpiar��5#      $��Limpiar��6%   $��1��67   $��4��6D   $��Center=�6T   $
��Left=�6o   $��90��6�   $��40,0,0,0q�6�   $
 Hand��6�   &   System.Windows.Controls.DataGrid  5%   
   -   5          Name$ 	gridStock��5%      $��2��6&   $��5��63      AutoGenerateColumns$ False��5&      	   
IsReadOnly$
	 True��5'      
   SelectionMode$
 Single=�5(      $��	0,20,0,20q�6*      RowStyle 5)      �� 5*       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5+      ��  �� Property � �� �� Value$ 30��6/   5,         Columns 5.      0  *System.Windows.Controls.DataGridTextColumn  5/      ,  &System.Windows.Controls.DataGridColumn   Header$ ID Stock��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 10* 67   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *
stock_id+  50      $ Lote ID��6%   $	 20* 66    6B   �� *	lote_id+  51      $ Cantidad Disponible��6%   $	 22* 6B    6N   �� *cantidad_disponible+  52      $ 	Estado ID��6%   $	 20* 68      
Visibility$ Hidden=�6e    6D   �� *stock_estado+6e     53      $ Estado��6%   $	 20* 65    6A   �� *estado_nombre+55      �� 56   
   $��3��6   $��0��6"   $��5��62   $��Stretch=�6F    �� Orientation$ 
Horizontal=�6d   .+�57      �� -   5        $ btnNuevo��57      $��Nuevo��6'   $	��100��67   $��30��6C   $��0,0,15,0q�6O   $
 Hand��6x   �� 58      -   5        $ btnActualizar��58      $��
Actualizar��6,   $	��100��6A   $��30��6M   $��0,0,15,0q�6Y   $
 Hand��6�   �� 59      -   5        $ btnEliminar��59      $��Eliminar��6*   $	��100��6=   $��30��6I   $��0,0,15,0q�6U   $
 Hand��6�   5:      5<      5=      