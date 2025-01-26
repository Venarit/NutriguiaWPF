CREATE TABLE [Food].[DishFood] (
    [idDishFood]  INT                IDENTITY (1, 1) NOT NULL,
    [idDish]      INT                NOT NULL,
    [idFood]      INT                NOT NULL,
    [Equivalent]  DECIMAL (10, 1)    NOT NULL,
    [Quantity]    DECIMAL (10, 1)    NOT NULL,
    [Kcal]        INT                NULL,
    [Protein]     DECIMAL (10, 2)    NULL,
    [Lipids]      DECIMAL (10, 2)    NULL,
    [Hco]         DECIMAL (10, 2)    NULL,
    [Active]      BIT                DEFAULT ((1)) NULL,
    [UpdDateTime] DATETIMEOFFSET (7) DEFAULT (getdate()) NULL,
    [InsDateTime] DATETIMEOFFSET (7) DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([idDishFood] ASC),
    FOREIGN KEY ([idDish]) REFERENCES [Food].[Dish] ([idDish])
);


GO


GO


GO


GO


GO
