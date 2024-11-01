namespace PresupuestoRepositoryNamespace;
using Microsoft.Data.Sqlite;
using TiendaNamespace;

interface PresupuestoRepository
{
    bool crearPresupuesto(string nombreDestinatario);
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