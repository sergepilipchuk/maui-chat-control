using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChatApplication {

    public partial class ChatViewModel : ObservableObject {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SendMessageCommand))]
        string editMessageText;
        public ChatUser Me { get; set; } = new ChatUser() { Name = "Me" };
        public ChatUser Сompanion { get; set; } = new ChatUser() { Name = "Dallas Lou" };
        public ObservableCollection<Message> Messages { get; set; }
        public ObservableCollection<SuggestedAction> SuggestedActions { get; set; }

        Random r = new Random();

        public ChatViewModel() {
            Messages = new ObservableCollection<Message>() {
                new Message() { Text = "Hi John", SentAt = DateTime.Now, Sender = Сompanion },
                new Message() { Text = "I hope you are doing well!", SentAt = DateTime.Now.AddMinutes(5), Sender = Сompanion, IsLastMessage = true },
                new Message() { Text = "Hi Dallas! I was just about to message you! I ran into Stephen earlies today.", SentAt = DateTime.Now.AddMinutes(10), Sender = Me, IsLastMessage = true },
                new Message() { Text = "What did he say? Did he mention the meeting tomorrow?", SentAt = DateTime.Now.AddMinutes(15), Sender = Сompanion, IsLastMessage = true },
                new Message() { Text = "Yes, but he didn't mention specifics. Something about changing the venue for next Saturday.", SentAt = DateTime.Now.AddMinutes(18), Sender = Me },
                new Message() { Text = "I suppose it wasn't available.", SentAt = DateTime.Now.AddMinutes(20), Sender = Me, IsLastMessage = true },
                new Message() { Text = "They mentioned there was an issue with their reservation system and already assigned it to another client.", SentAt = DateTime.Now.AddMinutes(25), Sender = Сompanion, IsLastMessage = true},
            };
            SuggestedActions = new ObservableCollection<SuggestedAction>() {
                new SuggestedAction() { Message = new Message() { Sender = Me, SentAt = DateTime.Now, Text = "Sure" }, Text = "Sure" },
                new SuggestedAction() { Message = new Message() { Sender = Me, SentAt = DateTime.Now, Text = "Great" }, Text = "Great" },
                new SuggestedAction() { Message = new Message() { Sender = Me, SentAt = DateTime.Now, Text = "Thank you" }, Text = "Thank you" },
                new SuggestedAction() { Message = new Message() { Sender = Me, SentAt = DateTime.Now, Text = "My pleasure" }, Text = "My pleasure" }
            };
        }
        public void SubmitMessage(object message) {
            if (message is string messageText) {
                Messages.Add(new Message() { Text = messageText, Sender = Me, SentAt = DateTime.Now });
                EditMessageText = string.Empty;
            }
            else if (message is Message messageObj) {
                messageObj.SentAt = DateTime.Now;
                Messages.Add(messageObj);
            }
        }
        [RelayCommand(CanExecute = nameof(CanSendMessage))]
        void SendMessage(object parameter) {
            if (parameter is SuggestedAction action) {
                SubmitMessage(action.Message);
                return;
            }
            SubmitMessage(EditMessageText);
        }
        bool CanSendMessage(object message) {
            if (message is string messageText)
                return !string.IsNullOrEmpty(messageText) && !string.IsNullOrWhiteSpace(messageText);
            return message != null;
        }
    }

    public class Message {
        public Guid Id { get; } = Guid.NewGuid();
        public ChatUser Sender { get; set; }
        public DateTime? SentAt { get; set; }
        public string Text { get; set; }
        public bool IsLastMessage { get; set; }
    }

    public class ChatUser {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Initials => String.Concat(Name.AsSpan(0, 1), Name.Split(null)[1].AsSpan(0, 1));
    }

    public class SuggestedAction {
        public Message Message { get; set; }
        public string Text { get; set; }
    }
}
