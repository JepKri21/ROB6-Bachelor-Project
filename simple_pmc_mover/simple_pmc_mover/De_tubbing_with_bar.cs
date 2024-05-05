using PMCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_pmc_mover
{
    internal class De_tubbing_with_bar : Movement
    {
        private static SystemCommands _systemCommand = new SystemCommands();
        //this class contains a collection of xbot commands, such as discover xbots, mobility control, linear motion, etc.
        private static XBotCommands _xbotCommand = new XBotCommands();

        int selector = 6;

        double unloaded_gearlift_speed = 0.2;
        double unloaded_scissorlift_speed = 0.2;

        double loaded_gearlift_speed;
        double loaded_scissorlift_speed;

        public int setSelectorOne()
        {
            return selector;
        }

        public void runDeTubbingWithBar(int[] XID)
        {
            int[] xbot_ids = XID;
            selector = 6;
            Console.Clear();
            Console.WriteLine(" De-tubbing with bar");
            Console.WriteLine("0    Return ");
            Console.WriteLine("1    Start position for lift");
            Console.WriteLine("2    Start postion for scissor lift");
            Console.WriteLine("3    DEMO");
            Console.WriteLine("4    Step by step");
            Console.WriteLine("5    ");
            Console.WriteLine("6    ");
            Console.WriteLine("7    ");
            Console.WriteLine("9    Empty");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.KeyChar)
            {

                case '0':
                    selector = 1;
                    break;

                case '1':                    
                    // start position of the gear lifts when performing a denest operation

                    _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.1065, 0.713, 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.1065, 0.579, 0, 0.1, 0.1);

                    _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.6165, 0.713, 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.6165, 0.579, 0, 0.1, 0.1);
                    break;
                case '2':
                    //initial position for the tub pick up
                    initialPosition(4, xbot_ids[4], xbot_ids[5], xbot_ids[6], xbot_ids[7]);


                    break;

                case '3':
                    //Demo
                    
                    break;
                case '4':
                    runDeTubbingWithBarStepByStep(XID);
                    break;

            }
        }
        public void runDeTubbingWithBarStepByStep(int[] XID)
        {
            selector = 60;
            int[] xbot_ids = XID;
            Console.Clear();
            Console.WriteLine(" System tests");
            Console.WriteLine("0    Return ");
            Console.WriteLine("1    Move tub to denesting");
            Console.WriteLine("2    Lower the hooks");
            Console.WriteLine("3    Rise the hooks");
            Console.WriteLine("4    Graps the nest");
            Console.WriteLine("5    Release the nest");
            Console.WriteLine("6    ");
            Console.WriteLine("7    ");
            Console.WriteLine("8    ");
            Console.WriteLine("9    ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.KeyChar)
            {
                case '0':
                    selector = 6;
                    break;
                case '1':
                    // Lift the tub out of nest
                    MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], -0.015, Movement.DIRECTION.Y, 0.01, 0.01);
                    MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], 0.015, Movement.DIRECTION.Y, 0.01, 0.01);

                    // Move tub to the denester
                    MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], 0.495, Movement.DIRECTION.Y, 0.15, 0.5);
                    MoveRelativeTogether(1, xbot_ids[6], xbot_ids[7], 0.495, Movement.DIRECTION.Y, 0.15, 0.5);
                    break;
                case '2':
                    // Lower the hooks
                    MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.093, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);
                    MoveOpposite(0, xbot_ids[2], xbot_ids[3], -0.093, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);
                    break;

                case '3':                
                    // Rise the hooks
                    MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.093, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);
                    MoveOpposite(0, xbot_ids[2], xbot_ids[3], 0.093, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);
                    
                    break;
                case '4':
                    // Grasp the nest
                    MoveRelativeTogether(0, xbot_ids[0], xbot_ids[1], -0.015, Movement.DIRECTION.X, 0.15, 0.5);
                    MoveRelativeTogether(0, xbot_ids[2], xbot_ids[3], 0.015, Movement.DIRECTION.X, 0.15, 0.5);
                    break;

                case '5':
                    // Release the nest
                    MoveRelativeTogether(0, xbot_ids[0], xbot_ids[1], 0.015, Movement.DIRECTION.X, 0.15, 0.5);
                    MoveRelativeTogether(0, xbot_ids[2], xbot_ids[3], -0.015, Movement.DIRECTION.X, 0.15, 0.5);
                    break;

                

                case '7':
                    _xbotCommand.LinearMotionSI(0, xbot_ids[8], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.120, 0.120, 0, 0.1, 0.1);
                    break;
                case '8':
                    _xbotCommand.RotaryMotionP2P(2, XID[8], ROTATIONMODE.WRAP_TO_2PI_CW, 1.5707, 5, 2, POSITIONMODE.RELATIVE);
                    break;

                case '9':
                    _xbotCommand.RotaryMotionP2P(1, XID[8], ROTATIONMODE.WRAP_TO_2PI_CCW, 1.5707, 5, 2, POSITIONMODE.RELATIVE);
                    break;


            }

        }

    }
}
