   M S B A M L   `   `   `  ����B  >Prac_01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Prac_01Prac_01  5   	       Prac_01.StudyModeWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Prac_01     5   	     � Title$.  (Вікно режиму навчання��5      $��294.8��6&   $��314.4��65   $��16(�5	      $��Courier New)�6   $
��Bold"�6(    � WindowStartupLocation$ CenterScreen=�5
       � 
ResizeMode$ NoResize=�6&   .��5      � ���	#FFBEF0E2.+�5   
   �� $!��РЕЖИМ НАВЧАННЯ��6   $
��Left=�6)   $��8,10,0,0q�5      $	��Top=�6   $��28��6+   $	��297��5      $��Center=�6   $��18(�5      �� 5   
   -   5         ��Name$ 
VerifField��5      $
��Left=�6(   $��
171,44,0,0q�5      $
��Wrap=�6   $	��Top=�5      $��22��6   $	��125��6%    �� TextAlignment$ Justify=�5      ���	#FF764F22$��длагнитор��5      �� 5   
   -   5         ��Name$ 
InputField��5      $
��Left=�6&   $��28��5      $��	8,141,0,0q�6    �� TextWrapping$
 Wrap=�6    $	��Top=�5      $	��212��6   ���	#FFDBE6F9$��Stretch=�5      6M   �� 5   
   -   5         ��Name$	 la1��5      $'��!Поле для введення:��6   $
��Left=�6:   $��	8,108,0,0q�5      $	��Top=�6   $��28��6,   $	��205��5      $
��Left=�6   $��18(�6/   �� 5!   
   -   5         ��Name$ CloseStudyMode��5!      $ ��Вийти з режиму��6)   $
��Left=�5#      $��	9,210,0,0q�6    $	��Top=�5$      $	��191��6   $��35��6)   65   �� 5&   
   $��Кодове слово:��6   $
��Left=�6(   $��8,38,0,0q�5'      $	��Top=�6   $��28��6+   $	��155��5(      $
��Left=�6   $��18(�6/   �� 5)   
   $$��Кількість спроб:��6   $
��Left=�6+   $��	9,174,0,0q�5*      $	��Top=�6   $��28��6,   $	��191��5+      $
��Left=�6   $��18(�6/   �� 5,   
   -   5        	 ��Name$	 CountProtection��5,      $
��Left=�6,   $��205,178,0,0q�5-      $	��Top=�6   $��55��6.   
 �� SelectedIndex$
 0��5.      .@�50      �� $��3��6   �� 51      $��4��6   �� 52      $��5��6   �� 53      $��6��6   �� 54      $��7��6   �� 55      56      �� 57   
   $3��-Кількість введених симв:��6   $
��Left=�58      $��8,71,0,0q�6   $	��Top=�59      $��28��6   $	��270��6%   $
��Left=�5:      $��18(�6#   �� 5<   
   -   5        $ SymbolCount��5<      $��0��6%   $
��Left=�5>      $��218,108,0,0q�6   $	��Top=�5?      $��28��6   $��37��6%   $��18(�60   5A      5B      