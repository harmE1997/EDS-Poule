﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_V4.ViewModels
{
    public class scrPlayersVm
    {
        public void NewPlayerCommand()
        { 
            Views.TotoForm totoForm = new Views.TotoForm();
            totoForm.Show();
        }
    }
}