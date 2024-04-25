using PMCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_pmc_mover
{
    internal class System_tests : Movement
    {
        private static SystemCommands _systemCommand = new SystemCommands();
        //this class contains a collection of xbot commands, such as discover xbots, mobility control, linear motion, etc.
        private static XBotCommands _xbotCommand = new XBotCommands();

        int selector = 4;

        public int setSelectorOne()
        {
            return selector;
        }

        public void runSystemTests(int[] XID)
        {

            int[] xbot_ids = XID;
            selector = 4;
            Console.Clear();
            Console.WriteLine(" System tests");
            Console.WriteLine("0    Return ");
            Console.WriteLine("1    start position for lift");
            Console.WriteLine("2    DEMO");
            Console.WriteLine("3    EMPTY");
            Console.WriteLine("4    Lift nest");
            Console.WriteLine("5    Lower Nest");
            Console.WriteLine("6    Move together");
            Console.WriteLine("7    Move apart");
            Console.WriteLine("9    EMPTY");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.KeyChar)
            {

                case '0':
                    selector = 1;
                    break;

                case '9':
                    
                    break;

                case '1':
                    // start position of the gear lifts when performing a denest operation

                    _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.1035, 0.712 + 0.12, 0, 0.01, 0.01);
                    _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.1035, 0.391 + 0.12, 0, 0.01, 0.01);

                    _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.6165, 0.720 - 0.020 + 0.12, 0, 0.01, 0.01);
                    _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.6165, 0.399 - 0.020 + 0.12, 0, 0.01, 0.01);



                    break;

                case '3':
                    
                    break;

                case '4':
                    //Lift the nest
                    MoveOpposite(xbot_ids[0], xbot_ids[1], 0.053, Movement.DIRECTION.Y, 0.005, 0.01);
                    MoveOpposite(xbot_ids[2], xbot_ids[3], 0.053, Movement.DIRECTION.Y, 0.005, 0.01);


                    break;
                case '5':
                    //Lower the nest
                    MoveOpposite(xbot_ids[0], xbot_ids[1], -0.053, Movement.DIRECTION.Y, 0.005, 0.01);
                    MoveOpposite(xbot_ids[2], xbot_ids[3], -0.053, Movement.DIRECTION.Y, 0.005, 0.01);
                    break;

                case '6':
                    // move together denester together
                    MoveOpposite(xbot_ids[2], xbot_ids[0], 0.001, Movement.DIRECTION.X, 0.001, 0.01);
                    MoveOpposite(xbot_ids[3], xbot_ids[1], 0.001, Movement.DIRECTION.X, 0.001, 0.01);
                    break;

                case '7':
                    // move denester appart
                    MoveOpposite(xbot_ids[2], xbot_ids[0], -0.001, Movement.DIRECTION.X, 0.001, 0.01);
                    MoveOpposite(xbot_ids[3], xbot_ids[1], -0.001, Movement.DIRECTION.X, 0.001, 0.01);
                    break;

                case '2':
                    WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
                    double time = 0;

                    // full Demo
                    // we assume that we always start after the tubs and gearlifts have been reset using their respective
                    // Initial position commands

                    // Lift the tub out of nest
                    time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                    MoveRelativeTogether(xbot_ids[5], xbot_ids[7], 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);

                    // Move tub to the denester
                    time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], 0.52, Movement.DIRECTION.Y, 0.15, 0.5);
                    MoveRelativeTogether(xbot_ids[6], xbot_ids[7], 0.52, Movement.DIRECTION.Y, 0.15, 0.5);

                    // We now have to wait for all of these motions in order to sync up the gear lifts
                    time_params.delaySecs = time;

                    _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                    _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);
                    _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);
                    _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);

                    time = 0;
                    // Lower the hooks
                    time += MoveOpposite(xbot_ids[0], xbot_ids[1], -0.053, Movement.DIRECTION.Y, 0.005, 0.01);
                    MoveOpposite(xbot_ids[2], xbot_ids[3], -0.053, Movement.DIRECTION.Y, 0.005, 0.01);

                    // Grasp the nest
                    time += MoveOpposite(xbot_ids[2], xbot_ids[0], 0.008, Movement.DIRECTION.X, 0.001, 0.01);
                    MoveOpposite(xbot_ids[3], xbot_ids[1], 0.008, Movement.DIRECTION.X, 0.001, 0.01);

                    // Lift the nest
                    MoveOpposite(xbot_ids[0], xbot_ids[1], 0.053, Movement.DIRECTION.Y, 0.005, 0.01);
                    time += MoveOpposite(xbot_ids[2], xbot_ids[3], 0.053, Movement.DIRECTION.Y, 0.005, 0.01);




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
                    time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                    MoveRelativeTogether(xbot_ids[5], xbot_ids[7], -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);

                    // Scissor Lift out of range for nest
                    time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.26, Movement.DIRECTION.Y, 0.15, 0.5);
                    MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.26, Movement.DIRECTION.Y, 0.15, 0.5);

                    // Scissor lifts the nest
                    time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                    MoveRelativeTogether(xbot_ids[5], xbot_ids[7], 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);


                    // Scissor lift moves fully back
                    time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.26, Movement.DIRECTION.Y, 0.15, 0.5);
                    MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.26, Movement.DIRECTION.Y, 0.15, 0.5);

                    // Scissor lift places the tub
                    time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], 0.02, Movement.DIRECTION.Y, 0.0025, 0.005);
                    MoveRelativeTogether(xbot_ids[5], xbot_ids[7], -0.02, Movement.DIRECTION.Y, 0.0025, 0.005);

                    // Scissor lift moves out under nest
                    time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], 0.52, Movement.DIRECTION.Y, 0.15, 0.5);
                    MoveRelativeTogether(xbot_ids[6], xbot_ids[7], 0.52, Movement.DIRECTION.Y, 0.15, 0.5);

                    // Scissor lift seperates
                    time += MoveOpposite(xbot_ids[6], xbot_ids[4], -0.06, Movement.DIRECTION.X, 0.01, 0.01);
                    MoveOpposite(xbot_ids[7], xbot_ids[5], -0.06, Movement.DIRECTION.X, 0.01, 0.01);

                    break;
                case '\u001b': //escape key
                    return; //exit the program

            }
        }


    }
}
