   M S B A M L   `   `   `  ����H  DInventarioApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:InventarioAppInventarioApp  5   	   9!clr-namespace:InventarioApp.ViewsInventarioApp.Views  5   	   C&clr-namespace:InventarioApp.ViewModelsInventarioApp.ViewModels  5   	       InventarioApp.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:InventarioApp     5   	   3views!clr-namespace:InventarioApp.Views     5   	   =
viewmodels&clr-namespace:InventarioApp.ViewModels     5   	   -   5        $��InventarioMainWindow��5   	     � Title$  Inventario App��5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$ SingleBorderWindow=�5   	    � 
ResizeMode$ NoResize=�5   	   $	��750��5   	   $
��1350��5   	    � 	Resources 5         MouseOverButtonStyle �� 
TargetType �� Triggers P� Property P� Value	 �� Property
 �� Value  SubtleImageEffectStyle �� 	Resources3  -System.Windows.Media.Effects.DropShadowEffect  Key   ShadowDepth   
BlurRadius   Opacity �� ScaleX �� ScaleY ]� 
Conditions �� Property �� Value  ShadowEffect �� DataType0   *InventarioApp.ViewModels.ProductoViewModel*   $InventarioApp.Views.ProductoListView8   2InventarioApp.ViewModels.RegistroProductoViewModel2   ,InventarioApp.Views.RegistroProductoListView2   ,InventarioApp.ViewModels.ExtraccionViewModel,   &InventarioApp.Views.ExtraccionListView.   (InventarioApp.ViewModels.PedidoViewModel(	   "InventarioApp.Views.PedidoListView3
   -InventarioApp.ViewModels.PedidoNuevoViewModel-   'InventarioApp.Views.PedidoNuevoListView1   +InventarioApp.ViewModels.CategoriaViewModel+   %InventarioApp.Views.CategoriaListView2   ,InventarioApp.ViewModels.InventarioViewModel,   &InventarioApp.Views.InventarioListView,   &InventarioApp.ViewModels.LoteViewModel&    InventarioApp.Views.LoteListView-   'InventarioApp.ViewModels.StockViewModel'   !InventarioApp.Views.StockListView5   /InventarioApp.ViewModels.ProductoNuevoViewModel/   )InventarioApp.Views.ProductoNuevoListView1   +InventarioApp.ViewModels.ProveedorViewModel+   %InventarioApp.Views.ProveedorListView��%�  &	        &	 �     7  (�� �    5A   1   *, +)(�� �    5E   9   *, +)(�� *    5I   ;   *, +)(�� {    5M   /   *, +)(�� �    5Q   4   *,
 +)(�� 
    5U   2   *, +)(�� R    5Y   3   *, +)(�� �    5]   -   *, +)(�� �    5a   .   *, +)(�� "    5e   6   *, +)(�� n    5i   2   *, +)�� 5   
   " ��6-    5      P� 5       �@J�IsMouseOver$
 True��61   . �5      �� 	 � ��$

 Bold"�63   5      P� 5       �@J�IsMouseOver$ False��61   . �5      �� 	 � ��$
 Normal"�63   5      5      �� 5   
   " ��6/    5      ��  5       ShadowEffect $ 3Q�68   $ 5Q�6H   $	 0.5Q�6W   5!      .�5"      �� 	 � z�
 5#      �� 5$      $ 1Q�6%   $ 1Q�60   5&       5'      ]� 5(       5)      �� 5*       �@J�IsMouseOver$
 True��6;   ./�5,      �� 	 � z�
 5-      �� 5.      $
 1.12Q�6-   $
 1.12Q�6;   50      �� 51      	 �@J�Effect8
   6/   52      ]� 53       54      �� 55       �@J�IsMouseOver$ False��6;   ./�57      �� 	 � z�
 58      �� 59      $ 1Q�6-   $ 1Q�68   5;      �� 5<      	 �@J�Effect
 6/   Z� 5=      5?      �� 5A   
   ProductoTemplate��"  61   .R�5B        5C      �� 5E   
   RegistroProductoTemplate��"  69   .R�5F        5G      �� 5I   
   ExtraccionProductoTemplate��"  6;   .R�5J        5K      �� 5M   
   PedidoTemplate��"  6/   .R�5N      	  5O      �� 5Q   
   PedidoNuevoTemplate��" 
 64   .R�5R        5S      �� 5U   
   CategoriaTemplate��"  62   .R�5V        5W      �� 5Y   
   InventarioTemplate��"  63   .R�5Z        5[      �� 5]   
   LoteTemplate��"  6-   .R�5^        5_      �� 5a   
   StockTemplate��"  6.   .R�5b        5c      �� 5e   
   ProductoNuevoTemplate��"  66   .R�5f        5g      �� 5i   
   ProveedorTemplate��"  62   .R�5j        5k      5n      .��5p      � $��0,0,10,0q�6    � Cursor$
 Hand��6    � RowDefinitions 5q   
   �� 5r      $��1* �6   �� 5s      �� 5t      �� 5u      �� 5v      �� 5w      �� 5x      �� 5y      �� 5z      �� 5{      �� 5|      �� 5}      �� 5~      $
��1.5* �6    � ColumnDefinitions 5�   
   �� 5�      $	��184 �6   �� 5�      �� 5�      $	��193 �6   .+�5�   
   �� $��0��6   $��13��6$   $��	0,0,10,10q�66   ��5�      �� 5�       �� 
StartPoint$	 1,0&�6&    �� EndPoint$	 1,1&�67   .��5�      �  � Color$ 	#FF044511��6#    � Offset$ 0Q�65   � 5�      $ 	#FF7BB30D��6#   $
 0.65Q�65   � 5�      $ 	#FFEAE515��6#   $
 0.80Q�65   � 5�      $ #ffffff��6#   $ 1Q�63   5�      5�      �� 5�   
   $��2��6   $��13��6$   $��0,0,0,10q�66   ��5�      �� 5�      $	 1,0&�6&   $	 1,1&�67   .��5�      � $ 	#FF044511��6#   $ 0Q�65   � 5�      $ 	#FF7BB30D��6#   $
 0.65Q�65   � 5�      $ 	#FFEAE515��6#   $
 0.80Q�65   � 5�      $ #ffffff��6#   $ 1Q�63   5�      5�      �� 5�   
   -   5        $��imgLogo��5�      $��./Images/E-lote.png��6   $��3��6<   $	��140��6M   $	��140��6Y   $	��Top=�6f   $
��Left=�6~   $��	17,10,0,0q�6�   �� 5�   
   $��1��6   $��0��6!   ���	#FFD3E6AD �� CornerRadius$ 10��6E   $��7,7,7,7q�6W   ._�5�      �� -   5         ��Name$ 
lblVetName��5�   1   $��Inventario El-Lote��6   
���   �$��25(�6X   $��Center=�6f   $��Center=�6�   5�      �� 5�   
   $��1��6   $��1��6!   ���	#FF316302$ 10��6E   $��7,7,7,7q�6W   ._�5�      �� -   5        $ lblWindowName��5�   /   $��Pagina Principal��6   
�������$��20(�6Y   $��Center=�6g   $��Center=�6�   5�      �� 5�   
   $��1��6-   $��2��6=   $��11��6J   $
��Auto��6\   $
��Auto��6j   $��0,0,10,0q�6w   ��6   �� 6w   �� 5�   
   -   5        $��btnProducto��5�      $	��170��6$   $��50��60   $��2��6<   $��1��6L   $��11,0,0,0q�6Y   $
��Left=�6k   $��Center=�6�   $
 Hand��6�   #��[  6�   .��5�      ��  �� Orientation$ 
Horizontal=�6   $	��180��62   .+�5�      �� $��./Images/producto.png��6   $��30��67   $��30��6B   $��5,0,15,0q�6N   $
��Left=�6`   #��[ 6{   �� 5�      $��	Productos��6   $��20(�6-   $
��Left=�6;   $��Center=�6V   5�      5�      �� 5�   
   -   5        $��btnRegProdNuev��5�      $	��170��6'   $��50��63   $��2��6?   $��2��6O   $��11,0,0,0q�6\   $
