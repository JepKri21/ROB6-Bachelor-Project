using PMCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_pmc_mover
{
    internal class Movement
    {
        //this class contains a collection of system commands such as connecting to the PMC, gain mastership, etc.
        private static SystemCommands _systemCommand = new SystemCommands();
        //this class contains a collection of xbot commands, such as discover xbots, mobility control, linear motion, etc.
        private static XBotCommands _xbotCommand = new XBotCommands();

        public void ConnectAndGainMastership()
        {
            _systemCommand.AutoSearchAndConnectToPMC(); //connect to PMC

            _systemCommand.GainMastership(); // gain mastership

            _xbotCommand.ActivateXBOTS();

        }

        public static int[] GetXbotIds()
        {
            XBotIDs tempId = _xbotCommand.GetXBotIDS();

            int[] xbotIds = tempId.XBotIDsArray;

            return xbotIds;

        }

        /// <summary>
        /// Moves all of the given xbots in the same direction based on the distance given
        /// </summary>
        public double MoveRelativeTogether(int xbot1, int xbot2, double distance, DIRECTION direction, double maxSpeed, double maxAcc)
        {
            int[] xbots = {xbot1, xbot2 };
            double total_motion_time = 0;
            switch (direction)
            {
                case DIRECTION.X:

                    for (int i = 0; i < xbots.Length; i++)
                    {
                        MotionRtn xbot = _xbotCommand.LinearMotionSI(0, xbots[i], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, distance, 0, 0, maxSpeed, maxAcc);
                        total_motion_time = xbot.TravelTimeSecs;
                    }
                    break;
                case DIRECTION.Y:

                    for (int i = 0; i < xbots.Length; i++)
                    {
                        MotionRtn xbot = _xbotCommand.LinearMotionSI(0, xbots[i], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, distance, 0, maxSpeed, maxAcc);
                        total_motion_time = xbot.TravelTimeSecs;
                    }
                    break;
            }

            return total_motion_time;
        }


        public double MoveOpposite(int first_xbot, int second_xbot, double distance, DIRECTION direction, double maxSpeed, double maxAcc)
        {
            double total_motion_time = 0;

            switch (direction)
            { 
                case DIRECTION.X:

                    MotionRtn first_xbotX = _xbotCommand.LinearMotionSI(0, first_xbot, POSITIONMODE.RELATIVE,0,-distance,0,0,maxSpeed,maxAcc);
                    MotionRtn second_xbotX = _xbotCommand.LinearMotionSI(0, second_xbot, POSITIONMODE.RELATIVE, 0, distance, 0, 0, maxSpeed, maxAcc);
                    total_motion_time = second_xbotX.TravelTimeSecs;
                    break;

                case DIRECTION.Y:
                    MotionRtn first_xbotY = _xbotCommand.LinearMotionSI(0, first_xbot, POSITIONMODE.RELATIVE, 0, 0, -distance, 0, maxSpeed, maxAcc);
                    MotionRtn second_xbotY = _xbotCommand.LinearMotionSI(0, second_xbot, POSITIONMODE.RELATIVE, 0, 0, distance, 0, maxSpeed, maxAcc);
                    total_motion_time = second_xbotY.TravelTimeSecs;
                    break;

            }
            return total_motion_time;
        }






        public enum DIRECTION
        {
            X,
            Y
        }


        public void initialPosition(int xbot4, int xbot5, int xbot6, int xbot7)
        {
            int[] xbotIds = { xbot4, xbot5, xbot6, xbot7 };
            double[] start_x_meters = { 0.297,0.297,0.423,0.423 };
            double[] start_y_meters = { 0.248, 0.070 ,0.248,0.070 };

            double[] max_speeds = { 0.15,0.15, 0.15,0.15 };
            double[] end_speeds = { 0, 0,0,0 };
            double[] max_acc = { 0.5, 0.5,0.5,0.5 };

            _xbotCommand.SyncMotionSI(4, xbotIds, start_x_meters, start_y_meters, end_speeds, max_speeds, max_acc);

        }

        public void move_xbot_out(int[] ids)
        {
            int[] xbotIdArray = GetXbotIds();
            _xbotCommand.LinearMotionSI(0, ids[0], POSITIONMODE.RELATIVE, 0, 0.130, 0.00, 0, 0.005, 0.01);
            _xbotCommand.LinearMotionSI(0, ids[2], POSITIONMODE.RELATIVE, 0, 0.130, 0.00, 0, 0.005, 0.01);
            _xbotCommand.LinearMotionSI(0, ids[1], POSITIONMODE.RELATIVE, 0, -0.130, 0.00, 0, 0.005, 0.01);
            _xbotCommand.LinearMotionSI(0, ids[3], POSITIONMODE.RELATIVE, 0, -0.130, 0.00, 0, 0.005, 0.01);

        }

        public void move_xbot_in(int[] ids)
        {
            int[] xbotIdArray = GetXbotIds();
            _xbotCommand.LinearMotionSI(0, ids[0], POSITIONMODE.RELATIVE, 0, -0.130, 0.00, 0, 0.005, 0.01);
            _xbotCommand.LinearMotionSI(0, ids[2], POSITIONMODE.RELATIVE, 0, -0.130, 0.00, 0, 0.005, 0.01);
            _xbotCommand.LinearMotionSI(0, ids[1], POSITIONMODE.RELATIVE, 0, 0.130, 0.00, 0, 0.005, 0.01);
            _xbotCommand.LinearMotionSI(0, ids[3], POSITIONMODE.RELATIVE, 0, 0.130, 0.00, 0, 0.005, 0.01);
        }
        public void move_xbot_back()
        {
            int[] xbotIdArray = GetXbotIds();
            _xbotCommand.LinearMotionSI(0, 1, POSITIONMODE.RELATIVE, 0, 0.05, -0.50, 0, 1, 0.5);
            _xbotCommand.LinearMotionSI(0, 2, POSITIONMODE.RELATIVE, 0, -0.05, -0.50, 0, 1, 0.5);
        }

        


    }
}
