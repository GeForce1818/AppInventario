   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5      +    %InventarioApp.Views.ProveedorListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:InventarioApp.Views     5      -   5        $��18(�5      
�������.��5      � $��10q�6     � RowDefinitions  5   
   �� 5      $��7* �6   �� 5      $��7* �6   �� 5      $	��79* �6   �� 5      $��7* �6    � ColumnDefinitions 5   
   �� 5      $	��20* �6   �� 5      $	��20* �6   �� 5      $	��20* �6   �� 5      $	��20* �6   �� 5      $	��15* �6   �� 5      $	��20* �6   .+�5   
   �� -   5         ��Name$ lblProveedorId��5      $��Proveedor ID��6(   $��Center=�6?   $��0��6Z   $��0��6g   �� 5   
   -   5         ��Name$ txtProveedorId��5      $	��140��6*   $��30��66   $��0��6B   $��1��6O   �� 5   
   -   5        $ lblProveedorNombre��5      $��Nombre Proveedor��6,   $��Center=�6G   $��0��6b   $��2��6o   $��17(�6   �� 5   
   -   5        $ txtProveedorNombre��5      $	��140��6.   $��30��6:   $��0��6F   $��3��6S   �� 5   
   -   5        $ lblNumeroTelefono��5      $��Número Teléfono��6+   $��Center=�6E   $��1��6`   $��0��6m   �� 5    
   -   5        $ txtNumeroTelefono��5       $	��140��6-   $��30��69   $��1��6E   $��1��6R   �� 5!   
   -   5        $ lblEmail��5!      $��Email��6"   $��Center=�62   $��1��6M   $��2��6Z   �� 5"   
   -	   5        $ txtEmail��5"      $	��140��6$   $��30��60   $��1��6<   $��3��6I   �� 5#   
   -
   5        $ lblDireccion��5#      $��
Dirección��6&   $��Center=�6:   $��1��6U   $��4��6b   �� 5$   
   -   5        $ txtDireccion��5$      $	��150��6(   $��30��64   $��1��6@   $��5��6M   �� 5%   
   -   5         ��Name$ 	btnBuscar��5%      $��Buscar��6$   $��0��65   $��4��6B   $��Center=�6R   $
��Left=�6m   $��90��6�   $��20,0,0,0q�6�    � Cursor$
 Hand��6�   �� 5&   
   -   5        $ 
btnLimpiar��5&      $��Limpiar��6%   $��0��67   $��5��6D   $��Center=�6T   $
��Left=�6o   $��90��6�   $��40,0,0,0q�6�   $
 Hand��6�   &   System.Windows.Controls.DataGrid  5(   
   -   5          Name$ gridProveedores��5(      $��2��6,   $��6��69      AutoGenerateColumns$ False��5)         
IsReadOnly$
 True��5*      	   SelectionMode$	 Single=�5+      $��	0,20,0,20q�6*   
   RowStyle
 5,      �� 5-       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5.      ��  �� Property � �� �� Value$ 30��6/   5/         Columns 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn   Header$ ID Proveedor��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 10* 6;   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *proveedor_id+  53      $ Nombre��6%   $	 20* 65    6A   �� *proveedor_nombre+  54      $ 	Teléfono��6%   $	 20* 67    6C   �� *numerotelefono+  55      $ Email��6%   $	 22* 64    6@   �� *email+  56      $ 
Dirección��6%   $	 15* 68    6D   �� *	direccion+58      �� 59   
   $��3��6   $��0��6"   $��5��62   $��Stretch=�6F    �� Orientation$ 
Horizontal=�6d   .+�5:      �� -   5        $ btnNuevo��5:      $��Nuevo��6'   $	��100��67   $��30��6C   $��0,0,15,0q�6O   $
 Hand��6x   �� 5;      -   5        $ btnActualizar��5;      $��
Actualizar��6,   $	��100��6A   $��30��6M   $��0,0,15,0q�6Y   $
 Hand��6�   �� 5<      -   5        $ btnEliminar��5<      $��Eliminar��6*   $	��100��6=   $��30��6I   $��0,0,15,0q�6U   $
 Hand��6�   �� 5=      -   5        $ btnImprimir��5=      $��Imprimir��6*   $	��100��6=   $��30��6I   $��
100,0,15,0q�6U   $
 Hand��6i   5>      5@      5A      