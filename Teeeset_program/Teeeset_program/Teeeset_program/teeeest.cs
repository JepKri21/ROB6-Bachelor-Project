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
        public void moving()
        {
            int[] xbotIdArray = GetXbotIds();

            _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, 0.2, 0.2, 0, 0.05, 0.1);
            _xbotCommand.LinearMotionSI(0, xbotIdArray[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, 0.24, 0.2, 0, 0.05, 0.1);

            _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, -0.04, 0, 0, 0.05, 0.1);
        }
        // 6 cm
    }
}
