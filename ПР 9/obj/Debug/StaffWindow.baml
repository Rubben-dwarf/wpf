   M S B A M L   `   `   `  ����A  =ПР 9, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ПР_9ПР_9  5   	       ПР_9.StaffWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:ПР_9     5   	     � Title$  Сотрудники��5   	   $	��450��6   $	��800��6)   .��5	      �� .+�5
   
   �� -   5         ��Name$ Panel1��5
       �� Orientation$ 
Horizontal=�6%   .+�5      �� -   5         ��Name$ RolesComboBox��5      $	��150��6.   $��20��6;    �� 
IsEditable .  �� Text$ Поиск по роли��6Y   $��10, 5, 10, 5q�6n    �� DisplayMemberPath$ RoleName��6�   6�   �� 5      -   5         ��Name$ AddUserButton��5      $/��)Добавить пользователя��6,   $	��150��6L   $��25��6X   $��15, 0, 15, 0q�6d   6z   �� 5      -   5        $ DeleteUserButton��5      $-��'Удалить пользователя��6/   $	��150��6N   $��25��6Z   $��15, 0, 15, 0q�6f   6|   �� 5      -   5        $ EditUserButton��5      $/��)Изменить пользователя��6-   $	��150��6M   $��25��6Y   $��15, 0, 15, 0q�6e   6{   �� 5      -   5        $ ReturnButton��5      $��
Назад��6+   $��Right=�5      $��Bottom=�6)   $��0, 10, 240, 10q�6D   $��50��6\   6g   5      &   System.Windows.Controls.DataGrid  5      -   5          Name$ UserDataGrid��5      	   AutoGenerateColumns	 .  $	��800��6I   $	��410��6U   
   
IsReadOnly
 .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     5      $ Имя��6%   $
 Auto 6P    62   �� *	FirstName+6P     5      $ Роль��6%   $
 Auto 6P    63   �� *
RoleName+6P     5      $ Страна��6%   $
 Auto 6U    65   �� *CountryName+6U     5      $ Пол��6%   $
 Auto 6Q    62   �� *
GenderName+6Q   5      5      5      