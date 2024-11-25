CREATE PROCEDURE [Food].[GetFood]
	@IdFoodType INT = NULL,
	@IdUnit INT = NULL
AS
BEGIN
	SELECT * FROM [Food].[Food] 
	WHERE	(@IdFoodType IS NULL OR @IdFoodType LIKE idFoodType)
	AND		(@IdUnit IS NULL OR @IdUnit LIKE idUnit)

	SELECT * FROM [Food].[FoodType]

	SELECT * FROM [Food].[Unit]
END