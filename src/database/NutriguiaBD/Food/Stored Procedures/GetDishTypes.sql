CREATE PROCEDURE [Food].[GetDishTypes]
AS
BEGIN
	SELECT * FROM [Food].DishType WHERE Active = 1
END