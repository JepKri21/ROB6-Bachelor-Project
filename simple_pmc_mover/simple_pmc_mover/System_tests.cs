﻿using PMCLIB;
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
            Console.WriteLine("2    start position for scissor lift");
            Console.WriteLine("3    DEMO");
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

                    _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.102, 0.713, 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.102, 0.579, 0, 0.1, 0.1);

                    _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.616, 0.724 - 0.020, 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.616, 0.590 - 0.020, 0, 0.1, 0.1);



                    break;

                case '2':
                    initialPosition(4, xbot_ids[4], xbot_ids[5], xbot_ids[6], xbot_ids[7]);
                    break;

                case '4':
                    //Lift the nest
                    MoveOpposite(0,xbot_ids[0], xbot_ids[1], 0.093, Movement.DIRECTION.Y, 0.005, 0.01);
                    MoveOpposite(0, xbot_ids[2], xbot_ids[3], 0.093, Movement.DIRECTION.Y, 0.005, 0.01);


                    break;
                case '5':
                    //Lower the nest
                    MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.093, Movement.DIRECTION.Y, 0.005, 0.01);
                    MoveOpposite(0, xbot_ids[2], xbot_ids[3], -0.093, Movement.DIRECTION.Y, 0.005, 0.01);
                    break;

                case '6':
                    // move together denester together
                    MoveOpposite(0,xbot_ids[2], xbot_ids[0], 0.08, Movement.DIRECTION.X, 0.01, 0.01);
                    MoveOpposite(0,xbot_ids[3], xbot_ids[1], 0.08, Movement.DIRECTION.X, 0.01, 0.01);
                    break;

                case '7':
                    // move denester appart
                    MoveOpposite(0, xbot_ids[2], xbot_ids[0], -0.08, Movement.DIRECTION.X, 0.01, 0.01);
                    MoveOpposite(0, xbot_ids[3], xbot_ids[1], -0.08, Movement.DIRECTION.X, 0.01, 0.01);
                    break;

                case '3':
                    
                    WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
                    
                    for (int i = 0; i < 100; i++)
                    {
                        // full Demo
                        // we assume that we always start after the tubs and gearlifts have been reset using their respective
                        // Initial position commands
                        Console.WriteLine("Movement starts, Iterations: " + (i+1));
                        /*
                        _xbotCommand.LinearMotionSI(0, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.295, 0.252, 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, xbot_ids[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.295, 0.73, 0, 0.1, 0.1);

                        _xbotCommand.LinearMotionSI(0, xbot_ids[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.425, 0.252, 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, xbot_ids[7], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.425, 0.73, 0, 0.1, 0.1);


                        time_params.delaySecs = 5;
                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.CMD_LABEL, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.CMD_LABEL, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.CMD_LABEL, time_params);


                        _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.102, 0.713, 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.102, 0.579, 0, 0.1, 0.1);

                        _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.616, 0.724 - 0.020, 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.616, 0.590 - 0.020, 0, 0.1, 0.1);

                        */

                        


                        // Lift the tub out of nest
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], -0.015, Movement.DIRECTION.Y, 0.01, 0.01);
                        MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], 0.015, Movement.DIRECTION.Y, 0.01, 0.01);

                        // Move tub to the denester
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], 0.49, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(1, xbot_ids[6], xbot_ids[7], 0.49, Movement.DIRECTION.Y, 0.15, 0.5);

                        // We now have to wait for all of these motions in order to sync up the gear lifts
                        //time_params.delaySecs = time;
                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[6];
                        CMD_params.triggerCmdLabel = 1;

                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        // Lower the hooks
                        MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.093, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);
                        MoveOpposite(0, xbot_ids[2], xbot_ids[3], -0.093, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);

                        // Grasp the nest
                        MoveOpposite(0, xbot_ids[2], xbot_ids[0], 0.007, Movement.DIRECTION.X, 0.01, 0.01);
                        MoveOpposite(0, xbot_ids[3], xbot_ids[1], 0.007, Movement.DIRECTION.X, 0.01, 0.01);

                        // Lift the nest
                        MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.093, Movement.DIRECTION.Y, 0.02, 0.01);
                        MoveOpposite(2, xbot_ids[2], xbot_ids[3], 0.093, Movement.DIRECTION.Y, 0.02, 0.01);

                        // The scissor lifts waits for the gear lift to remove the nest
                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[2];
                        CMD_params.triggerCmdLabel = 2;

                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.CMD_LABEL, CMD_params);


                        // the gear lift lands
                        _xbotCommand.LevitationCommand(xbot_ids[0], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[1], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[3], LEVITATEOPTIONS.LAND);

                        // Scissor Lift lowers
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], 0.015, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.5);
                        MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], -0.015, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.5);

                        // ScissorLift moves  out of range for nest
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], -0.250, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], -0.250, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.5);

                        // Scissor lifts the tub
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], -0.015, Movement.DIRECTION.Y, 0.035, 0.2);
                        MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], 0.015, Movement.DIRECTION.Y, 0.035, 0.2);


                        // Scissor lift moves fully back
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], -0.240, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], -0.240, Movement.DIRECTION.Y, 0.15, 0.5);

                        // Scissor lift places the tub
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], 0.015, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.1);
                        MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], -0.015, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.1);

                        // Scissor lift moves under the hanging nest
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], 0.50, Movement.DIRECTION.Y, unloaded_scissorlift_speed * 2, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], 0.50, Movement.DIRECTION.Y, unloaded_scissorlift_speed * 2, 0.5);

                        // Scissor lift seperates
                        MoveOpposite(0, xbot_ids[6], xbot_ids[4], -0.06, Movement.DIRECTION.X, unloaded_scissorlift_speed, 0.5);
                        MoveOpposite(0, xbot_ids[7], xbot_ids[5], -0.06, Movement.DIRECTION.X, unloaded_scissorlift_speed, 0.5);

                        // homemade delay function as the wait untill command does not work for landed
                        XBotStatus status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        while (status.XBOTState != XBOTSTATE.XBOT_IDLE)
                        {
                            status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        }


                        //levitate the gear lift
                        _xbotCommand.LevitationCommand(xbot_ids[0], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[1], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[3], LEVITATEOPTIONS.LEVITATE);

                        //lift the scissor up again
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], -0.025, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.005);
                        MoveRelativeTogether(1, xbot_ids[5], xbot_ids[7], 0.025, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.005);

                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[5];
                        CMD_params.triggerCmdLabel = 1;

                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        //Lower the gear
                        MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.140, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);
                        MoveOpposite(2, xbot_ids[2], xbot_ids[3], -0.140, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);

                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[2];
                        CMD_params.triggerCmdLabel = 2;

                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        //grasp the nest with the scissor
                        MoveOpposite(0, xbot_ids[6], xbot_ids[4], 0.007, Movement.DIRECTION.X, 0.01, 0.01);
                        MoveOpposite(1, xbot_ids[7], xbot_ids[5], 0.007, Movement.DIRECTION.X, 0.01, 0.01);

                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[5];
                        CMD_params.triggerCmdLabel = 1;

                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);


                        //gear lift release
                        MoveOpposite(0, xbot_ids[2], xbot_ids[0], -0.008, Movement.DIRECTION.X, unloaded_gearlift_speed, 0.1);
                        MoveOpposite(0, xbot_ids[3], xbot_ids[1], -0.008, Movement.DIRECTION.X, unloaded_gearlift_speed, 0.1);
                        
                        //raise the gear 
                        MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.140, Movement.DIRECTION.Y, 0.1, 0.1);
                        MoveOpposite(2, xbot_ids[2], xbot_ids[3], 0.140, Movement.DIRECTION.Y, 0.1, 0.1);

                        // wait for the gear to lift
                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[2];
                        CMD_params.triggerCmdLabel = 2;

                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        //------------------- NEST PLACED IN SCISSOR LIFT -----------------------------//

                        // The nest out into open space
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], -0.245, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], -0.245, Movement.DIRECTION.Y, 0.15, 0.5);

                        time_params.delaySecs = 5;

                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.TIME_DELAY, time_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.TIME_DELAY, time_params);


                        // move the nest back under the gear lift
                        MoveRelativeTogether(1, xbot_ids[4], xbot_ids[5], 0.245, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], 0.245, Movement.DIRECTION.Y, 0.15, 0.5);
                        
                        //make the gear lifts wait for the scissor lift
                        
                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[5];
                        CMD_params.triggerCmdLabel = 1;
                        
                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        // lower the gear lift
                        MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.140, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);
                        MoveOpposite(0, xbot_ids[2], xbot_ids[3], -0.140, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);

                        // Grasp the nest
                        MoveOpposite(0, xbot_ids[2], xbot_ids[0], 0.008, Movement.DIRECTION.X, 0.01, 0.01);
                        MoveOpposite(2, xbot_ids[3], xbot_ids[1], 0.008, Movement.DIRECTION.X, 0.01, 0.01);

                        // wait for the gear to lift
                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[3];
                        CMD_params.triggerCmdLabel = 2;

                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        // release the nest from the scissor lift
                        MoveOpposite(0, xbot_ids[6], xbot_ids[4], -0.009, Movement.DIRECTION.X, 0.015, 0.01);
                        MoveOpposite(1, xbot_ids[7], xbot_ids[5], -0.009, Movement.DIRECTION.X, 0.015, 0.01);

                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[5];
                        CMD_params.triggerCmdLabel = 1;

                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        //--------------------------------ERRORS HAPPEn HERE------------------//
                        // Lift the nest
                        MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.140, Movement.DIRECTION.Y, 0.022, 0.02);
                        MoveOpposite(2, xbot_ids[2], xbot_ids[3], 0.140, Movement.DIRECTION.Y, 0.022, 0.02);


                        _xbotCommand.LevitationCommand(xbot_ids[0], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[1], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[3], LEVITATEOPTIONS.LAND);

                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[3];
                        CMD_params.triggerCmdLabel = 2;
                        
                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        //lower the scissor lift
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], 0.025, Movement.DIRECTION.Y, unloaded_scissorlift_speed * 5, 1);
                        MoveRelativeTogether(1, xbot_ids[5], xbot_ids[7], -0.025, Movement.DIRECTION.Y, unloaded_scissorlift_speed * 5, 1);

                        //------------------------------ERRORS END HERE --------------------------//

                        // the scissor lifts moves closer to prepare for a tub lift
                        MoveOpposite(0, xbot_ids[6], xbot_ids[4], 0.065, Movement.DIRECTION.X, unloaded_gearlift_speed * 5, 1);
                        MoveOpposite(0, xbot_ids[7], xbot_ids[5], 0.065, Movement.DIRECTION.X, unloaded_scissorlift_speed * 5, 1);

                        // move the scissor lift under the tub
                        _xbotCommand.LinearMotionSI(0, xbot_ids[4], POSITIONMODE.ABSOLUTE, 0, 0.295, 0.252, 0, 1, 2);
                        _xbotCommand.LinearMotionSI(0, xbot_ids[5], POSITIONMODE.ABSOLUTE, 0, 0.295, 0.073, 0, 1, 2);
                        _xbotCommand.LinearMotionSI(0, xbot_ids[6], POSITIONMODE.ABSOLUTE, 0, 0.425, 0.252, 0, 1, 2);
                        _xbotCommand.LinearMotionSI(0, xbot_ids[7], POSITIONMODE.ABSOLUTE, 0, 0.425, 0.073, 0, 1, 2);


                        // Lift the tub out of magazine
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], -0.015, Movement.DIRECTION.Y, 0.005, 0.005);
                        MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], 0.015, Movement.DIRECTION.Y, 0.005, 0.005);

                        // Move tub out of magazine
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], 0.240, Movement.DIRECTION.Y, 0.2, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], 0.240, Movement.DIRECTION.Y, 0.2, 0.5);

                        // lower the tub to pass under the syringes
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], 0.015, Movement.DIRECTION.Y, 0.005, 0.005);
                        MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], -0.015, Movement.DIRECTION.Y, 0.005, 0.005);

                        // move all the way under the denester
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], 0.248, Movement.DIRECTION.Y, 0.20, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], 0.248, Movement.DIRECTION.Y, 0.20, 0.5);

                        // Lift the tub out of nest
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], -0.015, Movement.DIRECTION.Y, 0.005, 0.005);
                        MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], 0.015, Movement.DIRECTION.Y, 0.005, 0.005);

                        status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        while (status.XBOTState != XBOTSTATE.XBOT_IDLE)
                        {
                            status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        }

                        _xbotCommand.LevitationCommand(xbot_ids[3], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[0], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[1], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LEVITATE);




                        // lower the nest back into the tub
                        MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.093, Movement.DIRECTION.Y, 0.025, 0.02);
                        MoveOpposite(0, xbot_ids[2], xbot_ids[3], -0.093, Movement.DIRECTION.Y, 0.025, 0.02);

                        // Release the nest
                        MoveOpposite(0, xbot_ids[2], xbot_ids[0], -0.007, Movement.DIRECTION.X, 0.01, 0.01);
                        MoveOpposite(0, xbot_ids[3], xbot_ids[1], -0.007, Movement.DIRECTION.X, 0.01, 0.01);

                        // Raise the gear lifts again
                        MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.093, Movement.DIRECTION.Y, 0.1, 0.3);
                        MoveOpposite(2, xbot_ids[2], xbot_ids[3], 0.093, Movement.DIRECTION.Y, 0.1, 0.3);

                        //scissor lift waits for the gear lift
                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[3];
                        CMD_params.triggerCmdLabel = 2;

                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.CMD_LABEL, CMD_params);


                        // Scissor lift moves fully back
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], -0.489, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], -0.489, Movement.DIRECTION.Y, 0.15, 0.5);

                        // Scissor lift places the tub                     
                        _xbotCommand.LinearMotionSI(2, xbot_ids[4], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.015, 0, 0.5, 1);
                        _xbotCommand.LinearMotionSI(2, xbot_ids[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.015, 0, 0.5, 1);
                        _xbotCommand.LinearMotionSI(2, xbot_ids[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.015, 0, 0.5, 1);
                        _xbotCommand.LinearMotionSI(2, xbot_ids[7], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.015, 0, 0.5, 1);

                       


                        Console.WriteLine("Motions stops");
                        XBotStatus gear_status = _xbotCommand.GetXbotStatus(xbot_ids[0]);
                        status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        while (status.XBOTState != XBOTSTATE.XBOT_IDLE && gear_status.XBOTState != XBOTSTATE.XBOT_IDLE)
                        {
                            status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                            gear_status = _xbotCommand.GetXbotStatus(xbot_ids[0]);
                            

                        }
                        
                        
                    }
                    



                    break;
                case '\u001b': //escape key
                    return; //exit the program

            }
        }

        
        
    }
}
