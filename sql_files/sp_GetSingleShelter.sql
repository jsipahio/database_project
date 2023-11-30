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
alter PROCEDURE GetSingleShelter
	-- Add the parameters for the stored procedure here
	@p_shelter_id     VARCHAR(6)
    ,@p_name          varchar(50) OUT
    ,@p_street        varchar(50) OUT
    ,@p_city          varchar(50) OUT
    ,@p_state         varchar(2) OUT
    ,@p_zip           varchar(5) OUT
    ,@p_phone         varchar(10) out
    ,@p_email         varchar(50) out
    ,@p_description   varchar(500) out
    ,@p_status        int OUT
    ,@p_errmsg        varchar(500) out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

    select 
        @p_name = shelter_name   
        ,@p_street = street_address
        ,@p_city = city   
        ,@p_state = state
        ,@p_zip = zip
        ,@p_phone = phone_num  
        ,@p_email = email_address
        ,@p_description = description
    from Shelter
    where shelter_id = @p_shelter_id

    set @p_status = 0
    set @p_errmsg = 'Success'

	SELECT 
        @p_name        
        ,@p_street       
        ,@p_city      
        ,@p_state     
        ,@p_zip        
        ,@p_phone   
        ,@p_email
        ,@p_description
        ,@p_status     
        ,@p_errmsg     
END
GO