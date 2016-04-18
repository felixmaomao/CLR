using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace JDD.Task.Log
{
    [RunInstaller(true)]
    public partial class LogInstaller : System.Configuration.Install.Installer
    {
        public LogInstaller()
        {
            InitializeComponent();
        }
    }
}
