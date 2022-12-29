use LinkedoutDB
delete from profile_has_competence;
delete from profile_has_education;
delete from profile_has_experience;
delete from profile_in_project;
delete from project;
delete from users;
delete from message;
delete from profile;
delete from competence;
delete from education;
delete from experience;
delete from AspNetUsers;
DBCC CHECKIDENT ('Profile', RESEED, 0);
DBCC CHECKIDENT ('competence', RESEED, 0);
DBCC CHECKIDENT ('education', RESEED, 0);
DBCC CHECKIDENT ('experience', RESEED, 0);
DBCC CHECKIDENT ('message', RESEED, 0);
DBCC CHECKIDENT ('Project', RESEED, 0);
insert into AspNetUsers values
('8a91112b-4526-4644-be48-06b841dd1c77','Rasmus123','RASMUS123',null,null,0,'AQAAAAIAAYagAAAAEDIS6XnOeFUB/157fpHHwC+W9ZNDl+/aVAd7iIsX2Ac+LgjPZ37gzDHAqD3zcNaKoQ==','P4DOHSK2CDSM6PROG7YHYOW2RD7IGMVQ','b20aa105-6b31-4eb8-83c9-fc975427ff24',null,0,0,null,1,0),
('ce947255-d180-4c7b-a554-8d061c5270fa','Becca123','BECCA123',NULL,NULL,0,'AQAAAAIAAYagAAAAEJZ2rdP8Hddn6JMoKbj6E/wK66ft+1OgMwAwn5Pw5k6Nvy0NJyrXnXzN33eyoiEb/A==','HCNQCTGBXI2BUBZOM55PFXTXCCX3QZP2','2de4d326-f6bf-451b-a56d-da73da2ef03e',NULL,0,0,NULL,1,0),
('e03711f2-70fc-4e92-985e-582875a1c0f7','Erik123','ERIK123',NULL,NULL,0,'AQAAAAIAAYagAAAAEK2564zph7NGrhLmKUxXM2/F0Yjg0PqmqWmOdqUpsS3/cuw7c1T/a8TU7qcUZo5niA==','AEFGKLINQGXSD25PZVA6S7RUTKIRW65U','de48d6e6-fd4e-4d31-940d-759698a804a3',NULL,0,0,NULL,1,0),
('c18a65ca-04cb-44cc-aece-e887355c4c24','Filip123','FILIP123',NULL,NULL,0,'AQAAAAIAAYagAAAAECJ1OcvmT03p8ONDU78nY1A9nN515m9BpH/GMPj+hnEcwZB84foMHzJKDJPJH0oQIw==','GS2U2M6EYDE4LDIXLT3I4RC4C3ZK4GWD','9baee366-8da4-4ad4-8153-809270e01bdf',NULL,0,0,NULL,1,0)

insert into experience values 
('Spotify', 'Utveckalde spotify wrapped'),
('PostNord', 'Delade ut brev ibland'),
('Stoneridge', 'Stod vid löpande band')

insert into education values
('Harward', 'AI'),
('Örebro Universitet', 'Systemvetenskap'),
('Karolinska', 'Natur')

insert into competence values
('SQL', 'Vass på databaser'),
('Java', 'Folk kallar mig javaHåkans'),
('HTML', 'Ser mig själv som frontend')

insert into [Profile] values 
('Erik', 'Askerblom', 750, 'erik.askerblom@hotmail.com', null, 0,'Erik123'),
('Filip', 'Erlingmark', 4, 'fillefotboll@gmail.com', null, 1,'Filip123'),
('Rebecca', 'Molnstrand', 100, 'molnet@cloud.com', null, 1,'Becca123'),
('Rasmus','Österberg',500,'Ras.osterberg@gmail.com',null,1,'Rasmus123')

insert into message values
('Urgent', 'Jag måste komma i kontakt med dig, återkom nu.', '2022-12-19',0, 1, 'Rasmus'),
('Test', 'Hej. Jag testar min nya mailfunktion.', '2021-05-12',1,2, 'Ask'),
('Nytt betyg inraporterat i Ladok.', 'U tyvärr', '2022-12-15', 1,3,'Håkans')

insert into project values
('Byggde nya västlänken', 'Rimfrost', 2),
('.net-projektet med bästa gruppen', 'Melle',1),
('Med hjälp av HTML och CSS byggde vi en vacker hemsida', 'ANSO',1)

insert into profile_in_project values
(1,1),
(1,2),
(2,3)

insert into profile_has_experience values 
(1,2,'2017-06-01',null),
(2,3,'2017-01-01','2017-03-05'),
(3,3,'2020-05-05','2020-05-06')

insert into profile_has_education values
(1,1,'2014-08-30','2017-06-05'),
(2,3,'2019-09-30','2020-01-15'),
(3,2,'2005-06-06','2024-06-01')

insert into profile_has_competence values
(1,1),
(1,2),
(2,1)

