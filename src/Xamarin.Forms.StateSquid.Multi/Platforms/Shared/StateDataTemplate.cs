using System;

namespace Xamarin.Forms.StateSquid
{
    public class StateDataTemplate : DataTemplate
    {
        private int repeatCount = 1;

        public State State { get; set; }
        public string CustomStateKey { get; set; }
        public int RepeatCount { get => repeatCount; set => repeatCount = value; }

        public StateDataTemplate()
        {
        }

        public StateDataTemplate(Func<object> loadTemplate) : base(loadTemplate)
        {
        }

        public StateDataTemplate(Type type) : base(type)
        {
        }
    }
}
