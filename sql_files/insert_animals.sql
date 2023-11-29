use PetReTail
go

insert into Animal (  animal_name,
                     [type],
                     breed,
                     gender,
                     date_received,
                     is_vaxed,
                     is_fixed,
                     [fees],
                     shelter_id,
                     age)
values  ('Quincy', 'Cat', 'Chantilly-Tiffany', 'M', '09-29-2023', 1, 1, 100.00, 'shltr1', 1)
        ,('Peter', 'Cat', 'Tabby', 'M', '10-31-2023', 1, 1, 100.00, 'shltr1', 1)
        ,('Mu(black) & Bean(Orange) *BONDED PAIR*', 'Cat', 'Domestic Shorthair (both)', 'M', '11-5-2023', 1, 0, 150.00, 'shltr2', 1)
        ,('Malibu', 'Cat', 'Russian Blue', 'F', '11-25-2023', 1, 0, 100.00, 'shltr1', 1)
        ,('Sullivan', 'Dog', 'Black Labrador Retriever', 'M', '10-16-2023', 1, 1, 300.00, 'shltr1', 1)
        ,('Peanut', 'Dog', 'Slovensky Kopov', 'M', '11-02-2023', 1, 0, 250.00, 'shltr2', 0)
        ,('Finn', 'Dog', 'Black Labrador Retriever', 'M', '10-15-2023', 1, 1, 300.00, 'shltr1', 1)
        ,('Snickerdoodle', 'Rabbit', 'English Spot Mix', 'F', '10-11-2023', NULL, 1, 90.00, 'shltr2', 1)
go 