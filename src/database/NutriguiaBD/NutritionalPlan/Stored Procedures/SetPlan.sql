CREATE PROCEDURE [NutritionalPlan].[SetPlan]
    @IdPlan INT,
    @IdPatient INT,
    @IdDishBreakfast INT,
    @IdDishCollation1 INT,
    @IdDishMeal INT,
    @IdDishCollation2 INT,
    @IdDishDinner INT,
    @Kcal INT,
    @Protein DECIMAL(10,2),
    @Lipids DECIMAL(10,2),
    @Hco DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PlanDishBreakfastId INT;
    DECLARE @PlanDishCollation1Id INT;
    DECLARE @PlanDishMealId INT;
    DECLARE @PlanDishCollation2Id INT;
    DECLARE @PlanDishDinnerId INT;

    -- Insertar o recuperar ID de PlanDish para cada plato
    IF NOT EXISTS (SELECT 1 FROM [NutritionalPlan].[PlanDish] WHERE idDish = @IdDishBreakfast)
    BEGIN
        INSERT INTO [NutritionalPlan].[PlanDish] (idDish) VALUES (@IdDishBreakfast);
        SET @PlanDishBreakfastId = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SELECT @PlanDishBreakfastId = idPlanDish FROM [NutritionalPlan].[PlanDish] WHERE idDish = @IdDishBreakfast;
    END

    IF NOT EXISTS (SELECT 1 FROM [NutritionalPlan].[PlanDish] WHERE idDish = @IdDishCollation1)
    BEGIN
        INSERT INTO [NutritionalPlan].[PlanDish] (idDish) VALUES (@IdDishCollation1);
        SET @PlanDishCollation1Id = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SELECT @PlanDishCollation1Id = idPlanDish FROM [NutritionalPlan].[PlanDish] WHERE idDish = @IdDishCollation1;
    END

    IF NOT EXISTS (SELECT 1 FROM [NutritionalPlan].[PlanDish] WHERE idDish = @IdDishMeal)
    BEGIN
        INSERT INTO [NutritionalPlan].[PlanDish] (idDish) VALUES (@IdDishMeal);
        SET @PlanDishMealId = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SELECT @PlanDishMealId = idPlanDish FROM [NutritionalPlan].[PlanDish] WHERE idDish = @IdDishMeal;
    END

    IF NOT EXISTS (SELECT 1 FROM [NutritionalPlan].[PlanDish] WHERE idDish = @IdDishCollation2)
    BEGIN
        INSERT INTO [NutritionalPlan].[PlanDish] (idDish) VALUES (@IdDishCollation2);
        SET @PlanDishCollation2Id = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SELECT @PlanDishCollation2Id = idPlanDish FROM [NutritionalPlan].[PlanDish] WHERE idDish = @IdDishCollation2;
    END

    IF NOT EXISTS (SELECT 1 FROM [NutritionalPlan].[PlanDish] WHERE idDish = @IdDishDinner)
    BEGIN
        INSERT INTO [NutritionalPlan].[PlanDish] (idDish) VALUES (@IdDishDinner);
        SET @PlanDishDinnerId = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SELECT @PlanDishDinnerId = idPlanDish FROM [NutritionalPlan].[PlanDish] WHERE idDish = @IdDishDinner;
    END

    -- Insertar nueva opción de plan usando los IDs recuperados o creados
    INSERT INTO [NutritionalPlan].[PlanOption] 
        (Breakfast, Collation1, Meal, Collation2, Dinner, Kcal, Protein, Lipids, Hco)
    VALUES 
        (@PlanDishBreakfastId, @PlanDishCollation1Id, @PlanDishMealId, @PlanDishCollation2Id, @PlanDishDinnerId, @Kcal, @Protein, @Lipids, @Hco);

    DECLARE @NewPlanOptionId INT = SCOPE_IDENTITY();
	DECLARE @IdNewPlan INT

    -- Insertar o actualizar el plan
    IF (@IdPlan IS NOT NULL AND @IdPlan <> 0)
    BEGIN
		IF ((SELECT idPlanOption_2 FROM [NutritionalPlan].[Plan] WHERE idPlan = @IdPlan) IS NULL)
		BEGIN
			UPDATE [NutritionalPlan].[Plan]
			SET idPlanOption_2 = @NewPlanOptionId, UpdDateTime = SYSDATETIME()
			WHERE idPlan = @IdPlan;
		END
		ELSE IF ((SELECT idPlanOption_3 FROM [NutritionalPlan].[Plan] WHERE idPlan = @IdPlan) IS NULL)
		BEGIN
			UPDATE [NutritionalPlan].[Plan]
			SET idPlanOption_3 = @NewPlanOptionId, UpdDateTime = SYSDATETIME()
			WHERE idPlan = @IdPlan;
		END
    END
    ELSE
    BEGIN
        INSERT INTO [NutritionalPlan].[Plan] (idPlanOption_1)
        VALUES (@NewPlanOptionId);
        SET @IdNewPlan = SCOPE_IDENTITY();
    END

    -- Insertar relación entre el plan y el paciente
    IF (@IdPlan IS NULL OR @IdPlan = 0)
    BEGIN
		UPDATE [NutritionalPlan].[PlanPatient] SET Active = 1 ,
		UpdDateTime =  SYSDATETIME()
		WHERE idPatient = @IdPatient;

        INSERT INTO [NutritionalPlan].[PlanPatient] (idPlan, idPatient)
        VALUES (@IdNewPlan, @IdPatient);
    END
END;