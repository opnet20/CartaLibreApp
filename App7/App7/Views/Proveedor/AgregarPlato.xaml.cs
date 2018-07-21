using App7.ServiceReference1;
using App7.ViewModel.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App7.Views.Proveedores
{
    public partial class AgregarPlato : ContentPage
    {
        AgregarPlatoViewModel _vm;
        public event EventHandler close;
        string mycategoria;

        public AgregarPlato(string tipo)
        {
            InitializeComponent();
            if (tipo == "Plato")
            {
                mycategoria = "";
                this.Title = "Agregar Plato";
                this.EventStack.HeightRequest = 0;
            }
            if (tipo == "Evento")
            {
                this.stackCategoria.HeightRequest = 0;
                mycategoria = tipo;
                this.Title = "Agregar Evento";
            }
            if (tipo == "Oferta")
            {
                this.stackCategoria.HeightRequest = 0;
                mycategoria = tipo;
                this.Title = "Agregar Oferta";
                this.EventStack.HeightRequest = 0;
            }
            
            CategoriaPi.ItemsSource = PGlobal.categorias.Select(p=>p.Categoria).ToList();
            BindingContext = _vm = new AgregarPlatoViewModel(this);
            if(_vm.Myplato.Estado == "Activo")
                checkAc.IsToggled = true;
        }

        void solohoy_Click(object sender, EventArgs e)
        {
            //solohoyBT.BackgroundColor = Color.FromHex("EF4F50");
            //solohoyBT.TextColor = Color.White;

            //alacartaBT.BackgroundColor = Color.White;
            //alacartaBT.TextColor = Color.FromHex("EF4F50");
            _vm.Myplato.Hoy = true;
        }

        public void AgregarPlatillo(Plato p)
        {
            _vm.Myplato = p;
            if (p.Estado == "Activo")
                checkAc.IsToggled = true;
            else
                checkAc.IsToggled = false;

            this.Title = "Editar Plato";
            this.EventStack.HeightRequest = 0;
            if (p.Categoria == "Evento")
            {
                //chekEvent.IsToggled = true;
                this.EventStack.HeightRequest = 60;
                this.stackCategoria.HeightRequest = 0;

                datepicker.Date = p.FechaEntrega;
                TimeSpan ts = new TimeSpan(p.FechaEntrega.Hour, p.FechaEntrega.Minute, 0);
                timepicker.Time = ts;

                this.Title = "Editar Evento";
                this.stackCategoria.HeightRequest = 0;
            }
            if (p.Categoria == "Oferta")
            {
                this.Title = "Editar Oferta";
                this.stackCategoria.HeightRequest = 0;
            }

            CategoriaPi.SelectedItem = p.Categoria;
            _vm.SetImagen();
        }

        void alacarta_Click(object sender, EventArgs e)
        {
            //alacartaBT.BackgroundColor = Color.FromHex("EF4F50");
            //alacartaBT.TextColor = Color.White;

            //solohoyBT.BackgroundColor = Color.White;
            //solohoyBT.TextColor = Color.FromHex("EF4F50");
            _vm.Myplato.Hoy = false;
        }

        //void l_Click(object sender, EventArgs e)
        //{
        //    if (_vm.Myplato.Lunes)
        //    {
        //        lBT.BackgroundColor = Color.White;
        //        lBT.TextColor = Color.FromHex("EF4F50");
        //    }
        //    else
        //    {
        //        lBT.BackgroundColor = Color.FromHex("EF4F50");
        //        lBT.TextColor = Color.White;
        //    }
        //    _vm.Myplato.Lunes = !_vm.Myplato.Lunes;
        //}
        //void m_Click(object sender, EventArgs e)
        //{
        //    if (_vm.Myplato.Martes)
        //    {
        //        mBT.BackgroundColor = Color.White;
        //        mBT.TextColor = Color.FromHex("EF4F50");
        //    }
        //    else
        //    {
        //        mBT.BackgroundColor = Color.FromHex("EF4F50");
        //        mBT.TextColor = Color.White;
        //    }
        //    _vm.Myplato.Martes = !_vm.Myplato.Martes;
        //}
        //void mi_Click(object sender, EventArgs e)
        //{
        //    if (_vm.Myplato.Miercoles)
        //    {
        //        miBT.BackgroundColor = Color.White;
        //        miBT.TextColor = Color.FromHex("EF4F50");
        //    }
        //    else
        //    {
        //        miBT.BackgroundColor = Color.FromHex("EF4F50");
        //        miBT.TextColor = Color.White;
        //    }
        //    _vm.Myplato.Miercoles = !_vm.Myplato.Miercoles;
        //}
        //void j_Click(object sender, EventArgs e)
        //{
        //    if (_vm.Myplato.Jueves)
        //    {
        //        jBT.BackgroundColor = Color.White;
        //        jBT.TextColor = Color.FromHex("EF4F50");
        //    }
        //    else
        //    {
        //        jBT.BackgroundColor = Color.FromHex("EF4F50");
        //        jBT.TextColor = Color.White;
        //    }
        //    _vm.Myplato.Jueves = !_vm.Myplato.Jueves;
        //}
        //void v_Click(object sender, EventArgs e)
        //{
        //    if (_vm.Myplato.Viernes)
        //    {
        //        vBT.BackgroundColor = Color.White;
        //        vBT.TextColor = Color.FromHex("EF4F50");
        //    }
        //    else
        //    {
        //        vBT.BackgroundColor = Color.FromHex("EF4F50");
        //        vBT.TextColor = Color.White;
        //    }
        //    _vm.Myplato.Viernes = !_vm.Myplato.Viernes;
        //}
        //void s_Click(object sender, EventArgs e)
        //{
        //    if (_vm.Myplato.Sabado)
        //    {
        //        sBT.BackgroundColor = Color.White;
        //        sBT.TextColor = Color.FromHex("EF4F50");
        //    }
        //    else
        //    {
        //        sBT.BackgroundColor = Color.FromHex("EF4F50");
        //        sBT.TextColor = Color.White;
        //    }
        //    _vm.Myplato.Sabado = !_vm.Myplato.Sabado;
        //}
        //void d_Click(object sender, EventArgs e)
        //{
        //    if (_vm.Myplato.Domingo)
        //    {
        //        dBT.BackgroundColor = Color.White;
        //        dBT.TextColor = Color.FromHex("EF4F50");
        //    }
        //    else
        //    {
        //        dBT.BackgroundColor = Color.FromHex("EF4F50");
        //        dBT.TextColor = Color.White;
        //    }
        //    _vm.Myplato.Domingo = !_vm.Myplato.Domingo;
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void Toggled_Click(object sender, ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    this._vm.Myplato.Estado = "Activo";
                }
                else
                    this._vm.Myplato.Estado = "Cerrado";
            }
            catch (Exception ex)
            {

            }
        }

        //public void Toggled_Click2(object sender, ToggledEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Value)
        //        {
        //            this.EventStack.HeightRequest = 60;
        //            this.stackCategoria.HeightRequest = 0;
        //            this._vm.Myplato.Categoria = "Evento";
        //        }
        //        else
        //        {
        //            this.EventStack.HeightRequest = 0;
        //            this.stackCategoria.HeightRequest = 60;
        //            this._vm.Myplato.Categoria = String.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


        //public void Toggled_Click3(object sender, ToggledEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Value)
        //        {
        //            //this.EventStack.HeightRequest = 60;
        //            //this.stackCategoria.HeightRequest = 0;
        //            //chekEvent.IsToggled = false;
        //            this._vm.Myplato.Categoria = "Oferta";
        //        }
        //        else
        //        {
        //            //this.EventStack.HeightRequest = 0;
        //            //this.stackCategoria.HeightRequest = 60;
        //            this._vm.Myplato.Categoria = String.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        public async void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                loading.IsRunning = true;
                string categoria;
                if (mycategoria == "")
                {
                    categoria = CategoriaPi.SelectedItem.ToString();
                }
                else
                {
                    categoria = mycategoria;
                }

                //if (chekEvent.IsToggled)
                //    categoria = "Evento";
                this._vm.Myplato.FechaEntrega = DateTime.Now;

                if ( categoria == "Evento")
                {
                    DateTime entre = (new DateTime(datepicker.Date.Year,
                      datepicker.Date.Month,
                      datepicker.Date.Day, timepicker.Time.Hours, timepicker.Time.Minutes, 0));

                    this._vm.Myplato.FechaEntrega = new DateTime(entre.Year,
                      entre.Month,
                      entre.Day, entre.Hour, entre.Minute, 0);
                }
                if (categoria == null || String.IsNullOrEmpty(this._vm.Myplato.Nombre) || String.IsNullOrEmpty(this._vm.Myplato.Descripcion))
                {
                    await this.DisplayAlert("Error", "Completar: Nombre, Descripción y Categoria", "OK");
                    loading.IsRunning = false;
                    return;
                }

                await _vm.Agregar(categoria);
                loading.IsRunning = false;
                close(CategoriaPi.SelectedItem, null);

                string imagenprove = "";
               

                if (Settings.Provee.ImageUri == null)
                    imagenprove = "http://cartalibre.com/images/logocl.png";
                else
                    imagenprove = Settings.Provee.ImageUri.ToString();

                //if (_vm.Myplato.ImageUri == null)
                //    imageplato = "";
                //else
                //    imageplato = _vm.Myplato.ImageUri.ToString();


                if (_vm.Myplato.Estado == "Activo")
                {
                    if (_vm.Myplato.Categoria == "Evento")
                    {
                        await _vm.dataPlato.EnviarNotificacionPlato(Settings.Provee._id, "Evento " + _vm.Myplato.Nombre,
                                                              Settings.Provee.Nombre,
                                                              _vm.Myplato.Precio.ToString(),
                                                              imagenprove,
                                                              _vm.Myplato._id);
                    }
                    else
                    {
                        await _vm.dataPlato.EnviarNotificacionPlato(Settings.Provee._id, _vm.Myplato.Nombre,
                                                              Settings.Provee.Nombre,
                                                              _vm.Myplato.Precio.ToString(),
                                                              imagenprove,
                                                              _vm.Myplato._id);
                    }
                }

                await this.DisplayAlert("¡Guardado!", "Registro correcto", "OK");
                await this.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", "Complete los campos y vuelva a intentarlo", "OK");
                loading.IsRunning = false;
            }
        }


    }
}
