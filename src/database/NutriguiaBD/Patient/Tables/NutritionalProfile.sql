CREATE TABLE [Patient].[NutritionalProfile] (
    [idNutritionalProfile] INT                IDENTITY (1, 1) NOT NULL,
    [idPatient]            INT                NOT NULL,
    [Height]               INT                NOT NULL,
    [Sex]                  NVARCHAR (50)      NOT NULL,
    [idActivity]           INT                NOT NULL,
    [idObjective]          INT                NOT NULL,
    [idMacronutrients]     INT                NOT NULL,
    [Active]               BIT                NOT NULL,
    [UpdDateTime]          DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NOT NULL,
    [InsDateTime]          DATETIMEOFFSET (7) DEFAULT (sysdatetimeoffset()) NOT NULL,
    PRIMARY KEY CLUSTERED ([idNutritionalProfile] ASC),
    FOREIGN KEY ([idActivity]) REFERENCES [Catalog].[Activity] ([idActivity]),
    FOREIGN KEY ([idMacronutrients]) REFERENCES [Catalog].[Macronutrients] ([idMacronutrients]),
    FOREIGN KEY ([idObjective]) REFERENCES [Catalog].[Objective] ([idObjective]),
    FOREIGN KEY ([idPatient]) REFERENCES [Patient].[Patient] ([idPatient])
);


GO


GO


GO


GO


GO


GO