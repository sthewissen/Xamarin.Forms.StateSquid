using System;

namespace Xamarin.Forms.StateSquid
{
    public class StateDataTemplate : DataTemplate
    {
        private int repeatCount = 1;

        public State State { get; set; }
        public string CustomState { get; set; }
        public int RepeatCount { get => repeatCount; set => repeatCount = value; }
    }
}
