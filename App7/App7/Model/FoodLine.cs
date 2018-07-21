using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App7.ServiceReference1
{
    public partial class FoodLine: System.ComponentModel.INotifyPropertyChanged
    {
        string usuario;

        public string Usuario
        {
            get
            {
                if (this.CProveedor != null)
                {
                    return this.CProveedor.Nombre;
                }
                else
                {
                    return this.CPlato.NombreUsuario;
                }
            }

            set
            {
                usuario = value;
            }
        }

        public string SubUsuario
        {
            get
            {
                if (this.CProveedor != null)
                {
                    return this.CProveedor.Descripcion;
                }
                else
                {
                    return "Entrega: "+ this.CPlato.FechaEntrega.ToString("dd/MM/yyyy hh:mm tt") + "\n"+ this.CPlato.Direccion;
                }
            }

        }

        public Uri ImageUri
        {
            get
            {
                if (this.CProveedor != null)
                    return this.CProveedor.ImageUri;
                else
                {
                    if (String.IsNullOrEmpty(CPlato.ImagenUsuario))
                        //Imagen por defecto
                        return new System.Uri("http://opnetin-002-site25.dtempurl.com/images/defaultimage.png");
                    else
                    {
                        if (CPlato.ImagenUsuario == "si")
                        {
                            return new System.Uri("http://opnetin-002-site25.dtempurl.com/users/" + this.CPlato.idUsuario.ToString() + ".jpg");
                        }
                        else
                        {
                            return new System.Uri(CPlato.ImagenUsuario);
                        }
                    }
                }
            }
        }
        
        public bool Solicitud
        {
            get
            {
                if (this.CProveedor != null)
                    return false;
                else
                {
                    string id = Settings.User._id;
                    if (Settings.User.Tipo == "P" &&
                        id != this.CPlato.idUsuario &&
                        this.CPlato.Estado != "Cerrado")
                        return true;
                    else
                        return false;
                }
            }
           
        }

        public bool CerrarOferta
        {
            get
            {
                if (this.CProveedor != null)
                    return false;
                else
                {
                    string id = Settings.User._id;
                    if (id == this.CPlato.idUsuario && this.CPlato.Estado == "Abierto")
                        return true;
                    else
                        return false;
                }
            }
            set
            {
                this.RaisePropertyChanged("CerrarOferta");
            }
        }

        public bool Plato
        {
            get
            {
                if (this.CProveedor != null)
                    return true;
                else
                {
                    return false;
                }
            }
        }


        public bool IsTitulo
        {
            get
            {
                if (this.CProveedor == null || this.CPlato.Categoria == "Evento") //Si es un pedido(Sin Proveedor) o es CAtegoria Evento
                    return true;
                else
                {
                    return false;
                }
            }
        }
        
        public string Titulo
        {
            get
            {
                if (this.CProveedor == null) //Si es un pedido(Sin Proveedor)  
                    return "PEDIDO PERSONALIZADO";
                if (this.CPlato.Categoria == "Evento") //CAtegoria Evento
                    return "EVENTO " + this.CPlato.FechaEntrega.ToString("ddd d MMM hh:mm");
                else
                    return "";
            }
        }

        public string PicTitulo
        {
            get
            {
                if (this.CProveedor == null) //Si es un pedido(Sin Proveedor)  
                    return "ic_restaurant_red_light_18dp.png";
                if (this.CPlato.Categoria == "Evento") //CAtegoria Evento
                    return "ic_event_available_cyan_A700_18dp.png";
                else
                    return "";
            }
        }

        public string FoodColor
        {
            get
            {
                if (this.CProveedor != null)
                    return "#c4c4c4";
                else
                {
                    return "#F7A305";
                }
            }
        }

        public bool LikeImg
        {
            get
            {
                if (this.like)
                    return false;
                else
                {
                    return true;
                }
            }

            set
            {
                Device.BeginInvokeOnMainThread( () =>
                {
                    likeImg = value;
                    this.like = true;
                    this.RaisePropertyChanged("LikeImg");
                });
            }
        }
        
        bool likeImg;

        public string Sunombre
        {
            get
            {
                if (this.CProveedor == null)
                {
                    return "BUSCO\n" + this.CPlato.Nombre;
                }
                if (String.IsNullOrEmpty(this.CPlato.Imagen))
                {
                    return this.CPlato.Nombre;
                }
               
                else
                    return "";
            }
            set
            {
                sunombre = value;
            }
        }

        string sunombre;

        public FoodLine This
        {
            get
            {
                return this;
            }
        }
    }
}
