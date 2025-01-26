CREATE PROCEDURE [Food].[SetDish]
	@IdDish INT
	,@Code NVARCHAR(100) = NULL
	,@Name NVARCHAR(100) = NULL
	,@Description NVARCHAR(150) = NULL
	,@Kcal INT = NULL
	,@Protein DECIMAL(10,2) = NULL
	,@Lipids DECIMAL(10,2) = NULL
	,@Hco DECIMAL(10,2) = NULL
	,@DishFoods [Food].[TVP_DishFood] READONLY
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

		IF (@IdDish IS NULL OR @IdDish = 0)
		BEGIN
			INSERT INTO [Food].[Dish](Code, [Name], [Description], Kcal, Protein, Lipids, Hco, Active)
			VALUES (@Code, @Name, @Description, @Kcal, @Protein, @Lipids, @Hco, 1)

			 DECLARE @NewDishId INT = SCOPE_IDENTITY();

			INSERT INTO [Food].[DishFood](idDish, idFood, Equivalent, Quantity, Kcal, Protein, Lipids, Hco, Active)
			SELECT @NewDishId, FoodId, Equivalent, Quantity, Kcal, Protein, Lipids, Hco, 1
			FROM @DishFoods
		END
		ELSE
		BEGIN
			UPDATE [Food].[Dish]
			SET
				Code = @Code
				,[Name] = @Name
				,[Description] = @Description
				,Kcal = @Kcal
				,Protein = @Protein
				,Lipids = @Lipids
				,Hco = @Hco
				,UpdDateTime = SYSDATETIME()
			WHERE
				idDish = @IdDish

			DELETE FROM [Food].[DishFood]
            WHERE idDish = @IdDish;

			INSERT INTO [Food].[DishFood] (idDish, idFood, Equivalent, Quantity, Kcal, Protein, Lipids, Hco, Active)
            SELECT @IdDish, FoodId, Equivalent, Quantity, Kcal, Protein, Lipids, Hco, 1
            FROM @DishFoods;
		END

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END