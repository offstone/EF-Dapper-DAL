
if OBJECTPROPERTY(object_id('dbo.usp_SearchOrders'), N'IsProcedure') = 1
DROP PROCEDURE [dbo].[usp_SearchOrders]
GO

if OBJECTPROPERTY(object_id('dbo.usp_SearchOrdersMulti'), N'IsProcedure') = 1
DROP PROCEDURE [dbo].[usp_SearchOrdersMulti]
GO


