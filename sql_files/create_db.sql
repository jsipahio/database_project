create database PetReTail
go

use PetReTail
go

create table Shelter(
    shelter_id      varchar(6) primary key,
    shelter_name    varchar(50),
    street_address  varchar(255),
    city            varchar(100),
    [state]         varchar(2),
    zip             varchar(5),
    phone_num       varchar(10),
    email_address   varchar(10),
	[description]	varchar(500)
)
go

create table Animal(
    animal_id       int primary key identity(1,1),
    animal_name     varchar(50),
    [type]          varchar(50),
    breed           varchar(50),
    gender          varchar(1),
	age				int,
    date_received   datetime,
    is_vaxed        bit,
    is_fixed        bit,
    [fees]          decimal(6, 2),
    shelter_id      varchar(6) foreign key references Shelter(shelter_id),
	[description]	varchar(500)
)
go

create table Adopter(
    adopter_id      int primary key identity(1,1),
    first_name      varchar(50),
    last_name       varchar(50),
    street_address  varchar(255),
    city            varchar(100),
    [state]         varchar(2),
    zip             varchar(5),
    phone_num       varchar(10),
    email_address   varchar(255)
)
go 

create table [User](
    username        varchar(50) primary key,
    [password]      varchar(16),
    [role]          varchar(10),
    shelter_id      varchar(6) foreign key references Shelter(shelter_id),
    first_name      varchar(50),
    last_name       varchar(50),
    email_address   varchar(255)  
)
go 

create table AdoptionDetails(
    adoption_id     int primary key identity(1,1),
    adoption_date   datetime,
    animal_id       int foreign key references Animal(animal_id),
    shelter_id      varchar(6) foreign key references Shelter(shelter_id),
    adopter_id      int foreign key references Adopter(adopter_id)
)
go