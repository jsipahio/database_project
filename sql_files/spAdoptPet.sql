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
create PROCEDURE AdoptPet 
	-- Add the parameters for the stored procedure here
    @p_adopterID    int
    ,@p_animalID    int
    ,@p_shelterID   varchar(6)
    ,@p_status      int OUT
    ,@p_errmsg      varchar(500) out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

    insert into AdoptionDetails (  
        adopter_id
        ,animal_id
        ,shelter_id
        ,adoption_date
    ) 
    values(
        @p_adopterID 
        ,@p_animalID 
        ,@p_shelterID
        ,getdate()
    )

    set @p_status = 0
    set @p_errmsg = 'Success'

	SELECT 
        @p_status     
        ,@p_errmsg     
END
GO