using Microsoft.VisualBasic;
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
        double[] intialPosUnitCarrier = { 0.600, 0.600, 0.600, 0.360, 0.600, 0.120 };
        double[] intialPosSecondUnitCarrier = { 0.600, 0.120 };
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

            _xbotCommand.LinearMotionSI(0, xbot1, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.211, 0.737, 0, 0.1, 0.1);
            _xbotCommand.LinearMotionSI(0, xbot2, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.336, 0.737, 0, 0.1, 0.1);

            for (int i = 0; i < 3; i++)
            {

                if (i == 2) //This finishes the last filling and moves the filling station away from the unit carrier
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

            _xbotCommand.WaitUntil(0, weighingID, TRIGGERSOURCE.CMD_LABEL, wait_params);
            _xbotCommand.LinearMotionSI(0, weighingID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.536, 0.6648, 0, 0.1, 0.1);

            for (int i = 0; i < 3; i++)
            {
                
                if (i == 2) //This finishes the last weighing and moves the weghing station away from the unit carrier
                {
                    _xbotCommand.LinearMotionSI(0, weighingID, POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.0152, 0, 0.1, 0.1);
                    _xbotCommand.ShortAxesMotionSI(0, weighingID, POSITIONMODE.ABSOLUTE, 0.004, 0, 0, 0, 0.1, 0, 0, 0);
                    time_params.delaySecs = 0.2;

                    _xbotCommand.WaitUntil(0, weighingID, TRIGGERSOURCE.TIME_DELAY, time_params);

                    _xbotCommand.ShortAxesMotionSI(0, weighingID, POSITIONMODE.ABSOLUTE, 0.001, 0, 0, 0, 0.1, 0, 0, 0);
                    _xbotCommand.LinearMotionSI(command_label, weighingID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.600, 0.600, 0, 0.1, 0.1);

                    if (last_round)
                    {
                        _xbotCommand.LinearMotionSI(0, weighingID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, intialPosUnitWeighing[0], intialPosUnitWeighing[1], 0, 0.1, 0.1);
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

            for (int i = 0; i < 3; i++)
            {
                if (i == 2)
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




            selector = 8;
            Console.Clear();
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

                    WaitUntilTriggerParams InspectionDone1 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams InspectionDone2 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams InspectionDone3 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams InspectionDone4 = new WaitUntilTriggerParams();

                    _xbotCommand.MotionBufferControl(xbot_ids[0], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[1], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[2], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[3], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[4], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[5], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[6], MOTIONBUFFEROPTIONS.BLOCKBUFFER);


                    time_params.delaySecs = 5;

                    CarrierMoves = unitCarrierToFilling(carrier_xbot, time_params);
                    FillingDone1 = FILLING(filling_xbot1, filling_xbot2, CarrierMoves, 20);

                    RotationDone1 = carrierRotates(carrier_xbot, FillingDone1, 10);
                    FillingDone2 = FILLING(filling_xbot1, filling_xbot2, RotationDone1, 21);
                    WeighingDone = WEIGHING(weighing_xbot, RotationDone1, 30,false);

                    RotationDone2 = carrierRotates(carrier_xbot, FillingDone2, 11);
                    FillingDone3 = FILLING(filling_xbot1, filling_xbot2, RotationDone2, 22);
                    WeighingDone = WEIGHING(weighing_xbot, RotationDone2, 31, false);
                    InspectionDone1 = INSPECTION(inspection_xbot, RotationDone2, 40);

                    RotationDone3 = carrierRotates(carrier_xbot, FillingDone3, 12);
                    FillingDone4 = FILLING(filling_xbot1, filling_xbot2, RotationDone3, 23);
                    WeighingDone = WEIGHING(weighing_xbot, RotationDone3, 32, false);
                    InspectionDone2 = INSPECTION(inspection_xbot, RotationDone3, 41);

                    RotationDone4 = carrierRotates(carrier_xbot, FillingDone4,13);
                    WeighingDone = WEIGHING(weighing_xbot, RotationDone4, 33, true);
                    InspectionDone3 = INSPECTION(inspection_xbot, RotationDone4, 42);

                    RotationDone5 = carrierRotates(carrier_xbot, WeighingDone, 14);
                    InspectionDone4 = INSPECTION(inspection_xbot, RotationDone5, 44);

                    _xbotCommand.LinearMotionSI(0, carrier_xbot, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.111, 0.293, 0, 0.1, 0.1);

                    _xbotCommand.MotionBufferControl(xbot_ids[0], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[1], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[2], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[3], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[4], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[5], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[6], MOTIONBUFFEROPTIONS.RELEASEBUFFER);


                    break;

                case '\u001b': //escape key
                    return;
            }





        }
    }
}
