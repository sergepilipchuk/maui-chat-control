using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication {
    public class MessageTemplateSelector : DataTemplateSelector, IMarkupExtension {
        public DataTemplate SenderTemplate { get; set; }
        public DataTemplate RecipientTemplate { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider) => this;

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            return item is Message message ? message.Sender.Name == "Me" ? SenderTemplate : RecipientTemplate : null;
        }
    }
}
