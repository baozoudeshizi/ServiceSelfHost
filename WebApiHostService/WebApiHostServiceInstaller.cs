using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiHostService
{
    [RunInstaller(true)]
    public partial class WebApiHostServiceServiceInstaller : System.Configuration.Install.Installer
    {
        public WebApiHostServiceServiceInstaller()
        {
            InitializeComponent();
        }
    }
}
