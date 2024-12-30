﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UBIOCClass.Views
{
    /// <summary>
    /// WinAlarm.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WinAlarm : Window
    {
        public WinAlarm()
        {
            InitializeComponent();
        }


        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
