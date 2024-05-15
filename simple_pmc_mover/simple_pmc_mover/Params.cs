using PMCLIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_pmc_mover
{
    internal class Params
    {
        private static SystemCommands _systemCommand = new SystemCommands();
        //this class contains a collection of xbot commands, such as discover xbots, mobility control, linear motion, etc.
        private static XBotCommands _xbotCommand = new XBotCommands();


        public DataTable table1 { get; set; }
        public DataTable table2 { get ; set;}

        public XBotStatus status { get; set; }
        public int[] xbots_to_sample {  get; set; }
        public double[,] positions {  get; set; }

    }
}
