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
create PROCEDURE AddShelter
	-- Add the parameters for the stored procedure here
	@p_shelter_id     VARCHAR(6)
    ,@p_name          varchar(50)
    ,@p_street        varchar(50)
    ,@p_city          varchar(50)
    ,@p_state         varchar(2) 
    ,@p_zip           varchar(5) 
    ,@p_phone         varchar(10)
    ,@p_email         varchar(50)
    ,@p_description   varchar(1500) 
    ,@p_status        int OUT
    ,@p_errmsg        varchar(500) out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

    insert into Shelter (
        shelter_id      
        ,shelter_name    
        ,street_address  
        ,city            
        ,[state]         
        ,zip             
        ,phone_num       
        ,email_address   
        ,[description]	
    )
    values (
        @p_shelter_id
        ,@p_name        
        ,@p_street     
        ,@p_city       
        ,@p_state      
        ,@p_zip        
        ,@p_phone      
        ,@p_email      
        ,@p_description
    )

    set @p_status = 0
    set @p_errmsg = 'Success'

	SELECT 
        @p_status     
        ,@p_errmsg     
END
GO