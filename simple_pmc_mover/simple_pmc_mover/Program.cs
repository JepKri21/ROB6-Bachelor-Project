// See https://aka.ms/new-console-template for more information

using PMCLIB;
using System.ComponentModel.DataAnnotations;

namespace simple_pmc_mover
{
    class Program
    {
        private Movement movement = new Movement();
        private connection_handler connectionHandler = new connection_handler();
        private gear_lift_tests gearTest = new gear_lift_tests();
        private scissor_lift_test scissorTest = new scissor_lift_test();
        private static XBotCommands _xbotCommand = new XBotCommands();
        private int selector = 1;

        int[] xbot_ids;

       

        /// <summary>
        /// This Function is used for calibaration of the xbot ids, it is meant to work in tandem 
        /// with the planar motor tool. Each mechanism has been assigned a certain position in the array
        /// with the hierachy generally being scissor lifts first and gear lifts second
        /// TODO Write the full list in the summary
        /// </summary>
        public void xbotCalibration()
        {
            Console.WriteLine("The ");
            Console.WriteLine("Input the ids for the Xbots in the following ");

            XBotIDs tempId = _xbotCommand.GetXBotIDS();
            xbot_ids = new int[8];
            for (int i = 0; i < xbot_ids.Length; i++)
            {
                xbot_ids[i] = Convert.ToInt32(Console.ReadLine());
            }

        }

        public void Run()

        {
            bool tub_been_lifted  = false;
            bool nest_been_lifted = false;
            bool moved_together   = false;
            
            //int[] collectedXbots = { xbot_ids[4], xbot_ids[5] , xbot_ids[6], xbot_ids[7] };
            //int[] xbots1 = { xbot_ids[4], xbot_ids[5] };
            //int[] xbots2 = { xbot_ids[6], xbot_ids[7] };
            do
            {
                CONNECTIONSTATUS status = connectionHandler.ConnectAndGainMastership();
                Console.WriteLine(status);
                Console.WriteLine("Demo Program V 1.0");
                
                while (selector == 1)
                {
                    Console.WriteLine();
                    Console.WriteLine("Choose Test program by entering the appropriate number: ");
                    Console.WriteLine("1    Scissor lift tests");
                    Console.WriteLine("2    Gear Lift tests ");
                    Console.WriteLine("3    System tests");
                    ConsoleKeyInfo keyinfo = Console.ReadKey();

                    switch (keyinfo.KeyChar)
                    {
                        case '1':
                            selector = 2;
                            break;

                        case '2':
                            selector = 3;
                            break;

                        case '3':
                            selector = 4;
                            break;
                    }

                }


                while (selector == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine("Choose Demo by entering the appropriate number: ");
                    Console.WriteLine("0    Return");
                    Console.WriteLine("1    Reconnect");
                    Console.WriteLine("2    Initial position for tub lift");
                    Console.WriteLine("3    Perform tub Set");
                    Console.WriteLine("4    Move Xbots out of magazine");
                    Console.WriteLine("5    Move tub into magazine");
                    Console.WriteLine("6    Perform Precision test");
                    Console.WriteLine("7    Move tub out from under nest");
                    Console.WriteLine("9    Perform tub lift");
                    Console.WriteLine("ESC  Exit");

                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    
                    switch (keyInfo.KeyChar)
                    {
                        case '0':
                            selector = 1;
                            break;
                        case '1':
                            //example 1, run start up routine
                            xbotCalibration();
                            break;
                        case '2':
                            //initial position for the tub pick up
                            movement.initialPosition(xbot_ids[4], xbot_ids[5], xbot_ids[6], xbot_ids[7]);
                            break;
                        case '3':
                            //Perform a tub set down

                            movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[6], 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                            movement.MoveRelativeTogether(xbot_ids[5], xbot_ids[7], -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                            break;
                        case '4':
                            // Move the tub out
                            movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[5], 0.52, Movement.DIRECTION.Y, 0.15, 0.5);
                            movement.MoveRelativeTogether(xbot_ids[6], xbot_ids[7], 0.52, Movement.DIRECTION.Y, 0.15, 0.5);
                            break;

                        case '5':
                            // Move the tub in
                            movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.52, Movement.DIRECTION.Y, 0.15, 0.5);
                            movement.MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.52, Movement.DIRECTION.Y, 0.15, 0.5);
                            break;

                        //case '6':
                        //    //perform the full motion from 1 press

                        //    // first the initial position
                        //    movement.initialPosition(collectedXbots);
                        //    //lift the tub
                        //    movement.MoveRelativeTogether(xbots1, 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                        //    movement.MoveRelativeTogether(xbots2, -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);

                        //    //drive the tub out
                        //    movement.MoveRelativeTogether(collectedXbots, 0.6, Movement.DIRECTION.Y, 0.15, 0.5);
                        //    //lower the tub
                        //    movement.MoveRelativeTogether(xbots1, -0.01, Movement.DIRECTION.Y, 0.0025, 0.005);
                        //    movement.MoveRelativeTogether(xbots2, 0.01, Movement.DIRECTION.Y, 0.0025, 0.005);
                        //    //lift the tub
                        //    movement.MoveRelativeTogether(xbots1, 0.01, Movement.DIRECTION.Y, 0.0025, 0.005);
                        //    movement.MoveRelativeTogether(xbots2, -0.01, Movement.DIRECTION.Y, 0.0025, 0.005);

                        //    //move it back
                        //    movement.MoveRelativeTogether(collectedXbots, -0.6, Movement.DIRECTION.Y, 0.15, 0.5);

                        //    //set the tub down
                        //    movement.MoveRelativeTogether(xbots1, -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                        //    movement.MoveRelativeTogether(xbots2, 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);


                        //    break;

                        case '7':
                            //move tub part ways out from under the nest to lift the tub back up into the mag
                            movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.26, Movement.DIRECTION.Y, 0.15, 0.5);
                            movement.MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.26, Movement.DIRECTION.Y, 0.15, 0.5);
                            break;
                        case '8':
                            for (int i = 0; i < 20; i++)
                            {
                                scissorTest.execute();
                                scissorTest.Drive();
                            }
                            break;
                        case '9':
                            // perform a tub lift
                            movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[6], -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                            movement.MoveRelativeTogether(xbot_ids[5], xbot_ids[7], 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);

