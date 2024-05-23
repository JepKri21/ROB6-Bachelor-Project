using PMCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace simple_pmc_mover
{
    internal class gear_lift_tests : Movement
    {
        //this class contains a collection of system commands such as connecting to the PMC, gain mastership, etc.
        private static SystemCommands _systemCommand = new SystemCommands();
        //this class contains a collection of xbot commands, such as discover xbots, mobility control, linear motion, etc.
        private static XBotCommands _xbotCommand = new XBotCommands();

        bool hasBeenCorrected = false;

        public static int[] GetXbotIds()
        {
            XBotIDs tempId = _xbotCommand.GetXBotIDS();

            int[] xbotIds = tempId.XBotIDsArray;

            return xbotIds;

        }

        int[] publicIds = GetXbotIds();

        public int setSelectorOne()
        {
            return 1;
        }


        /// <summary>
        /// Calculates the position of the xbots when the end effector is placed facing the y-axis
        /// based on the desired x and y position of the input
        /// </summary>
        /// <returns>Position of the end-effector</returns>


        public double distance(double x_1, double x_2, double y_1, double y_2)
        {
            return Math.Sqrt(Math.Pow(x_2-x_1, 2)+ Math.Pow(y_2 - y_1, 2));
        }

        public double[] GetXbotPosition(int xbot_id)
        {
            XBotStatus temp_position_0 = _xbotCommand.GetXbotStatus(xbot_id, 0);
            double[] position_0 = temp_position_0.FeedbackPositionSI;

            return position_0;
        }

        // this function is simply for repeatability
        public void PerformTest()
        {
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
            time_params.delaySecs = 0;
            _xbotCommand.LinearMotionSI(0, 5, POSITIONMODE.RELATIVE, 0, -0.165, 0, 0, 0.015, 0.05);
            _xbotCommand.LinearMotionSI(0, 2, POSITIONMODE.RELATIVE, 0, 0.165, 0, 0, 0.015, 0.05);

            _xbotCommand.WaitUntil(0, 5, TRIGGERSOURCE.TIME_DELAY, time_params);
            _xbotCommand.WaitUntil(0, 2, TRIGGERSOURCE.TIME_DELAY, time_params);

            _xbotCommand.LinearMotionSI(0, 5, POSITIONMODE.RELATIVE, 0, 0.165, 0, 0, 0.015, 0.05);
            _xbotCommand.LinearMotionSI(0, 2, POSITIONMODE.RELATIVE, 0, -0.165, 0, 0, 0.015, 0.05);

        }

        public void InitialPosition(int xbot_count,int xbot_1, int xbot_2)
        {
            int[]  xbotIds = {5, 2};
            double[] start_x_meters = { 0.295, 0.420 };
            double[] start_y_meters = { 0.845, 0.845};

            double[] max_speeds = { 0.15, 0.15};
            double[] end_speeds = { 0, 0 };
            double[] max_acc = { 0.5, 0.5 };

            _xbotCommand.SyncMotionSI(xbot_count, xbotIds, start_x_meters, start_y_meters, end_speeds, max_speeds, max_acc);


        }

        public void PrecisionTest() 
        {
            
            int[] xbotIdArray = GetXbotIds();

            double[] position_xbot_0 = GetXbotPosition(xbotIdArray[0]);
            double[] position_xbot_1 = GetXbotPosition(xbotIdArray[1]);


            if (hasBeenCorrected == false)
            {
                // place the xbots in a neutral position
                if (position_xbot_1[1] != position_xbot_0[1])
                {
                    _xbotCommand.LinearMotionSI(0, 1, POSITIONMODE.RELATIVE, 0, 0, position_xbot_1[1] - position_xbot_0[1], 0, 0.15, 0.5);
                }
                if (position_xbot_1[0] != position_xbot_0[0] + 0.118 || position_xbot_1[0] != position_xbot_0[0] - 0.118)
                {
                    _xbotCommand.LinearMotionSI(0, 1, POSITIONMODE.RELATIVE, 0, 0, position_xbot_1[1] - position_xbot_0[1], 0, 0.15, 0.5);
                }
                hasBeenCorrected = true;
            }



            
            double[] start_x_meters = { 0.360, 0.360 };
            double[] start_y_meters = { 0.480, 0.480 };

            double[] max_speeds = { 0.15, 0.15 };
            double[] end_speeds = { 0, 0 };
            double[] max_acc = { 0.5, 0.5 };

            _xbotCommand.SyncMotionSI(2, xbotIdArray, start_x_meters, start_y_meters, end_speeds, max_speeds, max_acc);



            


            int precision_macro_xy1 = 128;
            int precision_macro_xy2 = 129;

            _xbotCommand.LinearMotionSI(0, precision_macro_xy1, POSITIONMODE.RELATIVE, 0, 0, 0, 0.20, 0.15, 0.5);
            _xbotCommand.LinearMotionSI(0, precision_macro_xy1, POSITIONMODE.RELATIVE, 0, 0, 0, -0.20, 0.15, 0.5);

            _xbotCommand.LinearMotionSI(0, precision_macro_xy2, POSITIONMODE.RELATIVE, 0, 0, 0.15, -0.40, 0.15, 0.5);
            _xbotCommand.LinearMotionSI(0, precision_macro_xy2, POSITIONMODE.RELATIVE, 0, 0, -0.15, 0.40, 0.15, 0.5);

            _xbotCommand.RunMotionMacro(0, precision_macro_xy1, xbotIdArray[0]);
            _xbotCommand.RunMotionMacro(0, precision_macro_xy1, xbotIdArray[1]);
            
            _xbotCommand.RunMotionMacro(0, precision_macro_xy2, xbotIdArray[0]);
            _xbotCommand.RunMotionMacro(0, precision_macro_xy2, xbotIdArray[1]);
            

        }

       

        public void LoadTest()
        {

            int[] xbotIdArray = GetXbotIds();

            double[] position_xbot_0 = GetXbotPosition(xbotIdArray[2]);
            double[] position_xbot_1 = GetXbotPosition(xbotIdArray[3]);

            

            if (hasBeenCorrected == false) { 
            // place the xbots in a neutral position
            if (position_xbot_1[1] != position_xbot_0[1])
            {
                _xbotCommand.LinearMotionSI(0, 4, POSITIONMODE.RELATIVE, 0, 0, position_xbot_0[1] - position_xbot_1[1], 0, 0.7, 1);
                    Console.WriteLine("corrected in state 1");
            }
            if (position_xbot_1[0] != position_xbot_0[0] + 0.118 || position_xbot_1[0] != position_xbot_0[0] - 0.118)
            {
                   _xbotCommand.LinearMotionSI(0, 4, POSITIONMODE.RELATIVE, 0, position_xbot_0[0] - position_xbot_1[0]+0.122,0, 0, 0.7 , 1);
                    Console.WriteLine("corrected in state 2");
                }
                
                WaitUntilTriggerParams std_delay = new WaitUntilTriggerParams();
                std_delay.delaySecs = 5;

                _xbotCommand.WaitUntil(0, xbotIdArray[2], TRIGGERSOURCE.TIME_DELAY, std_delay);
                _xbotCommand.WaitUntil(0, xbotIdArray[3], TRIGGERSOURCE.TIME_DELAY, std_delay);

                hasBeenCorrected = true;
            }
            

            position_xbot_0 = GetXbotPosition(xbotIdArray[2]);
            position_xbot_1 = GetXbotPosition(xbotIdArray[3]);
            double[] target_x_meters = { 0, 0 };

            // we Define the arrays of positions for later use in sync
            if (position_xbot_0[0] > position_xbot_1[0])
            {
                target_x_meters[0] = position_xbot_0[0] + 0.109;
                target_x_meters[1] = position_xbot_1[0] - 0.109;

            } else if (position_xbot_0[0] < position_xbot_1[0]) {

                target_x_meters[0] = position_xbot_0[0] - 0.150;
                target_x_meters[1] = position_xbot_1[0] + 0.150;
            }


            double[] target_y_meters = { position_xbot_0[1], position_xbot_1[1] };
            double[] home_x_meters = { position_xbot_0[0], position_xbot_1[0] };
            double[] home_y_meters = { position_xbot_0[1], position_xbot_1[1] };

            double[] max_speeds = {0.15, 0.15};
            double[] end_speeds = { 0, 0 };
            double[] max_acc ={0.5, 0.5 };

            int[] idsAgain = { 3, 4 };
            
            MotionRtn sync_time = _xbotCommand.SyncMotionSI(2, idsAgain, target_x_meters, target_y_meters, end_speeds, max_speeds, max_acc);

            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
            time_params.delaySecs = sync_time.TravelTimeSecs;
           


            _xbotCommand.WaitUntil(0, xbotIdArray[2], TRIGGERSOURCE.TIME_DELAY, time_params);
            _xbotCommand.WaitUntil(0, xbotIdArray[3], TRIGGERSOURCE.TIME_DELAY, time_params);


            _xbotCommand.LinearMotionSI(0, xbotIdArray[2], POSITIONMODE.ABSOLUTE, 0, home_x_meters[0], home_y_meters[0],0,0.15,0.5);
            _xbotCommand.LinearMotionSI(0, xbotIdArray[3], POSITIONMODE.ABSOLUTE, 0, home_x_meters[1], home_y_meters[1], 0, 0.15, 0.5);

            //_xbotCommand.SyncMotionSI(2, xbotIdArray, home_x_meters, home_y_meters, end_speeds, max_speeds, max_acc);
            Console.WriteLine("sanity");
            

        }

        int selector = 1;

        public void runGearLiftTests(int[] XID)
        {
            selector = 3;
            Console.Clear();
            Console.WriteLine(" Gear Lift tests");
            Console.WriteLine("0    Return ");
            Console.WriteLine("1    Reconnect");
            Console.WriteLine("2    initial position");
            Console.WriteLine("3    Perform test");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            int[] ids = { 1, 2, 3, 4 };
            switch (keyInfo.KeyChar)
            {

                case '0':
                    selector = 1;
                    break;

                case '1':
                    //example 1, run start up routine
                    //status = connectionHandler.ConnectAndGainMastership();
                    break;
                case '2':
                    //Move the xbots further apart
                    InitialPosition(2, 1, 2);
                    break;
                case '3':
                    PerformTest();
                    break;
                case '4':
                    move_xbot_in(0, ids);
                    break;

                case '5':
                    _xbotCommand.LinearMotionSI(0, XID[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.1068, 0.534, 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.1068, 0.400, 0, 0.1, 0.1);

                    _xbotCommand.LinearMotionSI(0, XID[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.635, 0.534 , 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.635, 0.400 , 0, 0.1, 0.1);
                    break;

                case '6':
                    MoveOpposite(0, XID[0], XID[1], -0.165, Movement.DIRECTION.Y, 0.05, 0.01);
                    MoveOpposite(0, XID[2], XID[3], -0.165, Movement.DIRECTION.Y, 0.05, 0.01);


                    MoveOpposite(0, XID[0], XID[1], 0.165, Movement.DIRECTION.Y, 0.05, 0.01);
                    MoveOpposite(0, XID[2], XID[3], 0.165, Movement.DIRECTION.Y, 0.05, 0.01);
                    break;

                case '\u001b': //escape key
                    return; //exit the program


            }
        }


    }
}
