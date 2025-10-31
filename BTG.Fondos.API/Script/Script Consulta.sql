USE BTG;
GO

SELECT DISTINCT c.nombre, c.apellidos
FROM cliente c
WHERE EXISTS (
    SELECT 1
    FROM inscripcion i
    WHERE i.idCliente = c.id
      AND NOT EXISTS (
          SELECT 1
          FROM disponibilidad d
          WHERE d.idProducto = i.idProducto
            AND d.idSucursal NOT IN (
                SELECT v.idSucursal
                FROM visitan v
                WHERE v.idCliente = c.id
            )
      )
);
