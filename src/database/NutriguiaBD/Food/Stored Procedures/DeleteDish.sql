CREATE PROCEDURE [Food].[DeleteDish]
	@IdDish INT
AS
BEGIN
	DELETE FROM [Food].[DishFood] WHERE idDish = @IdDish;
	DELETE FROM [Food].[Dish] WHERE idDish = @IdDish;
END