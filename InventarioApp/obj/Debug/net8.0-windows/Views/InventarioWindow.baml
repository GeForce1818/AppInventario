   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5   	   *    $InventarioApp.Views.InventarioWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:InventarioApp.Views     5   	   -   5          � Title$  
Inventario��5   	   $��18(�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	   $	��342��5      $	��462��6   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFA9E309��6   � 5      $ White��6    � Offset$ 1Q�6)   5      .��5      � $��20q�6    � ColumnDefinitions 5   
   �� 5      $	��42* �6   �� 5      $	��73* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      $��2* �6   .+�5"   
   �� -   5        	 ��Name$	 lblInventarioId��5"      $��Id Inventario��6)   $��0��6A   $��Center=�6Q   $
��Left=�6l   $��10,0,0,0q�6�   $��1��6�   $��34��6�   $	��110��6�   �� 5#   
   -   5        
 ��Name$
 txtInventarioId��5#       �� 	MaxLength$ 6��6+   $��1��69   $��1��6F   $	��200��6V   $��30��6b   $��Center=�6n   $
��Left=�6�   $��10,0,0,0q�6�   �� 5%   
   -   5        $	 lblInventarioNombre��5%      $��Nombre Inventario��6-   $��2��6I   $��Center=�6V   $��Center=�6q   $��34��6�   $	��168��6�   �� 5&   
   -   5        $
 txtInventarioNombre��5&      $	��200��6/   $��30��6;   $��2��6G   $��1��6T   $	��Top=�6d   $
��Left=�6|   $��	10,11,0,0q�6�   �� 5)   
   $��6��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $
��Left=�6_   $	��Top=�6z   $��40��6�   $��
135,58,0,0q�6�   $	��190��6�   .+�5*      �� -   5         ��Name$ 
btnGuardar��5*      $��Guardar��6)   $��80��6;   $��30��6F   $��	0,0,15,10q�6R    � Cursor$
 Hand��6~   �� 5+      -   5        $ 	btnCerrar��5+      $��Cerrar��6(   $��80��69   $��30��6D   $��	0,0,15,10q�6P   $
 Hand��6|   5,      5.      5/      