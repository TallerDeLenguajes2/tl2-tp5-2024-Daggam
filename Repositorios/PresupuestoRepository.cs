namespace PresupuestoRepositoryNamespace;

using System.Data;
using Microsoft.Data.Sqlite;
using TiendaNamespace;

interface PresupuestoRepository
{
    bool crearPresupuesto(string nombreDestinatario);
    List<Presupuesto>? obtenerPresupuestos();
    // bool modificarProducto(int idProducto, string descripcion, int precio);
    // List<Producto>? obtenerProductos();
    // Producto? obtenerProducto(int id);

    // bool eliminarProducto(int id);
}

class SQLitePresupuestoRepository : PresupuestoRepository
{
    string connectionString = @"Data Source=db\Tienda.db;Cache=Shared";
    public bool crearPresupuesto(string nombreDestinatario)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@NombreDestinatario, @FechaCreacion);";
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@NombreDestinatario", nombreDestinatario);
                command.Parameters.AddWithValue("@FechaCreacion",DateTime.Now.ToString("yyyy-MM-dd"));
                int filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
        return false;
    }
    
    public List<Presupuesto>? obtenerPresupuestos()
    {
        try{
            List<Presupuesto> presupuestos = new List<Presupuesto>();
            using(var connection = new SqliteConnection(connectionString)){
                connection.Open();
                //Creo las consultas que hagan lo siguiente
                /*
                    1. Me regresan todos los presupuestos.
                    2. De esos presupuestos, regresar sus DetallePresupuestos
                    3. De esos detallesPresupuestos, sus productos.
                    4. De esos productos, sus datos.
                */
                string queryPresupuesto = "SELECT * FROM Presupuestos;";
                var command = new SqliteCommand(queryPresupuesto,connection);
                using(var reader = command.ExecuteReader()){
                    while(reader.Read()){
                        int idPresupuesto = reader.GetInt32(0);
                        string nombreDestinatario = reader.GetString(1);
                        List<PresupuestoDetalle> detalles = new List<PresupuestoDetalle>();
                        //Todo lo necesario para obtener los productos de un PresupuestoDetalle dado un idPresupuesto
                        string queryDetalles = @"SELECT idProducto,Descripcion,Precio,Cantidad FROM PresupuestosDetalle
                        INNER JOIN Productos USING(idProducto)
                        WHERE idPresupuesto=@idPresupuesto";
                        var commandDetalles = new SqliteCommand(queryDetalles,connection);
                        commandDetalles.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);
                        using(var reader2 = commandDetalles.ExecuteReader()){
                            while(reader2.Read()){
                                Producto p = new Producto(reader2.GetInt32(0),reader2.GetString(1),reader2.GetInt32(2));
                                detalles.Add(new PresupuestoDetalle(p,reader2.GetInt32(3)));        
                            }
                        }
                        presupuestos.Add(new Presupuesto(idPresupuesto,nombreDestinatario, detalles));
                    }
                }
                connection.Close();
            }
            return presupuestos;
        }catch(SqliteException e){
            Console.WriteLine(e.Message);
        }
        return null;
    }
    // public bool modificarProducto(int idProducto,string descripcion, int precio){
    //     try{
    //         using(SqliteConnection connection = new SqliteConnection(connectionString))
    //         {
    //             connection.Open();
    //             string queryString = "UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto = @idProducto;";
    //             var command = new SqliteCommand(queryString, connection);
    //             command.Parameters.AddWithValue("@idProducto",idProducto);
    //             command.Parameters.AddWithValue("@Descripcion",descripcion);
    //             command.Parameters.AddWithValue("@Precio",precio);
    //             int filasAfectadas = command.ExecuteNonQuery();
    //             connection.Close();
    //         }
    //         return true;
    //     }catch(SqliteException e){
    //         Console.WriteLine(e.Message);
    //     }
    //     return false;
    // }

    // public List<Producto>? obtenerProductos()
    // {
    //     try{
    //         List<Producto> productos = new List<Producto>();
    //         using(SqliteConnection connection = new SqliteConnection(connectionString)){
    //             connection.Open();
    //             string queryString = "SELECT * FROM Productos";
    //             var command = new SqliteCommand(queryString,connection);
    //             using(var reader = command.ExecuteReader())
    //             {
    //                 while(reader.Read())
    //                 {
    //                     productos.Add( new Producto(reader.GetInt32(0),reader.GetString(1),reader.GetInt32(2)));
    //                 }
    //             }
    //             connection.Close();
    //         } 
    //         return productos;
    //     }catch(SqliteException e){
    //         Console.WriteLine(e.Message);
    //     }
    //     return null;
    // }

    // public Producto? obtenerProducto(int id){
    //     Producto? producto=null;
    //     try{
    //         using(var connection = new SqliteConnection(connectionString))
    //         {
    //             connection.Open();
    //             string queryString = "SELECT * FROM Productos WHERE idProducto = @idProducto;";
    //             var command = new SqliteCommand(queryString,connection);
    //             command.Parameters.AddWithValue("@idProducto",id);
    //             using(var reader = command.ExecuteReader())
    //             {
    //                 while(reader.Read())
    //                 {
    //                     producto = new Producto(reader.GetInt32(0),reader.GetString(1),reader.GetInt32(2));
    //                 }
    //             }
    //             connection.Close();
    //         }
    //     }catch(SqliteException e){
    //         Console.WriteLine(e.Message);
    //     }
    //     return producto;
    // }

    // public bool eliminarProducto(int id)
    // {
    //     try
    //     {
    //         using(var connection = new SqliteConnection(connectionString))
    //         {
    //             connection.Open();
    //             string queryString = "DELETE FROM Productos WHERE idProducto = @idProducto;";
    //             var command = new SqliteCommand(queryString,connection);
    //             command.Parameters.AddWithValue("@idProducto",id);
    //             int filasAfectadas = command.ExecuteNonQuery();
    //             connection.Close();
    //         }
    //         return true;
    //     }catch(SqliteException e){
    //         Console.WriteLine(e.Message);
    //     }
    //     return false;
    // }
}