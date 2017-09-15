using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Jly.MemberImprot.Behavior
{
    public static class SelectAllWhenGotFocusBehavior
    {
        public static bool GetIsSelectAllWhenGotFocus(Control ctl)
        {
            return (bool)ctl.GetValue(IsSelectAllWhenGotFocusProperty);
        }

        public static void SetIsSelectAllWhenGotFocus(Control ctl, bool value)
        {
            ctl.SetValue(IsSelectAllWhenGotFocusProperty, value);
        }

        public static readonly DependencyProperty IsSelectAllWhenGotFocusProperty =
            DependencyProperty.RegisterAttached(
            "IsSelectAllWhenGotFocus",
            typeof(bool),
            typeof(SelectAllWhenGotFocusBehavior),
            new UIPropertyMetadata(false, OnIsSelectAllWhenGotFocusChanged));

        static void OnIsSelectAllWhenGotFocusChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            Control ctl = depObj as Control;
            if (ctl == null)
                return;

            if (e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
                ctl.GotFocus += OnControlGotFocus;
            else
                ctl.GotFocus -= OnControlGotFocus;
        }

        static void OnControlGotFocus(object sender, RoutedEventArgs e)
        {
            Control ctl = sender as Control;
            if (ctl is TextBox)
                (ctl as TextBox).SelectAll();
            else if (ctl is PasswordBox)
                (ctl as PasswordBox).SelectAll();
        }

    }
}
