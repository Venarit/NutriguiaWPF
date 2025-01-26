CREATE TABLE [Catalog].[Macronutrients] (
    [idMacronutrients] INT                IDENTITY (1, 1) NOT NULL,
    [Code]             NVARCHAR (50)      NOT NULL,
    [Name]             NVARCHAR (50)      NULL,
    [Description]      NVARCHAR (150)     NULL,
    [Hco]              DECIMAL (2, 2)     NOT NULL,
    [Lipids]           DECIMAL (2, 2)     NOT NULL,
    [Protein]          DECIMAL (2, 2)     NOT NULL,
    [Active]           BIT                DEFAULT ((1)) NULL,
    [UpdDateTime]      DATETIMEOFFSET (7) DEFAULT (getdate()) NULL,
    [InsDateTime]      DATETIMEOFFSET (7) DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([idMacronutrients] ASC)
);


GO


GO


GO


GO

CREATE TRIGGER [Catalog].Macronutrientes_UpdateDateTime
ON [Catalog].[Macronutrients]
AFTER UPDATE 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [Common].[Macronutrientes]
	SET [UpdDateTime] = CURRENT_TIMESTAMP
END;