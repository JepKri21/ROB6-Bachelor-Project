using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMCLIB;

namespace Teeeset_program
{
    internal class teeeest
    {
        private XBotCommands _xbotCommand = new XBotCommands();

        public int[] GetXbotIds()
        {
            XBotIDs tempId = _xbotCommand.GetXBotIDS();

            int[] xbotIds = tempId.XBotIDsArray;

            return xbotIds;

        }

        public void get_position()
        {
            int[] xbotIdArray = GetXbotIds();

            _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, 0.2, 0.2, 0, 0.05, 0.1);
            _xbotCommand.LinearMotionSI(0, xbotIdArray[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, 0.34, 0.2, 0, 0.05, 0.1);

        }
        public void moving_left()
        {
            int[] xbotIdArray = GetXbotIds();
            _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, -0.01, 0, 0, 0.05, 0.1);
        }

        public void moving_right()
        {
            int[] xbotIdArray = GetXbotIds();
            _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, 0.01, 0, 0, 0.05, 0.1);
        }

        public void rotation_around_own_axis()
        {
            int[] xbotIdArray = GetXbotIds();
            _xbotCommand.ShortAxesMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, 0.002, 0, 0, 4, 0.1, 0, 0, 0.3);
        }
    }
}
