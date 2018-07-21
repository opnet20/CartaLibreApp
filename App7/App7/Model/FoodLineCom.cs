using App7.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App7.Model
{
    public class FoodLineCom
    {
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
                    return "Solicitud de Pedido";
                    // return "Entrega: " + this.CPlato.FechaEntrega.ToString("dd/MM/yyyy hh:mm tt") + "\n" + this.CPlato.Direccion;
                }
            }
        }

        public string Icon
        {
            get
            {
                if (this.CProveedor != null)
                {
                    if (this.CPlato != null)
                    {
                        if (this.CPlato.Categoria == "Oferta")
                            return "descuento.png";
                        if (this.CPlato.Categoria == "Evento")
                            return "calendarrr.png";
                        return "";
                    }
                    else
                        return "";
                }
                else
                {
                    return "announcement.png";
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
                    if (String.IsNullOrEmpty(this.CPlato.ImagenUsuario))
                        //Imagen por defecto
                        return new System.Uri("http://opnetin-002-site25.dtempurl.com/images/defaultimage.png");
                    else
                    {
                        if (this.CPlato.ImagenUsuario == "si")
                        {
                            return new System.Uri("http://opnetin-002-site25.dtempurl.com/users/" + this.CPlato.idUsuario.ToString() + ".jpg");
                        }
                        else
                        {
                            return new System.Uri(this.CPlato.ImagenUsuario);
                        }
                    }
                }
            }
        }

        public int Columnas
        {
            get
            {
                if (this.CProveedor != null)
                    return 3;
                else
                {
                    return 1;
                }
            }
        }

        Proveedor cProveedor;   
        List<FoodLine> lines;
        FoodLine line;
        Plato cPlato;
       

        public Proveedor CProveedor
        {
            get
            {
                return cProveedor;
            }

            set
            {
                cProveedor = value;
            }
        }

        public List<FoodLine> Lines
        {
            get
            {
                return lines;
            }

            set
            {
                lines = value;
            }
        }

        public Plato CPlato
        {
            get
            {
                return cPlato;
            }

            set
            {
                cPlato = value;
            }
        }

        public string Aviso
        {
            get
            {
                try
                {
                    return "" + this.CPlato.Nombre;
                }
                catch
                {
                    return "";
                }
            }
        }

      

        public bool Ver
        {
            get
            {
                if (this.CPlato != null)
                    return false;
                else
                {
                    return true;
                }
            }
        }

        public bool NoVer
        {
            get
            {
                if (this.CPlato != null )
                {
                    return true;
                }
                else //Solicitud
                {
                    return false;
                }
            }
        }

    }
}
