CREATE TABLE [Food].[Food] (
    [idFood]        INT                IDENTITY (1, 1) NOT NULL,
    [idFoodType]    INT                NOT NULL,
    [Name]          NVARCHAR (100)     NOT NULL,
    [Quantity]      NUMERIC (18, 2)    NOT NULL,
    [idUnit]        INT                NOT NULL,
    [GrossWeight]   NUMERIC (18, 2)    NOT NULL,
    [NetWeight]     NUMERIC (18, 2)    NOT NULL,
    [Energy]        NUMERIC (18, 2)    NOT NULL,
    [Protein]       NUMERIC (18, 2)    NOT NULL,
    [Lipids]        NUMERIC (18, 2)    NOT NULL,
    [Hco]           NUMERIC (18, 2)    NOT NULL,
    [Fiber]         NUMERIC (18, 2)    NULL,
    [VitaminA]      NUMERIC (18, 2)    NULL,
    [AscorbicAcid]  NUMERIC (18, 2)    NULL,
    [FolicAcid]     NUMERIC (18, 2)    NULL,
    [Iron]          NUMERIC (18, 2)    NULL,
    [Potasium]      NUMERIC (18, 2)    NULL,
    [GlycemicIndex] NUMERIC (18, 2)    NULL,
    [GlycemicLoad]  NUMERIC (18, 2)    NULL,
    [Sugar]         NUMERIC (18, 2)    NULL,
    [Sodium]        NUMERIC (18, 2)    NULL,
    [Calcium]       NUMERIC (18, 2)    NULL,
    [Selenium]      NUMERIC (18, 2)    NULL,
    [Phosphorus]    NUMERIC (18, 2)    NULL,
    [Colesterol]    NUMERIC (18, 2)    NULL,
    [Active]        BIT                DEFAULT ((1)) NULL,
    [UpdDateTime]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NOT NULL,
    [InsDateTime]   DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NOT NULL,
    PRIMARY KEY CLUSTERED ([idFood] ASC),
    FOREIGN KEY ([idFood]) REFERENCES [Food].[Food] ([idFood]),
    FOREIGN KEY ([idFoodType]) REFERENCES [Food].[FoodType] ([idFoodType]),
    FOREIGN KEY ([idUnit]) REFERENCES [Food].[Unit] ([idUnit])
);


GO


GO


GO


GO


GO

