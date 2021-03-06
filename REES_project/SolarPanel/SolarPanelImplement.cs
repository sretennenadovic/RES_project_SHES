﻿using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarPanel
{
    public class SolarPanelImplement : ISolarPanel
    {
        public void listSolarPanels(Dictionary<string, double> panels, bool ready)
        {
            if(panels.Count == 0)
            {
                throw new ArgumentException("Dictionary must have values!");
            }

            Program.panels = panels;
            Program.ready = ready;
        }
    }
}
