using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7.ServiceReference1
{
    public partial class Pedido : System.ComponentModel.INotifyPropertyChanged
    {
        string imagen;

        public string Imagen
        {
            get
            {
                if (this.Estado == "Por Confirmar")
                    return "porconfirmar2.png";
                if (this.Estado == "Confirmado")
                    return "confirmado.png";
                if (this.Estado == "Rechazado")
                    return "rechazado.png";
                if (this.Estado == "Aceptado")
                    return "confirmado.png";
                if (this.Estado == "Declinado")
                    return "declinado.png";
                else
                    return "oferta.png";
            }

            set
            {
                imagen = value;
            }
        }

        public double MEnvio
        {
            get
            {
                return menvio;
            }

            set
            {
                menvio = value;
                this.Envio = menvio;
                this.MTotal = this.Total + this.Envio + this.Adicional;
            }
        }

        public double MAdicional
        {
            get
            {
                return madicional;
            }

            set
            {
                madicional = value;
                this.Adicional = madicional;
                this.MTotal = this.Total + this.Adicional + this.Envio;
            }
        }

        public double MTotal
        {
            get
            {
                return mtotal;
            }

            set
            {
                mtotal = value;
                this.RaisePropertyChanged("MTotal");
            }
        }

        public int Alto
        {
            get
            {
                if (this.Estado == "Confirmado")
                    return 40;
                else
                    return 0;
            }

            set
            {
                alto = value;
            }
        }

        int alto;
        double menvio;
        double madicional;
        double mtotal;
        long msgs;
        long altoMs;

        public Uri ImagePlato
        {
            get
            {
                try
                {
                    if(this.Categoria == "Oferta")
                        return new System.Uri("http://opnetin-002-site25.dtempurl.com/platos/" + this.idPlato.ToString() + ".jpg");
                    if (this.Categoria == "Evento")
                        return new System.Uri("http://opnetin-002-site25.dtempurl.com/platos/" + this.idPlato.ToString() + ".jpg");
                    else
                        return new System.Uri("http://opnetin-002-site25.dtempurl.com/platos/" + this.idPlato.ToString() + ".jpg");
                }
                catch
                {
                    return new System.Uri("http://opnetin-002-site25.dtempurl.com/images/coverfood.png");
                }
            }
        }

        public string Icon
        {
            get
            {
                try
                {
                    if (this.Categoria == "Oferta")
                        return "descuento.png";
                    if (this.Categoria == "Evento")
                        return "calendarrr.png";
                    else
                        return null;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Uri ImageProveedor
        {
            get
            {
                try
                {
                    return new System.Uri("http://opnetin-002-site25.dtempurl.com/proveedores/" + this.idProv.ToString() + ".jpg");
                }
                catch
                {
                    return new System.Uri("http://opnetin-002-site25.dtempurl.com/images/chef.png");
                }
            }
        }

        public Uri ImageUsuario
        {
            get
            {
                try
                {
                    return new System.Uri("http://opnetin-002-site25.dtempurl.com/users/" + this.idUsu.ToString() + ".jpg");
                }
                catch
                {
                    return new System.Uri("http://opnetin-002-site25.dtempurl.com/images/defaultimage.png");
                }
            }
        }

        public long Msgs
        {
            get
            {
                return msgs;
            }

            set
            {
                msgs = value;
                this.RaisePropertyChanged("Msgs");
            }
        }

        public long AltoMs
        {
            get
            {
                return altoMs;
            }

            set
            {
                altoMs = value;
                this.RaisePropertyChanged("AltoMs");
            }
        }
    }
}
