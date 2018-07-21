using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using App7.Model;
using App7.ServiceReference1;
using System.Collections.ObjectModel;

namespace App7.Interfaces
{
    public interface IDataStore
    {
        #region Old
        //Task Init();
        //Task<IEnumerable<Proveedor>> GetProveedoresAsync(); 
        //Task<bool> RemoveProveedorAsync(Proveedor store);

        //Task<IEnumerable<Plato>> GetPlatosAsync();
        //Task<bool> RemovePlatoAsync(Plato store);

        ////Task<Store> UpdateStoreAsync(Store store);
        //Task<Feedback> AddFeedbackAsync(Feedback feedback);
        //Task<IEnumerable<Feedback>> GetFeedbackAsync();
        //Task<bool> RemoveFeedbackAsync(Feedback feedback);

        //Task SyncPlatosAsync();
        //Task SyncPlatosRestAsync();
        //Task SyncStoresAsync();
        //Task SyncFeedbacksAsync();

        ////Pedidos
        //Task<IEnumerable<Pedido>> GetPedidosAsync(string idProve);

        ////Carta Proveedor
        //Task<IEnumerable<Plato>> GetPlatosProveedorAsync(string idProve);

        ////Pedidos Usuario
        //Task<IEnumerable<Pedido>> GetPedidosUsuarioAsync(string idUsuario);

        //Task UpdatePedido(Pedido p); 
        #endregion

        //Mongo

        #region Util
        Task SetFoto(byte[] stream, string path);

        Task<ObservableCollection<FoodLineCom>> GetFoodLine(double la, double lo, double distancia, string idUsuario, string categoria);
        #endregion

        #region Proveedores
        Task SetProveedor(Proveedor p);

        Task<ObservableCollection<Proveedor>> GetProveedores(string ciudad, string text);
        
        Task<Proveedor> GetProveedorPorUsuario(string idUsuario);

        Task<ObservableCollection<Proveedor>> GetProveedoresGPS(double la, double lo, double distancia);
        #endregion

        #region Platos
        Task<ObservableCollection<Plato>> GetPlatos(string idUsuario);

        Task<string> SetPlato(Plato p);

        Task<ObservableCollection<Plato>> GetPlatosProveedor(string idProv);

        Task<ObservableCollection<Plato>> GetPlatosActivosProveedor(string idProv);

        Task UpdatePlato(string idPlato, string estado);
        #endregion

        #region Pedido
        Task<long> GetComentariosNuevos(string idPlato, DateTime fecha);

        Task<ObservableCollection<Pedido>> GetPedidoProveedor(string idProve);

        Task<ObservableCollection<Pedido>> GetPedidoUsuario(string idUsuario);

        Task SetPedido(Pedido p);

        Task UpdatePedido(Pedido p);

        Task SetVistoPedidoUsuario(string idPedido, DateTime fecha);

        Task SetVistoPedidoProveedor(string idPedido, DateTime fecha);

        #endregion

        #region Usuario
        Task SetUsuario(Usuario u);

        Task<Usuario> GetUsuario(string user, string facebookid, string pass);

        Task SetUsuarioProv(string id, string tipo, string idProv);

        Task<bool> SetLike(string idPlato, string idUsuario);

        Task<ObservableCollection<Proveedor>> GetFavoritos(string idUsuario);

        Task<ObservableCollection<Result>> Search(string str);
        #endregion

        #region Comentarios
        Task SetComentario(Comentario c);

        Task<ObservableCollection<Comentario>> GetComentario(string idPlato);
        #endregion

        #region Seguidores
        Task SetSeguidor(Seguidores p);
        Task DeleteSeguidor(string idUsuario, string idProveedor);
        Task<bool> SoySeguidor(string idUsuario, string idProveedor);
        Task<long> NumSeguidores(string idProveedor);
        Task<ObservableCollection<Seguidores>> GetSeguidores(string idProveedor);
        Task<ObservableCollection<Seguidores>> GetPreferencias(string idUsuario);
        Task EnviarNotificacionPlato(string idProve,
                                                    string plato,
                                                    string prove,
                                                    string precio,
                                                    string imageProv,
                                                    string imagePlat);
        Task<FoodLine> GetOneFoodLine(string id);
       
        #endregion
    }
}

