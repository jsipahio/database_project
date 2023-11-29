-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
use PetReTail
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- =============================================
alter PROCEDURE GetSingleAnimal 
	-- Add the parameters for the stored procedure here
	@p_animal_id    INT
    ,@p_name        varchar(50) OUT
    ,@p_type        varchar(50) OUT
    ,@p_breed       varchar(50) OUT
    ,@p_gender      varchar(1) OUT
    ,@p_age         int OUT
    ,@p_is_vaxed    varchar(10) out
    ,@p_is_fixed    varchar(10) out
    ,@p_fees        decimal(6,2) out
    ,@p_shelter_id  varchar(6) out
    ,@p_status      int OUT
    ,@p_errmsg      varchar(500) out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

    select 
        @p_name = animal_name   
        ,@p_type = type    
        ,@p_breed = breed   
        ,@p_gender = gender
        ,@p_age = age
        ,@p_is_vaxed = is_vaxed  
        ,@p_is_fixed = is_fixed
        ,@p_fees = fees 
        ,@p_shelter_id = shelter_id 
    from Animal
    where animal_id = @p_animal_id

    if @p_is_fixed = '1'
    BEGIN
        set @p_is_fixed = 'True'
    end
    ELSE
    BEGIN
        set @p_is_fixed = 'False'
    END

    if @p_is_vaxed = '1'
    BEGIN
        set @p_is_vaxed = 'True'
    end
    ELSE
    BEGIN
        set @p_is_vaxed = 'False'
    END

    set @p_status = 0
    set @p_errmsg = 'Success'

	SELECT 
        @p_name        
        ,@p_type       
        ,@p_breed      
        ,@p_gender     
        ,@p_age        
        ,@p_is_vaxed   
        ,@p_is_fixed   
        ,@p_fees       
        ,@p_shelter_id 
        ,@p_status     
        ,@p_errmsg     
END
GO