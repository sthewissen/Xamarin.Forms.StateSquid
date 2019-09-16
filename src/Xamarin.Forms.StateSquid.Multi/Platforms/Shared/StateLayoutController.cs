using System;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Forms.StateSquid
{
    public class StateLayoutController
    {
        private readonly WeakReference<Layout<View>> _layoutWeakReference;
        private bool _layoutIsGrid = false;
        private IList<View> _originalContent;
        private State _previousState = State.None;

        public IList<StateView> StateViews { get; set; }

        public StateLayoutController(Layout<View> layout)
        {
            _layoutWeakReference = new WeakReference<Layout<View>>(layout);
        }

        public void SwitchToContent()
        {
            Layout<View> layout;

            if (!_layoutWeakReference.TryGetTarget(out layout))
            {
                return;
            }

            _previousState = State.None;

            // Put the original content back in.
            layout.Children.Clear();

            foreach (var item in _originalContent)
            {
                layout.Children.Add(item);
            }
        }

        public void SwitchToTemplate(string customState)
        {
            SwitchToTemplate(State.Custom, customState);
        }

        public void SwitchToTemplate(State state, string customState)
        {
            Layout<View> layout;

            if (!_layoutWeakReference.TryGetTarget(out layout))
            {
                return;
            }

            // Put the original content somewhere where we can restore it.
            if (_previousState == State.None)
            {
                _originalContent = new List<View>();

                foreach (var item in layout.Children)
                    _originalContent.Add(item);
            }

            if (HasTemplateForState(state, customState))
            {
                _previousState = state;

                // Add the loading template.
                layout.Children.Clear();

                var repeatCount = GetRepeatCount(state, customState);

                if (layout is Grid grid)
                {
                    var s = new StackLayout();

                    if(grid.RowDefinitions.Any())
                        Grid.SetRowSpan(s, grid.RowDefinitions.Count);

                    if(grid.ColumnDefinitions.Any())
                        Grid.SetColumnSpan(s, grid.ColumnDefinitions.Count);

                    layout.Children.Add(s);

                    _layoutIsGrid = true;
                }

                for (int i = 0; i < repeatCount; i++)
                {
                    if (_layoutIsGrid)
                    {
                        if (layout.Children[0] is StackLayout stack)
                        {
                            var view = CreateItemView(state, customState);

                            if (view != null)
                            {
                                stack.Children.Add(view);
                            }
                        }
                    }
                    else
                    {
                        var view = CreateItemView(state, customState);

                        if (view != null)
                        {
                            layout.Children.Add(view);
                        }
                    }
                }
            }
        }

        private bool HasTemplateForState(State state, string customState)
        {
            var template = StateViews.FirstOrDefault(x => (x.StateKey == state && state != State.Custom) ||
                            (state == State.Custom && x.CustomStateKey == customState));

            return template != null;
        }

        private int GetRepeatCount(State state, string customState)
        {
            var template = StateViews.FirstOrDefault(x => (x.StateKey == state && state != State.Custom) ||
                           (state == State.Custom && x.CustomStateKey == customState));

            if (template != null)
            {
                return template.RepeatCount;
            }

            return 1;
        }

        View CreateItemView(State state, string customState)
        {
            var template = StateViews.FirstOrDefault(x => (x.StateKey == state && state != State.Custom) ||
                            (state == State.Custom && x.CustomStateKey == customState));

            if (template != null)
            {
                return template.Content;
            }
            else
            {
                return new Label() { Text = $"Template for {state.ToString()}{customState} not defined." };
            }
        }
    }
}
