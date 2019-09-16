using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.StateSquid;

namespace StateSquidSample.PageModels
{
    public class ListViewPageModel : FreshMvvm.FreshBasePageModel
    {  
        public ObservableCollection<Order> Orders { get; set; }
        public State CurrentState { get; set; }
        public ICommand ToggleLoadingCommand { get; set; }

        public ListViewPageModel()
        {
            ToggleLoadingCommand = new Command(async (x) => {
                CurrentState = State.Loading;
                await Task.Delay(2000);
                CurrentState = State.None;
            });

            Orders = new ObservableCollection<Order>
            {
                new Order
                {
                    AmountOfBoxes = 3,
                    OrderNumber = 1072191,
                    AmountOfProducts = 92,
                    DeliveryDate = DateTime.Now.AddDays(30),
                    Price = 389.29m
                },
                new Order
                {
                    AmountOfBoxes = 62,
                    OrderNumber = 664362,
                    AmountOfProducts = 56,
                    DeliveryDate = DateTime.Now.AddDays(23),
                    Price = 430.31m
                },
                new Order
                {
                    AmountOfBoxes = 96,
                    OrderNumber = 329953,
                    AmountOfProducts = 39,
                    DeliveryDate = DateTime.Now.AddDays(12),
                    Price = 59.24m
                }
            };
        }
    }

    public class Order
    {
        public int OrderNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal Price { get; set; }
        public int AmountOfProducts { get; set; }
        public int AmountOfBoxes { get; set; }
    }
}
