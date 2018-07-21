using App7.Interfaces;
using App7.Views;
using App7.ServiceReference1;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App7
{
    class HomeViewModel : ViewModelBase
    {
        //readonly IDataStore dataPlato;
        //public ObservableRangeCollection<Plato> Platos { get; set; }
        //public ObservableRangeCollection<Grouping<string, Plato>> PlatosGrouped { get; set; }
        public bool ForceSync { get; set; }

        public HomeViewModel(Page page) : base(page)
        {
            Title = "Carta";
            //dataPlato = DependencyService.Get<IDataStore>();
            //Platos = new ObservableRangeCollection<Plato>();
            //PlatosGrouped = new ObservableRangeCollection<Grouping<string, Plato>>();
        }
        //public Action<Plato> ItemSelected { get; set; }

        //Plato selectedPlato;
        //public Plato SelectedPlato
        //{
        //    get { return selectedPlato; }
        //    set
        //    {
        //        try
        //        {
        //            selectedPlato = value;
        //            OnPropertyChanged("SelectedPlato");
        //            if (selectedPlato == null)
        //                return;

        //            if (ItemSelected == null)
        //            {
        //                page.Navigation.PushAsync(new PlatosRestPage(selectedPlato));
        //                SelectedPlato = null;
        //            }
        //            else
        //            {
        //                ItemSelected.Invoke(selectedPlato);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}


        //public async Task DeletePlato(Plato plato)
        //{
        //    if (IsBusy)
        //        return;
        //    IsBusy = true;
        //    try
        //    {
        //        await dataPlato.RemovePlatoAsync(plato);
        //        Platos.Remove(plato);
        //        Sort();
        //    }
        //    catch (Exception ex)
        //    {
        //        await page.DisplayAlert("Uh Oh :(", $"Unable to remove {plato?.Nombre ?? "Unknown"}, please try again: {ex.Message}", "OK");
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }



        //}

        //private Command forceRefreshCommand;
        //public Command ForceRefreshCommand
        //{
        //    get
        //    {
        //        return forceRefreshCommand ??
        //            (forceRefreshCommand = new Command(async () =>
        //            {
        //                ForceSync = true;
        //                await ExecuteGetPlatosCommand();
        //            }));
        //    }
        //}

        //private Command getPlatosCommand;
        //public Command GetPlatosCommand
        //{
        //    get
        //    {
        //        return getPlatosCommand ??
        //            (getPlatosCommand = new Command(async () => await ExecuteGetPlatosCommand(), () => { return !IsBusy; }));
        //    }
        //}

        //private async Task ExecuteGetPlatosCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    if (ForceSync)
        //        Settings.LastSync = DateTime.Now.AddDays(-30);

        //    IsBusy = true;
        //    GetPlatosCommand.ChangeCanExecute();
        //    var showAlert = false;
        //    try
        //    {
        //        Platos.Clear();

        //        var platos = await dataPlato.GetPlatosAsync();

        //        Platos.ReplaceRange(platos);


        //        Sort();
        //    }
        //    catch (Exception ex)
        //    {
        //        showAlert = true;

        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //        GetPlatosCommand.ChangeCanExecute();
        //    }

        //    if (showAlert)
        //        await page.DisplayAlert("Uh Oh :(", "Unable to gather Platos.", "OK");


        //}

        //private void Sort()
        //{

        //    PlatosGrouped.Clear();
        //    var sorted = from Plato in Platos
        //                 orderby Plato.Categoria, Plato.Nombre
        //                 group Plato by Plato.Categoria into platoGroup
        //                 select new Grouping<string, Plato>(platoGroup.Key, platoGroup);

        //    PlatosGrouped.ReplaceRange(sorted);
        //}

  

    }
}
