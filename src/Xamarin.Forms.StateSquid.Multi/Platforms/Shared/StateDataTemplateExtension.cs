using System;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.StateSquid
{
    [ContentProperty(nameof(TypeName))]
    // TODO: This is in the default datatemplate extension, do I need it? What does it do?
    // [ProvideCompiled("Xamarin.Forms.Build.Tasks.StateDataTemplateExtension")]
    public sealed class StateDataTemplateExtension : IMarkupExtension<StateDataTemplate>
    {
        public string TypeName { get; set; }

        public StateDataTemplate ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));
            if (!(serviceProvider.GetService(typeof(IXamlTypeResolver)) is IXamlTypeResolver typeResolver))
                throw new ArgumentException("No IXamlTypeResolver in IServiceProvider");
            if (string.IsNullOrEmpty(TypeName))
            {
                var li = (serviceProvider.GetService(typeof(IXmlLineInfoProvider)) is IXmlLineInfoProvider lip) ? lip.XmlLineInfo : new XmlLineInfo();
                throw new XamlParseException("TypeName isn't set.", li);
            }

            if (typeResolver.TryResolve(TypeName, out var type))
                return new StateDataTemplate(type);

            var lineInfo = (serviceProvider.GetService(typeof(IXmlLineInfoProvider)) is IXmlLineInfoProvider lineInfoProvider) ? lineInfoProvider.XmlLineInfo : new XmlLineInfo();
            throw new XamlParseException($"DataTemplateExtension: Could not locate type for {TypeName}.", lineInfo);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<StateDataTemplate>).ProvideValue(serviceProvider);
        }
    }
}
