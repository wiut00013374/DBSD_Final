DECLARE @ProductID INT = 1; -- Declare the @ProductID parameter and set its value

SELECT @ProductID AS ProductID, ProductName, ProductPrice
FROM dbo.Product
WHERE ProductID = @ProductID; -- Use the @ProductID parameter in your WHERE clause
