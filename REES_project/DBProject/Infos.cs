using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBProject
{
    [Table("Infos")]
    public class Infos
    {
        [Key]
        public DateTime Time { get; set; }
        public double BatteryPower { get; set; }
        public double PanelPower { get; set; }
        public double ConsumersPower { get; set; }
        public double UtilityMoney { get; set; }

    }
}
