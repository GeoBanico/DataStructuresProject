   M S B A M L   `   `   `  ����Y  UBanico_DataStruct-FinalProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Banico_DataStruct_FinalProjectBanico_DataStruct_FinalProject  5   	   B >WpfMath, Version=0.9.0.0, Culture=neutral, PublicKeyToken=nullD/clr-namespace:WpfMath.Controls;assembly=WpfMathWpfMath.Controls 5	   	   ;    5Banico_DataStruct_FinalProject.SimplePolynomialSolver   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:Banico_DataStruct_FinalProject     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   >controls/clr-namespace:WpfMath.Controls;assembly=WpfMath  5	   	      MaterialDesignBody#���   5
   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$( "MathGeo - Simple Polynomial Solver��5   	   $	��407��64   $	��800��6A    � SizeToContent$ WidthAndHeight=�6M    � WindowStyle$ SingleBorderWindow=�6l    � WindowStartupLocation$ CenterScreen=�6�    � 
ResizeMode$ NoResize=�6�   .��5      � .+�5   
   �� -   5        $��TxtEq1��5      $
��Left=�6    $��
208,68,0,0q�6;   $�� ��6O    �� TextWrapping$
 Wrap=�6W   $	��Top=�6k   $	��186��6�   $��14(�6�   $��Arial)�6�   �� 5   
   -   5        $��CmbOperation��5      $
��Left=�6'   $��
432,66,0,0q�6B   $	��Top=�6V   $��36��6n   $��20(�6y   $
��Bold"�6�   $��Arial Rounded MT Bold)�6�   .@�5      �� .��6   +6   �� 5      .��6   -6   �� 5      .��6   *6   �� 5      .��6   /6   5      �� 5   
   -   5        $��TxtEq2��5      $
��Left=�6    $��
506,68,0,0q�6;   $�� ��6O   $
 Wrap=�6W   $	��Top=�6k   $	��186��6�   $��14(�6�   $��Arial)�6�   �� 5   
   -   5        $��BtnSolve��5      $��=��6!   $
��Left=�6-   $��
718,72,0,0q�6H   $	��Top=�6\    J� RenderTransformOrigin$ 0.5,0.5&�6t   	 J� SnapsToDevicePixels$
	 True��6�   $��44��6�   $��15(�6�   $��Arial)�6�   
�������$��23��6  ${�False��6  � 5   
   $
��Left=�6   $	��420��6*   $��Center=�67   $	��147��6R   ���	#FFEE1D1D.+�5      �� $��Center=�6   $	��127��61   $��0,9,0,0q�6>   $	��Top=�6O   $	��181��6g   $+��%/f887648d5dabff73f97e5ce2c44e1aed.png��6s   $
��Fill=�6�   �� 5      $��Center=�6   $��	0,122,0,0q�61   $��MathGeo��6D   $	��Top=�6V   $��30(�6n   $��Bahnschrift)�6|   $
��Bold"�6�   $��Normal$�6�   ���	#FFFBFBFB6�   �� 5      $��	*Addition��6   $
��Left=�6(   $��
31,296,0,0q�6C   $	��Top=�6W   $!��Bahnschrift Light Condensed)�6o   $
��Bold"�6�   $��Normal$�6�   ���	#FFFBFBFB$��17(�6�   �� 5       $��
Polynomial��6   $
��Left=�6)   $	��Top=�6D   $$��Bahnschrift SemiBold Condensed)�6\   $
��Bold"�6�   $��Normal$�6�   ���	#FFFBFBFB$��22(�6�   $��
31,217,0,0q�6�   �� 5!      $��
-includes-��6   $��Center=�6)   $��	0,267,0,0q�6F   $	��Top=�6Y   $!��Bahnschrift Light Condensed)�6q   $��Normal"�6�   $��Italic$�6�   ���	#FFFBFBFB$��15(�6�   �� 5"      $��Manipulation��6   $��Center=�6+   $	��Top=�6H   $$��Bahnschrift SemiBold Condensed)�6`   $
��Bold"�6�   $��Normal$�6�   ���	#FFFBFBFB$��22(�6�   $��	0,238,0,0q�6�   �� 5#      $��*Subtraction��6   $
��Left=�6+   $��
31,318,0,0q�6F   $	��Top=�6Z   $!��Bahnschrift Light Condensed)�6r   $
��Bold"�6�   $��Normal$�6�   ���	#FFFBFBFB$��17(�6�   �� 5$      $��*Multiplication��6   $
��Left=�6.   $��
31,340,0,0q�6I   $	��Top=�6]   $!��Bahnschrift Light Condensed)�6u   $
��Bold"�6�   $��Normal$�6�   ���	#FFFBFBFB$��17(�6�   �� 5%      $��	*Division��6   $
��Left=�6(   $��
31,362,0,0q�6C   $	��Top=�6W   $!��Bahnschrift Light Condensed)�6o   $
��Bold"�6�   $��Normal$�6�   ���	#FFFBFBFB$��17(�6�   5&      � 5(   
   $
