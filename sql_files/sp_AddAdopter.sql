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
alter PROCEDURE AddAdopter 
	-- Add the parameters for the stored procedure here
    @p_id           int OUT
    ,@p_first       varchar(50)
    ,@p_last        varchar(50)
    ,@p_street      varchar(255)
    ,@p_city        varchar(100) 
    ,@p_state       varchar(2) 
    ,@p_zip         varchar(5) 
    ,@p_phone       varchar(10) 
    ,@p_email       varchar(255)
    ,@p_status      int OUT
    ,@p_errmsg      varchar(500) out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

    insert into Adopter (  
        first_name  
        ,last_name       
        ,street_address        
        ,city       
        ,[state]			
        ,zip
        ,phone_num     
        ,email_address
    ) 
    values(
        @p_first    
        ,@p_last    
        ,@p_street    
        ,@p_city 
        ,@p_state
        ,@p_zip
        ,@p_phone 
        ,@p_email
    )
    select @p_id = adopter_id
    from Adopter where
    first_name = @p_first
    and last_name = @p_last
    and street_address = @p_street
    and city = @p_city
    and [state] = @p_state
    and zip = @p_zip
    and phone_num = @p_phone
    and email_address = @p_email

    set @p_status = 0
    set @p_errmsg = 'Success'

	SELECT 
        @p_status
        ,@p_id     
        ,@p_errmsg     
END
GO