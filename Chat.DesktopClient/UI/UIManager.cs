using Chat.DesktopClient.Data;
using Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace Chat.DesktopClient.Views
{
    public class UIManager
    {
        public static StackPanel ViewTargetMessages { get; set; }
        public static StackPanel ViewTargetUsers { get; set; }
        public static StackPanel ViewTargetUser { get; set; }
        public static ScrollViewer ScrollViewChat { get; set; }

        public static void CreateUserView(User user, bool isUser = false)
        {
            ImageBrush imBrush = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/user_skin.jpg"))
            };

            Border borderAvatar = new Border();
            borderAvatar.Width = 40;
            borderAvatar.Height = 40;
            borderAvatar.Background = imBrush;
            borderAvatar.Padding = new Thickness(18);
            borderAvatar.CornerRadius = new CornerRadius(20);

            //#FF79DC71
            Border borderOnline = new Border();
            borderOnline.Width = 10;
            borderOnline.Height = 10;
            borderOnline.Background = new SolidColorBrush(Color.FromRgb(162, 162, 162)); ;
            borderOnline.Margin = new Thickness(-11, 0, 0, -28);
            borderOnline.CornerRadius = new CornerRadius(10);

            TextBlock textBlockText = new TextBlock();
            textBlockText.Text = user.Name;
            textBlockText.FontSize = 16;
            textBlockText.Padding = new Thickness(10, 0, 0, 0);
            textBlockText.VerticalAlignment = VerticalAlignment.Center;
            if (isUser)
                textBlockText.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Children.Add(borderAvatar);
            stackPanel.Children.Add(borderOnline);
            stackPanel.Children.Add(textBlockText);

            Border borderBox = new Border();
            borderBox.Width = 210;
            borderBox.Padding = new Thickness(5);
            borderBox.Margin = new Thickness(0, 5, 0, 5);
            borderBox.HorizontalAlignment = HorizontalAlignment.Left;
            borderBox.CornerRadius = new CornerRadius(20);
            borderBox.Child = stackPanel;

            if (isUser)
                ViewTargetUser.Children.Add(borderBox);
            else
                ViewTargetUsers.Children.Add(borderBox);
        }

        public static void CreateMessageView(Message message)
        {
            TextBlock textBlockAuthor = new TextBlock();
            textBlockAuthor.Text = $"{message.Name}";
            textBlockAuthor.FontSize = 16;
            textBlockAuthor.FontWeight = FontWeights.Bold;
            textBlockAuthor.Padding = new Thickness(0, 0, 0, 5);

            TextBlock textBlockText = new TextBlock();
            textBlockText.Text = message.Text;
            textBlockText.FontSize = 16;
            textBlockText.Padding = new Thickness(0, 0, 0, 5);

            TextBlock textBlockTime = new TextBlock();
            textBlockTime.Text = message.Time.ToString("HH:mm");
            textBlockTime.FontSize = 16;
            textBlockTime.HorizontalAlignment = HorizontalAlignment.Right;

            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(textBlockAuthor);
            stackPanel.Children.Add(textBlockText);
            stackPanel.Children.Add(textBlockTime);

            DropShadowEffect effect = new DropShadowEffect();
            effect.BlurRadius = 20;
            effect.Color = Colors.LightGray;
            effect.ShadowDepth = 0;

            Border border = new Border();
            border.MinWidth = 250;
            border.MaxWidth = 400;
            if (SessionRepository.targetUser.Id == message.SenderId)
                border.Background = new SolidColorBrush(Color.FromRgb(171, 221, 255));
            else
                border.Background = Brushes.White;
            border.Padding = new Thickness(18);
            border.Margin = new Thickness(10);
            if (SessionRepository.targetUser.Id == message.SenderId)
                border.HorizontalAlignment = HorizontalAlignment.Right;
            else
                border.HorizontalAlignment = HorizontalAlignment.Left;
            border.CornerRadius = new CornerRadius(20);
            border.Effect = effect;
            border.Child = stackPanel;

            ViewTargetMessages.Children.Add(border);

            ScrollViewChat.ScrollToEnd();
        }
    }
}
