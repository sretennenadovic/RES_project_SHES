﻿using Contracts.UtilityConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class UtilityImplement : IUtility
    {
        public double CalculateMoney(double energy)
        {
            //cenu nije definisana pa smo nasumicno odabrali 
            return energy * 10;
        }
    }
}