��Left=�6n   $��Center=�6�   $
 Hand��6�   #��[  6�   .��5�      �� $ 
Horizontal=�6   $	��180��62   .+�5�      �� $%��./Images/registro-productos.png��6   $��30��6A   $��30��6L   $��5,0,10,0q�6X   $
��Left=�6j   #��[ 6�   �� 5�      $ ��Registrar Productos Nuevos��6   $��13(�6>   $��Center=�6L   $
��Wrap=�6g   $	��123��6{   5�      5�      �� 5�   
   -   5        $��btnExtra��5�      $	��170��6!   $��50��6-   $��2��69   $��3��6I   $��11,0,0,0q�6V   $
��Left=�6h   $��Center=�6�   $
 Hand��6�   #��[  6�   .��5�      �� $ 
Horizontal=�6   $	��180��62   .+�5�      �� $��./Images/Extracción.png��6   $��30��69   $��30��6D   $��5,0,11,0q�6P   $
��Left=�6b   #��[ 6}   �� 5�      $��Extracción de Producto��6   $��18(�6:   $��Center=�6H   $
��Wrap=�6c   $	��112��6w   5�      5�      �� 5�   
   -   5        $��btnStock��5�      $	��170��6!   $��50��6-   $��2��69   $��4��6I   $��11,0,0,0q�6V   $
��Left=�6h   $��Center=�6�   $
 Hand��6�   #��[  6�   .��5�      �� $ 
Horizontal=�6   $	��180��62   .+�5�      �� $��./Images/Stock.png��6   $��30��64   $��30��6?   $��5,0,15,0q�6K   $
��Left=�6]   #��[ 6x   �� 5�      $��Stock��6   $��20(�6)   $
��Left=�67   $��Center=�6R   5�      5�      �� 5�   
   -	   5        $��btnLote��5�      $	��170��6    $��50��6,   $��2��68   $��5��6H   $��11,0,0,0q�6U   $
