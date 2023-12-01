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
alter PROCEDURE AddAnimal 
	-- Add the parameters for the stored procedure here
    @p_name         varchar(50)
    ,@p_type        varchar(50)
    ,@p_breed       varchar(50)
    ,@p_gender      varchar(1) 
    ,@p_age         int 
    ,@p_is_vaxed    varchar(10) 
    ,@p_is_fixed    varchar(10) 
    ,@p_fees        decimal(6,2) 
    ,@p_shelter_id  varchar(6) 
    ,@p_description varchar(500) 
    ,@p_datereceived datetime
    ,@p_status      int OUT
    ,@p_errmsg      varchar(500) out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

    insert into Animal (  
        animal_name  
        ,[type]       
        ,breed        
        ,gender       
        ,age			
        ,date_received
        ,is_vaxed     
        ,is_fixed     
        ,[fees]       
        ,shelter_id   
        ,[description]
    ) 
    values(
        @p_name    
        ,@p_type    
        ,@p_breed    
        ,@p_gender 
        ,@p_age
        ,@p_datereceived
        ,@p_is_vaxed  
        ,@p_is_fixed
        ,@p_fees 
        ,@p_shelter_id 
        ,@p_description
    )

    set @p_status = 0
    set @p_errmsg = 'Success'

	SELECT 
        @p_status     
        ,@p_errmsg     
END
GO