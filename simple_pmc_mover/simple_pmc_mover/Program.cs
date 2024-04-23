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
        private int selector = 0;

        int[] xbot_ids;

        public void defineIdList()
        {
            XBotIDs tempId = _xbotCommand.GetXBotIDS();
            xbot_ids = new int[tempId.XBotIDsArray.Length];
        }

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

            for (int i = 0; i < xbot_ids.Length; i++)
            {
                xbot_ids[i] = Convert.ToInt32(Console.ReadLine());
            }

        }

        public void Run()

        {
            int[] xbots1 = { 1, 3 };
            int[] xbots2 = { 2, 4 };
            int[] collectedXbots = { 1, 2, 3, 4 };
            do
            {
                CONNECTIONSTATUS status = connectionHandler.ConnectAndGainMastership();
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
                    Console.WriteLine("7    Perform scissor lift lift");
                    Console.WriteLine("9    Perform tub lift");
                    Console.WriteLine("ESC  Exit");

                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    
                    switch (keyInfo.KeyChar)
                    {
                        case '0':
                            selector = 0;
                            break;
                        case '1':
                            //example 1, run start up routine
                            status = connectionHandler.ConnectAndGainMastership();
                            break;
                        case '2':
                            //Move the xbots further apart
                            movement.initialPosition(collectedXbots);
                            break;
                        case '3':
                            //Perform a tub set down

                            movement.MoveRelativeTogether(xbots1, -0.0005, Movement.DIRECTION.Y, 0.0025, 0.005);
                            movement.MoveRelativeTogether(xbots2, 0.0005, Movement.DIRECTION.Y, 0.0025, 0.005);
                            break;
                        case '4':
                            // Move the tub out
                            movement.MoveRelativeTogether(collectedXbots, 0.4, Movement.DIRECTION.Y, 0.15, 0.5);
                            break;

                        case '5':
                            // Move the tub in
                            movement.MoveRelativeTogether(collectedXbots, -0.4, Movement.DIRECTION.Y, 0.15, 0.5);
                            break;

                        case '6':
                            //perform the full motion from 1 press

                            // first the initial position
                            movement.initialPosition(collectedXbots);
                            //lift the tub
                            movement.MoveRelativeTogether(xbots1, 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                            movement.MoveRelativeTogether(xbots2, -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);

                            //drive the tub out
                            movement.MoveRelativeTogether(collectedXbots, 0.6, Movement.DIRECTION.Y, 0.15, 0.5);
                            //lower the tub
                            movement.MoveRelativeTogether(xbots1, -0.01, Movement.DIRECTION.Y, 0.0025, 0.005);
                            movement.MoveRelativeTogether(xbots2, 0.01, Movement.DIRECTION.Y, 0.0025, 0.005);
                            //lift the tub
                            movement.MoveRelativeTogether(xbots1, 0.01, Movement.DIRECTION.Y, 0.0025, 0.005);
                            movement.MoveRelativeTogether(xbots2, -0.01, Movement.DIRECTION.Y, 0.0025, 0.005);

                            //move it back
                            movement.MoveRelativeTogether(collectedXbots, -0.6, Movement.DIRECTION.Y, 0.15, 0.5);

                            //set the tub down
                            movement.MoveRelativeTogether(xbots1, -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                            movement.MoveRelativeTogether(xbots2, 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);


                            break;

                        case '7':
                            scissorTest.execute();
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
                            movement.MoveRelativeTogether(xbots1, 0.0005, Movement.DIRECTION.Y, 0.0025, 0.005);
                            movement.MoveRelativeTogether(xbots2, -0.0005, Movement.DIRECTION.Y, 0.0025, 0.005);

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
                            selector = 0;
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
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    switch (keyInfo.KeyChar)
                    {

                        case '0':
                            selector = 0;
                            break;

                        case '1':
                            status = connectionHandler.ConnectAndGainMastership();
                            break;

                        case '2':

                            xbotCalibration();

                            break;

                        case '3':



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



