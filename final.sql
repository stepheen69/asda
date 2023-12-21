Create Database Final

Use Final

Create table UserAccount(

     Fullname varchar (50) NOT NULL,
	 Pass varchar (50) NOT NULL,
	 Usertype varchar (50) NOT NULL
	 );

CREATE TABLE AdminView (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(255) NOT NULL,
    UserType NVARCHAR(50) NOT NULL,
    LoginTime DATETIME NOT NULL
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
	UserName NVARCHAR(100),
    FoodName NVARCHAR(255),
    Quantity INT,
    Price DECIMAL(10,2)   
);

CREATE TABLE FoodItems
(
    FoodID INT PRIMARY KEY,
    FoodName NVARCHAR(100),
    Price INT,
    QuantityAvailable INT
);


INSERT INTO FoodItems (FoodID, FoodName, Price, QuantityAvailable)
VALUES
    (1, 'Egg with Tocino and Rice', 120, 10),
    (2, '5pcs Lumpia With Rice', 60, 15),
    (3, 'Beef Tapa With Egg and Rice', 80, 20),
    (4, '4pcs Siomai with Rice', 60, 12),
    (5, '2pcs Lumpia w/ Pansit Rice', 130, 8),
    (6, '1pc w/ Chicken and Rice', 50, 5),
	(7, '12pcs Lumpia', 165, 8),
	(8, '1 tupperware of Pansit', 199, 8),
	(9, '1 pan of Spaghetti', 235, 8),
	(10, 'Chicken', 456, 8);

-- Update specific food and their quantity
UPDATE FoodItems
SET QuantityAvailable = 100
WHERE FoodID = 4;

CREATE TABLE OrderInformation
(
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(100),
    OrderDetails NVARCHAR(MAX), -- You can use NVARCHAR(MAX) to store a larger amount of data
    TotalPrice DECIMAL(10, 2),
    OrderStatus NVARCHAR(50) DEFAULT 'Pending'
);
	--Admin :PoV
	 SELECT *FROM AdminView
	SELECT *FROM UserAccount

	 --Seller :PoV	
	 SELECT *FROM FoodItems
	 SELECT *FROM Receipt

	SELECT * INTO Receipt
FROM Orders
WHERE 1 = 0;
	 
	 SELECT *FROM Orders

	 --Delivery Rider PoV
	 SELECT *FROM OrderInformation

	 --Delete all rows inside the called table
	 DELETE FROM FoodItems;
	 

	--Stored Procedure
	CREATE PROCEDURE SelectAllCustomer
AS
SELECT * FROM AdminView
GO;

EXEC SelectAllCustomer;


CREATE PROCEDURE SelectFood @Price INT
AS
SELECT * FROM FoodItems WHERE Price = @Price;


EXEC SelectFood @Price = '60';



--View


