   M S B A M L   `   `   `  ����  InventarioApp,    &InventarioApp.Views.InventarioListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        
�������.��5
      � $��20q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� CornerRadius$ 10��6   $��1q�60   
����������#fcfbfc._�5      � $��10q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'   .+�5      �� $��ID Inventario:��6   $��0��61   $��Center=�6A   $��Right=�6\   �� 5      -   5         ��Name$ txtInventarioId��5      $��1��63   $	��150��6C   $��25��6O   
�������6[   �� 5      $��Nombre:��6   $��2��6*   $��Center=�6:   $��Right=�6U   �� 5      -   5        $ txtInventarioNombre��5      $��3��67   $	��150��6G   $��25��6S   
�������6_   5       5!      �� 5$   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��0,10,0,0q�6X   .+�5%      �� -   5         ��Name$ btnNuevo��5%      $��Nuevo��6'   $	��100��67   $��5q�6C   6N   �� 5&      -   5        $ btnActualizar��5&      $��
Actualizar��6,   $	��100��6A   $��5q�6M   6X   �� 5'      -   5        $ btnEliminar��5'      $��Eliminar��6*   $	��100��6=   $��5q�6I   6T   �� 5(      -   5        $ 	btnBuscar��5(      $��Buscar��6(   $	��100��69   $��5q�6E   6P   �� 5)      -   5        $ 
btnLimpiar��5)      $��Limpiar��6)   $	��100��6;   $��5q�6G   6R   5*      &   System.Windows.Controls.DataGrid  5-   
   -	   5          Name$ gridInventarios��5-      $��2��6,      AutoGenerateColumns$ False��69      
IsReadOnly$
 True��6U   	   SelectionMode$	 Single=�6g   $��0,10,0,0q�6~   
   RowStyle
 5.      �� 5/       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�50      ��  �� Property � �� �� Value$ 30��6/   51         Columns 53      0  *System.Windows.Controls.DataGridTextColumn  54      ,  &System.Windows.Controls.DataGridColumn   Header$ ID Inventario��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6<   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   �� *inventario_id+  55      $ Nombre��6%   $ 2* 65    6@   �� *inventario_nombre+57      58      59      