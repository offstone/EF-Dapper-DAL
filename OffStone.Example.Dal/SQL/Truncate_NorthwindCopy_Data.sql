
TRUNCATE TABLE [dbo].[OrderDetails]
GO

ALTER TABLE [dbo].[OrderDetails] NOCHECK CONSTRAINT FK_Order_Details_Orders
DELETE FROM [dbo].[Orders]
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT FK_Order_Details_Orders

GO
ALTER TABLE [dbo].[Orders] NOCHECK CONSTRAINT FK_Orders_Shippers
DELETE FROM [dbo].[Shippers]
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT FK_Orders_Shippers
GO


ALTER TABLE [dbo].[OrderDetails] NOCHECK CONSTRAINT FK_Order_Details_Products
DELETE FROM [dbo].[Products]
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT FK_Order_Details_Products
GO

ALTER TABLE [dbo].[Products] NOCHECK CONSTRAINT FK_Products_Suppliers
DELETE FROM [dbo].[Suppliers]
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT FK_Products_Suppliers
GO

TRUNCATE TABLE [dbo].[EmployeeTerritories]
GO
ALTER TABLE [dbo].[EmployeeTerritories] NOCHECK CONSTRAINT FK_EmployeeTerritories_Territories
DELETE FROM [dbo].[Territories]
ALTER TABLE [dbo].[EmployeeTerritories] CHECK CONSTRAINT FK_EmployeeTerritories_Territories
GO

ALTER TABLE [dbo].[Orders] NOCHECK CONSTRAINT FK_Orders_Employees
ALTER TABLE [dbo].[EmployeeTerritories] NOCHECK CONSTRAINT FK_EmployeeTerritories_Employees
DELETE FROM [dbo].[Employees]
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT FK_Orders_Employees
ALTER TABLE [dbo].[EmployeeTerritories] CHECK CONSTRAINT FK_EmployeeTerritories_Employees
GO

ALTER TABLE [dbo].[Orders] NOCHECK CONSTRAINT FK_Orders_Customers
DELETE FROM [dbo].[Customers]
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT FK_Orders_Customers
GO


ALTER TABLE [dbo].[Products] NOCHECK CONSTRAINT FK_Products_Categories
DELETE FROM [dbo].[Categories]
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT FK_Products_Categories