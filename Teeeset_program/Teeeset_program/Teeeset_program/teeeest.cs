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
            _xbotCommand.LinearMotionSI(0, xbotIdArray[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, 0.32, 0.2, 0, 0.05, 0.1);

        }
        public void moving_left()
        {
            int[] xbotIdArray = GetXbotIds();
            _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, -0.005, 0, 0, 0.05, 0.1);
        }

        public void moving_right()
        {
            int[] xbotIdArray = GetXbotIds();
            _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, 0.005, 0, 0, 0.05, 0.1);
        }

        public void rotation_around_own_axis()
        {
            int[] xbotIdArray = GetXbotIds();
            _xbotCommand.ShortAxesMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, 0.002, 0, 0, 4, 0.1, 0, 0, 0.3);
        }

        public void full_motion()
        {
            int[] xbotIdArray = GetXbotIds();
            _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, -0.05, 0, 0, 0.01, 0.05);
            _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, 0.05, 0, 0, 0.01, 0.05);

        }

        public void move_Lifting()
        {
            int[] xbotIdArray = GetXbotIds();
            int[] xbotPlanets = { 3 };
            WaitUntilTriggerParams TriggerParams = new WaitUntilTriggerParams();
            WaitUntilTriggerParams TriggerParams2 = new WaitUntilTriggerParams();
            TriggerParams.delaySecs = 1;
            TriggerParams2.delaySecs = 3;
            // _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, -0.05, 0, 0, 0.01, 0.05);
            //_xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, 0.05, 0, 0, 0.01, 0.05);
            _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, -0.04, 0, 0, 0.02, 0.05);


            _xbotCommand.WaitUntil(0, xbotIdArray[0], TRIGGERSOURCE.TIME_DELAY, TriggerParams);
            _xbotCommand.WaitUntil(0, xbotIdArray[1], TRIGGERSOURCE.TIME_DELAY, TriggerParams2);

            // Block motion buffer to test if it enables them to move simoultaneously
            _xbotCommand.MotionBufferControl(xbotIdArray[0], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
            _xbotCommand.MotionBufferControl(xbotIdArray[1], MOTIONBUFFEROPTIONS.BLOCKBUFFER);

            // move both xbots "down the line" 
            _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, 0, 0.3, 0, 0.08, 0.05);
            _xbotCommand.LinearMotionSI(0, xbotIdArray[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, 0, 0.3, 0, 0.08, 0.05);

            // Release what the grabber has lifted
            _xbotCommand.LinearMotionSI(0, xbotIdArray[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, 0.04, 0, 0, 0.02, 0.05);

            _xbotCommand.MotionBufferControl(xbotIdArray[0], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
            _xbotCommand.MotionBufferControl(xbotIdArray[1], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
        }
    }
}
