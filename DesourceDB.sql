create database ResourceDB;
go
use ResourceDB;
go

create table Resources
(
Id int identity(1,1) primary key,
Name nvarchar(255) not null

)
go

insert into Resources(Name) values('База данных');
insert into Resources(Name) values('Общая папка на сервере');
insert into Resources(Name) values('Excel-таблица с товарами');
go