using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.StateSquid;

namespace StateSquidSample.PageModels
{
    public class MainPageModel : FreshMvvm.FreshBasePageModel
    {
        public State CurrentState { get; set; }

        //public bool IsFullscreenLoading { get; set; }
        //public bool IsSkeletonLoading { get; set; }

        //public ICommand FullscreenLoadingCommand { get; set; }
        //public ICommand SkeletonCommand { get; set; }
        //public ICommand RepeatingCommand { get; set; }

        public ICommand CycleStatesCommand { get; set; }

        public MainPageModel()
        {
            //FullscreenLoadingCommand = new Command(async (x) =>
            //{
            //    IsFullscreenLoading = true;
            //    await Task.Delay(2000);
            //    IsFullscreenLoading = false;
            //});

            //SkeletonCommand = new Command(async (x) =>
            //{
            //    IsSkeletonLoading = true;
            //    await Task.Delay(2000);
            //    IsSkeletonLoading = false;
            //});

            //RepeatingCommand = new Command(async (x) =>
            //{
            //    await CoreMethods.PushPageModel<ListViewPageModel>();
            //});

            CycleStatesCommand = new Command(async (x) => await CycleStates());
        }

        private async Task CycleStates()
        {
            CurrentState = State.Loading;
            await Task.Delay(3000);
            CurrentState = State.Saving;
            await Task.Delay(3000);
            CurrentState = State.Error;
            await Task.Delay(3000);
            CurrentState = State.Empty;
            await Task.Delay(3000);
            CurrentState = State.Success;
            await Task.Delay(3000);
            CurrentState = State.None;
        }
    }
}
