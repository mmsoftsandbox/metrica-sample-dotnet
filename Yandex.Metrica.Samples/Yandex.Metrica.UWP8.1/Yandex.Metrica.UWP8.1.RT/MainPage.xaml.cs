﻿using System;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Yandex.Metrica.UWP8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public Version Version { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public TimeSpan SessionTimeout { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Version = new Version(1, 0, 0, 0);

            YandexMetrica.ReportEvent("MainPage Ctor");

            Loaded += (sender, args) => YandexMetrica.ReportEvent("MainPage Loaded");
            Unloaded += (sender, args) => YandexMetrica.ReportEvent("MainPage Unloaded");
            CrashTrakingCheckBox.Checked += (sender, args) => YandexMetrica.SetReportCrashesEnabled(true);
            CrashTrakingCheckBox.Unchecked += (sender, args) => YandexMetrica.SetReportCrashesEnabled(false);
            LocationTrakingCheckBox.Checked += (sender, args) => YandexMetrica.SetTrackLocationEnabled(true);
            LocationTrakingCheckBox.Unchecked += (sender, args) => YandexMetrica.SetTrackLocationEnabled(false);

            EventButton.Click += (sender, args) => YandexMetrica.ReportEvent(EventNameTextBox.Text);
            CrashButton.Click += (sender, args) => { throw new Exception(); };
            ErrorButton.Click += (sender, args) =>
            {
                try
                {
                    throw new ArgumentException("Throw exception and catch it'");
                }
                catch (Exception ex)
                {
                    YandexMetrica.ReportError(ErrorNameTextBox.Text, ex);
                }
            };

            LibraryVersion.Text = YandexMetrica.LibraryVersion.ToString();

            AppVersionButton.Click += (sender, args) => YandexMetrica.SetCustomAppVersion(Version);
            LocationButton.Click += (sender, args) => YandexMetrica.SetLocation(Latitude, Longitude);
            SessionTimeoutButton.Click += (sender, args) => YandexMetrica.SetSessionTimeout(SessionTimeout);
        }
    }
}