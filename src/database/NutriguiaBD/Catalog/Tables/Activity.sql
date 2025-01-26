CREATE TABLE [Catalog].[Activity] (
    [idActivity]  INT                IDENTITY (1, 1) NOT NULL,
    [Code]        NVARCHAR (50)      NOT NULL,
    [Name]        NVARCHAR (50)      NULL,
    [Description] NVARCHAR (150)     NULL,
    [Factor]      DECIMAL (5, 3)     NOT NULL,
    [Active]      BIT                DEFAULT ((1)) NOT NULL,
    [UpdDateTime] DATETIMEOFFSET (7) DEFAULT (getdate()) NULL,
    [InsDateTime] DATETIMEOFFSET (7) DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([idActivity] ASC)
);


GO


GO


GO


GO

CREATE TRIGGER [Catalog].Actividad_UpdateDateTime
ON [Catalog].[Activity]
AFTER UPDATE 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [Common].[Actividad]
	SET [UpdDateTime] = CURRENT_TIMESTAMP
END;