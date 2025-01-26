CREATE PROCEDURE [NutritionalPlan].[GetPlanPatient]
	@IdPatient INT
AS
BEGIN

	SELECT * 
    FROM [NutritionalPlan].[PlanPatient] 
    WHERE idPatient = @IdPatient 
      AND Active = 1;

    SELECT P.*
    FROM [NutritionalPlan].[PlanPatient] PP
    JOIN [NutritionalPlan].[Plan] P ON PP.idPlan = P.idPlan
    WHERE PP.idPatient = @IdPatient 
      AND PP.Active = 1
      AND P.Active = 1;

    SELECT PO.*
    FROM [NutritionalPlan].[PlanOption] PO
    JOIN [NutritionalPlan].[Plan] P ON PO.idPlanOption IN (
        P.idPlanOption_1, P.idPlanOption_2, P.idPlanOption_3
    )
    WHERE P.idPlan IN (
        SELECT PP.idPlan
        FROM [NutritionalPlan].[PlanPatient] PP
        WHERE PP.idPatient = @IdPatient AND PP.Active = 1
    );

	WITH RelevantPlanOptions AS (
    SELECT PO.Breakfast, PO.Collation1, PO.Meal, PO.Collation2, PO.Dinner
    FROM [NutritionalPlan].[PlanOption] PO
    JOIN [NutritionalPlan].[Plan] P 
        ON PO.idPlanOption IN (P.idPlanOption_1, P.idPlanOption_2, P.idPlanOption_3)
		WHERE P.idPlan IN (
			SELECT PP.idPlan
			FROM [NutritionalPlan].[PlanPatient] PP
			WHERE PP.idPatient = @IdPatient AND PP.Active = 1
		)
	)
	SELECT PD.*
	FROM [NutritionalPlan].[PlanDish] PD
	WHERE PD.idPlanDish IN (
		SELECT Breakfast FROM RelevantPlanOptions
		UNION
		SELECT Collation1 FROM RelevantPlanOptions
		UNION
		SELECT Meal FROM RelevantPlanOptions
		UNION
		SELECT Collation2 FROM RelevantPlanOptions
		UNION
		SELECT Dinner FROM RelevantPlanOptions
	);

END