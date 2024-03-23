CREATE TABLE [Sales].[Orders] (
    [CustomerID] INT      NOT NULL,
    [OrderID]    INT      IDENTITY (1, 1) NOT NULL,
    [OrderDate]  DATETIME CONSTRAINT [Def_Orders_OrderDate] DEFAULT (getdate()) NOT NULL,
    [FilledDate] DATETIME NULL,
    [Status]     CHAR (1) CONSTRAINT [Def_Orders_Status] DEFAULT ('O') NOT NULL,
    [Amount]     INT      NOT NULL,
    CONSTRAINT [PK_Orders_OrderID] PRIMARY KEY CLUSTERED ([OrderID] ASC),
    CONSTRAINT [CK_Orders_FilledDate] CHECK ([FilledDate]>=[OrderDate] AND [FilledDate]<'01/01/2030'),
    CONSTRAINT [CK_Orders_OrderDate] CHECK ([OrderDate]>'01/01/2005' AND [OrderDate]<'01/01/2030'),
    CONSTRAINT [FK_Orders_Customer_CustID] FOREIGN KEY ([CustomerID]) REFERENCES [Sales].[Customer] ([CustomerID])
);