��Left=�6   $��80��6*   $��210,136,0,0q�66   $	��Top=�6K   $	��480��6c   ���	#FFD1D1D16o   �� 5)   
   -   5        $��	ImgPreEq1��5)      $
��Left=�6!   $��40��6<   $��222,160,0,0q�6H   $	��Top=�6]   $	��210��6u   $��Uniform=�6�   
 �� StretchDirection$

 Both=�6�   �� 5*   
   -   5        $��	ImgPreEq2��5*      $
��Left=�6!   $��40��6<   $��468,160,0,0q�6H   $	��Top=�6]   $	��210��6u   $��Uniform=�6�   $

 Both=�6�   �� 5+   
   $
��Left=�6   $��210,204,0,0q�6-   $
 Wrap=�6B   $	��Top=�6V   $	��480��6n   $
��Bold"�6z   ${�False��6�   � 5,   
   $
��Left=�6   $��80��6*   $��214,244,0,0q�66   $	��Top=�6K   $	��478��6c   ���	#FFD1D1D16o   �� 5-   
   -   5        $��ImgPost��5-      $
��Left=�6    $��40��6;   $��245,275,0,0q�6G   $	��Top=�6\   $	��439��6t   $��Uniform=�6�   $

 Both=�6�   � 5.   
   $��Center=�6   $��24��6,   $	��Top=�68   $	��800��6P   ���	#FFEE1D1D6\   �� 5/   
   -   5        $��	LblFormat��5/      $��	- - - - -��6"   $
��Left=�66   $��
520,98,0,0q�6Q   $	��Top=�6e   $��10(�6}   $	��172��6�   � 50   
   $
��Left=�6   $��3��6*   $��	0,185,0,0q�65   $	��Top=�6H   $	��147��6`   ���	#FF4644446l   � 51   
   $
��Left=�6   $��3��6*   $��	0,193,0,0q�65   $	��Top=�6H   $	��147��6`   ���	#FF4644446l   � 52   
   $
��Left=�6   $	��419��6*   $��	147,0,0,0q�67   $	��Top=�6J   $��9��6b   ���	#FF464444$ 0.5,0.5&�6�   z�53      V� 54      .~�55      ��  �� ScaleX$ 1Q�6%   �� 56      �� 57      T� 58      59      5;      �� 5<   
   -	   5        $��LblOp��5<      $�� ��6   $
��Left=�6(   $��445,166,0,0q�6C   $	��Top=�6X   $��20(�6p   $
��Bold"�6~   $��Arial Rounded MT Bold)�6�   �� 5=   
   -
   5        $
��Quit��5=      $
��Left=�6    $��705,350,0,0q�6;   $	��Top=�6P   
�������$��Bahnschrift)�6{   $��24��6�   6�   �� 5>   
   -   5        $��	BtnSwitch��5>      $
��«»��6"   $
��Left=�6/   $��422,339,0,0q�6J   $	��Top=�6_   $��21��6w   $��46��6�   
�������$��Bahnschrift)�6�   ${�False��6�   6�   �� 5?   
   $��Answer:��6   $
��Left=�6"   $��214,244,0,0q�6=   $	��Top=�6R   $��15(�6j   $��Bahnschrift)�6x   5C      5D      