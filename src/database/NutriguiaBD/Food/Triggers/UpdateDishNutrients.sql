CREATE TRIGGER trg_UpdateDishNutrients
ON [Food].[DishFood]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Handle cases where rows are deleted
    IF EXISTS (SELECT 1 FROM deleted)
    BEGIN
        -- Update all affected dishes after a DELETE
        UPDATE [Food].[Dish]
        SET 
            [Kcal] = COALESCE(SUM_DF.[TotalKcal], 0),
            [Protein] = COALESCE(SUM_DF.[TotalProtein], 0),
            [Lipids] = COALESCE(SUM_DF.[TotalLipids], 0),
            [Hco] = COALESCE(SUM_DF.[TotalHco], 0)
        FROM [Food].[Dish] D
        LEFT JOIN (
            SELECT [idDish],
                   SUM([Kcal]) AS [TotalKcal],
                   SUM([Protein]) AS [TotalProtein],
                   SUM([Lipids]) AS [TotalLipids],
                   SUM([Hco]) AS [TotalHco]
            FROM [Food].[DishFood]
            GROUP BY [idDish]
        ) SUM_DF ON D.[idDish] = SUM_DF.[idDish]
        WHERE D.[idDish] IN (SELECT DISTINCT [idDish] FROM deleted);
    END

    -- Handle cases where rows are inserted or updated
    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        -- Update all affected dishes after an INSERT or UPDATE
        UPDATE [Food].[Dish]
        SET 
            [Kcal] = COALESCE(SUM_DF.[TotalKcal], 0),
            [Protein] = COALESCE(SUM_DF.[TotalProtein], 0),
            [Lipids] = COALESCE(SUM_DF.[TotalLipids], 0),
            [Hco] = COALESCE(SUM_DF.[TotalHco], 0)
        FROM [Food].[Dish] D
        JOIN (
            SELECT [idDish],
                   SUM([Kcal]) AS [TotalKcal],
                   SUM([Protein]) AS [TotalProtein],
                   SUM([Lipids]) AS [TotalLipids],
                   SUM([Hco]) AS [TotalHco]
            FROM [Food].[DishFood]
            GROUP BY [idDish]
        ) SUM_DF ON D.[idDish] = SUM_DF.[idDish]
        WHERE D.[idDish] IN (SELECT DISTINCT [idDish] FROM inserted);
    END
END;
