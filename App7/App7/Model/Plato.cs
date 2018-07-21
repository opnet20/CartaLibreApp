using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7.ServiceReference1
{
    public partial class Plato
    {
        public Uri ImageUri
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Imagen))
                {
                    try
                    {
                        return new System.Uri("http://opnetin-002-site25.dtempurl.com/platos/" + this._id.ToString() + ".jpg");
                    }
                    catch
                    {
                        //return new System.Uri("http://opnetin-002-site25.dtempurl.com/images/sinplato2.gif");
                        return null;
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(this.idProv))
                    {
                        //return new System.Uri("http://opnetin-002-site25.dtempurl.com/images/pedido.gif");
                        return null;
                    }
                    else
                    {
                        //return new System.Uri("http://opnetin-002-site25.dtempurl.com/images/sinplato2.gif");
                        return null;
                    }
                }
            }
        }

        int hr;

        public int Hr
        {
            get
            {
                if (String.IsNullOrEmpty(this.Imagen))
                    return 180;
                else
                    return 180;
            }

            set
            {
                hr = value;
            }
        }

        string precioPedio;

        public string PrecioPedio
        {
            get
            {
                return "Pedir a S/." + this.Precio;
            }

            set
            {
                precioPedio = value;
            }
        }

        public string Sunombre
        {
            get
            {
                if (String.IsNullOrEmpty(this.Imagen))
                {
                    return this.Nombre;
                }

                else
                    return "";
            }
            set
            {
                
            }
        }

        public string Sucate
        {
            get
            {
                if (this.Estado == "Activo")
                {
                    return this.Categoria;
                }

                else
                    return this.Estado+"s";
            }
            set
            {

            }
        }

    }
}
