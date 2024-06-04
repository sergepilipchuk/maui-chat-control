using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.CollectionView;
using System.Collections.Specialized;
using System.Diagnostics;

namespace ChatApplication {
    public partial class MainPage : ContentPage {
        ChatViewModel vm;
        public MainPage() {
            InitializeComponent();
            vm = this.BindingContext as ChatViewModel;
            vm.Messages.CollectionChanged += OnMessagesCollectionChanged;
        }
        void OnMessagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            chatSurface.ScrollTo(chatSurface.GetItemHandle(vm.Messages.Count - 1));
        }
        private void MessagesCollectionSizeChanged(object sender, EventArgs e) {
            chatSurface.ScrollTo(chatSurface.GetItemHandleByVisibleIndex(chatSurface.VisibleItemCount - 1));
        }
    }
}
