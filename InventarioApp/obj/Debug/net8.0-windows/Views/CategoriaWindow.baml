   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5   	   )    #InventarioApp.Views.CategoriaWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:InventarioApp.Views     5   	   -   5          � Title$  	Categoria��5   	   $��18(�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	   $	��296��5   	   $	��540��6   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFA9E309��6   � 5      $ White��6    � Offset$ 1Q�6)   5      .��5      � $��20q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6   .+�5$   
   �� -   5        	 ��Name$	 lblCategoriaId��5$      $��Id Categoria��6(   $��0��6?   $��0��6L   $��Center=�6\   $��Right=�6w   $	��0,5q�6�   �� 5%   
   -   5        
 ��Name$
 txtCategoriaId��5%       �� 	MaxLength$ 6��6*   $��0��68   $��1��6E   $	��200��6U   $��30��6a   $��Center=�6m   $
��Left=�6�   $��10q�6�   �� 5'   
   -   5        $	 lblCategoriaNombre��5'      $��Nombre Categoria��6,   $��1��6G   $��0��6T   $��Center=�6d   $��Right=�6   $	��0,5q�6�   �� 5(   
   -   5        $
 txtCategoriaNombre��5(      $	��200��6.   $��30��6:   $��1��6F   $��1��6S   $��Center=�6c   $
��Left=�6~   $��10q�6�   �� 5*   
   -   5        $	 lblDescripcion��5*      $��Descripción��6(   $��2��6>   $��0��6K   $��Center=�6[   $��Right=�6v   $	��0,5q�6�   �� 5+   
   -   5        $
 txtDescripcion��5+      $	��300��6*   $��30��66   $��2��6B   $��1��6O   $��Center=�6_   $
��Left=�6z   $��10q�6�   �� 5.   
   $��6��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   $��Center=�6|   $��0,10,0,0q�6�   .+�5/      �� -   5         ��Name$ 
btnGuardar��5/      $��Guardar��6)   $��80��6;   $��30��6F   $��	0,0,15,10q�6R    � Cursor$
 Hand��6~   �� 50      -	   5        $ 	btnCerrar��50      $��Cerrar��6(   $��80��69   $��30��6D   $��	0,0,15,10q�6P   $
 Hand��6|   51      52      53      