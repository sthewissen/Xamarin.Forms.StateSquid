using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.StateSquid
{
    public static class StateLayout
    {
        public static readonly BindableProperty LoadingTemplateProperty = BindableProperty.CreateAttached("LoadingTemplate", typeof(StateDataTemplate), typeof(Layout<View>), default(StateDataTemplate), propertyChanged: (b, o, n) => { GetLayoutController(b).LoadingTemplate = (StateDataTemplate)n; });
        public static readonly BindableProperty SavingTemplateProperty = BindableProperty.CreateAttached("SavingTemplate", typeof(StateDataTemplate), typeof(Layout<View>), default(StateDataTemplate), propertyChanged: (b, o, n) => { GetLayoutController(b).SavingTemplate = (StateDataTemplate)n; });
        public static readonly BindableProperty EmptyTemplateProperty = BindableProperty.CreateAttached("EmptyTemplate", typeof(StateDataTemplate), typeof(Layout<View>), default(StateDataTemplate), propertyChanged: (b, o, n) => { GetLayoutController(b).EmptyTemplate = (StateDataTemplate)n; });
        public static readonly BindableProperty ErrorTemplateProperty = BindableProperty.CreateAttached("ErrorTemplate", typeof(StateDataTemplate), typeof(Layout<View>), default(StateDataTemplate), propertyChanged: (b, o, n) => { GetLayoutController(b).ErrorTemplate = (StateDataTemplate)n; });
        public static readonly BindableProperty SuccessTemplateProperty = BindableProperty.CreateAttached("SuccessTemplate", typeof(StateDataTemplate), typeof(Layout<View>), default(StateDataTemplate), propertyChanged: (b, o, n) => { GetLayoutController(b).SuccessTemplate = (StateDataTemplate)n; });

        public static readonly BindableProperty StateProperty = BindableProperty.CreateAttached(nameof(State), typeof(State), typeof(Layout<View>), State.None, propertyChanged: (b, o, n) => OnStateChanged(b, (State)o, (State)n));

        static readonly BindableProperty LayoutControllerProperty = BindableProperty.CreateAttached("LayoutController", typeof(StateLayoutController), typeof(Layout<View>), default(StateLayoutController),
                 defaultValueCreator: (b) => new StateLayoutController((Layout<View>)b),
                 propertyChanged: (b, o, n) => OnControllerChanged(b, (StateLayoutController)o, (StateLayoutController)n));

        public static void SetState(BindableObject b, State value)
        {
            b.SetValue(StateProperty, value);
        }

        public static State GetState(BindableObject b)
        {
            return (State)b.GetValue(StateProperty);
        }

        public static void SetLoadingTemplate(BindableObject b, DataTemplate value)
        {
            b.SetValue(LoadingTemplateProperty, value);
        }

        public static StateDataTemplate GetLoadingTemplate(BindableObject b)
        {
            return (StateDataTemplate)b.GetValue(LoadingTemplateProperty);
        }

        public static void SetSavingTemplate(BindableObject b, DataTemplate value)
        {
            b.SetValue(SavingTemplateProperty, value);
        }

        public static StateDataTemplate GetSavingTemplate(BindableObject b)
        {
            return (StateDataTemplate)b.GetValue(SavingTemplateProperty);
        }

        public static void SetEmptyTemplate(BindableObject b, DataTemplate value)
        {
            b.SetValue(EmptyTemplateProperty, value);
        }

        public static StateDataTemplate GetEmptyTemplate(BindableObject b)
        {
            return (StateDataTemplate)b.GetValue(EmptyTemplateProperty);
        }

        public static void SetErrorTemplate(BindableObject b, DataTemplate value)
        {
            b.SetValue(ErrorTemplateProperty, value);
        }

        public static StateDataTemplate GetErrorTemplate(BindableObject b)
        {
            return (StateDataTemplate)b.GetValue(ErrorTemplateProperty);
        }

        public static void SetSuccessTemplate(BindableObject b, DataTemplate value)
        {
            b.SetValue(SuccessTemplateProperty, value);
        }

        public static StateDataTemplate GetSuccessTemplate(BindableObject b)
        {
            return (StateDataTemplate)b.GetValue(SuccessTemplateProperty);
        }

        static void OnStateChanged(BindableObject bindable, State oldValue, State newValue)
        {
            // Swap out the current children for the Loading Template.
            if (oldValue != newValue && newValue != State.None)
            {
                GetLayoutController(bindable).SwitchToTemplate(newValue);
            }
            else if (oldValue != newValue && newValue == State.None)
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

            newC.LoadingTemplate = GetLoadingTemplate(b);
            newC.SavingTemplate = GetSavingTemplate(b);
            newC.EmptyTemplate = GetEmptyTemplate(b);
            newC.ErrorTemplate = GetErrorTemplate(b);
            newC.SuccessTemplate = GetSuccessTemplate(b);
        }
    }
}
