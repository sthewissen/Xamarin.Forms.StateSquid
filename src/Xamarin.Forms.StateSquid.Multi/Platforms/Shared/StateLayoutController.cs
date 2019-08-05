using System;
using System.Collections.Generic;

namespace Xamarin.Forms.StateSquid
{
    public class StateLayoutController
    {
        private readonly WeakReference<Layout<View>> _layoutWeakReference;
        private bool _layoutIsGrid = false;
        private IList<View> _originalContent;
        private State _previousState = State.None;

        public StateDataTemplate LoadingTemplate { get; set; }
        public StateDataTemplate SavingTemplate { get; set; }
        public StateDataTemplate EmptyTemplate { get; set; }
        public StateDataTemplate ErrorTemplate { get; set; }
        public StateDataTemplate SuccessTemplate { get; set; }

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

            if (HasTemplateForState(state))
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
                            var view = CreateItemView(layout, state);

                            if (view != null)
                            {
                                stack.Children.Add(view);
                            }
                        }
                    }
                    else
                    {
                        var view = CreateItemView(layout, state);

                        if (view != null)
                        {
                            layout.Children.Add(view);
                        }
                    }
                }
            }
        }

        private bool HasTemplateForState(State state)
        {
            switch (state)
            {
                case State.Loading:
                    return LoadingTemplate != null;
                case State.Saving:
                    return SavingTemplate != null;
                case State.Success:
                    return SuccessTemplate != null;
                case State.Error:
                    return ErrorTemplate != null;
                case State.Empty:
                    return EmptyTemplate != null;
            }

            return false;
        }

        private int GetRepeatCount(State state)
        {
            switch (state)
            {
                case State.Loading:
                    return LoadingTemplate != null ? LoadingTemplate.RepeatCount : 1;
                case State.Saving:
                    return SavingTemplate != null ? SavingTemplate.RepeatCount : 1;
                case State.Success:
                    return SuccessTemplate != null ? SuccessTemplate.RepeatCount : 1;
                case State.Error:
                    return ErrorTemplate != null ? ErrorTemplate.RepeatCount : 1;
                case State.Empty:
                    return EmptyTemplate != null ? EmptyTemplate.RepeatCount : 1;
                case State.None:
                    break;
            }

            return 1;
        }

        /// <summary>
        /// Expand the LoadingDataTemplate or use the template selector.
        /// </summary>
        /// <returns>The item view.</returns>
        View CreateItemView(Layout<View> layout, State state)
        {
            switch (state)
            {
                case State.Loading:
                    return CreateItemView(LoadingTemplate, state);
                case State.Saving:
                    return CreateItemView(SavingTemplate, state);
                case State.Success:
                    return CreateItemView(SuccessTemplate, state);
                case State.Error:
                    return CreateItemView(ErrorTemplate, state);
                case State.Empty:
                    return CreateItemView(EmptyTemplate, state);
                case State.None:
                    break;
            }

            return null;
        }

        /// <summary>
        /// Expand the Loading Data Template.
        /// </summary>
        /// <returns>The item view.</returns>
        /// <param name="dataTemplate">Data template.</param>
        View CreateItemView(StateDataTemplate dataTemplate, State state)
        {
            if (dataTemplate != null)
            {
                var view = (View)dataTemplate.Template.CreateContent();
                return view;
            }
            else
            {
                return new Label() { Text = $"[{state.ToString()}Template] not defined." };
            }
        }
    }
}