                            break;

                        case '\u001b': //escape key
                            return; //exit the program
                        default:
                            break;
                    }
                } //while (selector == 1);

                while (selector == 3)
                {
                    Console.WriteLine(" Gear Lift tests");
                    Console.WriteLine("0    Return ");
                    Console.WriteLine("1    Reconnect");
                    Console.WriteLine("2    initial position");
                    Console.WriteLine("3    move out");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    int[] ids = { 1, 2, 3, 4 };
                    switch (keyInfo.KeyChar)
                    {
                        
                        case '0':
                            selector = 1;
                            break;

                        case '1':
                            //example 1, run start up routine
                            status = connectionHandler.ConnectAndGainMastership();
                            break;
                        case '2':
                            //Move the xbots further apart
                            int[] ids2 = { 1, 2 };
                            gearTest.InitialPosition(4,ids);
                            break;
                        case '3':
                            movement.move_xbot_out(ids);
                            break;
                        case '4':
                            movement.move_xbot_in(ids);
                            break;


                    }

                }

                while (selector == 4)
                {
                    Console.WriteLine(" System tests");
                    Console.WriteLine("0    Return ");
                    Console.WriteLine("1    Reconnect");
                    Console.WriteLine("2    Calibrate XIDs");
                    Console.WriteLine("3    start position for lift");
                    Console.WriteLine("4    Lift nest");
                    Console.WriteLine("5    Lower Nest");
                    Console.WriteLine("6    Move together");
                    Console.WriteLine("7    Move apart");
                    Console.WriteLine("9    DEMO");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    switch (keyInfo.KeyChar)
                    {

                        case '0':
                            selector = 1;
                            break;

                        case '1':
                            status = connectionHandler.ConnectAndGainMastership();
                            break;

                        case '2':

                            xbotCalibration();

                            break;

                        case '3':
                            // start position of the gear lifts when performing a denest operation

                            _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.1035, 0.712+0.12, 0, 0.01, 0.01);
                            _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.1035, 0.391+0.12, 0, 0.01, 0.01);

                            _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.6165, 0.720-0.020+0.12,0, 0.01, 0.01);
                            _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.6165, 0.399-0.020+0.12, 0, 0.01, 0.01);

                            break;

                        case '4':
                            //Lift the nest
                            movement.MoveOpposite(xbot_ids[0], xbot_ids[1], 0.053, Movement.DIRECTION.Y, 0.005, 0.01);
                            movement.MoveOpposite(xbot_ids[2], xbot_ids[3], 0.053, Movement.DIRECTION.Y, 0.005, 0.01);


                            break;
                        case '5':
                            //Lower the nest
                            movement.MoveOpposite(xbot_ids[0], xbot_ids[1], -0.053, Movement.DIRECTION.Y, 0.005, 0.01);
                            movement.MoveOpposite(xbot_ids[2], xbot_ids[3], -0.053, Movement.DIRECTION.Y, 0.005, 0.01);
                            break;

                        case '6':
                            // move together denester together
                            movement.MoveOpposite(xbot_ids[2], xbot_ids[0], 0.001, Movement.DIRECTION.X, 0.001, 0.01);
                            movement.MoveOpposite(xbot_ids[3], xbot_ids[1], 0.001, Movement.DIRECTION.X, 0.001, 0.01);
                            break;

                        case '7':
                            // move denester appart
                            movement.MoveOpposite(xbot_ids[2], xbot_ids[0], -0.001, Movement.DIRECTION.X, 0.001, 0.01);
                            movement.MoveOpposite(xbot_ids[3], xbot_ids[1], -0.001, Movement.DIRECTION.X, 0.001, 0.01);
                            break;

                        case '9':
                            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
                            double time = 0;

                            // full Demo
                            // we assume that we always start after the tubs and gearlifts have been reset using their respective
                            // Initial position commands

                            // Lift the tub out of nest
                            time += movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[6], -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                            movement.MoveRelativeTogether(xbot_ids[5], xbot_ids[7], 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);

                            // Move tub to the denester
                            time += movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[5], 0.52, Movement.DIRECTION.Y, 0.15, 0.5);
                            movement.MoveRelativeTogether(xbot_ids[6], xbot_ids[7], 0.52, Movement.DIRECTION.Y, 0.15, 0.5);

                            // We now have to wait for all of these motions in order to sync up the gear lifts
                            time_params.delaySecs = time;
    
                            _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                            _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);
                            _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);
                            _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);

                            time = 0;
                            // Lower the hooks
                            time += movement.MoveOpposite(xbot_ids[0], xbot_ids[1], -0.053, Movement.DIRECTION.Y, 0.005, 0.01);
                            movement.MoveOpposite(xbot_ids[2], xbot_ids[3], -0.053, Movement.DIRECTION.Y, 0.005, 0.01);

                            // Grasp the nest
                            time += movement.MoveOpposite(xbot_ids[2], xbot_ids[0], 0.008, Movement.DIRECTION.X, 0.001, 0.01);
                            movement.MoveOpposite(xbot_ids[3], xbot_ids[1], 0.008, Movement.DIRECTION.X, 0.001, 0.01);

                            // Lift the nest
                            movement.MoveOpposite(xbot_ids[0], xbot_ids[1], 0.053, Movement.DIRECTION.Y, 0.005, 0.01);
                            time += movement.MoveOpposite(xbot_ids[2], xbot_ids[3], 0.053, Movement.DIRECTION.Y, 0.005, 0.01);




                            time_params.delaySecs = time;


                            // The scissor lifts waits for the gear lift to remove the nest
                            _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.TIME_DELAY, time_params);
                            _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.TIME_DELAY, time_params);
                            _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.TIME_DELAY, time_params);
                            _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.TIME_DELAY, time_params);
                            time = 0;


                            // the gear lift lands
                            _xbotCommand.LevitationCommand(xbot_ids[0], LEVITATEOPTIONS.LAND);
                            _xbotCommand.LevitationCommand(xbot_ids[1], LEVITATEOPTIONS.LAND);
                            _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LAND);
                            _xbotCommand.LevitationCommand(xbot_ids[3], LEVITATEOPTIONS.LAND);

                            // Scissor Lift lowers
                            time += movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[6], 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                            movement.MoveRelativeTogether(xbot_ids[5], xbot_ids[7], -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);

                            // Scissor Lift out of range for nest
                            time += movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.26, Movement.DIRECTION.Y, 0.15, 0.5);
                            movement.MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.26, Movement.DIRECTION.Y, 0.15, 0.5);

                            // Scissor lifts the nest
                            time += movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[6], -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                            movement.MoveRelativeTogether(xbot_ids[5], xbot_ids[7], 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);


                            // Scissor lift moves fully back
                            time += movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.26, Movement.DIRECTION.Y, 0.15, 0.5);
                            movement.MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.26, Movement.DIRECTION.Y, 0.15, 0.5);

                            // Scissor lift places the tub
                            time += movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[6], 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                            movement.MoveRelativeTogether(xbot_ids[5], xbot_ids[7], -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);

                            // Scissor lift moves out under nest
                            time += movement.MoveRelativeTogether(xbot_ids[4], xbot_ids[5], 0.52, Movement.DIRECTION.Y, 0.15, 0.5);
                            movement.MoveRelativeTogether(xbot_ids[6], xbot_ids[7], 0.52, Movement.DIRECTION.Y, 0.15, 0.5);

                            // Scissor lift seperates
                            time += movement.MoveOpposite(xbot_ids[6], xbot_ids[4], -0.06, Movement.DIRECTION.X, 0.01, 0.01);
                            movement.MoveOpposite(xbot_ids[7], xbot_ids[5], -0.06, Movement.DIRECTION.X, 0.01, 0.01);

                            break;






                    }




                }



            } while (true);
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }
    }
}



