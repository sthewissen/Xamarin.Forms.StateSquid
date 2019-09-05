using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.StateSquid
{
    public static class StateLayout
    {
        internal static readonly BindablePropertyKey StateTemplatesPropertyKey = BindableProperty.CreateReadOnly("StateTemplates", typeof(IList<StateDataTemplate>), typeof(Layout<View>), default(IList<StateDataTemplate>),
            defaultValueCreator: bindable =>
            {
                var collection = new List<StateDataTemplate>();
                return collection;
            });

        public static readonly BindableProperty StateTemplatesProperty = StateTemplatesPropertyKey.BindableProperty;

        static readonly BindableProperty LayoutControllerProperty = BindableProperty.CreateAttached("LayoutController", typeof(StateLayoutController), typeof(Layout<View>), default(StateLayoutController),
                 defaultValueCreator: (b) => new StateLayoutController((Layout<View>)b),
                 propertyChanged: (b, o, n) => OnControllerChanged(b, (StateLayoutController)o, (StateLayoutController)n));

        public static readonly BindableProperty CurrentStateProperty = BindableProperty.CreateAttached("CurrentState", typeof(State), typeof(Layout<View>), State.None, propertyChanged: (b, o, n) => OnCurrentStateChanged(b, (State)o, (State)n));
        public static readonly BindableProperty CurrentCustomStateKeyProperty = BindableProperty.CreateAttached("CurrentCustomStateKey", typeof(string), typeof(Layout<View>), State.None, propertyChanged: (b, o, n) => OnCurrentCustomStateKeyChanged(b, (string)o, (string)n));
        
        public static IList<StateDataTemplate> GetStateTemplates(BindableObject b)
        {
           return (IList<StateDataTemplate>)b.GetValue(StateTemplatesProperty);
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
    
        static void OnCurrentStateChanged(BindableObject bindable, State oldValue, State newValue)
        {
            // Swap out the current children for the Loading Template.
            if (oldValue != newValue && newValue != State.None && newValue != State.Custom)
            {
                GetLayoutController(bindable).SwitchToTemplate(newValue);
            }
            else if (oldValue != newValue && newValue == State.None)
            {
                GetLayoutController(bindable).SwitchToContent();
            }
        }

        static void OnCurrentCustomStateKeyChanged(BindableObject bindable, string oldValue, string newValue)
        {
            var state = GetCurrentState(bindable);

            // Swap out the current children for the Loading Template.
            if (oldValue != newValue && state == State.Custom)
            {
                GetLayoutController(bindable).SwitchToTemplate(newValue);
            }
            else if (oldValue != newValue && state == State.None)
            {
                GetLayoutController(bindable).SwitchToContent();
            }
        }

        static StateLayoutController GetLayoutController(BindableObject b)
        {
            return (StateLayoutController)b.GetValue(LayoutControllerProperty);
        }

        static void SetLayoutController(BindableObject b, StateLayoutController value)
        {
            b.SetValue(LayoutControllerProperty, value);
        }

        static void OnControllerChanged(BindableObject b, StateLayoutController oldC, StateLayoutController newC)
        {
            if (newC == null)
            {
                return;
            }

            newC.StateTemplates = GetStateTemplates(b);
        }
    }
}
