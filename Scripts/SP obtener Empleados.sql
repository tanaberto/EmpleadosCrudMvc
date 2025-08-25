CREATE OR ALTER PROCEDURE [dbo].[ObtenerEmpleados]

AS
BEGIN
    SELECT Codigo, Nombre, Puesto, CodigoJefe
    FROM Empleados
END
