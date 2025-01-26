CREATE TABLE [Food].[Unit] (
    [idUnit]      INT                IDENTITY (1, 1) NOT NULL,
    [Code]        NVARCHAR (100)     NOT NULL,
    [Name]        NVARCHAR (100)     NULL,
    [Description] NVARCHAR (100)     NULL,
    [Symbol]      NVARCHAR (10)      NULL,
    [Active]      BIT                DEFAULT ((1)) NULL,
    [UpdDateTime] DATETIMEOFFSET (7) DEFAULT (getdate()) NULL,
    [InsDateTime] DATETIMEOFFSET (7) DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([idUnit] ASC)
);


GO


GO


GO


GO

CREATE TRIGGER [Food].Unidad_UpdateDateTime
ON [Food].[Unit]
AFTER UPDATE 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [Alimento].[Unidad]
	SET [UpdDateTime] = CURRENT_TIMESTAMP
END;