﻿using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.ViewModel
{
    public class NotificationViewModel : PropertyBase
    {
        private string _notificationText;
        public string NotificationText
        {
            get { return _notificationText; }
            set
            {
                _notificationText = value;
                OnPropertyChanged(nameof(NotificationText));
            }
        }
        public void SetNotificationText(string message)
        {
            NotificationText = message;
        }
    }
}
