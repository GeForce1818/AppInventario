   M S B A M L   `   `   `  ����  InventarioApp,    &InventarioApp.Views.InicioSesionWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Inicio de Sesión��5   	   $	��250��6"   $	��400��6/    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�6   .��5	      � $��20q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��/Images/icono_usuario.png��6   $��63��63   $��50��6>   $��0��6J   $��0��6W   $��2��6g   $��Center=�5      $��	0,10,0,20q�6-   �� 5   
   $��Nombre de Usuario:��6   $��1��6-   $��0��6:   $��Center=�6J   �� 5   
   -   5         ��Name$ 
txtUsuario��5      $	��200��6&   $��1��62   $��1��6?   $��5q�6O   �� 5   
   $��Contraseña:��6   $��2��6&   $��0��63   $��Center=�6C   G� 5    
   -   5         G�Name$ txtContrasena��5       $	��200��6-   $��2��69   $��1��6F   $��5q�6V   �� 5#   
   $��3��6   $��3��6"    �� Orientation$ 
Horizontal=�66   $��Center=�6O   $��10q�6l   .+�5$      �� -   5        $��Iniciar Sesión��5$      $	��100��6.   $��5q�6:   6E   �� 5%      -   5        $��Cancelar��5%      $	��100��6(   $��5q�64   6?   5&      �� 5)   
   -   5         ��Name$ txtError��5)      $��4��6&   $��2��63   $'��!Usuario o contraseña incorrectos��6G   
���  ��$��12(�5*   %   $y�	Collapsed=�63   $��Center=�6J   5+      5,      