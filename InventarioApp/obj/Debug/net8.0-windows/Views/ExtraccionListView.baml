   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5      ,    &InventarioApp.Views.ExtraccionListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:InventarioApp.Views     5      -   5        
�������.��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $��7* �6   �� 5      $��7* �6   �� 5      $	��60* �6   �� 5      $��7* �6    � ColumnDefinitions 5   
   �� 5      $	��17* �6   �� 5      $	��20* �6   �� 5      $	��20* �6   �� 5      $	��25* �6   �� 5      $	��15* �6   �� 5      $	��25* �6   �� 5      $	��15* �6   .+�5   
   �� -   5         ��Name$ lblExtraccionId��5      $��Extraccion ID��6)   $��Center=�6A   $��0��6\   $��26��6l   �� 5   
   -   5         ��Name$ txtExtraccionId��5      $	��110��6+   $��30��67   $��0��6C   $��1��6P   $��8,9,8,9q�6`   �� 5   
   -   5        $ lblproductoId��5      $��Producto��6'   $��Center=�6:   $��2��6U   $��26��6e   �� 5   
   -   5         ��Name$ cmbProducto��5      $��0��6(   $��3��65   $��8,9,8,9q�6E   �� 5   
   -   5        $ 	lblLoteId��5      $��Lote ID��6#   $��Center=�65   $��0��6P   $��4��6]   $��26��6m   �� 5    
   -   5        $ 	cmbLoteId��5       $	��140��6&   $��30��63   $��0��6?   $��5��6L   $��1��6\   $��0,8,0,9q�6p   �� 5!   
   -   5        $ lblContidadId��5!      $��Cantidad a extraer��6'   $��Center=�6D   $��1��6_   $��0��6l   $��26��6|   �� 5"   
   -	   5        $ txtCantidadId��5"      $	��110��6)   $��30��65   $��1��6A   $��1��6N   $��8,9,8,9q�6^   �� 5#   
   -
   5        $ lblFechaExtraccion��5#      $��Fecha Extracción��6,   $��1��6G   $��2��6T   $
��Left=�6d   $��Center=�6   (  "System.Windows.Controls.DatePicker  5$   
   -   5          Name$ calFechaExtraccion��5$      $��1��61   $��3��6>   $��30��6N   $
��Left=�6Z   $��Center=�6u   �� 5%   
   -   5         ��Name$ 
btnExtraer��5%      $��Extraer��6%   $��80��67   $��Center=�6B   $��0��6_   $��6��6l   $��0,9,0,9q�6}    � Cursor$
 Hand��6�   6�   �� 5&   
   -   5        $ btnVerificar��5&      $��Verificar Stock��6'   $��80��6A   $��Center=�6L   $��1��6i   $��5��6v   $��0,9,0,9q�6�   $
 Hand��6�   6�   �� 5'   
   -   5        $ 
btnLimpiar��5'      $��Limpiar��6%   $��80��67   $��Center=�6B   $��1��6_   $��6��6l   $��0,8,0,0q�6|   $
 Hand��6�   $��30��6�   $	��Top=�6�   &   System.Windows.Controls.DataGrid  5)   
   -   5          Name$ gridExtraccion��5)      $��2��6+   $��7��68   	   AutoGenerateColumns$	 False��5*      
   
IsReadOnly$

 True��5+         SelectionMode$ Single=�5,      $��	0,20,0,20q�6*      RowStyle 5-      �� 5.       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5/      ��  �� Property � �� �� Value$ 30��6/   50         Columns 52      0  *System.Windows.Controls.DataGridTextColumn  53      ,  &System.Windows.Controls.DataGridColumn   Header$ ID Extracción��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 10* 6<   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *extraccion_id+  54      $ ID Producto��6%   $ 0 6:      
Visibility$ Hidden=�6d    6D   �� *producto_id+6d     55      $ Nombre de Producto��6%   $	 22* 6A    6M   �� *nombre+  56      $ ID lote��6%   $	 10* 66    6B   �� *	lote_id+  57      $ Cantidad Extraida��6%   $ 8* 6@    6K   �� *cantidad_extraida+  58      $ Fecha de Extracción��6%   $	 12* 6B    6N   �� *fecha_extraccion+ �� StringFormat$ {0:dd/MM/yyyy}��5:      �� 5;   
   $��5��6   $��0��6"   $��7��62    �� Orientation$ 
Horizontal=�6F   $��0,5,0,0q�6_   $��2��6p   .+�5<      �� -   5        $ btnEliminar��5<      $��Eliminar��6*   $	��100��6=   $��30��6I   $��0,0,15,0q�6U   $
 Hand��6�   5>      5@      5A      