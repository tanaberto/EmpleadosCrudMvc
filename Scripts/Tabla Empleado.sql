CREATE TABLE Empleados 
(
    Codigo INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Puesto NVARCHAR(100) NOT NULL,
    CodigoJefe INT NULL,

    CONSTRAINT FK_Empleados_Jefe
        FOREIGN KEY (CodigoJefe) REFERENCES Empleados(Codigo)
        ON DELETE NO ACTION
);


