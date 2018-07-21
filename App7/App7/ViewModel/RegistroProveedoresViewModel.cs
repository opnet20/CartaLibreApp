using App7.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;
using XLabs.Platform.Services.Geolocation;
using XLabs.Platform.Services.Media;

namespace App7.ViewModel
{
    public class RegistroProveedoresViewModel : ViewModelBase
    {

        Proveedor _proveedor;
        public Proveedor Prov
        {
            get
            {
                return _proveedor;
            }

            set
            {
                _proveedor = value;
                OnPropertyChanged("Prov");
            }
        }
        byte[] imagenArray;
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
        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private IMediaPicker _mediaPicker;
        private ImageSource _imageSource;
        private string _videoInfo;
        private Command _takePictureCommand;
        private Command _selectPictureCommand;
        private Command _selectVideoCommand;
        private string _status;
        private string selectedItem;
        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }

            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        //GPS
        private IGeolocator _geolocator;
        private IPhoneService _phoneService;
        private IDevice _device;
        private CancellationTokenSource _cancelSource;
        private string _positionStatus = string.Empty;
        private string _positionLatitude = string.Empty;
        private string _positionLongitude = string.Empty;
        
        public string PositionStatus
        {
            get
            {
                return _positionStatus;
            }
            set
            {
                SetProperty(ref _positionStatus, value);
            }
        }

        /// <summary>
        /// Gets or sets the position latitude.
        /// </summary>
        /// <value>The position latitude.</value>
        public string PositionLatitude
        {
            get
            {
                return _positionLatitude;
            }
            set
            {
                SetProperty(ref _positionLatitude, value);
            }
        }

        /// <summary>
        /// Gets or sets the position longitude.
        /// </summary>
        /// <value>The position longitude.</value>
        public string PositionLongitude
        {
            get
            {
                return _positionLongitude;
            }
            set
            {
                SetProperty(ref _positionLongitude, value);
            }
        }

        private IGeolocator Geolocator
        {
            get
            {
                if (_geolocator == null)
                {
                    var device = Resolver.Resolve<IGeolocator>();

                    ////RM: hack for working on windows phone? ?? Resolver.Resolve<IGeolocator>();
                    //_mediaPicker = DependencyService.Get<IMediaPicker>() ?? Resolver.Resolve<IGeolocator>();

                    _geolocator = DependencyService.Get<IGeolocator>() ?? device;
                    _geolocator.PositionError += _geolocator_PositionError; ;
                    _geolocator.PositionChanged += _geolocator_PositionChanged; ;
                }
                return _geolocator;
            }
        }

        private void _geolocator_PositionChanged(object sender, PositionEventArgs e)
        {
            ////			BeginInvokeOnMainThread (() => {
            ////				ListenStatus.Text = e.Position.Timestamp.ToString("G");
            ////				ListenLatitude.Text = "La: " + e.Position.Latitude.ToString("N4");
            ////				ListenLongitude.Text = "Lo: " + e.Position.Longitude.ToString("N4");
            ////			});
        }

        private void _geolocator_PositionError(object sender, PositionErrorEventArgs e)
        {
            ////			BeginInvokeOnMainThread (() => {
            ////				ListenStatus.Text = e.Error.ToString();
            ////			});
        }

        //End GPS

        ObservableCollection<string> items;
        public ObservableCollection<string> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
                OnPropertyChanged("Items");
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
        
        public RegistroProveedoresViewModel(Page page, Proveedor p) : base(page)
        {
            //IMediaPicker mediaPicker
            this.Prov = p;
            Setup();
        }

      
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

            //if(String.IsNullOrEmpty(this.Prov.Imagen))
            //    this.ImageSource = "rest.png";
        }

        public void LoadTipos()
        {
            //var items = await service.GetProducts(term);
            
            Items = new ObservableCollection<string>();
          
            Items.Add("Pescados y mariscos");
            Items.Add("Parrillas");
            Items.Add("Autoservicio");
            Items.Add("De menú y a la carta");
            Items.Add("Cafeteria");
            Items.Add("Peña");
            Items.Add("Tasca");
            Items.Add("Carnotzet");
            Items.Add("Snack");
            Items.Add("Tipica");
            Items.Add("Chifa");
            Items.Add("Polleria");
            Items.Add("Tortas");
            Items.Add("Anticucheria");
            Items.Add("Dulceria	");
            Items.Add("Taquería");
            Items.Add("Vegetariano");
            Items.Add("Buffet");
            Items.Add("Cocina Francesa");
            Items.Add("Cocina Italiana");
            Items.Add("Cocina Española");
            Items.Add("Cocina China");
            Items.Add("Cocina Medio Oriente");
            Items.Add("Cocina Caribeña");
            Items.Add("Cocina Tailandesa");
            Items.Add("Cocina Mexicana");
            Items.Add("Comida rápida (fast food)");
            Items.Add("Alta cocina o gourmet");
            Items.Add("Ladies - bar");
            Items.Add("Piano - bar");
            Items.Add("Lobby - bar");
            Items.Add("Terraza - bar");
            Items.Add("Piscina - bar");
            Items.Add("Salón de té");
            Items.Add("Night Club o Boite de Nuit");
            Items.Add("Pub");
            Items.Add("Teatro - bar");

            OnPropertyChanged("Items");
        }
        Command<string> _searchCommand;
        public Command<string> SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>(
                    obj => { },
                    obj => !string.IsNullOrEmpty(obj.ToString())));
            }
        }
        
        /// <summary>c
        /// Takes the picture.
        /// </summary>
        /// <returns>Take Picture Task.</returns>
        private async Task<MediaFile> TakePicture()
        {
            try
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
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Selects the picture.
        /// </summary>
        /// <returns>Select Picture Task.</returns>
        /// 
        public System.IO.Stream imgSource;

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

        /// <summary>
        /// Selects the video.
        /// </summary>
        /// <returns>Select Video Task.</returns>
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

        public void Guardar()
        {
            
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

                var returbuff = App.ResizeImageAndroid(buffer, 200, 200);
                return returbuff;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public async Task GetPosition()
        {
            _cancelSource = new CancellationTokenSource();

            PositionStatus = string.Empty;
            PositionLatitude = string.Empty;
            PositionLongitude = string.Empty;
            IsBusy = true;
            await
                Geolocator.GetPositionAsync(10000, _cancelSource.Token, true)
                    .ContinueWith(t =>
                    {
                        IsBusy = false;
                        if (t.IsFaulted)
                        {
                            PositionStatus = ((GeolocationException)t.Exception.InnerException).Error.ToString();
                        }
                        else if (t.IsCanceled)
                        {
                            PositionStatus = "Canceled";
                        }
                        else
                        {
                            PositionStatus = t.Result.Timestamp.ToString("G");
                            PositionLatitude = "La: " + t.Result.Latitude.ToString("N4");
                            PositionLongitude = "Lo: " + t.Result.Longitude.ToString("N4");
                        }
                    }, _scheduler);
        }

    }
}
