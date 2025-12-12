using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Productivitism.Model;

namespace Productivitism.View
{
    internal partial class MainViewModel : ObservableObject
    {

        private DispatcherTimer timer;
        private DispatcherTimer btimer;

        public MainViewModel() 
        {
            CurrentPage = new WelcomePage();

            (TimeForABreak, AllTimeSpent, Tcoins) = Saves.Init();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (s, e) =>
            {
                if (SessionTimeRemaining > TimeSpan.Zero)
                    SessionTimeRemaining -= TimeSpan.FromSeconds(1);
                else
                {
                    timer.Stop();
                    ClaimButton = true;
                }
            };

            // Set initial remaining time
            SessionTimeRemaining = SessionTime;

            //For Break Timer

            btimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            btimer.Tick += (s, e) =>
            {
                if (TimeForABreak > TimeSpan.Zero)
                    TimeForABreak -= TimeSpan.FromSeconds(1);
                else
                {
                    btimer.Stop();
                }
            };



        }

        [ObservableProperty]
        private UserControl currentPage;

        [ObservableProperty]
        private TimeSpan allTimeSpent;

        [ObservableProperty]
        private TimeSpan timeForABreak;

        [ObservableProperty]
        private int tcoins;

        [RelayCommand]
        private void OpenStartSession()
        {
            var page = new StartSession();
            page.DataContext = this; 
            CurrentPage = page;
        }


        [ObservableProperty]
        private TimeSpan sessionTime = TimeSpan.FromMinutes(20);

        [ObservableProperty]
        private TimeSpan sessionTimeRemaining;

        [ObservableProperty]
        private bool goodMood;

        [ObservableProperty]
        private bool midMood = true;

        [ObservableProperty]
        private bool badMood;


        [RelayCommand]
        public void StartTimer()
        {
            var page = new SessionTimer();
            page.DataContext = this;
            CurrentPage = page;

            SessionTimeRemaining = SessionTime;

            timer.Start();
            Saves.SaveData(TimeForABreak, allTimeSpent, Tcoins);
        }

        [ObservableProperty]
        private bool claimButton = false;

        [RelayCommand]
        private void ClaimReward()
        {
            ClaimButton = false;
            CurrentPage = new WelcomePage();

            Tcoins = Algos.CalculateTcoins(SessionTime, GoodMood, MidMood, BadMood);

            AllTimeSpent += SessionTime;
            Saves.SaveData(TimeForABreak, allTimeSpent, Tcoins);

        }

        [RelayCommand]
        private void OpenShop()
        {
            var page = new Shop();
            page.DataContext = this;
            CurrentPage = page;
            Saves.SaveData(TimeForABreak, allTimeSpent, Tcoins);
        }

        //SHOP COMMANDS
        [RelayCommand]
        private void Purchase1() 
        { 
            if (Tcoins >= 5)
            {
                Tcoins -= 5;
                TimeForABreak = TimeForABreak.Add(TimeSpan.FromMinutes(10));
                Saves.SaveData(TimeForABreak, allTimeSpent, Tcoins);
            }

        }

        [RelayCommand]
        private void Purchase2()
        {
            if (Tcoins >= 25)
            {
                Tcoins -= 25;
                TimeForABreak = TimeForABreak.Add(TimeSpan.FromMinutes(30));
                Saves.SaveData(TimeForABreak, allTimeSpent, Tcoins);
            }

        }

        [RelayCommand]
        private void Purchase3()
        {
            if (Tcoins >= 40)
            {
                Tcoins -= 40;
                TimeForABreak = TimeForABreak.Add(TimeSpan.FromMinutes(60));
                Saves.SaveData(TimeForABreak, allTimeSpent, Tcoins);
            }

        }

        [RelayCommand]
        private void Purchase4()
        {
            if (Tcoins >= 70)
            {
                Tcoins -= 70;
                TimeForABreak = TimeForABreak.Add(TimeSpan.FromMinutes(120));
                Saves.SaveData(TimeForABreak, allTimeSpent, Tcoins);
            }

        }

        [RelayCommand]
        private void OpenBreakTimer()
        {
            var page = new BreakTimer();
            page.DataContext = this;
            CurrentPage = page;
            Saves.SaveData(TimeForABreak, allTimeSpent, Tcoins);


        }

        [RelayCommand]
        private void StartBreakTimer()
        {
            btimer.Start();
            Saves.SaveData(TimeForABreak, allTimeSpent, Tcoins);
        }

        [RelayCommand]
        private void StopBreakTimer()
        {
            btimer.Stop();
            Saves.SaveData(TimeForABreak, allTimeSpent, Tcoins);
        }

        [RelayCommand]
        private void OpenSetting()
        {
            var page = new Settings();
            page.DataContext= this;
            CurrentPage = page;
        }

        [RelayCommand]
        private void DeleteData()
        {
            TData data = new TData();
            data.TimeForABreak = "00:00:00";
            data.AllTimeSpended = "00:00:00";
            data.Tcoins = 0;

            string rawdata = File.ReadAllText(@"K:\Repo For VS\Projects\Productivitism\Productivitism\TimerData.json");
            string newdata = JsonSerializer.Serialize(data);
            File.WriteAllText(@"K:\Repo For VS\Projects\Productivitism\Productivitism\TimerData.json", newdata);
            (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.Shutdown();
        }

        [RelayCommand]
        private void OpenStatistics()
        {
            var page = new Statistics();
            page.DataContext = this;
            CurrentPage = page;


        }



    }
}
