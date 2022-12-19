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
('Erik', 'Askerblom', 750, 'erik.askerblom@hotmail.com', null, 0),
('Filip', 'Erlingmark', 4, 'fillefotboll@gmail.com', null, 1),
('Rebecca', 'Molnstrand', 100, 'molnet@cloud.com', null, 1)

insert into message values
('Urgent', 'Jag måste komma i kontakt med dig, återkom nu.', '2022-12-19',0, 1),
('Test', 'Hej. Jag testar min nya mailfunktion.', '2021-05-12',1,2),
('Nytt betyg inraporterat i Ladok.', 'U tyvärr', '2022-12-15', 1,3)

insert into users values
('erikask', 'hej123',1),
('lenny', 'fotbollenhej', 2),
('rere10','handbroms',3)

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

