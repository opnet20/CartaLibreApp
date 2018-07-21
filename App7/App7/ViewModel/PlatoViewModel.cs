using System;
using Xamarin.Forms;
using Plugin.ExternalMaps;
using App7.Helpers;
using App7.Model;
using App7.ServiceReference1;
using MvvmHelpers;
using System.Threading.Tasks;
using App7.Interfaces;
using System.Linq;

namespace App7.ViewModel
{
      public class PlatoViewModel : ViewModelBase
    {
        public FoodLine Plato { get; set; }
        public event EventHandler SetHeight;
        private ObservableRangeCollection<Comentario> comentarios;
        public ObservableRangeCollection<Comentario> Comentarios
        {
            get
            {
                return comentarios;
            }

            set
            {
                comentarios = value;
                OnPropertyChanged("Comentarios");
            }
        }
        //public string Address1 { get { return Plato.Direccion; } }
        //public string Address2 { get { return string.Format("{0}, {1} {2}", Plato.Ciudad, Plato.Distrito, Plato.Pais); } }
        readonly IDataStore dataWCF;
        public PlatoViewModel(FoodLine Plato, Page page) : base(page)
        {
            this.Plato = Plato;
            dataWCF = DependencyService.Get<IDataStore>();
            this.Comentarios = new ObservableRangeCollection<Comentario>();
            Load();
            //this.Comentarios.Add(new Comentario { Imagen = "https://s3.hdstatic.net/gridfs/holadoctor/625x470_4f62c7f0b937955271000000_6_32-1331876302.jpg", Contenido = "Este plato esta muy bueno", Usuario = "Juan Perez" });
            //this.Comentarios.Add(new Comentario { Imagen = "https://s-media-cache-ak0.pinimg.com/736x/65/2a/c8/652ac88b9fa5bfca6211ea2f0de5b7ab.jpg", Contenido = "No lo prueben me engañaron", Usuario = "Luis Diaz" });
            //this.Comentarios.Add(new Comentario { Imagen = "http://arcdn02.mundotkm.com/2015/09/Famosos-1-500x750-267x400.jpg", Contenido = "Este plato lo sirven  mejor en otro lugar", Usuario = "Luis Alfaro" });
            //this.Comentarios.Add(new Comentario { Imagen = "https://s3.hdstatic.net/gridfs/holadoctor/625x470_4f62c7f0b937955271000000_6_32-1331876302.jpg", Contenido = "Podrian prepararme uno especial", Usuario = "Pedro Arrollo" });
            //this.Comentarios.Add(new Comentario { Imagen = "https://s-media-cache-ak0.pinimg.com/736x/65/2a/c8/652ac88b9fa5bfca6211ea2f0de5b7ab.jpg", Contenido = "A este plato le falta su nose que", Usuario = "Teresa Olmedo" });
            //this.Comentarios.Add(new Comentario { Imagen = "http://arcdn02.mundotkm.com/2015/09/Famosos-1-500x750-267x400.jpg", Contenido = "Este plato esta muy bueno", Usuario = "Torres J." });
        }
        public async void Load()
        {
            var items = await dataWCF.GetComentario(this.Plato.CPlato._id);
            this.Comentarios = new ObservableRangeCollection<Comentario>(items.OrderBy(p => p.Fecha));
            SetHeight(this.Comentarios.Count, null);
        }

        private Command getPlatosCommand;
        public Command GetPlatosCommand
        {
            get
            {
                return getPlatosCommand ??
                    (getPlatosCommand = new Command(async () => await ExecuteGetPlatosCommand(), () => { return !IsBusy; }));
            }
        }

     

        private async Task ExecuteGetPlatosCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            GetPlatosCommand.ChangeCanExecute();
            var showAlert = false;
            try
            {
                Comentarios.Clear();

                var comentarios = await dataWCF.GetComentario(this.Plato.CPlato._id);

                Comentarios.ReplaceRange(comentarios);

                //Sort();
            }
            catch (Exception ex)
            {
                showAlert = true;

            }
            finally
            {
                IsBusy = false;
                GetPlatosCommand.ChangeCanExecute();
            }

            if (showAlert)
                await page.DisplayAlert("Uh Oh :(", "Ocurrio un error.", "OK");


        }

    }
}

