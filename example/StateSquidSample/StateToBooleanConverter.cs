using System;
using System.Globalization;
using Xamarin.Forms;

namespace Xamarin.Forms.StateSquid
{
    public class StateToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is State state && parameter is State stateToCompare)
            {
                return state == stateToCompare;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return State.None;
        }
    }
}
