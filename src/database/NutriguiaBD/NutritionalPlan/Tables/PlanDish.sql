CREATE TABLE [NutritionalPlan].[PlanDish] (
    [idPlanDish]  INT                IDENTITY (1, 1) NOT NULL,
    [idDish]      INT                NULL,
    [Active]      BIT                DEFAULT ((1)) NOT NULL,
    [InsDateTime] DATETIMEOFFSET (7) DEFAULT (sysdatetime()) NOT NULL,
    [UpdDateTime] DATETIMEOFFSET (7) DEFAULT (sysdatetime()) NOT NULL,
    PRIMARY KEY CLUSTERED ([idPlanDish] ASC),
    FOREIGN KEY ([idDish]) REFERENCES [Food].[Dish] ([idDish])
);


GO


GO


GO


GO


GO


GO