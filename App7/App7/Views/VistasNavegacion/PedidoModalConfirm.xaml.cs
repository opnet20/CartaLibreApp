using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App7.Views.VistasNavegacion
{
    public partial class PedidoModalConfirm : PopupPage
    {
        public event EventHandler confirmEvent;

        public PedidoModalConfirm(string mensaje)
        {
            InitializeComponent();
            msjLb.Text = mensaje;
        }

        async void cancelar_Click(object sender, EventArgs e)
        {
            await this.Navigation.PopPopupAsync();
        }

        async void confirmar_Click(object sender, EventArgs e)
        {
            confirmEvent(null,null);
            await this.Navigation.PopPopupAsync();
        }
    }
}
