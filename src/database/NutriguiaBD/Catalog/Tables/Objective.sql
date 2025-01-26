CREATE TABLE [Catalog].[Objective] (
    [idObjective] INT                IDENTITY (1, 1) NOT NULL,
    [Code]        NVARCHAR (50)      NOT NULL,
    [Name]        NVARCHAR (50)      NULL,
    [Description] NVARCHAR (150)     NULL,
    [Calories]    INT                NOT NULL,
    [Active]      BIT                DEFAULT ((1)) NULL,
    [UpdDateTime] DATETIMEOFFSET (7) DEFAULT (getdate()) NULL,
    [InsDateTime] DATETIMEOFFSET (7) DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([idObjective] ASC)
);


GO


GO


GO


GO

CREATE TRIGGER [Catalog].Objetivo_UpdateDateTime
ON [Catalog].[Objective]
AFTER UPDATE 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [Common].[Objetivo]
	SET [UpdDateTime] = CURRENT_TIMESTAMP
END;