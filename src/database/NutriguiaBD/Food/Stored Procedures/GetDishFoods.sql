CREATE PROCEDURE [Food].[GetDishFoods]
	@IdDish	INT
AS
BEGIN
	SELECT * FROM [Food].DishFood WHERE idDish = @IdDish
	
	SELECT F.* FROM [Food].DishFood DF 
	LEFT JOIN [Food].[Food] F ON DF.idFood = F.idFood
	WHERE idDish = @IdDish AND DF.Active = 1

	SELECT FT.* FROM [Food].DishFood DF 
	LEFT JOIN [Food].[Food] F ON DF.idFood = F.idFood
	LEFT JOIN [Food].[FoodType] FT ON F.idFoodType = FT.idFoodType
	WHERE idDish = @IdDish AND DF.Active = 1

	SELECT U.* FROM [Food].DishFood DF 
	LEFT JOIN [Food].[Food] F ON DF.idFood = F.idFood
	LEFT JOIN [Food].[Unit] U ON U.idUnit = F.idUnit
	WHERE idDish = @IdDish AND DF.Active = 1
END