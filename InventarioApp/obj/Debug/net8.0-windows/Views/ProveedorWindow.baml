   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5   	   )    #InventarioApp.Views.ProveedorWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:InventarioApp.Views     5   	   -   5          � Title$  	Proveedor��5   	   $��18(�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	   $	��450��5   	   $	��500��6   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFA9E309��6   � 5      $ White��6    � Offset$ 1Q�6)   5      .��5      � $��20q�6    � ColumnDefinitions 5   
   �� 5      $	��36* �6   �� 5      $	��79* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      $��2* �6   .+�5"   
   �� -   5        	 ��Name$	 lblProveedorId��5"      $��ID Proveedor��6(   $��0��6?   $��Center=�6O   $
��Left=�6j   $��34��6�   $	��120��6�   �� 5#   
   -   5        
 ��Name$
 txtProveedorId��5#       �� 	MaxLength$ 8��6*   $��1��68   $	��200��6H   $��30��6T   $��Center=�6`   $
��Left=�6{   �� 5%   
   -   5        $	 lblProveedorNombre��5%      $��Nombre��6,   $��1��6=   $��0��6J   $��Center=�6Z   $
��Left=�6u   $��34��6�   $	��100��6�   �� 5&   
   -   5        $
 txtProveedorNombre��5&      $��1��6.   $��1��6;   $	��200��6K   $��30��6W   $��Center=�6c   $
��Left=�6~   �� 5(   
   -   5        $	 lblNumeroTelefono��5(      $��	Teléfono��6+   $��2��6>   $��0��6K   $��Center=�6[   $
��Left=�6v   $��34��6�   $	��100��6�   �� 5)   
   -   5        $
 txtNumeroTelefono��5)      $��2��6-   $��1��6:   $	��200��6J   $��30��6V   $��Center=�6b   $
��Left=�6}   �� 5+   
   -   5        $	 lblEmail��5+      $��Email��6"   $��3��62   $��0��6?   $��Center=�6O   $
��Left=�6j   $��34��6�   $	��100��6�   �� 5,   
   -	   5        $
 txtEmail��5,      $��3��6$   $��1��61   $	��200��6A   $��30��6M   $��Center=�6Y   $
��Left=�6t   �� 5.   
   -
   5        $	 lblDireccion��5.      $��
Dirección��6&   $��4��6:   $��0��6G   $��Center=�6W   $
��Left=�6r   $��34��6�   $	��100��6�   �� 5/   
   -   5        $
 txtDireccion��5/      $	��300��6(   $��30��64   $��4��6@   $��1��6M   $��Center=�6]   $
��Left=�6x   �� 52   
   $��5��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $
��Left=�6_   $	��Top=�6z   $��40��6�   $��
135,58,0,0q�6�   $	��190��6�   .+�53      �� -   5         ��Name$ 
btnGuardar��53      $��Guardar��6)   $��80��6;   $��30��6F   $��	0,0,15,10q�6R    � Cursor$
 Hand��6~   �� 54      -   5        $ 	btnCerrar��54      $��Cerrar��6(   $��80��69   $��30��6D   $��	0,0,15,10q�6P   $
 Hand��6{   55      56      57      