using System;

using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using App7.Interfaces;
using App7.Model;
using Xamarin.Forms;
using App7.Services;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using App7.ServiceReference1;
using System.Collections.ObjectModel;
using System.ServiceModel;
//using MongoDB.Driver;

[assembly: Dependency(typeof(MongoDataStore))]
namespace App7.Services
{

    public class MongoDataStore : IDataStore
    {
        #region OldCOde

        //const string accountURL = @"https://cartalibreddb.documents.azure.com:443/";
        //const string accountKey = @"lFeslLrkHSmY1GdeJkjCwHa4kuAdXKpdJwOlM9deHgKrLTUEpwxsORPeH7yacW7INjPBRk9qxgf0qvC0UJlqRg==";
        //const string databaseId = @"cartalibre";

        //////private DocumentClient client = new DocumentClient(new System.Uri(accountURL), accountKey);

        //public async Task<IEnumerable<Proveedor>> GetProveedoresAsync()
        //{
        //    var Items = new List<Proveedor>();
        //    try
        //    {
        //        //Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, @"Proveedor");
        //        //var query = client.CreateDocumentQuery<Proveedor>(collectionLink, new FeedOptions { EnableCrossPartitionQuery = true })
        //        //      .Where(p => true)
        //        //      .AsDocumentQuery();

        //        //while (query.HasMoreResults)
        //        //{
        //        //    Items.AddRange(await query.ExecuteNextAsync<Proveedor>());
        //        //}
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }

        //    return Items;
        //}

        //public async Task<IEnumerable<Plato>> GetPlatosAsync()
        //{
        //    var Items = new List<Plato>();
        //    try
        //    {
        //        /**PROBAR**/

        //        //var con = new MongoConnectionStringBuilder("server=127.0.0.1;database=galary");
        //        //var se = new MongoClient("mongodb://resto:qweasdzxc@ds145039.mlab.com:45039/delivery");
        //        //var rr = se.GetServer().GetDatabase("delivery");
        //        //var restos = rr.GetCollection<Usuario>("Usuario");

        //        /**********/
        //        //Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, @"Plato");
        //        //var query = client.CreateDocumentQuery<Plato>(collectionLink, new FeedOptions { EnableCrossPartitionQuery = true })
        //        //      .Where(p => true)
        //        //      .AsDocumentQuery();

        //        //while (query.HasMoreResults)
        //        //{
        //        //    Items.AddRange(await query.ExecuteNextAsync<Plato>());
        //        //}
        //    }
        //    catch (Exception e)
        //    {
        //        return Items;
        //    }

        //    return Items;
        //}


        //public async Task<Feedback> AddFeedbackAsync(Feedback feedback)
        //{
        //    var emailTask = MessagingPlugin.EmailMessenger;
        //    if (emailTask.CanSendEmail)
        //    {
        //        emailTask.SendEmail("james.montemagno@xamarin.com", "My Shop Feedback", feedback.ToString());
        //    }

        //    return await Task.Run(() => { return feedback; });
        //}

        ///*public Task<Store> AddStoreAsync(Store store)
        //{
        //    return Task.FromResult(store);
        //}
        //*/

        //public async Task<IEnumerable<Feedback>> GetFeedbackAsync()
        //{
        //    return await Task.Run(() => { return new List<Feedback>(); });
        //}

        //public Task Init()
        //{
        //    return Task.Run(() => { });
        //}

        //public Task<bool> RemoveFeedbackAsync(Feedback feedback)
        //{
        //    return Task.FromResult(true);
        //}
        ///*
        //public Task<bool> RemoveStoreAsync(Store store)
        //{
        //    return Task.FromResult(true);
        //}
        //*/
        //public Task SyncFeedbacksAsync()
        //{
        //    return Task.Run(() => { });
        //}

        //public Task SyncStoresAsync()
        //{
        //    return Task.Run(() => { });
        //}
        ///*
        //public Task<Store> UpdateStoreAsync(Store store)
        //{
        //    return Task.FromResult(store);
        //}
        //*/
        //public Task SyncPlatosAsync()
        //{
        //    return Task.Run(() => { });
        //}
        //public Task SyncPlatosRestAsync()
        //{
        //    return Task.Run(() => { });
        //}
        //public Task<bool> RemovePlatoAsync(Plato plato)
        //{
        //    return Task.FromResult(true);
        //}

        //public Task<bool> RemoveProveedorAsync(Proveedor proveedor)
        //{
        //    return Task.FromResult(true);
        //}

