using PMCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_pmc_mover
{
    internal class Filling_and_inspection : Movement
    {
        private static SystemCommands _systemCommand = new SystemCommands();
        //this class contains a collection of xbot commands, such as discover xbots, mobility control, linear motion, etc.
        private static XBotCommands _xbotCommand = new XBotCommands();

        int selector = 7;
        double[] intialPosUnitCarrier = { 0.120, 0.600, 0.600, 0.360, 0.600, 0.120 };
        double[] intialPosFilling = { 0.295, 0.840, 0.425, 0.840 };
        double[] intialPosUnitWeighing = { 0.600, 0.600 };
        double[] intialPosUnitVisual = { 0.360, 0.360 };

        

        public int setSelectorOne()
        {
            return selector;
        }

        public void fillingWeighingInspectionDemo()
        {

        }

        public void runFillingAndInspection(int[] XID)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();

            int[] xbot_ids = XID;
            selector = 7;
            Console.Clear();
            Console.WriteLine(" Filling and inspection");
            Console.WriteLine("0    Return ");
            Console.WriteLine("1    Initial postion");
            Console.WriteLine("2    DEMO");
            Console.WriteLine("3    Lower filling");
            Console.WriteLine("4    Rise filling");
            Console.WriteLine("5    ");
            Console.WriteLine("6    ");
            Console.WriteLine("7    ");
            Console.WriteLine("9    ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.KeyChar)
            {
                
                case '0':
                selector = 1;
                break;


                    // initial postion
                    //united carrier
                    //X:0.120 Y:600

                    //Filling
                    //x1: 295 X2: 425 y: 840

                    //weighing
                    //x:600 y:600

                    //visual
                    //x: 360 y: 360

                    

                case '1':
                //double[] intialPosUnitCarrier = { 0.120, 0.600, 0.600, 0.360, 0.600, 0.120 };
                //double[] intialPosFilling = { 0.295, 0.840, 0.425, 0.840 };
                //double[] intialPosUnitWeighing = { 0.600, 0.600 };
                //double[] intialPosUnitVisual = { 0.360, 0.360 };
                _xbotCommand.LinearMotionSI(0, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitCarrier[0], intialPosUnitCarrier[1], 0, 0.1, 0.1);
                //_xbotCommand.LinearMotionSI(0, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitCarrier[2], intialPosUnitCarrier[3], 0, 0.1, 0.1);
                //_xbotCommand.LinearMotionSI(0, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitCarrier[4], intialPosUnitCarrier[5], 0, 0.1, 0.1);


                _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosFilling[0], intialPosFilling[1], 0, 0.1, 0.1);
                _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosFilling[2], intialPosFilling[3], 0, 0.1, 0.1);

                _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitWeighing[0], intialPosUnitWeighing[1], 0, 0.1, 0.1);

                _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitVisual[0], intialPosUnitVisual[1], 0, 0.1, 0.1);
                break;

                case '2':
                    for (int y = 0; y<6; y++)  //
                    {   
                        //------------------------------------------------------------------------
                        //------------------------------------------------------------------------
                        //This if-statement nakes sure that the filling station wait for the unit carrier to move into position
                        if (y == 0)
                        {
                            //Unit carrier moves into position
                            _xbotCommand.LinearMotionSI(1, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.360, 0.600, 0, 0.1, 0.1);

                            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                            CMD_params.triggerXbotID = xbot_ids[4];
                            CMD_params.triggerCmdLabel = 1;

                            _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                            _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                            //Filling station moves to unit carrier 
                            _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.363, 0.737, 0, 0.1, 0.1);
                            _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.488, 0.737, 0, 0.1, 0.1);
                        }


                        else if (y > 0) // This else-if makes sure that the unit carrier only spins when the stations are finished
                        {
                            if (y < 4) //Here it spins after the filling station moves away
                            {
                                CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                                CMD_params.triggerXbotID = xbot_ids[1];
                                CMD_params.triggerCmdLabel = 12;

                                _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                            }
                            else if ( y > 3 & y < 6) //Here it spins after the weighing station moves away
                            {
                                CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                                CMD_params.triggerXbotID = xbot_ids[2];
                                CMD_params.triggerCmdLabel = 30;

                                _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                            }
                           
                            //The unit carrier spins and we make wait parameters for the other stations to wait for the spin
                            _xbotCommand.LinearMotionSI(1, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.360, 0.600, 0, 0.1, 0.1);
                            _xbotCommand.RotaryMotionP2P(11, xbot_ids[4], ROTATIONMODE.WRAP_TO_2PI_CW, 1.570796, 1, 2, POSITIONMODE.RELATIVE);

                            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                            CMD_params.triggerXbotID = xbot_ids[4];
                            CMD_params.triggerCmdLabel = 11; 
                         

                            if (y < 4) //Here the filling station waits for the unit carrier spin
                            {
                                _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                                _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                                //Filling station moves to unit carrier 
                                _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.363, 0.737, 0, 0.1, 0.1);
                                _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.488, 0.737, 0, 0.1, 0.1);
                            }

                            if (y < 5) //Here the weighing module waits for the unit carrier spin and moves to the unit carrier after
                            {
                                _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                                // Weighing station moves to unit carrier
                                _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.536, 0.5314, 0, 0.1, 0.1);
                            }

                            if (y > 1) //Here the Visual module waits for the unit carrier spin
                            {
                                if (y == 2) //I have no idea why this makes it work, but the next wait command does not make it wait for the spin.
                                {
                                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                                    CMD_params.triggerXbotID = xbot_ids[4];
                                    CMD_params.triggerCmdLabel = 11;
                                    _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);


                                }
                                
                                //This wait command for some reason doesn't work on the first go of y==2
                                _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                                // visual station moves to unit carrier
                                _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.2916, 0.450, 0, 0.1, 0.1);
                            }
                            
                        }
                        //----------------------------------------------------------------------------------------------------------
                        //----------------------------------------------------------------------------------------------------------
                        for (int i = 0; i < 10; i++) //This for-loop is for all of the stations to move through the 10 syringes
                        {

                            //------------------------Filling----------------------------
                            if (y < 4) //Here, the filling station moves, and it only moves on the first 4 cycles (After the third rotation)
                            {
                                if (i == 9) //This finishes the last filling and moves the filling station away from the unit carrier
                                {
                                    MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.033, Movement.DIRECTION.X, 0.05, 0.01);
                                    time_params.delaySecs = 0.2;

                                    _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                                    _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);

                                    //Filling station moves up from the syringe
                                    MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.033, Movement.DIRECTION.X, 0.05, 0.01);

                                   
                                        
                                    _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosFilling[0], intialPosFilling[1], 0, 0.1, 0.1);
                                    _xbotCommand.LinearMotionSI(12, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosFilling[2], intialPosFilling[3], 0, 0.1, 0.1);
                                    
                                }
                                else //This completes all the first 9 fillings 
                                {
                                    MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.033, Movement.DIRECTION.X, 0.05, 0.01);
                                    time_params.delaySecs = 0.2;

                                    _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                                    _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);

                                    //Filling station moves up from the syringe
                                    MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.033, Movement.DIRECTION.X, 0.05, 0.01);

                                    MoveRelativeTogether(4, xbot_ids[0], xbot_ids[1], -0.0152, Movement.DIRECTION.X, 0.15, 0.5);
                                }
                            }

                            //-----------------------------Weighing----------------------------------
                            if (0<y & y<5) //Here, the Weighing station moves, and it only moves on the 2nd to 5th cycles
                            {
                                if (i == 9) //This finishes the last weighing and moves the weghing station away from the unit carrier
                                {
                                    _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.0152, 0, 0.1, 0.1);
                                    _xbotCommand.ShortAxesMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, 0.004, 0, 0, 0, 0.1, 0, 0, 0);
                                    time_params.delaySecs = 2;

                                    _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);

                                    _xbotCommand.ShortAxesMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, 0.001, 0, 0, 0, 0.1, 0, 0, 0);

                                    _xbotCommand.LinearMotionSI(30, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitWeighing[0], intialPosUnitWeighing[1], 0, 0.1, 0.1);

                                }
                                else //This completes all the first 9 fillings 
                                {
                                    if (i>0) //This is because the initial move moves under the first syringe so it doesn't have to do it on the first lap of the for-loop
                                    {
                                        _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.0152, 0, 0.1, 0.1);

                                    }
                                    _xbotCommand.ShortAxesMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, 0.004, 0, 0, 0, 0.1, 0, 0, 0);
                                    time_params.delaySecs = 2;

                                    _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);

                                    _xbotCommand.ShortAxesMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, 0.001, 0, 0, 0, 0.1, 0, 0, 0);
                                }
                            }

                            //Visual
                            if (y > 1)
                            {
                                if (i==9)
                                {
                                    //Last move and move away from unit carrier
                                    _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.RELATIVE, LINEARPATHTYPE.XTHENY, 0.0152, 0, 0, 0.1, 0.1);
                                    
                                    time_params.delaySecs = 2;

                                    _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);
                                    _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosUnitVisual[0], intialPosUnitVisual[1], 0, 0.1, 0.1);
                                }
                                else
                                {
                                    if (i == 0)
                                    {
                                        time_params.delaySecs = 2;

                                        _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);
                                    }
                                    //All the previous moves of the visual inspection
                                    _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.RELATIVE, LINEARPATHTYPE.XTHENY, 0.0152, 0, 0, 0.1, 0.1);

                                    time_params.delaySecs = 2;

                                    _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);
                                }
                            }

                            
                        }

                        /*
                        //Filling station waits for the unit carrier 
                        _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.363, 0.737, 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.488, 0.737, 0, 0.1, 0.1);


                        for (int i = 0; i < 10; i++)
                        {
                            
                            if (i == 9)
                            {
                                if (y > 0)
                                {
                                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_START;
                                    CMD_params.triggerXbotID = xbot_ids[2];
                                    CMD_params.triggerCmdLabel = 5;

                                    _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                                    _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                                }
                                
                                //Filling station moves down into the syringes
                                MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.033, Movement.DIRECTION.X, 0.005, 0.01);

                                time_params.delaySecs = 1;

                                _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                                _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);

                                //Filling station moves up from the syringe
                                MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.033, Movement.DIRECTION.X, 0.005, 0.01);

                                _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosFilling[0], intialPosFilling[1], 0, 0.1, 0.1);
                                _xbotCommand.LinearMotionSI(12, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosFilling[2], intialPosFilling[3], 0, 0.1, 0.1);
                            }
                            else
                            {
                                if (y > 0)
                                {
                                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_START;
                                    CMD_params.triggerXbotID = xbot_ids[2];
                                    CMD_params.triggerCmdLabel = 5;

                                    _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                                    _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                                }
                                //Filling station moves down into the syringes
                                MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.033, Movement.DIRECTION.X, 0.05, 0.01);

                                time_params.delaySecs = 1;

                                _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                                _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);

                                //Filling station moves up from the syringe
                                MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.033, Movement.DIRECTION.X, 0.05, 0.01);
                                //Filling station moves to the next syringe
                                MoveRelativeTogether(4, xbot_ids[0], xbot_ids[1], -0.0152, Movement.DIRECTION.X, 0.15, 0.5);
                            }
                            




                            //When the first row has been filled
                            if (y > 0)
                            {
                                //------------------------- Nów the weighing station
                                //It moves right under the syrige

                                if (y > 1)
                                {
                                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                                    CMD_params.triggerXbotID = xbot_ids[4];
                                    CMD_params.triggerCmdLabel = 11;

                                    _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                                }


                                _xbotCommand.LinearMotionSI(5, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.536, 0.5314, 0, 0.1, 0.1);
                                for (int j = 0; j < 9; j++)
                                {
                                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_START;
                                    CMD_params.triggerXbotID = xbot_ids[1];
                                    CMD_params.triggerCmdLabel = 4;

                                    _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                                    _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                                    
                                    _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LAND);
                                    _xbotCommand.LevitationCommand(xbot_ids[4], LEVITATEOPTIONS.LAND);

                                    XBotStatus status = _xbotCommand.GetXbotStatus(xbot_ids[0]);
                                    while (status.XBOTState != XBOTSTATE.XBOT_MOTION)
                                    {
                                        //take messurement
                                        status = _xbotCommand.GetXbotStatus(xbot_ids[0]);
                                    }                                   
                                                                       
                                    _xbotCommand.LevitationCommand(xbot_ids[2], LEVITATEOPTIONS.LEVITATE);
                                    _xbotCommand.LevitationCommand(xbot_ids[4], LEVITATEOPTIONS.LEVITATE);

                                    _xbotCommand.LinearMotionSI(5, xbot_ids[2], POSITIONMODE.RELATIVE,LINEARPATHTYPE.YTHENX, 0, 0.0152, 0, 0.1, 0.1);

                                }

                                _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitWeighing[0], intialPosUnitWeighing[1], 0, 0.1, 0.1);


                            }

                            if (y > 1)
                            {
                                if (y > 2)
                                {
                                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                                    CMD_params.triggerXbotID = xbot_ids[4];
                                    CMD_params.triggerCmdLabel = 11;

                                    _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                                }


                                _xbotCommand.LinearMotionSI(1, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.2916, 0.450, 0, 0.1, 0.1);
                                for (int l = 0; l<9; l++)
                                {
                                    time_params.delaySecs = 0.5;

                                    _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                                    _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);


                                    _xbotCommand.LinearMotionSI(5, xbot_ids[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.XTHENY, 0.0152,0 , 0, 0.1, 0.1);

                                }

                                _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitVisual[0], intialPosUnitVisual[1], 0, 0.1, 0.1);


                            }


                        


                        }
                        XBotStatus visual = _xbotCommand.GetXbotStatus(xbot_ids[3]);
                        XBotStatus weighing = _xbotCommand.GetXbotStatus(xbot_ids[2]);
                        XBotStatus filling = _xbotCommand.GetXbotStatus(xbot_ids[0]);

                        if (visual.XBOTState == XBOTSTATE.XBOT_WAIT && weighing.XBOTState == XBOTSTATE.XBOT_WAIT && filling.XBOTState == XBOTSTATE.XBOT_WAIT)
                        {
                            _xbotCommand.RotaryMotionP2P(11, xbot_ids[4], ROTATIONMODE.WRAP_TO_2PI_CW, 1.570796, 1, 2, POSITIONMODE.RELATIVE);
                        }
                        /*

                        XBotStatus visual = _xbotCommand.GetXbotStatus(xbot_ids[3]);
                        XBotStatus weighing = _xbotCommand.GetXbotStatus(xbot_ids[2]);
                        XBotStatus filling = _xbotCommand.GetXbotStatus(xbot_ids[0]);
                        while (visual.XBOTState != XBOTSTATE.XBOT_WAIT || weighing.XBOTState != XBOTSTATE.XBOT_WAIT || filling.XBOTState != XBOTSTATE.XBOT_WAIT)
                        {
                            visual = _xbotCommand.GetXbotStatus(xbot_ids[3]);
                            weighing = _xbotCommand.GetXbotStatus(xbot_ids[2]);
                            filling = _xbotCommand.GetXbotStatus(xbot_ids[0]);
                            
                            
                        }
                        */
                        //CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        //CMD_params.triggerXbotID = xbot_ids[1];
                        //CMD_params.triggerCmdLabel = 12;

                        //_xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);


                        //_xbotCommand.RotaryMotionP2P(11, xbot_ids[4], ROTATIONMODE.WRAP_TO_2PI_CW, 1.570796, 1, 2, POSITIONMODE.RELATIVE);




                    }
                    
                    // visual y: 450 X:291,6 for hver unit + 15.2
                    // Weighing X 536, afstand fra unit carrier 176 y: 531,4 + 15.2 for hver unit

                    // 
                    break;
                case '3':
                    //Lift the nest
                    MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.001, Movement.DIRECTION.X, 0.005, 0.01);


                    break;
                case '4':
                    //Lower the nest
                    MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.001, Movement.DIRECTION.X, 0.005, 0.01);
                    break;
                case '\u001b': //escape key
                    return;
            }  
            
            


            
        }
    }
}
