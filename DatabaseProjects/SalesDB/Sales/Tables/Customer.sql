CREATE TABLE [Sales].[Customer] (
    [CustomerID]   INT           IDENTITY (1, 1) NOT NULL,
    [CustomerName] NVARCHAR (40) NOT NULL,
    [YTDOrders]    INT           CONSTRAINT [Def_Customer_YTDOrders] DEFAULT ((0)) NOT NULL,
    [YTDSales]     INT           CONSTRAINT [Def_Customer_YTDSales] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Customer_CustID] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);

