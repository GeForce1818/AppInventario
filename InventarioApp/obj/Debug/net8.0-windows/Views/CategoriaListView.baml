   M S B A M L   `   `   `  ����  InventarioApp+    %InventarioApp.Views.CategoriaListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        
�������.��5
      � $��20q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� CornerRadius$ 10��6   $��1q�60   
����������#fcfbfc._�5      � $��10q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'   .+�5      �� $��ID Categoría:��6   $��0��60   $��Center=�6@   $��Right=�6[   �� 5      -   5         ��Name$ txtCategoriaId��5      $��1��62   $	��150��6B   $��25��6N   
�������6Z   �� 5       $��Nombre:��6   $��2��6*   $��Center=�6:   $��Right=�6U   �� 5!      -   5        $ txtCategoriaNombre��5!      $��3��66   $	��150��6F   $��25��6R   
�������6^   �� 5#      $��Descripción:��6   $��4��6/   $��Center=�6?   $��Right=�6Z   �� 5$      -   5        $ txtDescripcion��5$      $��5��62   $��60��6B   $	��Top=�6N   
������� �� AcceptsReturn$
 True��6}    �� TextWrapping$
 Wrap=�6�   $��2��6�   5%      5&      �� 5)   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��0,10,0,0q�6X   .+�5*      �� -   5         ��Name$ btnNuevo��5*      $��Nuevo��6'   $	��100��67   $��5q�6C   6N   �� 5+      -   5        $ btnActualizar��5+      $��
Actualizar��6,   $	��100��6A   $��5q�6M   6X   �� 5,      -   5        $ btnEliminar��5,      $��Eliminar��6*   $	��100��6=   $��5q�6I   6T   �� 5-      -   5        $ 	btnBuscar��5-      $��Buscar��6(   $	��100��69   $��5q�6E   6P   �� 5.      -	   5        $ 
btnLimpiar��5.      $��Limpiar��6)   $	��100��6;   $��5q�6G   6R   5/      &   System.Windows.Controls.DataGrid  52   
   -
   5          Name$ gridCategorias��52      $��2��6+   	   AutoGenerateColumns$	 False��68   
   
IsReadOnly$

 True��6T      SelectionMode$ Single=�6f   $��0,10,0,0q�6}      RowStyle 53      �� 54       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�55      ��  �� Property � �� �� Value$ 30��6/   56         Columns 58      0  *System.Windows.Controls.DataGridTextColumn  59      ,  &System.Windows.Controls.DataGridColumn   Header$ ID Categoría��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6;   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   �� *categoria_id+  5:      $ Nombre��6%   $ 2* 65    6@   �� *categoria_nombre+  5;      $ Descripción��6%   $ 3* 6:    6E   �� *descripcion+5=      5>      5?      