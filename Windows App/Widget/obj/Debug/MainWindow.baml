   M S B A M L   `   `   `  ����A  =Widget, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WidgetWidget  5   	       Widget.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Widget     5   	   -   5          � Title$  
MainWindow��5   	   $	��220��6   $	��400��6)    � AllowsTransparency . ���#2b2b2b � WindowStyle$
 None=�5	   	    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�5
   @   .��5      � -   5         �Name$ MainGrid��5       � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��0.3* �6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      .+�5   
   �� -   5         ��Name$ 	TimeLabel��5      $��0��6#   $��0��63   $��3��5      $��Center=�6$   
�������$	��120(�5   #   �� 5   
   -   5        $ TemperatureLabel��5      $��0��6*   $��1��6:   $��Center=�5      
�������$��30(�5   $   $��0q�62   �� 5   
   -   5        $ HumidityLabel��5      $��1��6'   $��1��67   $��Center=�6D   
�������$��30(�5   $   $��0q�62   �� 5   
   -   5        $ PressureLabel��5      $��2��6'   $��1��67   $��Center=�6D   
�������$��30(�5   $   $��0q�62   	 � ContextMenu	 5   
   �� 5       .@�5!      m� -   5        
 m�Name$
 EnableMovementItem��5!      $��Enable movement��67   6P   m� 5"      -   5        $��Open settings��5"      62   m� 5#      -	   5        $��Apply settings��5#      63   m� 5$      -
   5        $��Close��5$      6*   5%      5'      5(      