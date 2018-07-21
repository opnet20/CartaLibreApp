
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using App7.Interfaces;
using App7.Model;
using App7.Services;
using System;
using App7.ServiceReference1;
using System.Collections.ObjectModel;

//[assembly: Dependency(typeof(OfflineDataStore))]
namespace App7.Services
{
    public class OfflineDataStore : IDataStore
    {
        public Task DeleteSeguidor(string idUsuario, string idProveedor)
        {
            throw new NotImplementedException();
        }
        
        public Task EnviarNotificacionPlato(string idProve, string plato, string prove, string precio, string imageProv, string imagePlat)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Comentario>> GetComentario(string idPlato)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetComentariosNuevos(string idPlato, DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Proveedor>> GetFavoritos(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<FoodLineCom>> GetFoodLine(double la, double lo, double distancia, string idUsuario , string categoria)
        {
            throw new NotImplementedException();
        }

        public Task<FoodLine> GetOneFoodLine(string id)
        {
            throw new NotImplementedException();
        }
        #region old
        //public async Task<IEnumerable<Proveedor>> GetProveedoresAsync()
        //{
        //    try
        //    {
        //        //var json = ResourceLoader.GetEmbeddedResourceString(Assembly.Load(new AssemblyName("App7")), "stores.json");
        //        //var mongo = new MongoClient("mongodb://info:qweasdzxc123@ds035693.mlab.com:35693/infoestudiante");

        //        //var docentes = mongo.GetDatabase("infoestudiante").GetCollection<BsonDocument>("Docente");
        //        //var filtro = Builders<BsonDocument>.Filter;
        //        //var restr = filtro.Eq("Codigo", "1");
        //        //var prof = await docentes.Find(restr).FirstOrDefaultAsync();

        //        //string json = "[{\"id\":\"42d32dc3-d7bf-487b-901d-0e58584c1a7f\",\"codigoCiudad\":\"054\",\"pais\":\"Peru\",\"numeroTelefono\":\"123-456-789-1234\",\"longitude\":-122.401858,\"latitude\":37.797788,\"imagen\":\"http://starbuckssecretmenu.net/wp-content/uploads/2013/05/banana-feat-324x318.jpg\",\"distrito\":\"Paucarpata\",\"ciudad\":\"Arequipa\",\"direccion\":\"Av los incas\",\"referencia\":\"Al frente del estadio\",\"tipo\":\"Cevicheria\",\"descripcion\":\"Rico ceviche en bolsa y sorbete\",\"nombre\":\"El Tiburon\"},{\"id\":\"94fa54ab-72c2-40c7-85ee-491f2c9d023e\",\"codigoCiudad\":\"054\",\"pais\":\"Peru\",\"numeroTelefono\":\"220812\",\"longitude\":-71.5369925,\"latitude\":-16.394042,\"imagen\":\"https://www.migranciudad.com/images/logos/pe/restaurant/105_artika1.png\",\"distrito\":\"Cercado\",\"ciudad\":\"Arequipa\",\"direccion\":\" Av. Simón Bolivar 413\",\"referencia\":\"Un helado a tu lado\",\"tipo\":\"Heladeria\",\"descripcion\":\"Es una empresa rica en sabor, tradicion y tecnologia, lo viene demostrando ya mas de 10 anios. Nuestro sabor y calidad marcan la diferencia.\",\"nombre\":\"Artika\"},{\"id\":\"affad0c3-172e-496c-9697-665d48bb18f8\",\"codigoCiudad\":\"054\",\"pais\":\"Peru\",\"numeroTelefono\":\"28-2930\",\"longitude\":-71.5338003,\"latitude\":-16.4006157,\"imagen\":\"https://static.websguru.com.ar/var/m_e/ee/eef/33177/525518-pollos-a-la-brasa-el-pio-pio-logo.jpg\",\"distrito\":\"Cercado\",\"ciudad\":\"Arequipa\",\"direccion\":\"Calle Santo Domingo, 210 y 212\",\"referencia\":\"Tenemos el mejor pollo\",\"tipo\":\"Polleria\",\"descripcion\":\"Reconocido establecimiento con más de 45 años de trayectoria en el rubro gastronómico. Los pollos a la brasa que servimos son de piel dorada muy crocante, salen bien calientitos y apetitosos y son preparados al instante con nuestra fórmula especial para mantener esa inconfundible sazón.\",\"nombre\":\"El Pio Pio\"},{\"id\":\"d9064e62-df2e-4d16-ba26-bddc06a9c4a2\",\"codigoCiudad\":\"054\",\"pais\":\"Peru\",\"numeroTelefono\":\"461713\",\"longitude\":-71.51076023558198,\"latitude\":-16.413433554194132,\"imagen\":\"https://ii.ct-stc.com/3/logos/empresas/2015/03/24/39931916dc8842b6900ethumbnail.jpeg\",\"distrito\":\"Paucarpata\",\"ciudad\":\"Arequipa\",\"direccion\":\"Jesús 801, Arequipa\",\"referencia\":\"Ceviche al instante\",\"tipo\":\"Cevicheria\",\"descripcion\":\"Excelentes platos marinos donde la especialidad es nuestro ceviche con salsa edul.\",\"nombre\":\"Cevicheria Edul\"},{\"id\":\"42d32dc3-d7bf-487b-901d-0e58584c1a7f\",\"codigoCiudad\":\"054\",\"pais\":\"Peru\",\"numeroTelefono\":\"993-992-203\",\"longitude\":-71.51370553864041,\"latitude\":-16.416421097155087,\"imagen\":\"http://www.programasperu.com/restaurantes-peruanos/wp-content/uploads/2012/07/el-montonero.jpg\",\"distrito\":\"Paucarpata\",\"ciudad\":\"Arequipa\",\"direccion\":\"Av. Prongoche 500\",\"referencia\":\"La permanencia de una tradicion\",\"tipo\":\"Restaurant Turistico\",\"descripcion\":\" El Montonero brinda una amplia selección de sabrosos platos típicos.Excelente cocina y respeto por la tradición en un ambiente de gran belleza colonial y cómodos espacios al aire libre.\",\"nombre\":\"El Montonero Mall Aventura\"},{\"id\":\"94fa54ab-72c2-40c7-85ee-491f2c9d023e\",\"codigoCiudad\":\"054\",\"pais\":\"Peru\",\"numeroTelefono\":\"614818\",\"longitude\":-71.5143532921175,\"latitude\":-16.416479629789578,\"imagen\":\"http://www.mitsuiautomotriz.com/wp-content/uploads/2016/03/4.jpg\",\"distrito\":\"Paucarpata\",\"ciudad\":\"Arequipa\",\"direccion\":\"Av. Porongoche 500\",\"referencia\":\"Donde comer es un placer\",\"tipo\":\"Parrilladas\",\"descripcion\":\"Restaurante de primera donde podras encontrar todo tipo de platos\",\"nombre\":\"El Ekeko Mall Aventura\"},{\"id\":\"affad0c3-172e-496c-9697-665d48bb18f8\",\"codigoCiudad\":\"054\",\"pais\":\"Peru\",\"numeroTelefono\":\"054228380\",\"longitude\":-71.56877889999998,\"latitude\":-16.4359648,\"imagen\":\"https://pbs.twimg.com/profile_images/532667706378436608/VH3NsAdO_400x400.jpeg\",\"distrito\":\"Sachaca\",\"ciudad\":\"Arequipa\",\"direccion\":\"Av. Arancota B-12 A\",\"referencia\":\"Los chicharrones de Arequipa\",\"tipo\":\"Chicharroneria\",\"descripcion\":\"Restaurante-Chicharroneria con amplio local y orquestas fines de semana\",\"nombre\":\"La Quequita\"},{\"id\":\"d9064e62-df2e-4d16-ba26-bddc06a9c4a2\",\"codigoCiudad\":\"054\",\"pais\":\"Peru\",\"numeroTelefono\":\"20-1733\",\"longitude\":-71.56850531468046,\"latitude\":-16.434647621914987,\"imagen\":\"http://teayudo.msp.pe/f/5ff31.jpg\",\"distrito\":\"Sachaca\",\"ciudad\":\"Arequipa\",\"direccion\":\"Avenida Arancota, SN\",\"referencia\":\"Chicharrones con orquesta no son chicharrones\",\"tipo\":\"Chicharroneria\",\"descripcion\":\"El mejor restaurante-chicharroneria de Arequipa, pasa con nosotros fines de semanas en familia\",\"nombre\":\"La Cecilia\"}]";
        //        //return await Task.Run(() => JsonConvert.DeserializeObject<List<Proveedor>>(json));

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<IEnumerable<Plato>> GetPlatosAsync()
        //{
        //    try
        //    {
        //        //string json = "[{\"id\":\"42d32dc3-d7bf-487b-901d-0e58584c1a7g\",\"Nombre\":\"Aji de Gallina\",\"Restaurante\":\"El Montonero Mall\",\"Descripcion\":\"Rico aji de gallina con el sabor de casa\",\"Categoria\":\"tipica\",\"Precio\":8.00,\"Imagen\":\"https://upload.wikimedia.org/wikipedia/commons/thumb/4/44/Aj%C3%AD_de_gallina_-_Tradicional.jpg/245px-Aj%C3%AD_de_gallina_-_Tradicional.jpg\"}]";
        //        //return await Task.Run(() => JsonConvert.DeserializeObject<List<Plato>>(json));
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
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

        //public Task UpdatePedido(Pedido p)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> RemoveProveedorAsync(Proveedor store)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> RemovePlatoAsync(Plato store)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SyncPlatosAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SyncPlatosRestAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Pedido>> GetPedidosAsync(string idProve)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Plato>> GetPlatosProveedorAsync(string idProve)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Pedido>> GetPedidosUsuarioAsync(string idUsuario)
        //{
        //    throw new NotImplementedException();
        //} 
        #endregion
        public Task<ObservableCollection<Pedido>> GetPedidoProveedor(string idProve)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Pedido>> GetPedidoUsuario(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Plato>> GetPlatos(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Plato>> GetPlatosActivosProveedor(string idProv)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Plato>> GetPlatosProveedor(string idProv)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Seguidores>> GetPreferencias(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Proveedor>> GetProveedores(string ciudad, string text)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Proveedor>> GetProveedoresGPS(double la, double lo, double distancia)
        {
            throw new NotImplementedException();
        }

        public Task<Proveedor> GetProveedorPorUsuario(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Seguidores>> GetSeguidores(string idProveedor)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetUsuario(string user, string facebookid, string pass)
        {
            throw new NotImplementedException();
        }

        public Task<long> NumSeguidores(string idProveedor)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Result>> Search(string str)
        {
            throw new NotImplementedException();
        }

        public Task SetComentario(Comentario c)
        {
            throw new NotImplementedException();
        }

        public Task SetFoto(byte[] stream, string path)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetLike(string idPlato, string idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task SetPedido(Pedido p)
        {
            throw new NotImplementedException();
        }

        public Task<string> SetPlato(Plato p)
        {
            throw new NotImplementedException();
        }

        public Task SetProveedor(Proveedor p)
        {
            throw new NotImplementedException();
        }

        public Task SetSeguidor(Seguidores p)
        {
            throw new NotImplementedException();
        }

        public Task SetUsuario(Usuario u)
        {
            throw new NotImplementedException();
        }

        public Task SetUsuarioProv(string id, string tipo, string idProv)
        {
            throw new NotImplementedException();
        }

        public Task SetVistoPedidoProveedor(string idPedido, DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public Task SetVistoPedidoUsuario(string idPedido, DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoySeguidor(string idUsuario, string idProveedor)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePedido(Pedido p)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePlato(string idPlato, string estado)
        {
            throw new NotImplementedException();
        }
    }
}
