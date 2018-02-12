CREATE DATABASE MyDatabase

USE MyDatabase

CREATE TABLE Products
( 
    Id int, 
    Name varchar(20)
)

INSERT INTO Products
(Id, Name)
VALUES
  (1,'IPhone'),
  (2,'Samsung S8'),
  (3,'Nokia 8'),
  (4,'Asus ZenFone 3')


DROP TABLE ProductSales

 CREATE TABLE ProductSales
(
	ProductId int,
	Date date,
	Quantity int
)





SELECT * FROM Products

SELECT * FROM ProductSales

SELECT * FROM ProductSales
WHERE (MONTH (Date)) = '12' AND (YEAR(Date)) = 2016
GROUP BY Quantity

SELECT Id, Name, ProductId, Date, SUM (Quantity) FROM Products a
   JOIN ProductSales b                  
   ON a.Id = b.ProductId
WHERE (MONTH (Date)) = '12' AND (YEAR(Date)) = 2016
GROUP BY Quantity

SELECT Id, Name,
SELECT * FROM Products a
   JOIN ProductSales b                  
   ON a.Id = b.ProductId
WHERE (MONTH (Date)) = '12' AND (YEAR(Date)) = 2016


SELECT SUM(Quantity) AS TotalQuantity FROM ProductSales
WHERE (MONTH (Date)) = '12' AND (YEAR(Date)) = 2016
GROUP BY Quantity



SELECT ProductId, SUM(Quantity) AS TotalQuantity FROM ProductSales
WHERE ((MONTH (Date)) = '12' AND (YEAR(Date)) = 2016)
GROUP BY ProductId
-------------------
SELECT T1.ProductId FROM
(SELECT ProductId, SUM(Quantity) AS TotalQuantity FROM ProductSales
WHERE ((MONTH (Date)) = '12' AND (YEAR(Date)) = 2016)
GROUP BY ProductId) T1

WHERE T1.TotalQuantity>1000
-------------------------------

SELECT ProductId FROM ProductSales

SELECT * FROM Products a
   JOIN ProductSales b                  
   ON a.Id = b.ProductId
WHERE (MONTH (Date)) = '12' AND (YEAR(Date)) = 2016

SELECT a.Id, SUM (Quantity) FROM Products a
   JOIN ProductSales b                  
   ON a.Id = b.ProductId
WHERE (MONTH (Date)) = '12' AND (YEAR(Date)) = 2016
GROUP BY Id 

INSERT INTO ProductSales
(ProductId, Date, Quantity)
VALUES
  (1,'20161205', 500),
  (2,'20161201', 500),
  (1,'20161215', 600),
  (4,'20160501', 500),
  (3,'20150501', 800)

SELECT a.Id, a.Name, b.TotalQuantity FROM
	(SELECT ProductId, SUM(Quantity) AS TotalQuantity FROM ProductSales
	WHERE MONTH(Date) = '12' AND YEAR(Date) = 2016
	GROUP BY ProductId) b
JOIN Products a
ON b.ProductId = a.Id
WHERE b.TotalQuantity>1000