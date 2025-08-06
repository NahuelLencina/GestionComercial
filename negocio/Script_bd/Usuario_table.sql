create procedure insertarNuevo 
@email varchar(50),
@pass varchar(50)

as
insert into USUARIO (Email, Pass, TipoUser) output inserted.Id  values (@email, @pass, 0)

--exec insertarNuevo 'algo', '123'