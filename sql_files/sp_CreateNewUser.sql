use PetReTail
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- =============================================
alter PROCEDURE CreateNewUser
	-- Add the parameters for the stored procedure here
	@p_username varchar(50)
    ,@p_password varchar(100)
    ,@p_email varchar(255)
    ,@p_first varchar(50)
    ,@p_last varchar(50)
    ,@p_role varchar(10)
    ,@p_shelter varchar(6) = null
    ,@p_status int OUT
    ,@p_errmsg varchar(500) out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
BEGIN TRY
        insert into [User](
            username     
            ,[password]   
            ,[role]       
            ,shelter_id   
            ,first_name   
            ,last_name    
            ,email_address
        )
        values (@p_username, @p_password, @p_role, @p_shelter, @p_first, @p_last, @p_email)
        set @p_status = 0
        set @p_errmsg = 'Success'
END TRY
BEGIN CATCH
    set @p_status = 1
    set @p_errmsg = ERROR_MESSAGE()
END CATCH
END
GO