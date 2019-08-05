using System;

namespace Xamarin.Forms.StateSquid
{
    [ContentProperty(nameof(Template))]
    public class StateDataTemplate
    {
        private int repeatCount = 1;

        public DataTemplate Template { get; set; }

        public int RepeatCount { get => repeatCount; set => repeatCount = value; }
    }
}
