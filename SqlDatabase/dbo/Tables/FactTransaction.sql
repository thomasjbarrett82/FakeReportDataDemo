CREATE TABLE [dbo].[FactTransaction]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CustomerId] INT NULL, 
    [ProductId] INT NOT NULL, 
    [DateTimeSold] DATETIME2 NOT NULL,
    [QuantitySold] INT NOT NULL, 
    [PricePerUnit] DECIMAL(6, 2) NOT NULL DEFAULT 0, 
    [TotalPrice] DECIMAL(6, 2) NOT NULL DEFAULT 0,
    FOREIGN KEY (CustomerId) REFERENCES DimCustomer(Id),
    FOREIGN KEY (ProductId) REFERENCES DimProduct(Id)
)
GO

CREATE INDEX IX_Transaction_Customer ON [FactTransaction] (CustomerId)
GO

CREATE INDEX IX_Transaction_Product ON [FactTransaction] (ProductId)
GO
