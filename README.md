# MessagerTele
## Описание:
Клиент-серверное приложение, чат, написанное на C#.

## Стек для **DesktopClient**:
- TargetFramework: net6.0
- WPF + MVVM на Prism.Unity
- Microsoft.EntityFrameworkCore.SqlServer
- Newtonsoft.Json
- NLog
- MaterialDesignThemes

## Стек для **Server**:
- TargetFramework: net6.0
- Newtonsoft.Json
- NLog

## Как собрать приложение?
> Для того чтобы собрать приложение необходимо установить Microsoft SQL Server и Visual Studiо 2022, после чего нажать правой кнопкой мыши на проект **Server** и нажать на пункт *«Собрать»*. С проектом **DesktopClient** проделываем тоже самое.