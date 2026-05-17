-- DESKTOP-JJ9DM3F\SQLEXPRESS
create database CRUDVueNet1
use CRUDVueNet1

create table Departamento
(
	idDepartamento int not null identity(1,1),
	nombreDepartamento varchar(100) not null,
	constraint PK_Departamento primary key (idDepartamento)
)
insert into Departamento(nombreDepartamento)
values
('TI'),('Desarrollo de Software'),('Ventas'),
('Contabilidad'),('Recursos Humanos'),('Logística y Almacén'),
('Compras'),('Marketing'),('Operaciones'),('Atención al Client')
select * from Departamento

create table Empleado 
(
	idEmpleado int not null identity(1,1),
	nombreEmpleado varchar(100) not null,
	idDepartamento int not null,
	activo bit default 1,
	constraint PK_Empleado primary key (idEmpleado),
	constraint FK_EmpleadoDepartamento foreign key (idDepartamento)
										references Departamento(idDepartamento)
)
insert into Empleado(nombreEmpleado, idDepartamento)
values
('Sofía', 4),('Mateo', 9),('Valeria', 2),('Diego', 7),('Ximena', 1),
('Santiago', 10),('Camila', 5),('Leonardo', 3),('Andrea', 8),('Sebastián', 6),('Adrian', 4)
select * from Empleado

create table Usuario
(
	idUsuario int identity(1,1) not null,
	nombre varchar(50),
	correo varchar(100) not null,
	clave varchar(100) not null, -- En un proyecto real esto iría encriptado
	constraint PK_Usuario primary key (idUsuario)
)
insert into Usuario(nombre, correo, clave) 
values ('Administrador', 'admin@correo.com', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5'),
('Usuario', 'usuario@correo.com', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5')
select * from Usuario

create procedure sp_ListarEmpleadosPorIdDepartamento
(
	@idDepartamento int
)
as
begin 
	select e.idEmpleado,e.nombreEmpleado,e.activo,d.idDepartamento,d.nombreDepartamento from Empleado e
	inner join Departamento d
	on d.idDepartamento = e.idDepartamento
	where d.idDepartamento = @idDepartamento
	order by activo desc
end

create procedure sp_EstadoEmpleado
(
	@idEmpleado int
)
as
begin
	update Empleado 
	set activo = case when activo = 1 then 0 else 1 end
	where idEmpleado = @idEmpleado
end
