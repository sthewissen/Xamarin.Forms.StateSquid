using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.StateSquid;

namespace StateSquidSample.PageModels
{
    public class MainPageModel : FreshMvvm.FreshBasePageModel
    {
        public State MainState { get; set; }
        public State CurrentState { get; set; }
        public string CustomState { get; set; }
        public State SkeletonState { get; set; }

        public ICommand FullscreenLoadingCommand { get; set; }
        public ICommand SkeletonCommand { get; set; }
        public ICommand RepeatingCommand { get; set; }

        public ICommand CycleStatesCommand { get; set; }

        public MainPageModel()
        {
            FullscreenLoadingCommand = new Command(async (x) =>
            {
                MainState = State.Loading;
                await Task.Delay(2000);
                MainState = State.None;
            });

            SkeletonCommand = new Command(async (x) =>
            {
                SkeletonState = State.Loading;
                await Task.Delay(3000);
                SkeletonState = State.None;
            });

            RepeatingCommand = new Command(async (x) =>
            {
                await CoreMethods.PushPageModel<ListViewPageModel>();
            });

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
            CurrentState = State.Custom;
            CustomState = "ThisIsCustomHi";
            await Task.Delay(3000);
            CustomState = "ThisIsCustomToo";
            await Task.Delay(3000);
            CurrentState = State.Success;
            await Task.Delay(3000);
            CurrentState = State.None;
        }
    }
}
