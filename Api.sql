create database testing

use testing

create table ApiTesting
(
Sno int identity(1,1) not null,
Ename varchar(50),
Ephone varchar(50),
Eaddress varchar(50)
)

insert into ApiTesting values ('Track','45665455','UP')

select * from ApiTesting

ALTER TABLE ApiTesting
ADD CONSTRAINT constraint_namepk PRIMARY KEY (Sno);