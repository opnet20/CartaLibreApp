using App7.Views.Proveedores;
using App7.ServiceReference1;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App7.Interfaces;
using XLabs.Platform.Services.Media;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace App7.ViewModel.Proveedores
{
    public class AgregarPlatoViewModel : ViewModelBase
    {
        #region Variables

        Service1Client mds = new Service1Client();

        byte[] imagenArray;

        public readonly IDataStore dataPlato;

        Plato myplato;

        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();

        private IMediaPicker _mediaPicker;

        private ImageSource _imageSource;

        private string _videoInfo;

        private Command _takePictureCommand;

        private Command _selectPictureCommand;

        private Command _selectVideoCommand;

        private string _status;

        #endregion

        #region Propiedades

        public Plato Myplato
        {
            get
            {
                return myplato;
            }

            set
            {
                SetProperty(ref myplato, value);
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                SetProperty(ref _imageSource, value);
            }
        }

        public string VideoInfo
        {
            get
            {
                return _videoInfo;
            }
            set
            {
                SetProperty(ref _videoInfo, value);
            }
        }

        public Command TakePictureCommand
        {
            get
            {
                return _takePictureCommand ?? (_takePictureCommand = new Command(
                                                                       async () => await TakePicture(),
                                                                       () => true));
            }
        }

        public Command SelectVideoCommand
        {
            get
            {
                return _selectVideoCommand ?? (_selectVideoCommand = new Command(
                                                                       async () => await SelectVideo(),
                                                                       () => true));
            }
        }

        public Command SelectPictureCommand
        {
            get
            {
                return _selectPictureCommand ?? (_selectPictureCommand = new Command(
                                                                           async () => await SelectPicture(),
                                                                           () => true));
            }
        }

        public string Status
        {
            get { return _status; }
            private set { SetProperty(ref _status, value); }
        }

        public byte[] ImagenArray
        {
            get
            {
                return imagenArray;
            }

            set
            {
                imagenArray = value;
            }
        }
        string suplato;
        public string Suplato
        {
            get { return suplato; }
            set
            {
                SetProperty(ref suplato, value);
            }
        }
        #endregion

        public AgregarPlatoViewModel(Page page) : base(page)
        {
            //Title = "Agregar Plato";
            dataPlato = DependencyService.Get<IDataStore>();
       
            this.Myplato = new Plato
            {
                Hoy = false,
                Estado = "Activo",
                
            };

           
            this.Myplato.idProv = Settings.Provee._id;
            this.Myplato.Ciudad = Settings.Provee.Ciudad;
            
            //this.Myplato.Ciudad = Settings.User.Ciudad;
            //this.Myplato.idUsuario = Settings.User._id;
            //this.Myplato.ImagenUsuario = Settings.User.Imagen;
            //this.Myplato.NombreUsuario = Settings.User.Nombres+" "+ Settings.User.Apellidos;
            
            //Traer los pedidos actuales
            Setup();
        }

        #region Comandos

        public async Task Agregar(object cate)
        {
            try
            {
                if (cate != null)
                    this.Myplato.Categoria = cate.ToString();
                this.Myplato.FechaCreacion = DateTime.Now;
                if (this.ImagenArray != null && this.ImagenArray.Length > 0)
                {
                    this.Myplato.Imagen = "si";
                }
                string id = await dataPlato.SetPlato(this.Myplato);
                this.Myplato._id = id;
                
                if(this.ImagenArray != null)
                    await dataPlato.SetFoto(this.ImagenArray, "\\platos\\" + id.ToString() + ".jpg");

                //if (_modo == AgregarPlato.Modo.Proveedor)
                //{
                    
                //    //if (this.Myplato.Hoy) 
                //    //{
                //    //    ViewModelLocator.cartaViewModel.PlatosMenu.Add(this.Myplato);
                //    //}
                //    //else
                //    //{
                //    ViewModelLocator.cartaViewModel.PlatosCarta.Add(this.Myplato);
                //    //}
                //    //await page.Navigation.PopModalAsync();
                //}
                //if (_modo == AgregarPlato.Modo.Usuario)
                //{
                //    await this.page.DisplayAlert("¡Solicitud Agregada!", "Pronto un proveedor de nuestra red te respondera", "OK");
                //    await page.Navigation.PopModalAsync();
                //}
            }
            catch (Exception ex)
            {

            }
        }
        
        #endregion

        #region Operaciones Imagen

        public System.IO.Stream imgSource;

        private void Setup()
        {
            //this._mediaPicker = mediaPicker;

            if (_mediaPicker != null)
            {
                return;
            }

            var device = Resolver.Resolve<IDevice>();

            ////RM: hack for working on windows phone? 
            _mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;

            //this.ImageSource = "defaultimage.png";
        }

        public void SetImagen()
        {
            if (String.IsNullOrEmpty(Myplato.Imagen))
            {
                //Imagen por defecto
                //imagenCir.Source = ImageSource.FromUri(new Uri("http://icons.iconarchive.com/icons/blackvariant/button-ui-system-folders-alt/64/Users-icon.png"));
                //this.ImageSource = ImageSource.FromUri(new Uri("http://cartalibre.com/images/sinplato2.gif"));
                this.Suplato = Myplato.Nombre;
            }
            else
            {
                if (Myplato.Imagen == "si")
                {
                    this.ImageSource = ImageSource.FromUri(new Uri("http://opnetin-002-site2.dtempurl.com/platos/" + Myplato._id.ToString() + ".jpg"));
                }
                else
                {
                    this.ImageSource = ImageSource.FromUri(new Uri(Myplato.Imagen));
                }
            }
        }

        private async Task<MediaFile> TakePicture()
        {
            Setup();

            //ImageSource = null;

            return await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions { DefaultCamera = CameraDevice.Front, MaxPixelDimension = 400 }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Status = t.Exception.InnerException.ToString();
                }
                else if (t.IsCanceled)
                {
                    Status = "Canceled";
                }
                else
                {
                    var mediaFile = t.Result;

                    ImageSource = ImageSource.FromStream(() => mediaFile.Source);
                    this.ImagenArray = ReadToEnd(mediaFile.Source);
                    return mediaFile;
                }

                return null;
            }, _scheduler);
        }

        private async Task SelectPicture()
        {
            Setup();

            ImageSource = null;
            try
            {
                var mediaFile = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Front,
                    MaxPixelDimension = 400
                });
                imgSource = mediaFile.Source;
                ImageSource = ImageSource.FromStream(() => mediaFile.Source);
                this.ImagenArray = ReadToEnd(mediaFile.Source);
            }
            catch (System.Exception ex)
            {
                Status = ex.Message;
            }
        }

        private async Task SelectVideo()
        {
            Setup();

            //TODO Localize
            VideoInfo = "Selecting video";

            try
            {
                var mediaFile = await _mediaPicker.SelectVideoAsync(new VideoMediaStorageOptions());

                //TODO Localize
                VideoInfo = mediaFile != null
                                ? string.Format("Your video size {0} MB", ConvertBytesToMegabytes(mediaFile.Source.Length))
                                : "No video was selected";
            }
            catch (System.Exception ex)
            {
                if (ex is TaskCanceledException)
                {
                    //TODO Localize
                    VideoInfo = "Selecting video canceled";
                }
                else
                {
                    VideoInfo = ex.Message;
                }
            }
        }

        private static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }

                var returbuff = App.ResizeImageAndroid(buffer, 300, 300);
                return returbuff;
                //return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
        #endregion
    }
}
