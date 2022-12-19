create database LinkedoutDB;
use linkedoutDB;

create table Profile (
ID int Identity not null primary key,
FirstName nvarchar(200),
LastName nvarchar(200),
Visitors int,
Email Nvarchar(200),
PicUrl Nvarchar(200),
Private bit
)

create table users (
ID int identity not null primary key,
Username nvarchar(200),
Password nvarchar(200),
profileID int,
constraint fk_profile_id foreign key (profileID) references profile(id)
)

create table project (
ID int identity not null primary key,
description nvarchar(200),
Title nvarchar(200),
CreatorID int not null,
constraint fk_creator_id foreign key (CreatorID) references profile(id)
)

create table experience (
ID int identity not null primary key,
name nvarchar(200),
description nvarchar(200)
)
create table competence (
ID int identity not null primary key,
name nvarchar(200),
description nvarchar(200)
)
create table education (
ID int identity not null primary key,
name nvarchar(200),
description nvarchar(200)
)

create table message(
ID int identity not null primary key,
title nvarchar(200),
content nvarchar(200),
times datetime,
seen bit,
reciever int,
constraint fk_message_reciever foreign key (reciever) references profile(id)
)

create table profile_in_project (
projectid int,
profileid int,
constraint fk_project_id foreign key (projectid) references project(id),
constraint fk_profiles_id foreign key (profileid) references profile(id),
primary key (projectid,profileid)
)

create table profile_has_experience(
profileid int,
experienceid int,
startdate date,
enddate date,
constraint fk_experience_id foreign key (experienceid) references experience(id),
constraint fk_profiless_id foreign key (profileid) references profile(id),
primary key(profileid,experienceid)
)

create table profile_has_competence(
profileid int,
competenceid int,

constraint fk_competence_id foreign key (competenceid) references competence(id),
constraint fk_profilesss_id foreign key (profileid) references profile(id),
primary key(profileid,competenceid)
)

create table profile_has_education(
profileid int,
educationid int,
startdate date,
enddate date,
constraint fk_education_id foreign key (educationid) references education(id),
constraint fk_profilessss_id foreign key (profileid) references profile(id),
primary key(profileid,educationid)
)


