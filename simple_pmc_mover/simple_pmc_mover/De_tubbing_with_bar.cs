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
        WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

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
            XBotStatus status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
            XBotStatus gear_status = _xbotCommand.GetXbotStatus(xbot_ids[0]);

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

                    for (int i = 0; i<10; i++)
                    {

                        //-----------------Tub Is carriered out of magasin and the nest is lifted form the tub--------------//
                        // Lift the tub out of nest
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], -0.015, Movement.DIRECTION.Y, 0.01, 0.01);
                        MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], 0.015, Movement.DIRECTION.Y, 0.01, 0.01);

                        // Move tub to the denester
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], 0.495, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(1, xbot_ids[6], xbot_ids[7], 0.495, Movement.DIRECTION.Y, 0.15, 0.5);

                        // We now have to wait for all of these motions in order to sync up the gear lifts
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
                        MoveRelativeTogether(0, xbot_ids[0], xbot_ids[1], -0.015, Movement.DIRECTION.X, 0.15, 0.5);
                        MoveRelativeTogether(0, xbot_ids[2], xbot_ids[3], 0.015, Movement.DIRECTION.X, 0.15, 0.5);
                        //Lift the nest
                        MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.093, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);
                        MoveOpposite(2, xbot_ids[2], xbot_ids[3], 0.093, Movement.DIRECTION.Y, unloaded_gearlift_speed, 0.5);

                        // The scissor lifts waits for the gear lift to remove the nest
                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[2];
                        CMD_params.triggerCmdLabel = 2;

                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        /* is this still needed?????
                        // the gear lift lands
                        _xbotCommand.LevitationCommand(xbot_ids[0], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[1], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[3], LEVITATEOPTIONS.LAND);
                        */


                        //---------------The nest is now carryied by the de-tubbing modules.--------------//
                        //---------------The Tub now needs to be placed back into the holder.-------------//
                        // Scissor Lift lowers
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], 0.015, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.5);
                        MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], -0.015, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.5);

                        // ScissorLift moves  out of range for nest
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], -0.250, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], -0.250, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.5);

                        // Scissor lifts the tub
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], -0.015, Movement.DIRECTION.Y, 0.05, 0.1);
                        MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], 0.015, Movement.DIRECTION.Y, 0.05, 0.1);


                        // Scissor lift moves fully back
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], -0.240, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], -0.240, Movement.DIRECTION.Y, 0.15, 0.5);

                        // Scissor lift places the tub
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], 0.015, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.1);
                        MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], -0.015, Movement.DIRECTION.Y, unloaded_scissorlift_speed, 0.1);


                        /* Only needed if the gearlift needs to be landed 
                          
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
                        */

                        //-----------The Tub is now placed and the carriere moves back to retrive the nest.---------------//
                                                
                        // Scissor lift moves under the hanging nest
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], 0.50, Movement.DIRECTION.Y, unloaded_scissorlift_speed * 2, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], 0.50, Movement.DIRECTION.Y, unloaded_scissorlift_speed * 2, 0.5);

                        // Scissor lift seperates
                        MoveOpposite(0, xbot_ids[6], xbot_ids[4], -0.06, Movement.DIRECTION.X, unloaded_scissorlift_speed, 0.5);
                        MoveOpposite(0, xbot_ids[7], xbot_ids[5], -0.06, Movement.DIRECTION.X, unloaded_scissorlift_speed, 0.5);

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
                        MoveOpposite(0, xbot_ids[6], xbot_ids[4], 0.005, Movement.DIRECTION.X, 0.01, 0.01);
                        MoveOpposite(1, xbot_ids[7], xbot_ids[5], 0.005, Movement.DIRECTION.X, 0.01, 0.01);

                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[5];
                        CMD_params.triggerCmdLabel = 1;

                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        //Gear lift release the nest
                        MoveRelativeTogether(0, xbot_ids[0], xbot_ids[1], 0.015, Movement.DIRECTION.X, 0.15, 0.5);
                        MoveRelativeTogether(0, xbot_ids[2], xbot_ids[3], -0.015, Movement.DIRECTION.X, 0.15, 0.5);

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

                        // move the nest back under the gear lift
                        MoveRelativeTogether(1, xbot_ids[4], xbot_ids[5], 0.245, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], 0.245, Movement.DIRECTION.Y, 0.15, 0.5);

                        //---------------------Prosses begin to place the nest back into the tub----------//
                        //---------------------Lift the nest up agian with the gearlifts------------------//

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

                        //Grasp the nest
                        MoveRelativeTogether(0, xbot_ids[0], xbot_ids[1], -0.015, Movement.DIRECTION.X, 0.15, 0.5);
                        MoveRelativeTogether(2, xbot_ids[2], xbot_ids[3], 0.015, Movement.DIRECTION.X, 0.15, 0.5);

                        // wait for the gear to lift
                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[3];
                        CMD_params.triggerCmdLabel = 2;

                        _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[6], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[7], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        // release the nest from the scissor lift
                        MoveOpposite(0, xbot_ids[6], xbot_ids[4], -0.005, Movement.DIRECTION.X, 0.01, 0.01);
                        MoveOpposite(1, xbot_ids[7], xbot_ids[5], -0.005, Movement.DIRECTION.X, 0.01, 0.01);


                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[5];
                        CMD_params.triggerCmdLabel = 1;

                        _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        // Lift the nest
                        MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.140, Movement.DIRECTION.Y, 0.01, 0.02);
                        MoveOpposite(2, xbot_ids[2], xbot_ids[3], 0.140, Movement.DIRECTION.Y, 0.01, 0.02);

                        /* Dont know if need
                        _xbotCommand.LevitationCommand(xbot_ids[0], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[1], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LAND);
                        _xbotCommand.LevitationCommand(xbot_ids[3], LEVITATEOPTIONS.LAND);
                        */

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

                        //-----------------------Get the tub form the magasin----------------------//
                        // the scissor lifts moves closer to prepare for a tub lift
                        MoveOpposite(0, xbot_ids[6], xbot_ids[4], 0.06, Movement.DIRECTION.X, unloaded_gearlift_speed * 5, 1);
                        MoveOpposite(0, xbot_ids[7], xbot_ids[5], 0.06, Movement.DIRECTION.X, unloaded_scissorlift_speed * 5, 1);

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
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], 0.250, Movement.DIRECTION.Y, 0.20, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], 0.250, Movement.DIRECTION.Y, 0.20, 0.5);

                        //--------------------Place the nest In the tub---------------------//

                        // Lift the tub under the nest
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[6], -0.015, Movement.DIRECTION.Y, 0.005, 0.005);
                        MoveRelativeTogether(0, xbot_ids[5], xbot_ids[7], 0.015, Movement.DIRECTION.Y, 0.005, 0.005);

                        /*
                        status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        while (status.XBOTState != XBOTSTATE.XBOT_IDLE)
                        {
                            status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        }

                        _xbotCommand.LevitationCommand(xbot_ids[3], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[0], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[1], LEVITATEOPTIONS.LEVITATE);
                        _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LEVITATE);
                        */

                        // lower the nest back into the tub
                        MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.093, Movement.DIRECTION.Y, 0.01, 0.01);
                        MoveOpposite(0, xbot_ids[2], xbot_ids[3], -0.093, Movement.DIRECTION.Y, 0.01, 0.01);

                        // Release the nest
                        MoveRelativeTogether(0, xbot_ids[0], xbot_ids[1], 0.015, Movement.DIRECTION.X, 0.15, 0.5);
                        MoveRelativeTogether(0, xbot_ids[2], xbot_ids[3], -0.015, Movement.DIRECTION.X, 0.15, 0.5);

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

                        //-----------------Move the filled tub back into the magasin-----------------//

                        // Scissor lift moves fully back
                        MoveRelativeTogether(0, xbot_ids[4], xbot_ids[5], -0.49, Movement.DIRECTION.Y, 0.15, 0.5);
                        MoveRelativeTogether(0, xbot_ids[6], xbot_ids[7], -0.49, Movement.DIRECTION.Y, 0.15, 0.5);

                        // Scissor lift places the tub                     
                        _xbotCommand.LinearMotionSI(2, xbot_ids[4], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.015, 0, 0.5, 1);
                        _xbotCommand.LinearMotionSI(2, xbot_ids[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.015, 0, 0.5, 1);
                        _xbotCommand.LinearMotionSI(2, xbot_ids[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.015, 0, 0.5, 1);
                        _xbotCommand.LinearMotionSI(2, xbot_ids[7], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.015, 0, 0.5, 1);


                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[3];
                        CMD_params.triggerCmdLabel = 2;
                        
                        gear_status = _xbotCommand.GetXbotStatus(xbot_ids[0]);
                        status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                        while (status.XBOTState != XBOTSTATE.XBOT_IDLE && gear_status.XBOTState != XBOTSTATE.XBOT_IDLE)
                        {
                            status = _xbotCommand.GetXbotStatus(xbot_ids[6]);
                            gear_status = _xbotCommand.GetXbotStatus(xbot_ids[0]);
                        }

                    }

                    break;
                case '4':
                    runDeTubbingWithBarStepByStep(XID);
                    break;

                case '\u001b': //escape key
                    return; //exit the program

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

                
                /*
                case '7':
                    _xbotCommand.LinearMotionSI(0, xbot_ids[8], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.120, 0.120, 0, 0.1, 0.1);
                    break;
                case '8':
                    _xbotCommand.RotaryMotionP2P(2, XID[8], ROTATIONMODE.WRAP_TO_2PI_CW, 1.5707, 5, 2, POSITIONMODE.RELATIVE);
                    break;

                case '9':
                    _xbotCommand.RotaryMotionP2P(1, XID[8], ROTATIONMODE.WRAP_TO_2PI_CCW, 1.5707, 5, 2, POSITIONMODE.RELATIVE);
                    break;
                */
                case '\u001b': //escape key
                    return; //exit the program


            }

        }

    }
}
