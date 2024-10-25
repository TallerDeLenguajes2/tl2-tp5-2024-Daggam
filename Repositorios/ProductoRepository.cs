namespace ProductoRepositoryNamespace;
using Microsoft.Data.Sqlite;
interface ProductoRepository
{
    bool crearProducto(string descripcion, int precio);
}

class SQLiteProductoRepository : ProductoRepository
{
    string connectionString = @"Data Source=db\Tienda.db;Cache=Shared";
    // public SQLiteProductoRepository()
    // {

    // }
    public bool crearProducto(string descripcion, int precio)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio);";
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@Descripcion", descripcion);
                command.Parameters.AddWithValue("@Precio", precio);
                int filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}