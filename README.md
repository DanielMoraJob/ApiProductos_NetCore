# ApiProductos
Esta es un api para el CRUD de una lista de productos realizada en .Net Core
Cuenta con dos proyectos, la solución de API y el Tests de pruebas en XUNIT.

Para correr la sln, es necesario:
1. clonar el proyecto
2. descargar paquetes
3. configurar la cadena de conexión de la bd
4. hacer la correspondiente migración a la bd (add-migration xxxxx y update-database)
5. ejectutar el proyecto, se puede utilizar ya se por swagger o postmant

Para correr las pruebas, es necesario
1. Dirigirse al proyecto de Test_Productos
2. Abrir procutTest
3. Ejecutar una a una las pruebas ahí presentes con el explorador de pruebas
4. Vizualizar el resultado de cada una de ellas en la consola
Cada prueba tiene comentado la prueba que se está realizando y el tipo de respuesta

# SQL SERVER
A continuación se adicionan unos SP que manejan el CRUD de la tabla de productos en la BD 
Para correr cada SP, es necesario:

1.Copiar y pegar uno a uno cada SP en SQL Server Managment Studio
2.Ejecutarlo
3.Copiar y pegar el EXEC de cada sp
4.Ejecutarlo

    -- =============================================
    -- Author:		<Daniel Mora>
    -- Create date: <01/11/2024>
    -- Description:	<Create a product>
    -- =============================================

    CREATE PROCEDURE SPAddProduct
        @Name varchar(50),
        @Description varchar(255),
        @Price decimal(12,2),
        @Stock int
    AS
    BEGIN
        -- Verificar si el producto ya existe por nombre
        IF EXISTS (SELECT 1 FROM [dbo].[Products] WHERE Name = @Name)
        BEGIN
            PRINT 'El producto ya existe en el sistema.';
            RETURN;
        END
    
        -- Si no existe, insertar el producto
        INSERT INTO [dbo].[Products] (Name, Description, Price, [Stock])
        VALUES (@Name, @Description, @Price, @Stock)
    END


-- Ejecutar el procedimiento

    EXEC [dbo].[SPAddProduct] 'LLanta', 'Una descripción', 200000, 2;


    -- =============================================
    -- Author:		<Daniel Mora>
    -- Create date: <01/11/2024>
    -- Description:	<Update a product>
    -- =============================================
    
    CREATE PROCEDURE SPUpdateProduct
        @Id INT,
        @Name VARCHAR(50),
        @Description VARCHAR(255),
        @Price DECIMAL(12,2),
        @Stock INT
    AS
    BEGIN
        -- Verificar si el producto existe por Id
        IF EXISTS (SELECT 1 FROM [dbo].[Products] WHERE Id = @Id)
        BEGIN
            UPDATE [dbo].[Products]
            SET 
                Name = @Name,
                Description = @Description,
                Price = @Price,
                Stock = @Stock
            WHERE Id = @Id;
        END
        ELSE
        BEGIN
            PRINT 'El producto con el Id ' + CAST(@Id AS VARCHAR) + ' no existe en el sistema.';
        END
    END

-- Ejecutar el procedimiento 

    EXEC [dbo].[SPUpdateProduct] 1,'LLanta', 'Una descripción 2', 200000, 3;

    -- =============================================
    -- Author:		<Daniel Mora>
    -- Create date: <01/11/2024>
    -- Description:	<Get products>
    -- =============================================
    
    CREATE PROCEDURE SPGetProducts
    AS
    BEGIN
        SELECT * FROM [dbo].[Products]
    END
    
-- Ejecutar SP
    EXEC [dbo].[SPGetProducts];
    
    
    -- =============================================
    -- Author:		<Daniel Mora>
    -- Create date: <01/11/2024>
    -- Description:	<Delete a product>
    -- =============================================
    
    
    CREATE PROCEDURE SPDeleteProduct
        @Id INT
    AS
    BEGIN
        -- Verificar si el producto existe por id
        IF EXISTS (SELECT 1 FROM [dbo].[Products] WHERE Id = @Id)
        BEGIN
            DELETE FROM [dbo].[Products] WHERE ID = @Id
        END
        ELSE
        BEGIN
            PRINT 'El producto con el Id ' + CAST(@Id AS VARCHAR) + ' no existe en el sistema.';
        END
    END

-- Ejecutar procedimiento almacenado

    EXEC [dbo].[SPDeleteProduct] 1;
