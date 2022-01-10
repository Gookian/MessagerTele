using Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Chat.DesktopClient.Views
{
    public class UIElements
    {
        public static StackPanel ViewTarget { get; set; }

        public static void Show(string message)
        {
            TextBlock textBlockText = new TextBlock();
            textBlockText.Text = message;
            textBlockText.FontSize = 10;
            textBlockText.Padding = new Thickness(0, 0, 0, 5);

            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(textBlockText);

            DropShadowEffect effect = new DropShadowEffect();
            effect.BlurRadius = 20;
            effect.Color = Colors.LightGray;
            effect.ShadowDepth = 0;

            Border border = new Border();
            border.MinWidth = 250;
            border.MaxWidth = 700;
            border.Background = Brushes.White;
            border.Padding = new Thickness(18);
            border.Margin = new Thickness(10);
            border.HorizontalAlignment = HorizontalAlignment.Left;
            border.CornerRadius = new CornerRadius(20);
            border.Effect = effect;
            border.Child = stackPanel;

            ViewTarget.Children.Add(border);
        }

        public static void CreateMessageView(Message message)
        {
            TextBlock textBlockAuthor = new TextBlock();
            textBlockAuthor.Text = $"Автор: {message.Name}";
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
            border.Background = Brushes.White;
            border.Padding = new Thickness(18);
            border.Margin = new Thickness(10);
            //if (RepositoryTarget.User.Id == message.Author.Id)
            border.HorizontalAlignment = HorizontalAlignment.Left;
            border.CornerRadius = new CornerRadius(20);
            border.Effect = effect;
            border.Child = stackPanel;

            ViewTarget.Children.Add(border);
        }
    }
}
