
IF OBJECT_ID('usp_SearchOrdersMulti', 'P') IS NOT NULL
BEGIN
	PRINT '-- DROP: [dbo].[usp_SearchOrdersMulti]'
	DROP PROCEDURE [dbo].[usp_SearchOrdersMulti]
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Off Stone example DAL
-- Create date: 2021-02-25
-- Description:	Example stored procedure to return results of related table
--				data from multiple tables in multiple resultsets to be linked in
--				a large related graph of Order data by Dapper mappings.
-- Example:		EXEC usp_SearchOrdersMulti @CustomerId = 'ISLAT'
-- =============================================

PRINT '-- CREATE: [dbo].[usp_SearchOrdersMulti]'
GO
CREATE PROCEDURE usp_SearchOrdersMulti
	@CustomerId NCHAR(5) = NULL,
	@OrderDate DATE = NULL,
	@CompanyName NVARCHAR(40) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- Orders
    SELECT O.[OrderID], O.[CustomerID], O.[EmployeeID], O.[OrderDate], O.[RequiredDate],
		O.[ShippedDate], O.[ShipVia], O.[Freight], O.[ShipName], O.[ShipAddress],
		O.[ShipCity], O.[ShipRegion], O.[ShipPostalCode], O.[ShipCountry],
		C.[CustomerID], C.[CompanyName], C.[ContactName], C.[ContactTitle], 
		C.[Address], C.[City], C.[Region], C.[PostalCode], C.[Country], C.[Phone], C.[Fax]
	FROM [dbo].[Orders] O
	INNER JOIN [dbo].[Customers] C ON C.CustomerID = O.CustomerID
	WHERE	O.CustomerId = IsNull(@CustomerId, O.CustomerId)
	AND		O.OrderDate = IsNull(@OrderDate, O.OrderDate)
	AND		C.CompanyName = IsNull(@CompanyName, C.CompanyName)
	ORDER BY O.OrderDate DESC, O.OrderId ASC

	-- Customers
    SELECT C.[CustomerID], C.[CompanyName], C.[ContactName], C.[ContactTitle], 
		C.[Address], C.[City], C.[Region], C.[PostalCode], C.[Country], C.[Phone], C.[Fax]
	FROM [dbo].[Orders] O
	INNER JOIN [dbo].[Customers] C ON C.CustomerID = O.CustomerID
	WHERE	O.CustomerId = IsNull(@CustomerId, O.CustomerId)
	AND		O.OrderDate = IsNull(@OrderDate, O.OrderDate)
	AND		C.CompanyName = IsNull(@CompanyName, C.CompanyName)
	ORDER BY O.OrderDate DESC, O.OrderId ASC
	
	-- OrderDetails
    SELECT OD.[OrderID], OD.[ProductID], OD.[UnitPrice], OD.[Quantity], OD.[Discount]
	FROM [dbo].[Orders] O
	INNER JOIN [dbo].[OrderDetails] OD ON OD.OrderID = O.OrderID
	INNER JOIN [dbo].[Customers] C ON C.CustomerID = O.CustomerID
	WHERE	O.CustomerId = IsNull(@CustomerId, O.CustomerId)
	AND		O.OrderDate = IsNull(@OrderDate, O.OrderDate)
	AND		C.CompanyName = IsNull(@CompanyName, C.CompanyName)
	ORDER BY O.OrderDate DESC, O.OrderId ASC
	
END
GO




