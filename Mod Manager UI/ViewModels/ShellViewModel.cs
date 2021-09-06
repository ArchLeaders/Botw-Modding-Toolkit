using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private Color _background = Color.Black;

        public Color Background
        {
            get 
            { 
                return _background; 
            }
            set 
            {
                _background = value;
                NotifyOfPropertyChange(() => Background);
            }
        }


    }
}
