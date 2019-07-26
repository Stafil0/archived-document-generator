using System;

namespace DocumentGenerator.Document.Template.Element
{
    public abstract class BaseTextElement : BaseElement
    {
        public string format { get; set; }
        public Font font { get; set; }

        protected virtual Layout.Element.BaseElement Generate(Layout.Element.BaseTextElement baseElement, Default defaultValues)
        {
            baseElement = (Layout.Element.BaseTextElement)base.Generate(baseElement, defaultValues);
            string fontName = font != null ? Utils.GetRandomStringFromArray(font.fontFamily) : null;
            baseElement.font = Utils.GetRandomFontFromResources(
                    (fontName != null && Utils.FontExists(fontName)) ? font : defaultValues.font
                );
            baseElement.rotation = Utils.GetRandomTextRotationInRange(rotation);

            return baseElement;
        }
    }
}
