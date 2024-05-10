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
        double[] intialPosUnitCarrier = { 0.600, 0.600, 0.600, 0.360, 0.600, 0.120 };
        double[] intialPosFilling = { 0.295, 0.840, 0.425, 0.840 };
        double[] intialPosUnitWeighing = { 0.650, 0.840 };
        double[] intialPosUnitVisual = { 0.360, 0.360 };

        

        public int setSelectorOne()
        {
            return selector;
        }

        public void InitalFillingStep(int carrierModuleID, int[] xbot_ids)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            

            //Unit carrier moves into position
            _xbotCommand.LinearMotionSI(1, carrierModuleID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.360, 0.600, 0, 0.1, 0.1);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = carrierModuleID;
            CMD_params.triggerCmdLabel = 1;

            _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
            _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);

            //Filling station moves to unit carrier 
            _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.363, 0.737, 0, 0.1, 0.1);
            _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.488, 0.737, 0, 0.1, 0.1);
        }

        public WaitUntilTriggerParams unitCarrierRotate(int carrierModuleID, int[] xbot_ids, WaitUntilTriggerParams wait_params) //This function takes wait parameters depending on which module it should wait on
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, carrierModuleID, TRIGGERSOURCE.CMD_LABEL, wait_params);

            _xbotCommand.LinearMotionSI(0, carrierModuleID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.360, 0.600, 0, 0.1, 0.1);
            _xbotCommand.RotaryMotionP2P(2, carrierModuleID, ROTATIONMODE.WRAP_TO_2PI_CW, 1.570796, 1, 2, POSITIONMODE.RELATIVE);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = carrierModuleID;
            CMD_params.triggerCmdLabel = 2;

            return CMD_params;
        }

        public void fillingWaits(int carrierModuleID, int[] xbot_ids, WaitUntilTriggerParams wait_params)
        {
            _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.CMD_LABEL, wait_params);
            _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.CMD_LABEL, wait_params);
            //Filling station moves to unit carrier 
            _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.363, 0.737, 0, 0.1, 0.1);
            _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.488, 0.737, 0, 0.1, 0.1);
        }

        public void weighingWaits(int carrierModuleID, int[] xbot_ids, WaitUntilTriggerParams wait_params)
        {
            _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.CMD_LABEL, wait_params);
            // Weighing station moves to unit carrier
            _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.536, 0.5314, 0, 0.1, 0.1);
        }

        public void inspectionWaits(int carrierModuleID, int[] xbot_ids, WaitUntilTriggerParams wait_params)
        {
            _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.CMD_LABEL, wait_params);
            // visual station moves to unit carrier
            _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.2916, 0.450, 0, 0.1, 0.1);
        }

        public WaitUntilTriggerParams FILLING(int[] xbot_ids, int iteration)
        {
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            if (iteration == 9) //This finishes the last filling and moves the filling station away from the unit carrier
            {
                MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.033, Movement.DIRECTION.X, 0.05, 0.01);
                time_params.delaySecs = 0.2;

                _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);

                //Filling station moves up from the syringe
                MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.033, Movement.DIRECTION.X, 0.05, 0.01);



                _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosFilling[0], intialPosFilling[1], 0, 0.1, 0.1);
                _xbotCommand.LinearMotionSI(3, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosFilling[2], intialPosFilling[3], 0, 0.1, 0.1);

            }
            else //This completes all the first 9 fillings 
            {
                MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.033, Movement.DIRECTION.X, 0.05, 0.01);
                time_params.delaySecs = 0.2;

                _xbotCommand.WaitUntil(0, xbot_ids[0], TRIGGERSOURCE.TIME_DELAY, time_params);
                _xbotCommand.WaitUntil(0, xbot_ids[1], TRIGGERSOURCE.TIME_DELAY, time_params);

                //Filling station moves up from the syringe
                MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.033, Movement.DIRECTION.X, 0.05, 0.01);

                MoveRelativeTogether(0, xbot_ids[0], xbot_ids[1], -0.0152, Movement.DIRECTION.X, 0.15, 0.5);
            }

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = xbot_ids[1];
            CMD_params.triggerCmdLabel = 3;

            return CMD_params;
        }

        public WaitUntilTriggerParams WEIGHING(int[] xbot_ids, int iteration)
        {
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            if (iteration == 9) //This finishes the last weighing and moves the weghing station away from the unit carrier
            {
                _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.0152, 0, 0.1, 0.1);
                _xbotCommand.ShortAxesMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, 0.004, 0, 0, 0, 0.1, 0, 0, 0);
                time_params.delaySecs = 2;

                _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);

                _xbotCommand.ShortAxesMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, 0.001, 0, 0, 0, 0.1, 0, 0, 0);

                _xbotCommand.LinearMotionSI(4, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.600, 0.600, 0, 0.1, 0.1);


                //if (y == 4)
                //{
                //    _xbotCommand.LinearMotionSI(31, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.650, 0.840, 0, 0.1, 0.1);
                //}


            }
            else //This completes all the first 9 fillings 
            {
                if (iteration > 0) //This is because the initial move moves under the first syringe so it doesn't have to do it on the first lap of the for-loop
                {
                    _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.0152, 0, 0.1, 0.1);

                }
                _xbotCommand.ShortAxesMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, 0.004, 0, 0, 0, 0.1, 0, 0, 0);
                time_params.delaySecs = 2;

                _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);

                _xbotCommand.ShortAxesMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, 0.001, 0, 0, 0, 0.1, 0, 0, 0);
            }

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = xbot_ids[2];
            CMD_params.triggerCmdLabel = 4;
            return CMD_params;
        }
       
        public WaitUntilTriggerParams INSPECTION(int[] xbot_ids, int iteration)
        {
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            if (iteration == 9)
            {
                //Last move and move away from unit carrier
                _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.RELATIVE, LINEARPATHTYPE.XTHENY, 0.0152, 0, 0, 0.1, 0.1);

                time_params.delaySecs = 2;

                _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);
                
                _xbotCommand.LinearMotionSI(5, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosUnitVisual[0], intialPosUnitVisual[1], 0, 0.1, 0.1);
            }
            else
            {
                if (iteration == 0)
                {
                    time_params.delaySecs = 2;

                    _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);
                }
                //All the previous moves of the visual inspection
                _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.RELATIVE, LINEARPATHTYPE.XTHENY, 0.0152, 0, 0, 0.1, 0.1);

                time_params.delaySecs = 2;

                _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);
            }

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = xbot_ids[3];
            CMD_params.triggerCmdLabel = 5;
            return CMD_params;

        }





        public void fillingWeighingInspectionDemo(int carrierModuleID, int[] xbot_ids)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();

            for (int y = 0; y < 6; y++)  //
            {
                //------------------------------------------------------------------------
                //------------------------------------------------------------------------
                //This if-statement makes sure that the filling station wait for the unit carrier to move into position
                if (y == 0)
                {
                    //Unit carrier moves into position
                    _xbotCommand.LinearMotionSI(1, carrierModuleID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.360, 0.600, 0, 0.1, 0.1);

                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params.triggerXbotID = carrierModuleID;
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

                        _xbotCommand.WaitUntil(0, carrierModuleID, TRIGGERSOURCE.CMD_LABEL, CMD_params);
                    }
                    else if (y == 4 ) //Here it spins after the weighing station moves away
                    {
                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[2];
                        CMD_params.triggerCmdLabel = 30;

                        _xbotCommand.WaitUntil(0, carrierModuleID, TRIGGERSOURCE.CMD_LABEL, CMD_params);
                    }
                    else if (y==5) 
                    {
                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = xbot_ids[2];
                        CMD_params.triggerCmdLabel = 31;

                        _xbotCommand.WaitUntil(0, carrierModuleID, TRIGGERSOURCE.CMD_LABEL, CMD_params);
                    }


                    //The unit carrier spins and we make wait parameters for the other stations to wait for the spin
                    _xbotCommand.LinearMotionSI(1, carrierModuleID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.360, 0.600, 0, 0.1, 0.1);
                    _xbotCommand.RotaryMotionP2P(11, carrierModuleID, ROTATIONMODE.WRAP_TO_2PI_CW, 1.570796, 1, 2, POSITIONMODE.RELATIVE);

                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params.triggerXbotID = carrierModuleID;
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
                            CMD_params.triggerXbotID = carrierModuleID;
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
                    if (0 < y & y < 5) //Here, the Weighing station moves, and it only moves on the 2nd to 5th cycles
                    {
                        if (i == 9) //This finishes the last weighing and moves the weghing station away from the unit carrier
                        {
                            _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.0152, 0, 0.1, 0.1);
                            _xbotCommand.ShortAxesMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, 0.004, 0, 0, 0, 0.1, 0, 0, 0);
                            time_params.delaySecs = 2;

                            _xbotCommand.WaitUntil(0, xbot_ids[2], TRIGGERSOURCE.TIME_DELAY, time_params);

                            _xbotCommand.ShortAxesMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, 0.001, 0, 0, 0, 0.1, 0, 0, 0);

                            _xbotCommand.LinearMotionSI(30, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.600, 0.600, 0, 0.1, 0.1);

                            
                            if (y == 4)
                            {
                                _xbotCommand.LinearMotionSI(31, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.650, 0.840, 0, 0.1, 0.1);
                            }
              

                        }
                        else //This completes all the first 9 fillings 
                        {
                            if (i > 0) //This is because the initial move moves under the first syringe so it doesn't have to do it on the first lap of the for-loop
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
                        if (i == 9)
                        {
                            //Last move and move away from unit carrier
                            _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.RELATIVE, LINEARPATHTYPE.XTHENY, 0.0152, 0, 0, 0.1, 0.1);

                            time_params.delaySecs = 2;
                            
                            _xbotCommand.WaitUntil(0, xbot_ids[3], TRIGGERSOURCE.TIME_DELAY, time_params);
                            Console.WriteLine("sanity");
                            _xbotCommand.LinearMotionSI(40, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosUnitVisual[0], intialPosUnitVisual[1], 0, 0.1, 0.1);
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
            }
        }

        public void changeUnitCarrier(int filledUnitCarrier, int emptyUnitCarrier, int[] xbot_ids)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = xbot_ids[3];
            CMD_params.triggerCmdLabel = 40;

            _xbotCommand.WaitUntil(0, filledUnitCarrier, TRIGGERSOURCE.CMD_LABEL, CMD_params);
            _xbotCommand.WaitUntil(0, emptyUnitCarrier, TRIGGERSOURCE.CMD_LABEL, CMD_params);

            _xbotCommand.LinearMotionSI(0, filledUnitCarrier, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.111, 0.293, 0, 0.1, 0.1);
            _xbotCommand.LinearMotionSI(0, emptyUnitCarrier, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.360, 0.600, 0, 0.1, 0.1);


        }

        public void runFillingAndInspection(int[] XID)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();

            int[] xbot_ids = XID;
            selector = 7;
            //Console.Clear();
            Console.WriteLine(" Filling and inspection");
            Console.WriteLine("0    Return ");
            Console.WriteLine("1    Initial postion");
            Console.WriteLine("2    DEMO");
            Console.WriteLine("3    Lower filling");
            Console.WriteLine("4    Rise filling");
            Console.WriteLine("5    Function based DEMO (doesn't work)");
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
                    fillingWeighingInspectionDemo(3, xbot_ids);
                    changeUnitCarrier(3, 0, xbot_ids);
                    

                    break;
                case '3':
                    //Lift the nest
                    MoveOpposite(0, xbot_ids[0], xbot_ids[1], 0.001, Movement.DIRECTION.X, 0.005, 0.01);


                    break;
                case '4':
                    //Lower the nest
                    MoveOpposite(0, xbot_ids[0], xbot_ids[1], -0.001, Movement.DIRECTION.X, 0.005, 0.01);
                    break;

                case '5':
                    
                    for (int y = 0; y < 6; y++)
                    {
                        WaitUntilTriggerParams fillingParam = new WaitUntilTriggerParams();
                        WaitUntilTriggerParams weighingParam = new WaitUntilTriggerParams();
                        WaitUntilTriggerParams inspectionParam = new WaitUntilTriggerParams();
                        WaitUntilTriggerParams rotationParam = new WaitUntilTriggerParams();

                        if (y == 0) //The inital step that moves the unit carrier to the center and the filling station up to it
                        {
                            InitalFillingStep(3, xbot_ids);
                        }

                        //-------------------------------------------------------------------
                        // Here we wait for the unit carrier to position itself (rotation wise)
                        //-------------------------------------------------------------------

                        if (y == 1 ^ y == 2 ^ y == 3)
                        {
                            fillingWaits(3, xbot_ids, rotationParam);
                        }

                        /*
                        if (y == 1 ^ y == 2 ^ y == 3 ^ y == 4)
                        {
                            weighingWaits(3, xbot_ids, rotationParam);
                        }

                        if (y == 2 ^ y == 3 ^ y == 4 ^ y == 5)
                        {
                            inspectionWaits(3, xbot_ids, rotationParam);
                        }
                        */

                        //-------------------------------------------------------------------
                        // Here we then perform the processes
                        //-------------------------------------------------------------------

                        if (y == 0 ^ y == 1 ^ y == 2 ^ y == 3)
                        {

                            for (int i = 0; i < 10; i++)
                            {
                                fillingParam = FILLING(xbot_ids, i);
                            }

                        }

                        /*
                        if (y == 1 ^ y ==2 ^ y == 3 ^ y == 4)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                weighingParam = WEIGHING(xbot_ids, i);
                            }
                        }

                        if (y == 2 ^  y == 3 ^ y == 4 ^ y == 5)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                inspectionParam = INSPECTION(xbot_ids, i);
                            }
                        }
                        */

                        //-------------------------------------------------------------------
                        // Here we make the unit carrier wait for the other processes to finish before spinning
                        //-------------------------------------------------------------------
                        if (y == 0 ^ y == 1 ^ y ==2 ^ y == 3)
                        {
                            rotationParam = unitCarrierRotate(3, xbot_ids, fillingParam);
                        }
                        
                        if (y == 4)
                        {
                            rotationParam = unitCarrierRotate(3, xbot_ids, weighingParam);
                        }
                        if (y == 5)
                        {
                            rotationParam = unitCarrierRotate(3, xbot_ids, inspectionParam);
                        }

                        


                        

                    }
                    



                    break;
                case '\u001b': //escape key
                    return;
            }  
            
            


            
        }
    }
}
