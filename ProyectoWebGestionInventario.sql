use GestionInventario;

insert into Roles(rol) values 
('Administrador'),
('Vendedor');

insert into Usuarios(nombre, telefono, rut, clave, id_rol) values
('Administrador', '123456789', '10610838-2', 'admin', (select id from Roles where Roles.rol = 'Administrador') );

select * from Usuarios;