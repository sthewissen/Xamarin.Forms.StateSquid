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

        public IList<StateDataTemplate> StateTemplates { get; set; }

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
        }

        public void SwitchToTemplate(State state)
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

            if (HasTemplateForState(state, null))
            {
                _previousState = state;

                // Add the loading template.
                layout.Children.Clear();

                var repeatCount = GetRepeatCount(state);

                if (layout is Grid)
                {
                    layout.Children.Add(new StackLayout());
                    _layoutIsGrid = true;
                }
                for (int i = 0; i < repeatCount; i++)
                {
                    if (_layoutIsGrid)
                    {
                        if (layout.Children[0] is StackLayout stack)
                        {
                            var view = CreateItemView(layout, state, null);

                            if (view != null)
                            {
                                stack.Children.Add(view);
                            }
                        }
                    }
                    else
                    {
                        var view = CreateItemView(layout, state, null);

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
            var template = StateTemplates.FirstOrDefault(x => x.State == state ||
                            (state == State.Custom && x.CustomState == customState));

            return template != null;
        }

        private int GetRepeatCount(State state)
        {
            var template = StateTemplates.FirstOrDefault(x => x.State == state);

            if (template != null)
            {
                return template.RepeatCount;
            }

            return 1;
        }

        /// <summary>
        /// Expand the LoadingDataTemplate or use the template selector.
        /// </summary>
        /// <returns>The item view.</returns>
        View CreateItemView(Layout<View> layout, State state, string customState)
        {
            var template = StateTemplates.FirstOrDefault(x => x.State == state ||
                            (state == State.Custom && x.CustomState == customState));

            if(template != null)
            {
                CreateItemView(template, state, customState);
            }

            return null;
        }

        /// <summary>
        /// Expand the Loading Data Template.
        /// </summary>
        /// <returns>The item view.</returns>
        View CreateItemView(StateDataTemplate dataTemplate, State state, string customState)
        {
            if (dataTemplate != null)
            {
                var view = (View)dataTemplate.CreateContent();
                return view;
            }
            else
            {
                return new Label() { Text = $"Template for {state.ToString()}{customState} not defined." };
            }
        }
    }
}
