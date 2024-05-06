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

        double unloaded_gearlift_speed = 0.2;
        double unloaded_scissorlift_speed = 0.2;

        double loaded_gearlift_speed;
        double loaded_scissorlift_speed;

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

                    _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.1065, 0.713 , 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.1065, 0.579 , 0, 0.1, 0.1);

                    _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.6165, 0.720 - 0.020 , 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.6165, 0.586 - 0.020 , 0, 0.1, 0.1);



                    break;

                case '3':
                    
                    break;

                case '4':
                    //Lift the nest
                    MoveOpposite(xbot_ids[0], xbot_ids[1], 0.093, Movement.DIRECTION.Y, 0.005, 0.01);
                    MoveOpposite(xbot_ids[2], xbot_ids[3], 0.093, Movement.DIRECTION.Y, 0.005, 0.01);


                    break;
                case '5':
                    //Lower the nest
                    MoveOpposite(xbot_ids[0], xbot_ids[1], -0.093, Movement.DIRECTION.Y, 0.005, 0.01);
                    MoveOpposite(xbot_ids[2], xbot_ids[3], -0.093, Movement.DIRECTION.Y, 0.005, 0.01);
                    break;

                case '6':
                    // move together denester together
                    MoveOpposite(xbot_ids[2], xbot_ids[0], 0.01, Movement.DIRECTION.X, 0.01, 0.01);
                    MoveOpposite(xbot_ids[3], xbot_ids[1], 0.01, Movement.DIRECTION.X, 0.01, 0.01);
                    break;

                case '7':
                    // move denester appart
                    MoveOpposite(xbot_ids[2], xbot_ids[0], -0.01, Movement.DIRECTION.X, 0.01, 0.01);
                    MoveOpposite(xbot_ids[3], xbot_ids[1], -0.01, Movement.DIRECTION.X, 0.01, 0.01);
                    break;

                case '2':
                    WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
                    double time = 0;
                    double time_gear = 0;
                    for (int i = 0; i < 100; i++)
                    {
                        // full Demo
                        // we assume that we always start after the tubs and gearlifts have been reset using their respective
                        // Initial position commands
                        Console.WriteLine("we begin baby");

                        /*
                        for (int j = 1; j < 9; j++) {
                            _xbotCommand.MotionBufferControl(j, MOTIONBUFFEROPTIONS.CLEARBUFFER);
                        }
                        */

                        // Lift the tub out of nest
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], -0.015, Movement.DIRECTION.Y, 0.01, 0.01);
                        MoveRelativeTogether(xbot_ids[5], xbot_ids[7], 0.015, Movement.DIRECTION.Y, 0.01, 0.01);

                        // Move tub to the denester
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], 0.49, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(xbot_ids[6], xbot_ids[7], 0.49, Movement.DIRECTION.Y, 0.15, 0.5);

                        // We now have to wait for all of these motions in order to sync up the gear lifts
                        time_params.delaySecs = time;

                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);

                        time = 0;
                        // Lower the hooks
                        time += MoveOpposite(xbot_ids[0], xbot_ids[1], -0.093, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);
                        MoveOpposite(xbot_ids[2], xbot_ids[3], -0.093, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);

                        // Grasp the nest
                        time += MoveOpposite(xbot_ids[2], xbot_ids[0], 0.008, Movement.DIRECTION.X, 0.01, 0.01);
                        MoveOpposite(xbot_ids[3], xbot_ids[1], 0.008, Movement.DIRECTION.X, 0.01, 0.01);

                        // Lift the nest
                        MoveOpposite(xbot_ids[0], xbot_ids[1], 0.093, Movement.DIRECTION.Y, 0.01, 0.01);
                        time += MoveOpposite(xbot_ids[2], xbot_ids[3], 0.093, Movement.DIRECTION.Y, 0.01, 0.01);




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

                        //------------ GEAR LIFT IS NOW CARRYING THE NEST ---------------//

                        // Scissor Lift lowers
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], 0.015, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.5);
                        MoveRelativeTogether(xbot_ids[5], xbot_ids[7], -0.015, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.5);

                        // ScissorLift moves  out of range for nest
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.245, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.5);
                        MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.245, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.5);

                        // Scissor lifts the tub
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], -0.015, Movement.DIRECTION.Y, 0.05, 0.1);
                        MoveRelativeTogether(xbot_ids[5], xbot_ids[7], 0.015, Movement.DIRECTION.Y, 0.05, 0.1);


                        // Scissor lift moves fully back
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.245, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.245, Movement.DIRECTION.Y, 0.15, 0.5);

                        // Scissor lift places the tub
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], 0.015, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.1);
                        MoveRelativeTogether(xbot_ids[5], xbot_ids[7], -0.015, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.1);

                        // Scissor lift moves under the hanging nest
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], 0.50, Movement.DIRECTION.Y, unloaded_scissorlift_speed * 2, 0.5);
                        MoveRelativeTogether(xbot_ids[6], xbot_ids[7], 0.50, Movement.DIRECTION.Y, unloaded_scissorlift_speed * 2, 0.5);

                        // Scissor lift seperates
                        time += MoveOpposite(xbot_ids[6], xbot_ids[4], -0.06, Movement.DIRECTION.X, unloaded_scissorlift_speed, 0.5);
                        MoveOpposite(xbot_ids[7], xbot_ids[5], -0.06, Movement.DIRECTION.X, unloaded_scissorlift_speed, 0.5);

                        // homemade delay function as the wait untill command does not work for landed
                        XBotStatus status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        while (status.XBOTState != XBOTSTATE.XBOT_IDLE)
                        {
                            status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        }




                        //levitate the gear lift
                        _xbotCommand.LevitationCommand(xbot_ids[3], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[0], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[1], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LEVITATE);

                        time = 0;

                        //lift the scissor up again
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], -0.025, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.005);
                        MoveRelativeTogether(xbot_ids[5], xbot_ids[7], 0.025, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.005);
                        time_params.delaySecs = time;
                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);

                        //Lower the gear
                        time_gear = MoveOpposite(xbot_ids[0], xbot_ids[1], -0.140, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);
                        MoveOpposite(xbot_ids[2], xbot_ids[3], -0.140, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);

                        time_params.delaySecs = time_gear + 0.5;
                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.TIME_DELAY, time_params);


                        time = 0;
                        time_gear = 0;

                        //grasp the nest with the scissor
                        time += MoveOpposite(xbot_ids[6], xbot_ids[4], 0.005, Movement.DIRECTION.X, 0.01, 0.01) + 0.5;
                        MoveOpposite(xbot_ids[7], xbot_ids[5], 0.005, Movement.DIRECTION.X, 0.01, 0.01);



                        time_params.delaySecs = time;
                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);
                        time = 0;
                        //gear lift release
                        time_gear += MoveOpposite(xbot_ids[2], xbot_ids[0], -0.008, Movement.DIRECTION.X, unloaded_gearlift_speed, 0.1);
                        MoveOpposite(xbot_ids[3], xbot_ids[1], -0.008, Movement.DIRECTION.X, unloaded_gearlift_speed, 0.1);

                        //raise the gear 
                        time_gear += MoveOpposite(xbot_ids[0], xbot_ids[1], 0.140, Movement.DIRECTION.Y, 0.1, 0.1);
                        MoveOpposite(xbot_ids[2], xbot_ids[3], 0.140, Movement.DIRECTION.Y, 0.1, 0.1);

                        // wait for the gear to lift
                        time_params.delaySecs = time_gear;
                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.TIME_DELAY, time_params);
                        time_gear = 0;
                        time = 0;

                        //------------------- NEST PLACED IN SCISSOR LIFT -----------------------------//

                        // The nest out into open space
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.245, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.245, Movement.DIRECTION.Y, 0.15, 0.5);

                        // move the nest back under the gear lift
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], 0.245, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(xbot_ids[6], xbot_ids[7], 0.245, Movement.DIRECTION.Y, 0.15, 0.5);

                        //make the gear lifts wait for the scissor lift

                        time_params.delaySecs = time;
                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);
                        time = 0;

                        // lower the gear lift
                        time_gear = MoveOpposite(xbot_ids[0], xbot_ids[1], -0.140, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);
                        MoveOpposite(xbot_ids[2], xbot_ids[3], -0.140, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);


                        // Grasp the nest
                        time_gear += MoveOpposite(xbot_ids[2], xbot_ids[0], 0.008, Movement.DIRECTION.X, 0.01, 0.01);
                        MoveOpposite(xbot_ids[3], xbot_ids[1], 0.008, Movement.DIRECTION.X, 0.01, 0.01);

                        time_params.delaySecs = time_gear - 1;
                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.TIME_DELAY, time_params);
                        time_gear = 0;

                        // release the nest from the scissor lift
                        time += MoveOpposite(xbot_ids[6], xbot_ids[4], -0.005, Movement.DIRECTION.X, 0.01, 0.01);
                        MoveOpposite(xbot_ids[7], xbot_ids[5], -0.005, Movement.DIRECTION.X, 0.01, 0.01);

                        time_params.delaySecs = time + 1;
                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);
                        time = 0;

                        //--------------------------------ERRORS HAPPEn HERE------------------//
                        // Lift the nest
                        MoveOpposite(xbot_ids[0], xbot_ids[1], 0.140, Movement.DIRECTION.Y, 0.01, 0.02);
                        time_gear += MoveOpposite(xbot_ids[2], xbot_ids[3], 0.140, Movement.DIRECTION.Y, 0.01, 0.02);
                        _xbotCommand.LevitationCommand(xbot_ids[0], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[1], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[3], LEVITATEOPTIONS.LAND);

                        time_params.delaySecs = time_gear;
                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.TIME_DELAY, time_params);
                        time_gear = 0;

                        //lower the scissor lift
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], 0.025, Movement.DIRECTION.Y, unloaded_scissorlift_speed * 5, 1);
                        MoveRelativeTogether(xbot_ids[5], xbot_ids[7], -0.025, Movement.DIRECTION.Y, unloaded_scissorlift_speed * 5, 1);

                        //------------------------------ERRORS END HERE --------------------------//

                        time_params.delaySecs = time_gear - time;
                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.TIME_DELAY, time_params);
                        time_gear = 0;
                        time = 0;

                        // the scissor lifts moves closer to prepare for a tub lift
                        time += MoveOpposite(xbot_ids[6], xbot_ids[4], 0.06, Movement.DIRECTION.X, unloaded_gearlift_speed * 5, 1);
                        MoveOpposite(xbot_ids[7], xbot_ids[5], 0.06, Movement.DIRECTION.X, unloaded_scissorlift_speed * 5, 1);

                        // move the scissor lift under the tub
                        _xbotCommand.LinearMotionSI(0, xbot_ids[4], POSITIONMODE.ABSOLUTE, 0, 0.295, 0.252, 0, 1, 2);
                        _xbotCommand.LinearMotionSI(0, xbot_ids[5], POSITIONMODE.ABSOLUTE, 0, 0.295, 0.073, 0, 1, 2);
                        _xbotCommand.LinearMotionSI(0, xbot_ids[6], POSITIONMODE.ABSOLUTE, 0, 0.425, 0.252, 0, 1, 2);
                        MotionRtn temp = _xbotCommand.LinearMotionSI(0, xbot_ids[7], POSITIONMODE.ABSOLUTE, 0, 0.425, 0.073, 0, 1, 2);
                        time += temp.TravelTimeSecs;

                        // Lift the tub out of magazine
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], -0.015, Movement.DIRECTION.Y, 0.005, 0.005);
                        MoveRelativeTogether(xbot_ids[5], xbot_ids[7], 0.015, Movement.DIRECTION.Y, 0.005, 0.005);

                        // Move tub out of magazine
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], 0.240, Movement.DIRECTION.Y, 0.2, 0.5);
                        MoveRelativeTogether(xbot_ids[6], xbot_ids[7], 0.240, Movement.DIRECTION.Y, 0.2, 0.5);

                        // lower the tub to pass under the syringes
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], 0.015, Movement.DIRECTION.Y, 0.005, 0.005);
                        MoveRelativeTogether(xbot_ids[5], xbot_ids[7], -0.015, Movement.DIRECTION.Y, 0.005, 0.005);

                        // move all the way under the denester
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], 0.250, Movement.DIRECTION.Y, 0.20, 0.5);
                        MoveRelativeTogether(xbot_ids[6], xbot_ids[7], 0.250, Movement.DIRECTION.Y, 0.20, 0.5);

                        // Lift the tub out of nest
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[6], -0.015, Movement.DIRECTION.Y, 0.005, 0.005);
                        MoveRelativeTogether(xbot_ids[5], xbot_ids[7], 0.015, Movement.DIRECTION.Y, 0.005, 0.005);


                        status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        while (status.XBOTState != XBOTSTATE.XBOT_IDLE)
                        {
                            status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        }

                        _xbotCommand.LevitationCommand(xbot_ids[3], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[0], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[1], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LEVITATE);


                        //gear lift waits until tub is under the nest
                        /*
                        time_params.delaySecs = time;
                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);
                        time = 0;
                        */
                        // lower the nest back into the tub
                        time_gear += MoveOpposite(xbot_ids[0], xbot_ids[1], -0.093, Movement.DIRECTION.Y, 0.01, 0.01);
                        MoveOpposite(xbot_ids[2], xbot_ids[3], -0.093, Movement.DIRECTION.Y, 0.01, 0.01);

                        // Release the nest
                        time_gear += MoveOpposite(xbot_ids[2], xbot_ids[0], -0.008, Movement.DIRECTION.X, 0.01, 0.01);
                        time_gear += MoveOpposite(xbot_ids[3], xbot_ids[1], -0.008, Movement.DIRECTION.X, 0.01, 0.01);

                        // Raise the gear lifts again
                        time_gear += MoveOpposite(xbot_ids[0], xbot_ids[1], 0.093, Movement.DIRECTION.Y, 0.1, 0.3);
                        time_gear += MoveOpposite(xbot_ids[2], xbot_ids[3], 0.093, Movement.DIRECTION.Y, 0.1, 0.3);


                        //scissor lift waits for the gear lift
                        time_params.delaySecs = time_gear + 1;
                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.TIME_DELAY, time_params);
                        time_gear = 0;




                        // Scissor lift moves fully back
                        time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.49, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.49, Movement.DIRECTION.Y, 0.15, 0.5);

                        // Scissor lift places the tub
                        
                        
                        _xbotCommand.LinearMotionSI(2, xbot_ids[4], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.015, 0, 0.5, 1);
                        _xbotCommand.LinearMotionSI(2, xbot_ids[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.015, 0, 0.5, 1);
                        _xbotCommand.LinearMotionSI(2, xbot_ids[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.015, 0, 0.5, 1);
                        _xbotCommand.LinearMotionSI(2, xbot_ids[7], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.015, 0, 0.5, 1);
                        

                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        time_params.triggerXbotID = xbot_ids[3];
                        time_params.triggerCmdLabel = 3;

                        time_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        time_params.triggerXbotID = xbot_ids[7];
                        time_params.triggerCmdLabel = 2;

                       
                        _xbotCommand.WaitUntil(3, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, time_params);
                        _xbotCommand.WaitUntil(3, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, time_params);
                        _xbotCommand.WaitUntil(3, xbot_ids[2], TRIGGERSOURCE.CMD_LABEL, time_params);
                        _xbotCommand.WaitUntil(3, xbot_ids[3], TRIGGERSOURCE.CMD_LABEL, time_params);

                        _xbotCommand.WaitUntil(4, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(4, xbot_ids[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(4, xbot_ids[6], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(4, xbot_ids[7], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        
                        time = 0;


                        Console.WriteLine("we have reached the end");
                        XBotStatus gear_status = _xbotCommand.GetXbotStatus(xbot_ids[0]);
                        status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        while (status.XBOTState != XBOTSTATE.XBOT_IDLE && gear_status.XBOTState != XBOTSTATE.XBOT_IDLE)
                        {
                            status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                            gear_status = _xbotCommand.GetXbotStatus(xbot_ids[0]);
                            

                        }
                        
                        
                    }
                    //time += MoveRelativeTogether(xbot_ids[4], xbot_ids[5], -0.245, Movement.DIRECTION.Y, 0.15, 0.5);
                    //MoveRelativeTogether(xbot_ids[6], xbot_ids[7], -0.245, Movement.DIRECTION.Y, 0.15, 0.5);
                    //time += MoveRelativeTogether(xbot_ids[0], xbot_ids[1], -0.245, Movement.DIRECTION.Y, 0.15, 0.5);
                    //MoveRelativeTogether(xbot_ids[2], xbot_ids[3], -0.245, Movement.DIRECTION.Y, 0.15, 0.5);


                    break;
                case '\u001b': //escape key
                    return; //exit the program

            }
        }


    }
}
