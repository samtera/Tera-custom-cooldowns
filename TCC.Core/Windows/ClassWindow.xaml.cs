﻿using System.Windows;
using TCC.Data;
using TCC.ViewModels;

namespace TCC.Windows
{
    /// <summary>
    /// Logica di interazione per WarriorBar.xaml
    /// </summary>
    public partial class ClassWindow : TccWindow
    {
        public ClassWindow()
        {
            InitializeComponent();
            ButtonsRef = Buttons;
            MainContentRef = content;
            InitWindow(SettingsManager.ClassWindowSettings, ignoreSize: true);
            if (!SessionManager.Logged) return;
            if (ClassWindowViewModel.Instance.CurrentManager == null)
                ClassWindowViewModel.Instance.CurrentClass = SessionManager.CurrentPlayer.Class;
        }
    }
}
