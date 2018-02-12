# SQL-LINQ-test-middle

Прислали задание на вакансию C#-SQL c 3-6 годами опыта.

Тесты были решены, что  вызвало удивление у работодателя, узнавшего об отсутствии такого опыта.

Файл SQLQuery.sql - файл с запросами для реальной базы данных.  
Папки SQLQuery1 и SQLQuery2 - проекты для Visual Studio 2017 на С# для решений заданий 2.1 и 2.2 соответственно.  
Файл Answers.txt - файл с ответами в текстовом формате

## Задание:

#### 1. SQL

```text
  Имеем следующую структуру таблиц:  
Таблица Products { INT Id; VARCHAR Name; } – товары.  
Таблица ProductSales { INT ProductId; DATE Date; INT Quantity } – фактические продажи  
(на каждый день, по каждому товару, количество, проданное за весь день).  
Выбрать все товары, у которых суммарные продажи за декабрь 2016 г. больше 1000 шт. (список выдачи: Id, Name).
```

#### 2. LINQ

Имеем структуру классов:

```c#
// Группа товаров – таблица Groups
class Group 
    {
      int Id;
      string Name;
    }
// Товар – таблица Products
class Product 
    {
      int Id;
      string Name;
      int GroupId; // Идентификатор группы товаров (внешний ключ)
      Group Group; // Navigation property
    }
```

2.1.
```text
  Пусть в переменной List<int> productIds задан список идентификаторов товаров;  
IEnumerable<Product> products – коллекция товаров.  
Из указанной коллекции выбрать список записей вида:  
{ ProductName /* Имя товара */, GroupName /* Имя группы. */  },  
для всех товаров, идентификаторы которых присутствуют в списке productIds.
```

2.2.
```text
  Добавим условие, что в таблице Products нет внешнего ключа на Groups, т.е. свойства Group в классе нет совсем,  
а для значения GroupId может отсутствовать соответствующая запись в таблице Groups.  
Пусть IEnumerable<Product> products, IEnumerable<Group> groups – коллекции товаров и групп, соответственно.  
Из указанных коллекций выбрать список записей вида:  
{ ProductName /* Имя товара */, GroupName /* Имя группы, если она существует, или “No Group”,  
если такой группы нет. */  }.
Можно использовать лямбда-выражения или SQL-нотацию, на ваш выбор.
```
## Ответы:

1.
```sql
SELECT a.Id, a.Name, b.TotalQuantity FROM
	(SELECT ProductId, SUM(Quantity) AS TotalQuantity FROM ProductSales
	WHERE MONTH(Date) = '12' AND YEAR(Date) = 2016
	GROUP BY ProductId) b
JOIN Products a
ON b.ProductId = a.Id
WHERE b.TotalQuantity>1000
```

2.1.
```c#
var selected = products
                .Join(
                productIds,
                e => e.Id,
                o => o,
                (e, o) => new { Name = e.Name, Group = e.Group.Name });

foreach (var item in selected)
Console.WriteLine($"ProductName: {item.Name}, GroupName: {item.Group}");
Console.ReadLine();
```
или
```c#
var selected = from pr in products
               join prid in productIds on pr.Id equals prid
               select new { Name = pr.Name, Group = pr.Group.Name };

foreach (var item in selected)
Console.WriteLine($"ProductName: {item.Name}, GroupName: {item.Group}");
Console.ReadLine();
```
2.2.
```c#
var selected = products
               .GroupJoin(
               groups,
               e => e.GroupId,
               o => o.Id,
               (e, os) => os
                   .DefaultIfEmpty()
                   .Select(o => new
                   { Name = e.Name, GroupName = o != null ? o.Name : "No Group" }))
                       .SelectMany(r => r);

foreach (var item in selected)
Console.WriteLine($"ProductName: {item.Name}, GroupName: {item.GroupName}");
Console.ReadLine();
```
или
```c#
var selected = from pr in products
               join gr in groups on pr.GroupId equals gr.Id into pg
               from sub in pg.DefaultIfEmpty()
               select new { Name = pr.Name, GroupName = sub?.Name ?? "No Group" };

foreach (var item in selected)
Console.WriteLine($"ProductName: {item.Name}, GroupName: {item.GroupName}");
Console.ReadLine();
```

