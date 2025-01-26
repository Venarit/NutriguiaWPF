CREATE TABLE [Food].[FoodType] (
    [idFoodType]  INT                IDENTITY (1, 1) NOT NULL,
    [Code]        NVARCHAR (100)     NOT NULL,
    [Name]        NVARCHAR (100)     NOT NULL,
    [Description] NVARCHAR (100)     NOT NULL,
    [Active]      BIT                DEFAULT ((1)) NULL,
    [UpdDateTime] DATETIMEOFFSET (7) DEFAULT (getdate()) NULL,
    [InsDateTime] DATETIMEOFFSET (7) DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([idFoodType] ASC)
);


GO


GO


GO


GO

CREATE TRIGGER [Food].[Update_DateTime]
ON [Food].[FoodType]
AFTER UPDATE 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [TipoAlimento]
	SET [UpdDateTime] = CURRENT_TIMESTAMP
END;