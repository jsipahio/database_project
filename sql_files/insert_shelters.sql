use PetReTail
go

insert into Shelter (  
    shelter_id,
    shelter_name, 
    street_address,
    city,
    [state],
    zip,
    phone_num,
    email_address,
	[description]
)
values  ('shel01', 'Shelter 1', '123 Main Street', 'North Canton', 'OH', '44720', '3304997387', 'shelter1@gmail.com', 'The mission of Shelter 1 is to find a home for every pet :).')
        ,('shel02', 'Shelter 2', '753 Park Drive', 'Alliace', 'OH', '44601', '3308216888', 'shelter2@yahoo.com', 'The mission of Shelter 2 is also to find a home for every pet :).')