﻿// See https://aka.ms/new-console-template for more information

using PMCLIB;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography;

namespace simple_pmc_mover
{
    class Program
    {
        private Movement movement = new Movement();
        private connection_handler connectionHandler = new connection_handler();
        private gear_lift_tests gearTest = new gear_lift_tests();
        private scissor_lift_test scissorTest = new scissor_lift_test();
        private System_tests SystemTests = new System_tests();
        private De_nest_test deNestTest = new De_nest_test();
        private De_tubbing_with_bar deTubbingWithBar = new De_tubbing_with_bar();
        private capping capper = new capping();
        private Filling_and_inspection fillingAndInspection = new Filling_and_inspection();
        private Function_based_filling_weighing_inspection functionBasedFilling = new Function_based_filling_weighing_inspection();
        private DataLogger dataLogger = new DataLogger();



        private static XBotCommands _xbotCommand = new XBotCommands();
        private int selector = 0;
        string title = @"
                         _   _        __  __                        |
    /\                  | | (_)      |  \/  |                       |
   /  \   ___  ___ _ __ | |_ _  ___  | \  / | _____   _____ _ __    |
  / /\ \ / __|/ _ \ '_ \| __| |/ __| | |\/| |/ _ \ \ / / _ \ '__|   |
 / ____ \\__ \  __/ |_) | |_| | (__  | |  | | (_) \ V /  __/ |      | 
/_/    \_\___/\___| .__/ \__|_|\___| |_|  |_|\___/ \_/ \___|_|      |
                  | |                                               |
                  |_|                                               |
____________________________________________________________________| ";

        //If ID is always the same use outcommented line
        public int[] xbot_ids = {2,6,8,7,3,5,1,4};
        //int[] xbot_ids;

       

        /// <summary>
        /// This Function is used for calibaration of the xbot ids, it is meant to work in tandem 
        /// with the planar motor tool. Each mechanism has been assigned a certain position in the array
        /// with the hierachy generally being scissor lifts first and gear lifts second
        /// TODO Write the full list in the summary
        /// </summary>
        public void xbotCalibration()
        {
            Console.WriteLine("");
            Console.WriteLine("XID CALIBRATION ");
            Console.WriteLine("Input the ids for the xbots in the following order: ");
            Console.WriteLine("1: Gearlift pairs from left to right, each pair starting with the highest Y-value");
            Console.WriteLine("2: Scissorlift pairs from left to right, each pair starting with the highest Y-value");
            Console.WriteLine("Not that right to left refers to their position in the PM tool");
            Console.WriteLine("");
            XBotIDs tempId = _xbotCommand.GetXBotIDS();


            

            /*
            xbot_ids = new int[8];
            for (int i = 0; i < xbot_ids.Length; i++)
            {
                xbot_ids[i] = Convert.ToInt32(Console.ReadLine());
            }
            */

        }

        public void ExecuteCapping()
        {
            capper.PerformCapping(xbot_ids);
        }

        public void ExecuteDataLogger()
        {
            dataLogger.performSampling();
        }

        public async void Run()

        {

            //int[] collectedXbots = { xbot_ids[4], xbot_ids[5] , xbot_ids[6], xbot_ids[7] };
            //int[] xbots1 = { xbot_ids[4], xbot_ids[5] };
            //int[] xbots2 = { xbot_ids[6], xbot_ids[7] };
            do
            {
                Console.Title = "Aseptic Planar Technology";
                Console.WriteLine("Demo Program V 1.0");

                while (selector == 0)
                {
                    CONNECTIONSTATUS status = connectionHandler.ConnectAndGainMastership();
                    Console.WriteLine(status);
                    xbotCalibration();

                    selector = 1;


                }
                while (selector == 1)
                {
                    //Console.Clear();
                    Console.WriteLine(title);

                    Console.WriteLine("Choose Test program by entering the appropriate number: ");
                    Console.WriteLine("0:   Run Setup");
                    Console.WriteLine("1    Scissor lift tests");
                    Console.WriteLine("2    Gear Lift tests ");
                    Console.WriteLine("3    System tests");
                    Console.WriteLine("4    De-nest test");
                    Console.WriteLine("5    De-Tubbing with bar");
                    Console.WriteLine("7    Filling And Inspection");
                    Console.WriteLine("8    Function based filling and inspection");
                    Console.WriteLine("ESC: Exit program");
                    ConsoleKeyInfo keyinfo = Console.ReadKey();

                    switch (keyinfo.KeyChar)
                    {
                        case '0':
                            selector = 0;
                            break;

                        case '1':
                            selector = 2;
                            break;

                        case '2':
                            selector = 3;
                            break;

                        case '3':
                            selector = 4;
                            break;
                        case '4':
                            selector = 5;
                            break;
                        case '5':
                            selector = 6;
                            break;


                        case '7':
                            selector = 7;
                            break;

                        case '8':
                            selector = 8;
                            break;

                        case 's':

                           
                            
                            break;

                        case '\u001b': //escape key
                            return;
                        default:
                            Console.WriteLine("Invalid input");
                            selector = 1;
                            break;
                    }

                }

                while (selector == 2)
                {
                    scissorTest.Run_tests(xbot_ids);
                    selector = scissorTest.setSelectorOne();
                }

                while (selector == 3)
                {
                    gearTest.runGearLiftTests(xbot_ids);
                    selector = gearTest.setSelectorOne();

                }

                while (selector == 4)
                {


                    SystemTests.runSystemTests(xbot_ids);


                    selector = SystemTests.setSelectorOne();

                }
                 
                
                


                while (selector == 5)
                {
                    deNestTest.runDeNestTest(xbot_ids);
                    selector = deNestTest.setSelectorOne();

                }
                while (selector == 50)
                {
                    deNestTest.deNestingStepByStep(xbot_ids);
                    selector = deNestTest.setSelectorOne();
                }
                while (selector == 7)
                {
                    fillingAndInspection.runFillingAndInspection(xbot_ids);
                    selector = fillingAndInspection.setSelectorOne();

                }


                while (selector == 51)
                {
                    deNestTest.unitCarriereRotation(xbot_ids);
                    selector = deNestTest.setSelectorOne();
                }
                while (selector == 52)
                {
                    deNestTest.deNestingDemo(xbot_ids);
                    selector = deNestTest.setSelectorOne();
                }
                while (selector == 6)
                {
                    deTubbingWithBar.runDeTubbingWithBar(xbot_ids);
                    selector = deTubbingWithBar.setSelectorOne();

                }
                while (selector == 60)
                {
                    deTubbingWithBar.runDeTubbingWithBarStepByStep(xbot_ids);
                    selector = deTubbingWithBar.setSelectorOne();
                }

                while (selector == 8)
                {
                    functionBasedFilling.runFunctionBasedFillingAndInspection(xbot_ids);
                    selector = functionBasedFilling.setSelectorOne();
                }


            } while (true);
        }



        static void Main(string[] args)
        {

            Program program = new Program();
            Thread thread1 = new Thread(new ThreadStart(program.Run));
            Thread thread2 = new Thread(new ThreadStart(program.ExecuteCapping));
            //Thread thread3 = new Thread(new ThreadStart(program.ExecuteDataLogger));


            thread1.Name = "Main Program";
            thread2.Name = "capping";
            //thread3.Name = "DataLogger";


            thread1.Start();
            //thread2.Start();
            //thread3.Start();
        }
    }
}



