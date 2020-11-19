use Fabio
go
/*****************************************************
                 
alter table Usuarios drop constraint FKUsuariosEscolaridadecod 
drop table dbo.Usuarios
drop table Escolaridade
******************************************************/
if OBJECT_ID('Usuarios') IS NULL
	create table dbo.Usuarios(
	idusuario int identity, 
	codescolaridade int NOT NULL,
	nome varchar(90) NOT NULL, 
	sobrenome varchar(90) NOT NULL,
	email varchar(200) NOT NULL,
	dtnascimento datetime,
	dtinclusao datetime default getdate(),
	primary key(idusuario))
go
if OBJECT_ID('Escolaridade') IS NULL
begin
 create table dbo.Escolaridade(
  codescolaridade int,
	nivel varchar(90),
	primary key(codescolaridade)
 )
 insert into Escolaridade
 (codescolaridade, nivel)
 values
 (1,'Infantil'),
 (2,'Fundamental'),
 (3,'Médio'),
 (4,'Superior')
end
go
if not exists(select 1 
              from sys.sysconstraints 
							where OBJECT_NAME(constid) = 'FKUsuariosEscolaridadecod')
 alter table dbo.Usuarios 
   add constraint FKUsuariosEscolaridadecod
	 foreign key(codescolaridade)
	 references Escolaridade(codescolaridade)

	 /*
	 delete from usuarios
	 insert into Usuarios 
	 (codescolaridade, nome, sobrenome)
	 VALUES
	 (1,'Fabio','Domingues')
	 select * from Usuarios
	 */