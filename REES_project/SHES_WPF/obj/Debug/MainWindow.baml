   M S B A M L   `   `   `  ����C  ?SHES_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SHES_WPFSHES_WPF  5   	   l hSystem.Windows.Controls.DataVisualization.Toolkit, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting 5   	       SHES_WPF.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:SHES_WPF     5   	   �chartingToolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   	     � Title$  
MainWindow��5	   	   $	��525��6   $��831.5��6)   .��5
      � $��-4,0,-8,-31q�6   .+�5   
   �� -   5         ��Name$ button��5      $��	Start app��6!   $��18(�65   $
��Left=�6C   $��40��6^   $��
30,428,0,0q�6j   $	��Top=�6~   $	��150��6�    J� RenderTransformOrigin$ 	0.5,0.762&�6�   �� 5   
   -   5        $ button3��5      $��	Close app��6"   $��18(�66   $
��Left=�6D   $��40��6_   $��588,428,0,0q�6k   $	��Top=�6�   $	��150��6�   6�   �� 5   
   -   5        $ button1��5      $��Generate report��6"   $��18(�6<   $
��Left=�6J   $��40��6e   $��588,207,0,0q�6q   $	��Top=�6�   $	��150��6�   6�   �� 5   
   -   5         ��Name$ label��5      $$��Select Date to get the report:��6   $
��Left=�6H   $��43��6c   $��531,116,0,0q�6o   $	��Top=�6�   $	��275��6�   $��20(�6�   >  8System.Windows.Controls.DataVisualization.Charting.Chart  5   
   -   5        $��sr��5   �   $
��Left=�6    $��
30,116,0,0q�6;   $	��Top=�6O   $	��285��6g   $	��460��6t      Title$% Display consummation/production��6�   
���������5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ White��6#    � Offset$ 0Q�61   � 5      $ 	#FF9ADCF5��6#   $ 1Q�65   5      	   Series	 5      C  =System.Windows.Controls.DataVisualization.Charting.LineSeries  ?  9System.Windows.Controls.DataVisualization.Charting.Series
   Title$
 Solar panels��6-   H  BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   IndependentValueBinding 5      ��  �� Path$	 Key�   DependentValueBinding 5      �� $ Value�  5      $
 	Batteries��6-    5      �� $	 Key� 5      �� $ Value�  5!      $
 Utility��6-    5"      �� $	 Key� 5#      �� $ Value�  5%      $
 	Consumers��6-    5&      �� $	 Key� 5'      �� $ Value�   Axes 5+      C  =System.Windows.Controls.DataVisualization.Charting.LinearAxis  =  7System.Windows.Controls.DataVisualization.Charting.Axis   Orientation$ Y=�6-   D  >System.Windows.Controls.DataVisualization.Charting.DisplayAxis   Title$	 KWH��5,   %     5.      $ X=�6-   $ Hours��50   %   53      (  "System.Windows.Controls.DatePicker  54   
   -   5        $
��Date��54   u   $
��Left=�6   $��32��60   $��588,162,0,0q�6<   $	��Top=�6Q   $	��150��6i   6u   �� 55   
   $
��Left=�6   $	��119��6/   $��	351,5,0,0q�6<   
���   �$	��Top=�6^   $	��174��6v   $��0��6�   ��56      �� 57       �� ImageSource$9 334561782_1687694077950450_5568597469186490368_n.png��6   59      �� 5:   
   -   5        $ label1��5:      $�� ��6    $��18(�6+   $
��Left=�69   $��46��6T   $��515,269,0,0q�6`   $	��Top=�6u   $	��299��6�   �� 5;   
   -   5        $ label2��5;      $�� ��6    $��18(�6+   $
��Left=�69   $��42��6T   $��562,359,0,0q�6`   $	��Top=�6u   $	��202��6�   $ 0.488,0.533&�6�   �� 5<   
   -	   5        $ label3��5<      $�� ��6    $��18(�6+   $
��Left=�69   $��47��6T   $��531,299,0,0q�6`   $	��Top=�6u   $	��235��6�   5>      5?      