namespace Chat.DesktopClient.ViewModels
{
    using Chat.DesktopClient.DAL;
    using Chat.DesktopClient.Data;
    using Chat.DesktopClient.Views;
    using Core;
    using Core.Managers;
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using System.Windows;
    using static Core.Managers.LoggerManager;

    public class AuthorizationWindowViewModel : BindableBase
    {
        private LoggerManager loggerManager = new LoggerManager("logFileClient.txt", LogLeavel.Debug, LogLeavel.Fatal);

        private readonly MainWindow mainWindow = new MainWindow();

        public string Title { get; set; } = "Desktop Chat Client";

        public string Login { get; set; }

        public string Password { get; set; }

        public DelegateCommand AuthorizationCommand { get; private set; }

        public DelegateCommand RegistrationCommand { get; private set; }

        public AuthorizationWindowViewModel()
        {
            AuthorizationCommand = new DelegateCommand(Authorization);
            RegistrationCommand = new DelegateCommand(Registration);
        }

        private void Authorization()
        {
            bool isAuthenticated = false;

            foreach (User user in DatabaseRepository.GetAllUsers())
            {
                if (user.Name == Login && user.Password == Password)
                {
                    SessionRepository.Initialize(user);
                    SessionRepository.AuthorizationWindow.Close();
                    isAuthenticated = true;

                    loggerManager.logger.Info($"Пользователь [{user.Name}] авторизировался");

                    break;
                }
            }

            if (isAuthenticated)
            {
                mainWindow.Show();
            }
            else
            {
                loggerManager.logger.Error($"Ошибка авторизации");
                MessageBox.Show("Пользователя с таким логином не существует!");
            }
        }

        private void Registration()
        {
            bool isUserExists = false;

            foreach (User user in DatabaseRepository.GetAllUsers())
            {
                if (user.Name == Login)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                    isUserExists = true;
                    break;
                }
            }

            if (!isUserExists)
            {
                User newUser = new User()
                {
                    Id = Guid.NewGuid(),
                    Name = Login,
                    Password = Password
                };

                DatabaseRepository.Add(newUser);

                SessionRepository.Initialize(newUser);
                SessionRepository.AuthorizationWindow.Close();

                loggerManager.logger.Info($"Пользователь [{newUser.Name}] зарегистрирован");

                mainWindow.Show();
            }
        }
    }
}
