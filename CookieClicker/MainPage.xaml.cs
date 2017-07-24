using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CookieClicker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private long count;

        private long mult;

        private long multCost;

        private long cursors;

        private decimal cps;

        private decimal cursorCost;

        private string _cps;

        private string _cursorCost;

        private string _multCost;

        private string _mult;

        private string _cookieCount;

        public event PropertyChangedEventHandler PropertyChanged;

        public string CookieCount
        {
            get { return _cookieCount; }
            set
            {
                _cookieCount = value;
                NotifyPropertyChanged("CookieCount");
            }
        }

        public string Mult
        {
            get { return _mult; }
            set
            {
                _mult = value;
                NotifyPropertyChanged("Mult");
            }
        }

        public string MultCost
        {
            get { return _multCost; }
            set
            {
                _multCost = value;
                NotifyPropertyChanged("MultCost");
            }
        }

        public string CursorCost
        {
            get { return _cursorCost; }
            set
            {
                _cursorCost = value;
                NotifyPropertyChanged("CursorCost");
            }
        }

        public string CPS
        {
            get { return _cps; }
            set
            {
                _cps = value;
                NotifyPropertyChanged("CPS");
            }
        }

        private DispatcherTimer Timer;

        private void NotifyPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            cursors = 0;
            count = 0;
            mult = 1;
            multCost = 20;
            cursorCost = 15;
            cps = 0;
            CursorCost = $"Cursor Cost: {Math.Ceiling(cursorCost)}";
            MultCost = $"Upgrade Cost: {multCost}";
            Mult = $"x{mult}";
            CookieCount = $"{count} cookies";
            DataContext = this;
            MultiplierCost.IsEnabled = false;
            Cursor.IsEnabled = false;
            Timer = new DispatcherTimer();
            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            count += 1;
            CookieCount = $"{count} cookies";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            count += 1 * mult;
            CookieCount = $"{count} cookies";
            if (count >= multCost)
            {
                MultiplierCost.IsEnabled = true;
            }
            if (count >= cursorCost)
            {
                Cursor.IsEnabled = true;
            }
        }

        private void Mult_Click(object sender, RoutedEventArgs e)
        {
            count -= multCost;
            CookieCount = $"{count} cookies";
            multCost *= 4;
            MultCost = $"Upgrade Cost: {multCost}";
            mult *= 2;
            Mult = $"x{mult}";
            if (count <= multCost)
            {
                MultiplierCost.IsEnabled = false;
            }
            if (count <= cursorCost)
            {
                Cursor.IsEnabled = false;
            }
        }

        private void Cursor_Click(object sender, RoutedEventArgs e)
        {
            cursors++;
            count -= Convert.ToInt64(Math.Ceiling(cursorCost));
            CookieCount = $"{count} cookies";
            cursorCost = Decimal.Multiply(cursorCost, (decimal) 1.15);
            CursorCost = $"Cursor Cost: {Math.Ceiling(cursorCost)}";
            cps += (decimal) 0.1;
            CPS = $"per second: {cps}";
            Timer.Interval = new TimeSpan(Convert.ToInt64(10000000 / cps));
            if (cursors == 1)
            {
                Timer.Start();
            }
            if (count <= cursorCost)
            {
                Cursor.IsEnabled = false;
            }
            if (count <= multCost)
            {
                MultiplierCost.IsEnabled = false;
            }
        }
    }
}
