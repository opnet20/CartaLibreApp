using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace App7.ServiceReference1
{
    public partial class Proveedor
    {
        int hr;
        bool sigo;
        bool verSigo;
        public Uri ImageUri
        {
            get
            {
                if (String.IsNullOrEmpty(Imagen))
                    return new System.Uri("http://opnetin-002-site25.dtempurl.com/images/chef.png");
                else
                    return new System.Uri("http://opnetin-002-site25.dtempurl.com/service/proveedores/" + this._id.ToString() + ".jpg");
            }
        }

        public int Hr
        {
            get
            {
                if (String.IsNullOrEmpty(this.Imagen))
                    return 0;
                else
                    return 200;
            }

            set
            {
                hr = value;
            }
        }

        public string Telefono2
        {
            get
            {
                if (String.IsNullOrEmpty(this.NumeroTelefono))
                {
                    return "(vacío)";
                }
                else
                    return this.NumeroTelefono;
            }
            
            set {; }
        }

        public string Correo2
        {
            get
            {
                if (String.IsNullOrEmpty(this.Correo))
                {
                    return "(vacío)";
                }
                else
                    return this.Correo;
            }

            set {; }
        }

        public string Ruc2
        {
            get
            {
                if (String.IsNullOrEmpty(this.Ruc))
                {
                    return "(vacío)";
                }
                else
                    return this.Ruc;
            }

            set {; }
        }

        public string Registro2
        {
            get
            {
                if (String.IsNullOrEmpty(this.Registro))
                {
                    return "(vacío)";
                }
                else
                    return this.Registro;
            }

            set {; }
        }

        public string Representante2
        {
            get
            {
                if (String.IsNullOrEmpty(this.Representante))
                {
                    return "(vacío)";
                }
                else
                    return this.Representante;
            }

            set {; }
        }

        public string Referencia2
        {
            get
            {
                if (String.IsNullOrEmpty(this.Referencia))
                {
                    return "(vacío)";
                }
                else
                    return this.Referencia;
            }

            set {; }
        }

        public string Distrito2
        {
            get
            {
                if (String.IsNullOrEmpty(this.Distrito))
                {
                    return "(vacío)";
                }
                else
                    return this.Distrito;
            }

            set {; }
        }

        public string Direccion2
        {
            get
            {
                if (String.IsNullOrEmpty(this.Direccion))
                {
                    return "(vacío)";
                }
                else
                    return this.Direccion;
            }

            set {; }
        }

        public string Tipo2
        {
            get
            {
                if (String.IsNullOrEmpty(this.Tipo))
                {
                    return "(vacío)";
                }
                else
                    return this.Tipo;
            }

            set {; }
        }

        public string ImageCategoria
        {
            get
            {   //P:proceso //C:Calificado

                switch (this.Categoria)
                {
                    case "C":
                        return "check.png";
                    case "P":
                        return "settings.png";
                    default:
                        return "Egg.png";
                }
            }
        }

       
        public bool Sigo
        {
            get
            {
                return sigo;
            }

            set
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    sigo = value;
                    this.RaisePropertyChanged("Sigo");
                    this.RaisePropertyChanged("VerSigo");
                });
            }
        }

        public bool VerSigo
        {
            get
            {
                return !sigo;
            }

            set
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    VerSigo = value;
                    this.RaisePropertyChanged("VerSigo");
                });
            }
        }

        public string Abierto
        {
            get
            {
                var fecha = DateTime.Now;
                var day = fecha.DayOfWeek;
               
                switch (day)
                {
                    case DayOfWeek.Monday:
                        if (this.L)
                        {
                            var ini = new DateTime(fecha.Year, fecha.Month, fecha.Day, Lo, 0, 0);
                            var cierra = new DateTime(fecha.Year, fecha.Month, fecha.Day, Lc, 0, 0);
                            if (fecha > ini && fecha < cierra)
                            {
                                AbiertoDetalle = "Abierto";
                                return "ic_bookmark_green_300_24dp";
                            }
                            else
                            {
                                if (fecha < ini)
                                    AbiertoDetalle = "" + (ini - fecha).Hours + "h " + (ini - fecha).Minutes + "m";
                                else
                                    AbiertoDetalle = "";
                                return "ic_bookmark_grey_500_24dp";
                            }
                        }
                        else
                        {
                            AbiertoDetalle = "Cerrado";
                            return "ic_bookmark_grey_500_24dp";
                        }
                    case DayOfWeek.Tuesday:
                        if (this.M)
                        {
                            var ini = new DateTime(fecha.Year, fecha.Month, fecha.Day, Mo, 0, 0);
                            var cierra = new DateTime(fecha.Year, fecha.Month, fecha.Day, Mc, 0, 0);
                            if (fecha > ini && fecha < cierra)
                            {
                                AbiertoDetalle = "Abierto";
                                return "ic_bookmark_green_300_24dp";
                            }
                            else
                            {
                                if (fecha < ini)
                                    AbiertoDetalle = "" + (ini - fecha).Hours + "h " + (ini - fecha).Minutes + "m";
                                else
                                    AbiertoDetalle = "";
                                return "ic_bookmark_grey_500_24dp";
                            }
                        }
                        else
                        {
                            AbiertoDetalle = "Cerrado";
                            return "ic_bookmark_grey_500_24dp";
                        }
                    case DayOfWeek.Wednesday:
                        if (this.Mi)
                        {
                            var ini = new DateTime(fecha.Year, fecha.Month, fecha.Day, Mio, 0, 0);
                            var cierra = new DateTime(fecha.Year, fecha.Month, fecha.Day, Mic, 0, 0);
                            if (fecha > ini && fecha < cierra)
                            {
                                AbiertoDetalle = "Abierto";
                                return "ic_bookmark_green_300_24dp";
                            }
                            else
                            {
                                if (fecha < ini)
                                    AbiertoDetalle = "" + (ini - fecha).Hours + "h " + (ini - fecha).Minutes + "m";
                                else
                                    AbiertoDetalle = "";
                                return "ic_bookmark_grey_500_24dp";
                            }
                        }
                        else
                        {
                            AbiertoDetalle = "Cerrado";
                            return "ic_bookmark_grey_500_24dp";
                        }
                    case DayOfWeek.Thursday:
                        if (this.J)
                        {
                            var ini = new DateTime(fecha.Year, fecha.Month, fecha.Day, Jo, 0, 0);
                            var cierra = new DateTime(fecha.Year, fecha.Month, fecha.Day, Jc, 0, 0);
                            if (fecha > ini && fecha < cierra)
                            {
                                AbiertoDetalle = "Abierto";
                                return "ic_bookmark_green_300_24dp";
                            }
                            else
                            {
                                if (fecha < ini)
                                    AbiertoDetalle = "" + (ini - fecha).Hours + "h " + (ini - fecha).Minutes + "m";
                                else
                                    AbiertoDetalle = "";
                                return "ic_bookmark_grey_500_24dp";
                            }
                        }
                        else
                        {
                            AbiertoDetalle = "Cerrado";
                            return "ic_bookmark_grey_500_24dp";
                        }
                    case DayOfWeek.Friday:
                        if (this.V)
                        {
                            var ini = new DateTime(fecha.Year, fecha.Month, fecha.Day, Vo, 0, 0);
                            var cierra = new DateTime(fecha.Year, fecha.Month, fecha.Day, Vc, 0, 0);
                            if (fecha > ini && fecha < cierra)
                            {
                                AbiertoDetalle = "Abierto";
                                return "ic_bookmark_green_300_24dp";
                            }
                            else
                            {
                                if (fecha < ini)
                                    AbiertoDetalle = "" + (ini - fecha).Hours + "h " + (ini - fecha).Minutes + "m";
                                else
                                    AbiertoDetalle = "";
                                return "ic_bookmark_grey_500_24dp";
                            }
                        }
                        else
                        {
                            AbiertoDetalle = "Cerrado";
                            return "ic_bookmark_grey_500_24dp";
                        }
                        return "ic_bookmark_grey_500_24dp";
                    case DayOfWeek.Saturday:
                        if (this.S)
                        {
                            var ini = new DateTime(fecha.Year, fecha.Month, fecha.Day, So, 0, 0);
                            var cierra = new DateTime(fecha.Year, fecha.Month, fecha.Day, Sc, 0, 0);
                            if (fecha > ini && fecha < cierra)
                            {
                                AbiertoDetalle = "Abierto";
                                return "ic_bookmark_green_300_24dp";
                            }
                            else
                            {
                                if (fecha < ini)
                                    AbiertoDetalle = "" + (ini - fecha).Hours + "h " + (ini - fecha).Minutes + "m";
                                else
                                    AbiertoDetalle = "";
                                return "ic_bookmark_grey_500_24dp";
                            }
                        }
                        else
                        {
                            AbiertoDetalle = "Cerrado";
                            return "ic_bookmark_grey_500_24dp";
                        }
                    case DayOfWeek.Sunday:
                        if (this.D)
                        {
                            var ini = new DateTime(fecha.Year, fecha.Month, fecha.Day, Doo, 0, 0);
                            var cierra = new DateTime(fecha.Year, fecha.Month, fecha.Day, Dc, 0, 0);
                            if (fecha > ini && fecha < cierra)
                            {
                                AbiertoDetalle = "Abierto";
                                return "ic_bookmark_green_300_24dp";
                            }
                            else
                            {
                                if (fecha < ini)
                                    AbiertoDetalle = "" + (ini - fecha).Hours + "h " + (ini - fecha).Minutes + "m";
                                else
                                    AbiertoDetalle = "";
                                return "ic_bookmark_grey_500_24dp";
                            }
                        }
                        else
                        {
                            AbiertoDetalle = "Cerrado";
                            return "ic_bookmark_grey_500_24dp";
                        }
                }   
                return abierto;
            }
        }

        public string AbiertoDetalle
        {
            get
            {
                return abiertoDetalle;
            }

            set
            {
                abiertoDetalle = value;
            }
        }

        string abierto;
        string abiertoDetalle;
        //public int ButtonHr
        //{
        //    get
        //    {
        //        if (Sigo)
        //            return 0;
        //        else
        //            return 45;
        //    }
        //}
    }
}

