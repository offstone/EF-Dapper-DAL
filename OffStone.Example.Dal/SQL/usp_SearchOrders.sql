
IF OBJECT_ID('usp_SearchOrders', 'P') IS NOT NULL
BEGIN
	PRINT '-- DROP: [dbo].[usp_SearchOrders]'
	DROP PROCEDURE [dbo].[usp_SearchOrders]
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Off Stone example DAL
-- Create date: 2021-02-25
-- Description:	Example stored procedure to join a bunch of tables and return
--				a large related graph of Order data for mapping by Dapper.
-- Example:		EXEC usp_SearchOrders @CustomerId = 'ISLAT'
-- =============================================

PRINT '-- CREATE: [dbo].[usp_SearchOrders]'
GO
CREATE PROCEDURE usp_SearchOrders
	@CustomerId NCHAR(5) = NULL,
	@OrderDate DATE = NULL,
	@CompanyName NVARCHAR(40) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT O.[OrderID], O.[CustomerID], O.[EmployeeID], O.[OrderDate], O.[RequiredDate],
		O.[ShippedDate], O.[ShipVia], O.[Freight], O.[ShipName], O.[ShipAddress],
		O.[ShipCity], O.[ShipRegion], O.[ShipPostalCode], O.[ShipCountry],
		OD.[OrderID], OD.[ProductID], OD.[UnitPrice], OD.[Quantity], OD.[Discount],
		C.[CustomerID], C.[CompanyName], C.[ContactName], C.[ContactTitle], 
		C.[Address], C.[City], C.[Region], C.[PostalCode], C.[Country], C.[Phone], C.[Fax]
	FROM [dbo].[Orders] O
	INNER JOIN [dbo].[OrderDetails] OD ON OD.OrderID = O.OrderID
	INNER JOIN [dbo].[Customers] C ON C.CustomerID = O.CustomerID
	WHERE	O.CustomerId = IsNull(@CustomerId, O.CustomerId)
	AND		O.OrderDate = IsNull(@OrderDate, O.OrderDate)
	AND		C.CompanyName = IsNull(@CompanyName, C.CompanyName)
	ORDER BY O.OrderDate DESC, O.OrderId ASC

END
GO




