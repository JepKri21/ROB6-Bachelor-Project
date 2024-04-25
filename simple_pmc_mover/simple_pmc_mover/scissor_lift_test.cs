using PMCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace simple_pmc_mover
{
    internal class scissor_lift_test : Movement
    {
        //this class contains a collection of system commands such as connecting to the PMC, gain mastership, etc.
        private static SystemCommands _systemCommand = new SystemCommands();
        //this class contains a collection of xbot commands, such as discover xbots, mobility control, linear motion, etc.
        private static XBotCommands _xbotCommand = new XBotCommands();

        int selector = 2;

        public static int[] GetXbotIds()
        {
            XBotIDs tempId = _xbotCommand.GetXBotIDS();

            int[] xbotIds = tempId.XBotIDsArray;

            return xbotIds;

        }


        public void execute()
        {
            /*
             * As it is now the leftmost xbot (seeing from power switch) has to have the ID #1 while the one to the right has to 
             * be #2. For this to work we need to be able to permanently assign IDS to the xbots in correlation with their task.
            */
            int[] xbotIds = GetXbotIds();

            _xbotCommand.LevitationCommand(xbotIds[0], LEVITATEOPTIONS.LEVITATE);
            _xbotCommand.LevitationCommand(xbotIds[1], LEVITATEOPTIONS.LEVITATE);

            //double[] home_x_meters = { 0.258,0.436  }; // the original
            double[] home_x_meters = { 0.286, 0.408};
            double[] home_y_meters = { 0.731, 0.731 };

            double[] max_speeds = { 0.15, 0.15 };
            double[] end_speeds = { 0, 0 };
            double[] max_acc = { 0.5, 0.5 };

            //_xbotCommand.MotionBufferControl(xbotIds[0], MOTIONBUFFEROPTIONS.CLEARBUFFER);
            //_xbotCommand.MotionBufferControl(xbotIds[1], MOTIONBUFFEROPTIONS.CLEARBUFFER);

           
            MotionRtn waiter1 = _xbotCommand.LinearMotionSI(0, xbotIds[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, home_x_meters[0], home_y_meters[0], 0, 0.15, 0.5);
            MotionRtn waiter2 =_xbotCommand.LinearMotionSI(0, xbotIds[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, home_x_meters[1], home_y_meters[1], 0, 0.15, 0.5);

            WaitUntilTriggerParams time_params1 = new WaitUntilTriggerParams();
            WaitUntilTriggerParams time_params2 = new WaitUntilTriggerParams();
            time_params1.delaySecs = waiter1.TravelTimeSecs;
            time_params2.delaySecs = waiter2.TravelTimeSecs;




            _xbotCommand.WaitUntil(0, xbotIds[0], TRIGGERSOURCE.TIME_DELAY, time_params2);
            _xbotCommand.WaitUntil(0, xbotIds[1], TRIGGERSOURCE.TIME_DELAY, time_params1);

            _xbotCommand.LinearMotionSI(0, xbotIds[0], POSITIONMODE.RELATIVE, 0, -0.020, 0, 0, 0.05, 1);
            _xbotCommand.LinearMotionSI(0, xbotIds[1], POSITIONMODE.RELATIVE, 0, 0.020, 0, 0, 0.05, 1); //OG is 255


            _xbotCommand.LevitationCommand(xbotIds[0], LEVITATEOPTIONS.LAND);
            _xbotCommand.LevitationCommand(xbotIds[1], LEVITATEOPTIONS.LAND);



        }

        public void Drive()
        {
            int[] xbotIds = GetXbotIds();

            //_xbotCommand.MotionBufferControl(xbotIds[0], MOTIONBUFFEROPTIONS.CLEARBUFFER);
            //_xbotCommand.MotionBufferControl(xbotIds[1], MOTIONBUFFEROPTIONS.CLEARBUFFER);

            _xbotCommand.LinearMotionSI(0, xbotIds[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.5, 0, 2, 10);
            _xbotCommand.LinearMotionSI(0, xbotIds[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.5, 0, 2, 10);


            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
            time_params.delaySecs = 0.5;

            

            _xbotCommand.LinearMotionSI(0, xbotIds[0], POSITIONMODE.RELATIVE, 0, -0.0240, 0, 0, 0.05, 0.5);
            _xbotCommand.LinearMotionSI(0, xbotIds[1], POSITIONMODE.RELATIVE, 0, 0.0240, 0, 0, 0.05, 0.5);


            MotionRtn waiter1 = _xbotCommand.LinearMotionSI(0, xbotIds[0], POSITIONMODE.RELATIVE, 0, 0.0240, 0, 0, 0.05, 0.5);
            MotionRtn waiter2 = _xbotCommand.LinearMotionSI(0, xbotIds[1], POSITIONMODE.RELATIVE, 0, -0.0240, 0, 0, 0.05, 0.5);

            WaitUntilTriggerParams time_params1 = new WaitUntilTriggerParams();
            WaitUntilTriggerParams time_params2 = new WaitUntilTriggerParams();
            time_params1.delaySecs = waiter1.TravelTimeSecs;
            time_params2.delaySecs = waiter2.TravelTimeSecs;

            _xbotCommand.WaitUntil(0, xbotIds[0], TRIGGERSOURCE.TIME_DELAY, time_params2);
            _xbotCommand.WaitUntil(0, xbotIds[1], TRIGGERSOURCE.TIME_DELAY, time_params1);

            _xbotCommand.LinearMotionSI(0, xbotIds[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.5, 0, 2, 10);
            _xbotCommand.LinearMotionSI(0, xbotIds[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.5, 0, 2, 10);

            
        }

        public int setSelectorOne()
        {
            return selector;
        }

        public void Run_tests(int[] XID)
        {


            selector = 2;
            int[] xbot_ids = XID;
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Choose Demo by entering the appropriate number: ");
            Console.WriteLine("0    Return");
            Console.WriteLine("1    Initial position for tub lift");
            Console.WriteLine("2    Perform tub lift");
            Console.WriteLine("3    Perform tub Set");
            Console.WriteLine("4    Move Xbots out of magazine");
            Console.WriteLine("5    Move tub into magazine");
            Console.WriteLine("6    Move tub out from under nest");
            Console.WriteLine("7    EMPTY");
            Console.WriteLine("9    EMPTY");
            

            ConsoleKeyInfo keyInfo = Console.ReadKey();


            switch (keyInfo.KeyChar)
            {
                case '0':
                    selector = 1;   
                    break;
                case '1':
                    //initial position for the tub pick up
                    initialPosition(xbot_ids[4], xbot_ids[5], xbot_ids[6], xbot_ids[7]);

                    break;
                case '2':
                    // perform a tub lift
                    MoveRelativeTogether(xbot_ids[4], xbot_ids[6], -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                    MoveRelativeTogether(xbot_ids[5], xbot_ids[7], 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);

                    break;
                case '3':
                    //Perform a tub set down

                    MoveRelativeTogether(xbot_ids[4], xbot_ids[6], 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                    MoveRelativeTogether(xbot_ids[5], xbot_ids[7], -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                    break;
                case '4':
                    // Move the tub out
                    MoveRelativeTogether(xbot_ids[4], xbot_ids[5], 0.52, Movement.DIRECTION.Y, 0.15, 0.5);
                    MoveRelativeTogether(xbot_ids[6], xbot_ids[7], 0.52, Movement.DIRECTION.Y, 0.15, 0.5);
                    break;

                case '5':
                    // Move the tub in
                    MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.52, Movement.DIRECTION.Y, 0.15, 0.5);
                    MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.52, Movement.DIRECTION.Y, 0.15, 0.5);
                    break;

           

                case '6':
                    //move tub part ways out from under the nest to lift the tub back up into the mag
                    MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.26, Movement.DIRECTION.Y, 0.15, 0.5);
                    MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.26, Movement.DIRECTION.Y, 0.15, 0.5);
                    break;
                case '8':
                    for (int i = 0; i < 20; i++)
                    {
                        execute();
                        Drive();
                    }
                    break;
                case '9':
                    

                    break;

                case '\u001b': //escape key
                    return; //exit the program

                
            }




        }







    }
}