        ////PEDIDOS
        //public async Task<IEnumerable<Pedido>> GetPedidosAsync(string idProve)
        //{
        //    var Items = new List<Pedido>();
        //    try
        //    {
        //        //Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, @"Pedido");
        //        //var query = client.CreateDocumentQuery<Pedido>(collectionLink, new FeedOptions { EnableCrossPartitionQuery = true })
        //        //      .Where(p => p.idProv == idProve && (p.Estado == "Por Confirmar" || p.Estado == "Confirmado"))
        //        //      .AsDocumentQuery();

        //        //while (query.HasMoreResults)
        //        //{
        //        //    Items.AddRange(await query.ExecuteNextAsync<Pedido>());
        //        //}
        //    }
        //    catch (Exception e)
        //    {
        //        return Items;
        //    }

        //    return Items;
        //}

        ////CARTA PROVEEDOR
        //public async Task<IEnumerable<Plato>> GetPlatosProveedorAsync(string idProve)
        //{
        //    var Items = new List<Plato>();
        //    try
        //    {
        //        //Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, @"Plato");
        //        //var query = client.CreateDocumentQuery<Plato>(collectionLink, new FeedOptions { EnableCrossPartitionQuery = true })
        //        //      .Where(p => p.idProv == idProve)
        //        //      .AsDocumentQuery();

