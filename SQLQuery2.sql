CREATE PROCEDURE spGetProductByID
(
    @ProductID INT
)
AS
BEGIN 
    SELECT @ProductID AS ProductID, ProductName, ProductPrice
    FROM dbo.Product
    WHERE ProductID = @ProductID;
END
