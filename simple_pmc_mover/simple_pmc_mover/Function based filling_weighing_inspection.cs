﻿using Microsoft.VisualBasic;
using PMCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_pmc_mover 
{
    internal class Function_based_filling_weighing_inspection : Movement
    {
        private static SystemCommands _systemCommand = new SystemCommands();
        //this class contains a collection of xbot commands, such as discover xbots, mobility control, linear motion, etc.
        private static XBotCommands _xbotCommand = new XBotCommands();

        


        int selector = 8;
        //double[] intialPosUnitCarrier = { 0.600, 0.600, 0.600, 0.360, 0.600, 0.120 };
        double[] intialPosUnitCarrier = { 0.360, 0.600, 0.600, 0.360, 0.600, 0.120 };
        double[] intialPosSecondUnitCarrier = { 0.600, 0.360 };
        double[] intialPosThirdUnitCarrier = { 0.600, 0.120 };
        double[] intialPosFourUnitCarrier = { 0.360, 0.120 };
        double[] intialPosFilling = { 0.295, 0.840, 0.425, 0.840 };
        double[] intialPosUnitWeighing = { 0.650, 0.840 };
        double[] intialPosUnitVisual = { 0.360, 0.360 };

        public int setSelectorOne()
        {
            return selector;
        }

        public WaitUntilTriggerParams test_wait_command(int ID, WaitUntilTriggerParams wait_params, ushort command_label) 
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, ID, TRIGGERSOURCE.CMD_LABEL, wait_params);

            _xbotCommand.LinearMotionSI(command_label, ID, POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT,0.1,0,0,0.1,0.1);


            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = ID;
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }

        public WaitUntilTriggerParams unitCarrierToFilling(int carrierID, WaitUntilTriggerParams wait_params) 
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, carrierID, TRIGGERSOURCE.CMD_LABEL, wait_params);

            _xbotCommand.LinearMotionSI(1, carrierID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.360, 0.600, 0, 0.1, 0.1);


            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = carrierID;
            CMD_params.triggerCmdLabel = 1;

            return CMD_params;
        }

        public WaitUntilTriggerParams carrierRotates(int carrierID, WaitUntilTriggerParams wait_params, ushort command_label)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, carrierID, TRIGGERSOURCE.CMD_LABEL, wait_params);

            _xbotCommand.LinearMotionSI(0, carrierID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.360, 0.600, 0, 0.1, 0.1);
            _xbotCommand.RotaryMotionP2P(command_label, carrierID, ROTATIONMODE.WRAP_TO_2PI_CW, 1.570796, 3, 6, POSITIONMODE.RELATIVE);


            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = carrierID;
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }


        public WaitUntilTriggerParams FILLING(int xbot1, int xbot2, WaitUntilTriggerParams wait_params, ushort command_label) 
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, xbot1, TRIGGERSOURCE.CMD_LABEL, wait_params);
            _xbotCommand.WaitUntil(0, xbot2, TRIGGERSOURCE.CMD_LABEL, wait_params);

            _xbotCommand.LinearMotionSI(0, xbot1, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.229, 0.731, 0, 0.1, 0.1);
            _xbotCommand.LinearMotionSI(0, xbot2, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.354, 0.731, 0, 0.1, 0.1);

            for (int i = 0; i < 10; i++)
            {

                if (i == 9) //This finishes the last filling and moves the filling station away from the unit carrier
                {
                    MoveOpposite(0, xbot1, xbot2, 0.033, Movement.DIRECTION.X, 0.1, 0.01);
                    time_params.delaySecs = 0.2;

                    _xbotCommand.WaitUntil(0, xbot1, TRIGGERSOURCE.TIME_DELAY, time_params);
                    _xbotCommand.WaitUntil(0, xbot2, TRIGGERSOURCE.TIME_DELAY, time_params);

                    //Filling station moves up from the syringe
                    MoveOpposite(0, xbot1, xbot2, -0.033, Movement.DIRECTION.X, 0.1, 0.01);



                    _xbotCommand.LinearMotionSI(0, xbot1, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosFilling[0], intialPosFilling[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(command_label, xbot2, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosFilling[2], intialPosFilling[3], 0, 0.1, 0.1);

                }
                else //This completes all the first 9 fillings 
                {
                    MoveOpposite(0, xbot1, xbot2, 0.033, Movement.DIRECTION.X, 0.1, 0.01);
                    time_params.delaySecs = 0.2;

                    _xbotCommand.WaitUntil(0, xbot1, TRIGGERSOURCE.TIME_DELAY, time_params);
                    _xbotCommand.WaitUntil(0, xbot2, TRIGGERSOURCE.TIME_DELAY, time_params);

                    //Filling station moves up from the syringe
                    MoveOpposite(0, xbot1, xbot2, -0.033, Movement.DIRECTION.X, 0.1, 0.01);

                    MoveRelativeTogether(0, xbot1, xbot2, 0.0152, Movement.DIRECTION.X, 0.15, 0.5);
                }
            }

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = xbot2;
            CMD_params.triggerCmdLabel = command_label;


            return CMD_params;
        }

        public WaitUntilTriggerParams WEIGHING(int weighingID, WaitUntilTriggerParams wait_params, ushort command_label, bool last_round)
        {

            
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams rotation = new WaitUntilTriggerParams();

            WaitUntilTriggerParams inspection = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, weighingID, TRIGGERSOURCE.CMD_LABEL, wait_params);
            _xbotCommand.LinearMotionSI(0, weighingID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.536, 0.6648, 0, 0.1, 0.1);

            for (int i = 0; i < 10; i++)
            {
                
                if (i == 9) //This finishes the last weighing and moves the weghing station away from the unit carrier
                {
                    _xbotCommand.LinearMotionSI(0, weighingID, POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.0152, 0, 0.1, 0.1);
                    _xbotCommand.ShortAxesMotionSI(0, weighingID, POSITIONMODE.ABSOLUTE, 0.004, 0, 0, 0, 0.1, 0, 0, 0);
                    time_params.delaySecs = 0.4;

                    _xbotCommand.WaitUntil(0, weighingID, TRIGGERSOURCE.TIME_DELAY, time_params);

                    _xbotCommand.ShortAxesMotionSI(0, weighingID, POSITIONMODE.ABSOLUTE, 0.001, 0, 0, 0, 0.1, 0, 0, 0);
                    _xbotCommand.LinearMotionSI(command_label, weighingID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.600, 0.600, 0, 0.1, 0.1);

                    if (last_round)
                    {
                        
                        _xbotCommand.LinearMotionSI(100, weighingID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitWeighing[0], intialPosUnitWeighing[1], 0, 0.1, 0.1);                       


                        return CMD_params;

                    }

                }
                else
                {
                    if (i > 0) //This is because the initial move moves under the first syringe so it doesn't have to do it on the first lap of the for-loop
                    {
                        _xbotCommand.LinearMotionSI(0, weighingID, POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.0152, 0, 0.1, 0.1);

                    }
                    _xbotCommand.ShortAxesMotionSI(0, weighingID, POSITIONMODE.ABSOLUTE, 0.004, 0, 0, 0, 0.1, 0, 0, 0);
                    time_params.delaySecs = 0.2;

                    _xbotCommand.WaitUntil(0, weighingID, TRIGGERSOURCE.TIME_DELAY, time_params);
                    _xbotCommand.ShortAxesMotionSI(0, weighingID, POSITIONMODE.ABSOLUTE, 0.001, 0, 0, 0, 0.1, 0, 0, 0);
                }
            }


            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = weighingID;
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }

        public WaitUntilTriggerParams INSPECTION(int inspectionID, WaitUntilTriggerParams wait_params, ushort command_label)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
         
            _xbotCommand.WaitUntil(0, inspectionID, TRIGGERSOURCE.CMD_LABEL, wait_params);

            _xbotCommand.LinearMotionSI(0, inspectionID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.4284, 0.450, 0, 0.1, 0.1);

            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    //Last move and move away from unit carrier
                    _xbotCommand.LinearMotionSI(0, inspectionID, POSITIONMODE.RELATIVE, LINEARPATHTYPE.XTHENY, -0.0152, 0, 0, 0.1, 0.1);

                    time_params.delaySecs = 0.2;

                    _xbotCommand.WaitUntil(0, inspectionID, TRIGGERSOURCE.TIME_DELAY, time_params);

                    _xbotCommand.LinearMotionSI(command_label, inspectionID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosUnitVisual[0], intialPosUnitVisual[1], 0, 0.1, 0.1);
                }
                else
                {
                    if (i == 0)
                    {
                        time_params.delaySecs = 0.2;

                        _xbotCommand.WaitUntil(0, inspectionID, TRIGGERSOURCE.TIME_DELAY, time_params);
                    }
                    //All the previous moves of the visual inspection
                    _xbotCommand.LinearMotionSI(0, inspectionID, POSITIONMODE.RELATIVE, LINEARPATHTYPE.XTHENY, -0.0152, 0, 0, 0.1, 0.1);

                    time_params.delaySecs = 0.2;

                    _xbotCommand.WaitUntil(0, inspectionID, TRIGGERSOURCE.TIME_DELAY, time_params);
                }
            }



            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = inspectionID;
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }


        private int getCarrierIndex(int[] carrierIDs, int lastCarrierIndex)
        {
            double[] targetPosition = { 0.360, 0.600 };
            int carrierIndex  = -1;
            XBotStatus status1 = _xbotCommand.GetXbotStatus(carrierIDs[0]);
            double[] position1 = status1.FeedbackPositionSI;
            XBotStatus status2 = _xbotCommand.GetXbotStatus(carrierIDs[1]);
            double[] position2 = status2.FeedbackPositionSI;
            XBotStatus status3 = _xbotCommand.GetXbotStatus(carrierIDs[2]);
            double[] position3 = status3.FeedbackPositionSI;
            XBotStatus status4 = _xbotCommand.GetXbotStatus(carrierIDs[3]);
            double[] position4 = status4.FeedbackPositionSI;

            for (int i = 0; i < 4; i++)
            {
                double[] currentPosition = { 0, 0 };
                switch (i)
                {
                    case 0:
                        currentPosition = position1;
                        Console.WriteLine("hej");

                        break;
                    case 1:
                        currentPosition = position2;
                        Console.WriteLine("hej");
                        break;
                    case 2:
                        currentPosition = position3;
                        Console.WriteLine("hej");
                        break;
                    case 3:
                        currentPosition = position4;
                        Console.WriteLine("hej");
                        break;
                        //default:
                        //continue; // Skip invalid indices
                }

                currentPosition[0] = Math.Round(currentPosition[0], 3);
                currentPosition[1] = Math.Round(currentPosition[1], 3);

                // Check if the current position matches the target position
                if (currentPosition[0] == targetPosition[0] && currentPosition[1] == targetPosition[1])
                {
                    carrierIndex = i; // Store the index of the matching carrier
                    Console.WriteLine($"Carrier at index {carrierIndex} is placed at the position {targetPosition[0]}, {targetPosition[1]}");

                    break; // Exit the loop once a match is found
                }
            }
            while(carrierIndex == lastCarrierIndex)
            {
                getCarrierIndex(carrierIDs, carrierIndex);
            }
            
            return carrierIndex;

            
        }

        public WaitUntilTriggerParams carrierToNextPostion(int[] carrierIDs, int carrierIndex, WaitUntilTriggerParams wait_params, ushort command_label)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();


            _xbotCommand.WaitUntil(0, carrierIDs[carrierIndex], TRIGGERSOURCE.CMD_LABEL, wait_params);

            _xbotCommand.LinearMotionSI(33, carrierIDs[carrierIndex], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.111, 0.293, 0, 0.5, 0.1);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = carrierIDs[carrierIndex];
            CMD_params.triggerCmdLabel = 33;

            _xbotCommand.WaitUntil(0, carrierIDs[(carrierIndex + 1) % 4], TRIGGERSOURCE.CMD_LABEL, CMD_params);

            _xbotCommand.LinearMotionSI(command_label, carrierIDs[(carrierIndex + 1) % 4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.360, 0.600, 0, 0.5, 0.1);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = carrierIDs[(carrierIndex + 1) % 4];
            CMD_params.triggerCmdLabel = command_label;

            _xbotCommand.WaitUntil(0, carrierIDs[(carrierIndex + 2) % 4], TRIGGERSOURCE.CMD_LABEL, CMD_params);

            _xbotCommand.LinearMotionSI(35, carrierIDs[(carrierIndex + 2) % 4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.600, 0.360, 0, 0.5, 0.1);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = carrierIDs[(carrierIndex + 2) % 4];
            CMD_params.triggerCmdLabel = 35;

            
            
            _xbotCommand.WaitUntil(0, carrierIDs[(carrierIndex + 3) % 4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
            
            


            _xbotCommand.LinearMotionSI(36, carrierIDs[(carrierIndex + 3) % 4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.600, 0.120, 0, 0.5, 0.1);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = carrierIDs[(carrierIndex + 3) % 4];
            CMD_params.triggerCmdLabel = 36;

            
            int lastCarrierIndex = carrierIndex;            

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = carrierIDs[(carrierIndex + 1) % 4];
            CMD_params.triggerCmdLabel = command_label;
            

                

            return CMD_params;
        }


        public void runFunctionBasedFillingAndInspection(int[] xbot_ids)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();

            //int[] xbot_ids = XID;

            int filling_xbot1 = xbot_ids[0];
            int filling_xbot2 = xbot_ids[1];
            int weighing_xbot = xbot_ids[2];
            int inspection_xbot = xbot_ids[3];
            int carrier_xbot = xbot_ids[4];
            int[] cariier_xbotIDs = { xbot_ids[4], xbot_ids[5], xbot_ids[6], xbot_ids[7] };

            double[] backInLoop = { 0.360, 0.120 };


            selector = 8;
            //Console.Clear();
            Console.WriteLine(" Filling and inspection (function based)");
            Console.WriteLine("0    Return ");
            Console.WriteLine("1    Initial postion");
            Console.WriteLine("2    Function based DEMO");
            Console.WriteLine("3    ");
            Console.WriteLine("4    ");
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

                case '1':
                    //First Unit carrier moves to inintal position
                    _xbotCommand.LinearMotionSI(0, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosUnitCarrier[0], intialPosUnitCarrier[1], 0, 0.1, 0.1);

                    _xbotCommand.RotaryMotionP2P(120, xbot_ids[4], ROTATIONMODE.WRAP_TO_2PI_CCW, 0, 3, 6, POSITIONMODE.ABSOLUTE);
                    _xbotCommand.LinearMotionSI(0, xbot_ids[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosSecondUnitCarrier[0], intialPosSecondUnitCarrier[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, xbot_ids[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosThirdUnitCarrier[0], intialPosThirdUnitCarrier[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, xbot_ids[7], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosFourUnitCarrier[0], intialPosFourUnitCarrier[1], 0, 0.1, 0.1);


                    //Gearlift (filling station) moves to inital position
                    _xbotCommand.LinearMotionSI(0, xbot_ids[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosFilling[0], intialPosFilling[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, xbot_ids[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, intialPosFilling[2], intialPosFilling[3], 0, 0.1, 0.1);

                    //Weighing station moves to inital position
                    _xbotCommand.LinearMotionSI(0, xbot_ids[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitWeighing[0], intialPosUnitWeighing[1], 0, 1, 1);

                    //Visual inspection moves to inital position
                    _xbotCommand.LinearMotionSI(0, xbot_ids[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitVisual[0], intialPosUnitVisual[1], 0, 0.1, 0.1);

                    //Other unit carriers moves to inital position
                    //_xbotCommand.LinearMotionSI(0, xbot_ids[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosSecondUnitCarrier[0], intialPosSecondUnitCarrier[1], 0, 0.1, 0.1);
                    break;

                case '2':
                    //You don't actually need to create this many trigger params, I am fairly cartain you can just reuse the same one
                    //because of the changing labels
                    WaitUntilTriggerParams CarrierMoves = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams Capping = new WaitUntilTriggerParams();

                    WaitUntilTriggerParams CMD_params1 = new WaitUntilTriggerParams();

                    WaitUntilTriggerParams FillingDone1 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams FillingDone2 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams FillingDone3 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams FillingDone4 = new WaitUntilTriggerParams();

                    WaitUntilTriggerParams RotationDone1 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams RotationDone2 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams RotationDone3 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams RotationDone4 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams RotationDone5 = new WaitUntilTriggerParams();

                    WaitUntilTriggerParams WeighingDone = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams WeighingDone1 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams WeighingDone2 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams WeighingDone3 = new WaitUntilTriggerParams();

                    WaitUntilTriggerParams InspectionDone1 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams InspectionDone2 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams InspectionDone3 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams InspectionDone4 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams CarrierMovesAround = new WaitUntilTriggerParams();

                    
                    _xbotCommand.MotionBufferControl(xbot_ids[0], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[1], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[2], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[3], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[4], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[5], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[6], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[7], MOTIONBUFFEROPTIONS.BLOCKBUFFER);



                    time_params.delaySecs = 5;

                    CarrierMoves = unitCarrierToFilling(carrier_xbot, time_params);
                    FillingDone1 = FILLING(filling_xbot1, filling_xbot2, CarrierMoves, 20);

                    RotationDone1 = carrierRotates(carrier_xbot, FillingDone1, 10);
                    FillingDone2 = FILLING(filling_xbot1, filling_xbot2, RotationDone1, 21);
                    WeighingDone = WEIGHING(weighing_xbot, RotationDone1, 30,false);

                    RotationDone2 = carrierRotates(carrier_xbot, FillingDone2, 11);
                    FillingDone3 = FILLING(filling_xbot1, filling_xbot2, RotationDone2, 22);
                    WeighingDone1 = WEIGHING(weighing_xbot, RotationDone2, 31, false);
                    InspectionDone1 = INSPECTION(inspection_xbot, RotationDone2, 40);

                    RotationDone3 = carrierRotates(carrier_xbot, FillingDone3, 12);
                    FillingDone4 = FILLING(filling_xbot1, filling_xbot2, RotationDone3, 23);
                    WeighingDone2 = WEIGHING(weighing_xbot, RotationDone3, 32, false);
                    InspectionDone2 = INSPECTION(inspection_xbot, RotationDone3, 41);
                    RotationDone4 = carrierRotates(carrier_xbot, FillingDone4,13);


                    WeighingDone3 = WEIGHING(weighing_xbot, RotationDone4, 33, false);
                    InspectionDone3 = INSPECTION(inspection_xbot, RotationDone4, 42);

                    _xbotCommand.WaitUntil(0, weighing_xbot, TRIGGERSOURCE.CMD_LABEL, InspectionDone3);
                    _xbotCommand.LinearMotionSI(100, weighing_xbot, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitWeighing[0], intialPosUnitWeighing[1], 0, 0.1, 0.1);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = weighing_xbot;
                    CMD_params1.triggerCmdLabel = 100;

                    _xbotCommand.WaitUntil(0, cariier_xbotIDs[0], TRIGGERSOURCE.CMD_LABEL, WeighingDone3);                    
                    _xbotCommand.RotaryMotionP2P(120, cariier_xbotIDs[0], ROTATIONMODE.WRAP_TO_2PI_CW, 4.71238, 3, 6, POSITIONMODE.ABSOLUTE);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = cariier_xbotIDs[0];
                    CMD_params1.triggerCmdLabel = 120;


                    InspectionDone4 = INSPECTION(inspection_xbot, CMD_params1, 44);

                    _xbotCommand.WaitUntil(0, cariier_xbotIDs[0], TRIGGERSOURCE.CMD_LABEL, InspectionDone4);

                    _xbotCommand.LinearMotionSI(33, cariier_xbotIDs[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.111, 0.293, 0, 0.5, 0.1);

                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params.triggerXbotID = cariier_xbotIDs[0];
                    CMD_params.triggerCmdLabel = 33;

                    _xbotCommand.WaitUntil(0, cariier_xbotIDs[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                    _xbotCommand.LinearMotionSI(300, cariier_xbotIDs[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.360, 0.600, 0, 0.5, 0.1);

                    _xbotCommand.LinearMotionSI(300, cariier_xbotIDs[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.360, 0.600, 0, 0.5, 0.1);

                    CarrierMoves.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CarrierMoves.triggerXbotID = cariier_xbotIDs[1];
                    CarrierMoves.triggerCmdLabel = 300;

                    _xbotCommand.WaitUntil(0, cariier_xbotIDs[2], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                    _xbotCommand.LinearMotionSI(35, cariier_xbotIDs[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.600, 0.360, 0, 0.5, 0.1);

                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params.triggerXbotID = cariier_xbotIDs[2];
                    CMD_params.triggerCmdLabel = 35;

                    _xbotCommand.WaitUntil(0, cariier_xbotIDs[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);


                    _xbotCommand.LinearMotionSI(36, cariier_xbotIDs[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.600, 0.120, 0, 0.5, 0.1);

                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params.triggerXbotID = cariier_xbotIDs[2];
                    CMD_params.triggerCmdLabel = 35;

                    _xbotCommand.MotionBufferControl(xbot_ids[0], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[1], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[2], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[3], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[4], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[5], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[6], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[7], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    XBotStatus statusUnitCarrier1 = _xbotCommand.GetXbotStatus(cariier_xbotIDs[0]);
                    double[] UnitCarrier1 = statusUnitCarrier1.FeedbackPositionSI;

                    
                    /*
                    //_xbotCommand.MotionBufferControl(xbot_ids[0], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[1], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[2], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[3], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[4], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[5], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[6], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[7], MOTIONBUFFEROPTIONS.BLOCKBUFFER);

                    FillingDone1 = FILLING(filling_xbot1, filling_xbot2, CarrierMoves, 20);

                    RotationDone1 = carrierRotates(cariier_xbotIDs[1], FillingDone1, 10);
                    FillingDone2 = FILLING(filling_xbot1, filling_xbot2, RotationDone1, 21);
                    WeighingDone = WEIGHING(weighing_xbot, RotationDone1, 30, false);

                    RotationDone2 = carrierRotates(cariier_xbotIDs[1], FillingDone2, 11);
                    FillingDone3 = FILLING(filling_xbot1, filling_xbot2, RotationDone2, 22);
                    WeighingDone1 = WEIGHING(weighing_xbot, RotationDone2, 31, false);
                    InspectionDone1 = INSPECTION(inspection_xbot, RotationDone2, 40);

                    RotationDone3 = carrierRotates(cariier_xbotIDs[1], FillingDone3, 12);
                    FillingDone4 = FILLING(filling_xbot1, filling_xbot2, RotationDone3, 23);
                    WeighingDone2 = WEIGHING(weighing_xbot, RotationDone3, 32, false);
                    InspectionDone2 = INSPECTION(inspection_xbot, RotationDone3, 41);
                    RotationDone4 = carrierRotates(cariier_xbotIDs[1], FillingDone4, 13);

                    WeighingDone3 = WEIGHING(weighing_xbot, RotationDone4, 33, false);
                    InspectionDone3 = INSPECTION(inspection_xbot, RotationDone4, 42);

                    _xbotCommand.WaitUntil(0, weighing_xbot, TRIGGERSOURCE.CMD_LABEL, InspectionDone3);
                    _xbotCommand.LinearMotionSI(100, weighing_xbot, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitWeighing[0], intialPosUnitWeighing[1], 0, 0.1, 0.1);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = weighing_xbot;
                    CMD_params1.triggerCmdLabel = 100;

                    _xbotCommand.WaitUntil(0, cariier_xbotIDs[1], TRIGGERSOURCE.CMD_LABEL, WeighingDone3);
                    _xbotCommand.RotaryMotionP2P(120, cariier_xbotIDs[1], ROTATIONMODE.WRAP_TO_2PI_CW, 4.71238, 3, 6, POSITIONMODE.ABSOLUTE);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = cariier_xbotIDs[1];
                    CMD_params1.triggerCmdLabel = 120;

                    InspectionDone4 = INSPECTION(inspection_xbot, CMD_params1, 44);

                    Capping.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    Capping.triggerXbotID = cariier_xbotIDs[0];
                    Capping.triggerCmdLabel = 300;

                    _xbotCommand.MotionBufferControl(xbot_ids[1], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[2], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[3], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[4], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[5], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[6], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[7], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    */

                    /*

                    XBotStatus statusUnitCarrier1 = _xbotCommand.GetXbotStatus(cariier_xbotIDs[0]);
                    double[] UnitCarrier1 = statusUnitCarrier1.FeedbackPositionSI;

                    UnitCarrier1[0] = Math.Round(UnitCarrier1[0], 3);
                    UnitCarrier1[1] = Math.Round(UnitCarrier1[1], 3);

                    // Check if the current position matches the target position
                    while (UnitCarrier1[0] != backInLoop[0] && UnitCarrier1[1] != backInLoop[1])
                    {
                        statusUnitCarrier1 = _xbotCommand.GetXbotStatus(cariier_xbotIDs[0]);
                        UnitCarrier1 = statusUnitCarrier1.FeedbackPositionSI;

                        UnitCarrier1[0] = Math.Round(UnitCarrier1[0], 3);
                        UnitCarrier1[1] = Math.Round(UnitCarrier1[1], 3);
                        
                    }

                    _xbotCommand.WaitUntil(0, cariier_xbotIDs[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);


                    _xbotCommand.LinearMotionSI(36, cariier_xbotIDs[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.600, 0.120, 0, 0.5, 0.1);

                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params.triggerXbotID = cariier_xbotIDs[3];
                    CMD_params.triggerCmdLabel = 36;
                    */

                    //________________________________________________________________________________________
                    //________________________________________________________________________________________

                    /*
                    FillingDone1 = FILLING(filling_xbot1, filling_xbot2, CarrierMoves, 20);

                    RotationDone1 = carrierRotates(cariier_xbotIDs[1], FillingDone1, 10);
                    FillingDone2 = FILLING(filling_xbot1, filling_xbot2, RotationDone1, 21);
                    WeighingDone = WEIGHING(weighing_xbot, RotationDone1, 30, false);

                    RotationDone2 = carrierRotates(cariier_xbotIDs[1], FillingDone2, 11);
                    FillingDone3 = FILLING(filling_xbot1, filling_xbot2, RotationDone2, 22);
                    WeighingDone1 = WEIGHING(weighing_xbot, RotationDone2, 31, false);
                    InspectionDone1 = INSPECTION(inspection_xbot, RotationDone2, 40);

                    RotationDone3 = carrierRotates(cariier_xbotIDs[1], FillingDone3, 12);
                    FillingDone4 = FILLING(filling_xbot1, filling_xbot2, RotationDone3, 23);
                    WeighingDone2 = WEIGHING(weighing_xbot, RotationDone3, 32, false);
                    InspectionDone2 = INSPECTION(inspection_xbot, RotationDone3, 41);
                    RotationDone4 = carrierRotates(cariier_xbotIDs[1], FillingDone4, 13);

                    WeighingDone3 = WEIGHING(weighing_xbot, RotationDone4, 33, false);
                    InspectionDone3 = INSPECTION(inspection_xbot, RotationDone4, 42);

                    _xbotCommand.WaitUntil(0, weighing_xbot, TRIGGERSOURCE.CMD_LABEL, InspectionDone3);
                    _xbotCommand.LinearMotionSI(100, weighing_xbot, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitWeighing[0], intialPosUnitWeighing[1], 0, 0.1, 0.1);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = weighing_xbot;
                    CMD_params1.triggerCmdLabel = 100;

                    _xbotCommand.WaitUntil(0, cariier_xbotIDs[1], TRIGGERSOURCE.CMD_LABEL, WeighingDone3);
                    _xbotCommand.RotaryMotionP2P(120, cariier_xbotIDs[1], ROTATIONMODE.WRAP_TO_2PI_CW, 4.71238, 3, 6, POSITIONMODE.ABSOLUTE);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = cariier_xbotIDs[1];
                    CMD_params1.triggerCmdLabel = 120;

                    InspectionDone4 = INSPECTION(inspection_xbot, CMD_params1, 44);

                    Capping.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    Capping.triggerXbotID = cariier_xbotIDs[0];
                    Capping.triggerCmdLabel = 300;

                    CarrierMovesAround = carrierToNextPostion(cariier_xbotIDs, 1, Capping, 521);
                    */


                    break;

                case '3':

                    WaitUntilTriggerParams CarrierMovesAround1 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams time_paramsOne = new WaitUntilTriggerParams();
                    
                    time_paramsOne.delaySecs =1;

                    double[] targetPosition = { 0.360, 0.600 };
                    int carrierIndex = -1;


                    _xbotCommand.WaitUntil(0, cariier_xbotIDs[0], TRIGGERSOURCE.TIME_DELAY, time_paramsOne);
                    _xbotCommand.WaitUntil(0, cariier_xbotIDs[1], TRIGGERSOURCE.TIME_DELAY, time_paramsOne);
                    _xbotCommand.WaitUntil(0, cariier_xbotIDs[2], TRIGGERSOURCE.TIME_DELAY, time_paramsOne);
                    _xbotCommand.WaitUntil(0, cariier_xbotIDs[3], TRIGGERSOURCE.TIME_DELAY, time_paramsOne);

                    XBotStatus status1 = _xbotCommand.GetXbotStatus(cariier_xbotIDs[0]);
                    double[] position1 = status1.FeedbackPositionSI;
                    XBotStatus status2 = _xbotCommand.GetXbotStatus(cariier_xbotIDs[1]);
                    double[] position2 = status2.FeedbackPositionSI;
                    XBotStatus status3 = _xbotCommand.GetXbotStatus(cariier_xbotIDs[2]);
                    double[] position3 = status3.FeedbackPositionSI;
                    XBotStatus status4 = _xbotCommand.GetXbotStatus(cariier_xbotIDs[3]);
                    double[] position4 = status4.FeedbackPositionSI;

                    for (int i = 0; i < 4; i++)
                    {
                        double[] currentPosition = { 0, 0 };
                        switch (i)
                        {
                            case 0:
                                currentPosition = position1;
                                Console.WriteLine("hej");

                                break;
                            case 1:
                                currentPosition = position2;
                                Console.WriteLine("hej");
                                break;
                            case 2:
                                currentPosition = position3;
                                Console.WriteLine("hej");
                                break;
                            case 3:
                                currentPosition = position4;
                                Console.WriteLine("hej");
                                break;
                                //default:
                                //continue; // Skip invalid indices
                        }

                        currentPosition[0] = Math.Round(currentPosition[0], 3);
                        currentPosition[1] = Math.Round(currentPosition[1], 3);

                        // Check if the current position matches the target position
                        if (currentPosition[0] == targetPosition[0] && currentPosition[1] == targetPosition[1])
                        {
                            carrierIndex = i; // Store the index of the matching carrier
                            Console.WriteLine($"Carrier at index {carrierIndex} is placed at the position {targetPosition[0]}, {targetPosition[1]}");

                            break; // Exit the loop once a match is found
                        }
                    }
                        CarrierMovesAround1 = carrierToNextPostion(cariier_xbotIDs, carrierIndex, time_paramsOne, 521);

                    break;


                case '\u001b': //escape key
                    return;
            }





        }
    }
}
