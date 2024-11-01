namespace TiendaNamespace;

public class Producto
{
    int idProducto;
    string descripcion;
    int precio;

    public int IdProducto { get => idProducto; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public int Precio { get => precio; set => precio = value; }
}
