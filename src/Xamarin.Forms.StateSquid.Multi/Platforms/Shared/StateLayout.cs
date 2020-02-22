using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.StateSquid
{
    public static class StateLayout
    {
        internal static readonly BindablePropertyKey StateViewsPropertyKey = BindableProperty.CreateReadOnly("StateViews", typeof(IList<StateView>), typeof(Layout<View>), default(IList<StateView>),
            defaultValueCreator: bindable =>
            {
                var collection = new List<StateView>();
                return collection;
            });

        public static readonly BindableProperty StateViewsProperty = StateViewsPropertyKey.BindableProperty;

        static readonly BindableProperty LayoutControllerProperty = BindableProperty.CreateAttached("LayoutController", typeof(StateLayoutController), typeof(Layout<View>), default(StateLayoutController),
                 defaultValueCreator: (b) => new StateLayoutController((Layout<View>)b) { StateViews = GetStateViews(b) });

        public static readonly BindableProperty CurrentStateProperty = BindableProperty.CreateAttached("CurrentState", typeof(State), typeof(Layout<View>), default(State), propertyChanged: (b, o, n) => OnCurrentStateChanged(b, (State)o, (State)n));
        public static readonly BindableProperty CurrentCustomStateKeyProperty = BindableProperty.CreateAttached("CurrentCustomStateKey", typeof(string), typeof(Layout<View>), default(string), propertyChanged: (b, o, n) => OnCurrentCustomStateKeyChanged(b, (string)o, (string)n));
        public static readonly BindableProperty AnimateProperty = BindableProperty.CreateAttached("Animate", typeof(bool), typeof(Layout<View>), true, propertyChanged: (b, o, n) =>  ((BindableObject)b).SetValue(AnimateProperty, n));

        public static IList<StateView> GetStateViews(BindableObject b)
        {
           return (IList<StateView>)b.GetValue(StateViewsProperty);
        }

        public static void SetCurrentState(BindableObject b, State value)
        {
            b.SetValue(CurrentStateProperty, value);
        }

        public static State GetCurrentState(BindableObject b)
        {
            return (State)b.GetValue(CurrentStateProperty);
        }

        public static void SetCurrentCustomStateKey(BindableObject b, State value)
        {
            b.SetValue(CurrentCustomStateKeyProperty, value);
        }
       

        public static string GetCurrentCustomStateKey(BindableObject b)
        {
            return (string)b.GetValue(CurrentCustomStateKeyProperty);
        }

        public static void SetAnimate(BindableObject b, bool value)
        {
            b.SetValue(AnimateProperty, value);
        }

        public static bool GetAnimate(BindableObject b)
        {
            return (bool?)b.GetValue(AnimateProperty) ?? false;
        }

        static void OnCurrentStateChanged(BindableObject bindable, State oldValue, State newValue)
        {
            // Swap out the current children for the Loading Template.
            if (oldValue != newValue && newValue != State.None && newValue != State.Custom)
            {
                GetLayoutController(bindable).SwitchToTemplate(newValue, null, GetAnimate(bindable));
            }
            else if (oldValue != newValue && newValue == State.None)
            {
                GetLayoutController(bindable).SwitchToContent(GetAnimate(bindable));
            }
        }

        static void OnCurrentCustomStateKeyChanged(BindableObject bindable, string oldValue, string newValue)
        {
            var state = GetCurrentState(bindable);

            // Swap out the current children for the Loading Template.
            if (oldValue != newValue && state == State.Custom)
            {
                GetLayoutController(bindable).SwitchToTemplate(newValue, GetAnimate(bindable));
            }
            else if (oldValue != newValue && state == State.None)
            {
                GetLayoutController(bindable).SwitchToContent(GetAnimate(bindable));
            }
        }

        static StateLayoutController GetLayoutController(BindableObject b)
        {
            return (StateLayoutController)b.GetValue(LayoutControllerProperty);
        }

        
    }
}
