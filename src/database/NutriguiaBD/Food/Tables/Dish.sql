CREATE TABLE [Food].[Dish] (
    [idDish]      INT                IDENTITY (1, 1) NOT NULL,
    [idDishType]  INT                NULL,
    [Code]        NVARCHAR (100)     NULL,
    [Name]        NVARCHAR (100)     NULL,
    [Description] NVARCHAR (150)     NULL,
    [Kcal]        INT                NULL,
    [Protein]     DECIMAL (10, 2)    NULL,
    [Lipids]      DECIMAL (10, 2)    NULL,
    [Hco]         DECIMAL (10, 2)    NULL,
    [Active]      BIT                DEFAULT ((1)) NOT NULL,
    [UpdDateTime] DATETIMEOFFSET (7) DEFAULT (getdate()) NOT NULL,
    [InsDateTime] DATETIMEOFFSET (7) DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([idDish] ASC),
    FOREIGN KEY ([idDishType]) REFERENCES [Food].[DishType] ([idDishType])
);


GO


GO


GO


GO


GO