        //        //while (query.HasMoreResults)
        //        //{
        //        //    Items.AddRange(await query.ExecuteNextAsync<Plato>());
        //        //}
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }

        //    return Items;
        //}
        ////pedido usuario
        //public async Task<IEnumerable<Pedido>> GetPedidosUsuarioAsync(string idUsuario)
        //{
        //    var Items = new List<Pedido>();
        //    try
        //    {
        //        //Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, @"Pedido");
        //        //var query = client.CreateDocumentQuery<Pedido>(collectionLink, new FeedOptions { EnableCrossPartitionQuery = true })
        //        //      .Where(p => p.idUsu == idUsuario)
        //        //      .AsDocumentQuery();

        //        //while (query.HasMoreResults)
        //        //{
        //        //    Items.AddRange(await query.ExecuteNextAsync<Pedido>());
        //        //}
        //    }
        //    catch (Exception e)
        //    {
        //        return Items;
        //    }

        //    return Items;
        //}

        //public async Task UpdatePedido(Pedido p)
        //{
        //    //var Items = new List<Plato>();
        //    try
        //    {
        //        //Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, @"Pedido");
        //        //await client.UpsertDocumentAsync(collectionLink, p);
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //} 
        #endregion

        public IService1 proxy = new Service1Client(new BasicHttpBinding(), new EndpointAddress("http://opnetin-002-site25.dtempurl.com/service/Service1.svc"));

        /*********** MONGO ******************/

        #region [Proveedor]
        public async Task SetProveedor(Proveedor p)
        {
            try
            {
                await Task.Factory.FromAsync(proxy.BeginSetProveedor(p, null, null), proxy.EndSetProveedor);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<ObservableCollection<Proveedor>> GetProveedores(string ciudad, string text)
        {
            try
            {
                var xx = await Task.Factory.FromAsync<ObservableCollection<Proveedor>>(proxy.BeginGetProveedores(ciudad, text, null, null), proxy.EndGetProveedores);
                return xx;
            }
            catch (Exception ex)
            {
                return new ObservableCollection<Proveedor>();
            }
        }

        public async Task<Proveedor> GetProveedorPorUsuario(string idUsuario)
        {
            try
            {
                var xx = await Task.Factory.FromAsync<Proveedor>(proxy.BeginGetProveedorPorUsuario(idUsuario,null, null), proxy.EndGetProveedorPorUsuario);
                return xx;
            }
            catch (Exception ex)
            {
                return new Proveedor();
            }
        }
        #endregion

        #region Platos
        public async Task<ObservableCollection<Plato>> GetPlatos(string idUsuario)
        {
            try
            {
                var xx = await Task.Factory.FromAsync<ObservableCollection<Plato>>(proxy.BeginGetPlatos(idUsuario, null, null), proxy.EndGetPlatos);
                return xx;
            }
            catch(Exception ex)
            {
                return new ObservableCollection<Plato>();
            }
        }

        public async Task<string> SetPlato(Plato p)
        {
            try
            {
                return await Task.Factory.FromAsync(proxy.BeginSetPlato(p, null, null), proxy.EndSetPlato);   
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public async Task<ObservableCollection<Plato>> GetPlatosProveedor(string idProv)
        {
            try
            {
                var xx = await Task.Factory.FromAsync<ObservableCollection<Plato>>(proxy.BeginGetPlatosProveedor(idProv, null, null), proxy.EndGetPlatosProveedor);
                return xx;
            }
            catch (Exception ex)
            {
                return new ObservableCollection<Plato>();
            }
        }

        public async Task<ObservableCollection<Plato>> GetPlatosActivosProveedor(string idProv)
        {
            try
            {
                var xx = await Task.Factory.FromAsync<ObservableCollection<Plato>>(proxy.BeginGetPlatosActivosProveedor(idProv, null, null), proxy.EndGetPlatosActivosProveedor);
                return xx;
            }
            catch (Exception ex)
            {
                return new ObservableCollection<Plato>();
            }
        }
        #endregion

        #region Pedido

        public async Task<long> GetComentariosNuevos(string idPlato, DateTime fecha)
        {
            try
            {
                return await Task.Factory.FromAsync(proxy.BeginGetComentariosNuevos(idPlato , fecha, null, null), proxy.EndGetComentariosNuevos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetVistoPedidoUsuario(string idPedido, DateTime fecha)
        {
            try
            {
                await Task.Factory.FromAsync(proxy.BeginSetVistoPedidoUsuario(idPedido,fecha, null, null), proxy.EndSetVistoPedidoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetVistoPedidoProveedor(string idPedido, DateTime fecha)
        {
            try
            {
                await Task.Factory.FromAsync(proxy.BeginSetVistoPedidoProveedor(idPedido, fecha, null, null), proxy.EndSetVistoPedidoProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ObservableCollection<Pedido>> GetPedidoProveedor(string idProve)
        {
            try
            {
                var xx = await Task.Factory.FromAsync<ObservableCollection<Pedido>>(proxy.BeginGetPedidoProveedor(idProve, null, null), proxy.EndGetPedidoProveedor);
                return xx;
            }
            catch
            {
                return new ObservableCollection<Pedido>();
            }
        }

        public async Task<ObservableCollection<Pedido>> GetPedidoUsuario(string idUsuario)
        {
            try
            {
                var xx = await Task.Factory.FromAsync<ObservableCollection<Pedido>>(proxy.BeginGetPedidoUsuario(idUsuario, null, null), proxy.EndGetPedidoUsuario);
                return xx;
            }
            catch
            {
                return new ObservableCollection<Pedido>();
            }
        }

        public async Task SetPedido(Pedido p)
        {
            await Task.Factory.FromAsync(proxy.BeginSetPedido(p, null, null), proxy.EndSetPedido);
        }

        public async Task UpdatePedido(Pedido p)
        {
            await Task.Factory.FromAsync(proxy.BeginUpdatePedido(p, null, null), proxy.EndUpdatePedido);
        }

        #endregion

        #region [Usuario]
        public async Task<Usuario> GetUsuario(string user,string facebookid, string pass)
        {
            try
            {
                var xx = await Task.Factory.FromAsync<Usuario>(proxy.BeginGetUsuario(user, facebookid, pass, null, null), proxy.EndGetUsuario);
                return xx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetUsuario(Usuario u)
        {
            await Task.Factory.FromAsync(proxy.BeginSetUsuario(u, null, null), proxy.EndSetUsuario);
        }

        public async Task SetUsuarioProv(string id, string tipo, string idProv)
        {
            await Task.Factory.FromAsync(proxy.BeginSetUsuarioProv(id,tipo,idProv, null, null), proxy.EndSetUsuarioProv);
        }
        #endregion

        public async Task SetFoto(byte[] stream, string path)
        {
            try
            {
                await Task.Factory.FromAsync(proxy.BeginSetFoto(stream,path, null, null), proxy.EndSetFoto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ObservableCollection<FoodLineCom>> GetFoodLine(double la, double lo, double distancia, string idUsuario, string categoria)
        {
            try
            {
                ObservableCollection<FoodLine> xx ;
                if (la == 0)
                {
                    xx = await Task.Factory.FromAsync<ObservableCollection<FoodLine>>(proxy.BeginGetFoodLine(0, 0, 0, idUsuario, categoria, null, null), proxy.EndGetFoodLine);
                }
                else
                {
                    xx = await Task.Factory.FromAsync<ObservableCollection<FoodLine>>(proxy.BeginGetFoodLine(la, lo, distancia, idUsuario, categoria, null, null), proxy.EndGetFoodLine);
                }

                //Los platos por proveedor
                var gr = xx.Where(p=>p.CProveedor != null &&
                                  p.CPlato.Categoria != "Oferta" && 
                                  p.CPlato.Categoria != "Evento").GroupBy(p => p.Usuario);
                ObservableCollection<FoodLineCom> comp = new ObservableCollection<FoodLineCom>();
                foreach (var i in gr)
                {
                        comp.Add(new FoodLineCom { CProveedor = i.First().CProveedor, Lines = i.ToList() });  
                }
                /////////////////////////

                ///////Ofertas y eventos
                var ofertas = xx.Where(p => p.CPlato.Categoria == "Oferta");
                foreach (var i in ofertas)
                {
                    comp.Add(new FoodLineCom { CPlato = i.CPlato, CProveedor = i.CProveedor, Lines = new List<FoodLine>() { i } });
                }
                var eventos = xx.Where(p => p.CPlato.Categoria == "Evento");
                foreach (var i in eventos)
                {
                    comp.Add(new FoodLineCom { CPlato = i.CPlato, CProveedor = i.CProveedor, Lines = new List<FoodLine>() { i } });
                }

                /////////////////////////


                //Solicitudes
                var g2r = xx.Where(p => p.CProveedor == null);
                foreach (var i in g2r)
                {
                        comp.Add(new FoodLineCom { CPlato = i.CPlato, Lines = new List<FoodLine>() { i } });
                }
                //////////////
                return new ObservableCollection<FoodLineCom>(comp.OrderBy(p=>p.Usuario));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetComentario(Comentario c)
        {
            try
            {
                await Task.Factory.FromAsync(proxy.BeginSetComentario(c, null, null), proxy.EndSetComentario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ObservableCollection<Comentario>> GetComentario(string idPlato)
        {
            try
            {
                var xx = await Task.Factory.FromAsync<ObservableCollection<Comentario>>(proxy.BeginGetComentario(idPlato, null, null), proxy.EndGetComentario);
                return xx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SetLike(string idPlato, string idUsuario)
        {
            try
            {
                return await Task.Factory.FromAsync(proxy.BeginSetLike(idPlato,idUsuario, null, null), proxy.EndSetLike);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdatePlato(string idPlato, string estado)
        {
            try
            {
                await Task.Factory.FromAsync(proxy.BeginUpdatePlato(idPlato, estado, null, null), proxy.EndUpdatePlato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SetSeguidor(Seguidores p)
        {
            try
            {
                await Task.Factory.FromAsync(proxy.BeginSetSeguidor(p, null, null), proxy.EndSetSeguidor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteSeguidor(string idUsuario, string idProveedor)
        {
            try
            {
                await Task.Factory.FromAsync(proxy.BeginDeleteSeguidor(idUsuario, idProveedor, null, null), proxy.EndDeleteSeguidor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SoySeguidor(string idUsuario, string idProveedor)
        {
            try
            {
                return await Task.Factory.FromAsync(proxy.BeginSoySeguidor(idUsuario, idProveedor, null, null), proxy.EndSoySeguidor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<long> NumSeguidores(string idProveedor)
        {
            try
            {
                return await Task.Factory.FromAsync(proxy.BeginNumSeguidores(idProveedor, null, null), proxy.EndNumSeguidores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ObservableCollection<Seguidores>> GetSeguidores(string idProveedor)
        {
            try
            {
                return await Task.Factory.FromAsync(proxy.BeginGetSeguidores(idProveedor, null, null), proxy.EndGetSeguidores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task EnviarNotificacionPlato(string idProve, string plato, string prove, string precio, string imageProv, string imagePlat)
        {
            try
            {
                await Task.Factory.FromAsync(proxy.BeginEnviarNotificacionPlato(idProve, plato, prove, precio,imageProv,imagePlat, null, null), proxy.EndEnviarNotificacionPlato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FoodLine> GetOneFoodLine(string id)
        {
            try
            {
                return await Task.Factory.FromAsync<FoodLine>(proxy.BeginGetOneFoodLine(id, null, null), proxy.EndGetOneFoodLine);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ObservableCollection<Proveedor>> GetFavoritos(string idUsuario)
        {
            try
            {
                return await Task.Factory.FromAsync(proxy.BeginGetFavoritos(idUsuario, null, null), proxy.EndGetFavoritos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ObservableCollection<Result>> Search(string str)
        {
            try
            {
                return await Task.Factory.FromAsync(proxy.BeginSearch(str, null, null), proxy.EndSearch);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ObservableCollection<Seguidores>> GetPreferencias(string idUsuario)
        {
            try
            {
                return await Task.Factory.FromAsync(proxy.BeginGetPreferencias(idUsuario, null, null), proxy.EndGetPreferencias);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ObservableCollection<Proveedor>> GetProveedoresGPS(double la, double lo, double distancia)
        {
            try
            {
                return await Task.Factory.FromAsync(proxy.BeginGetProveedoresGPS(la, lo, distancia,null, null), proxy.EndGetProveedoresGPS);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