��Left=�6g   $��Center=�6�   $
 Hand��6�   #��[  6�   .��5�      �� $ 
Horizontal=�6   $	��180��62   .+�5�      �� $��./Images/lote.png��6   $��30��63   $��30��6>   $��5,0,15,0q�6J   $
��Left=�6\   #��[ 6w   �� 5�      $
��Lote��6   $��20(�6(   $
��Left=�66   $��Center=�6Q   5�      5�      �� 5�   
   -
   5        $��btnProductoNuevo��5�      $	��170��6)   $��50��65   $��2��6A   $��6��6Q   $��11,0,0,0q�6^   $
��Left=�6p   $��Center=�6�   $
 Hand��6�   #��[  6�   .��5�      �� $ 
Horizontal=�6   $	��180��62   .+�5�      �� $ ��./Images/productopedir.png��6   $��30��6<   $��30��6G   $��5,0,15,0q�6S   $
��Left=�6e   #��[ 6�   �� 5�      $��Productos a pedir��6   $��17(�65   $��Center=�6C   $
��Wrap=�6^   $	��122��6r   5�      5�      �� 5�   
   -   5        $��btnPedidoReposicion��5�      $	��170��6,   $��50��68   $��2��6D   $��7��6T   $��11,0,0,0q�6a   $
��Left=�6s   $��Center=�6�   $
 Hand��6�   #��[  6�   .��5�      �� $ 
Horizontal=�6   $	��180��62   .+�5�      �� $��./Images/pedido.png��6   $��30��65   $��30��6@   $��5,0,15,0q�6L   $
��Left=�6^   #��[ 6y   �� 5�      $��Pedido Reposición��6   $��18(�65   $��Center=�6C   $
��Wrap=�6^   $	��122��6r   $��51��6~   5�      5�      �� 5�   
   -   5        $��btnPedidoNuevo��5�      $	��170��6'   $��50��63   $��2��6?   $��8��6O   $��11,0,0,0q�6\   $
��Left=�6n   $��Center=�6�   $
 Hand��6�   #��[  6�   .��5�      �� $ 
Horizontal=�6   $	��180��62   .+�5�      �� $��./Images/pedidonuevo.png��6   $��30��6:   $��30��6E   $��7,0,15,0q�6Q   $
��Left=�6c   #��[ 6~   �� 5�      $��Pedido Nuevo��6   $��18(�60   $��Center=�6>   $
��Wrap=�6Y   $	��115��6m   5�      5�      �� 5�   
   -   5        $��btnProveedor��5�      $	��170��6%   $��50��61   $��2��6=   $��9��6M   $��11,0,0,0q�6Z   $
��Left=�6l   $��Center=�6�   $
 Hand��6�   #��[  6�   .��5�      �� $ 
Horizontal=�6   $	��180��62   .+�5�      �� $��./Images/proveedor.png��6   $��30��68   $��30��6C   $��5,0,15,0q�6O   $
��Left=�6a   #��[ 6|   �� 5�      $��Proveedores��6   $��20(�6/   $
��Left=�6=   $��Center=�6X   5�      5�      �� 5�   
   -   5        $��btnCategoría��5�      $	��170��6%   $��50��61   $��2��6=   $��10��6M   $��11,0,0,0q�6[   $
��Left=�6m   $��Center=�6�   $
 Hand��6�   #��[  6�   .��5�      �� $ 
Horizontal=�6   $	��180��62   .+�5�      �� $��./Images/categoria.png��6   $��30��68   $��30��6C   $��5,0,15,0q�6O   $
��Left=�6a   #��[ 6|   �� 5�      $��Categorías��6   $��20(�6.   $
��Left=�6<   $��Center=�6W   5�      5�      �� 5�   
   -   5        $��
btnFactura��5�      $	��170��6#   $��50��6/   $��2��6;   $��11��6K   $��11,0,0,0q�6Y   $
��Left=�6k   $��Center=�6�   $
 Hand��6�   #��[  6�   .��5      �� $ 
Horizontal=�6   $	��180��62   .+�5     �� $��./Images/inventario.png��6   $��30��69   $��30��6D   $��5,0,15,0q�6P   $
��Left=�6b   #��[ 6}   �� 5     $��Inventarios��6   $��20(�6/   $
��Left=�6=   $��Center=�6X   5     5     �� 5  
   $��12��6   $��2��6"   $��16(�62   $	��Top=�6@   $��Center=�6X   $��0,1,0,0q�6u   $��21��6�   $	��116��6�   �� 5     -   5         ��Name$ 	lnkCerrar��5     
���   �.�6V   Salir del Sistema6i   5	     5
     5     