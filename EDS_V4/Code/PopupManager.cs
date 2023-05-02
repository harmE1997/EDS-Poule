﻿using System;

namespace EDS_V4
{
    public static class PopupManager
    {
        public static event MessageEventHandler MessageEvent;
        public delegate void MessageEventHandler(string arg);

        public static void OnMessage(string message)
        {
            MessageEvent?.Invoke(message);
        }
    }
}
