using PMCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_pmc_mover
{
    internal class De_nest_test : Movement
    {
        int selector = 5;
        private static XBotCommands _xbotCommand = new XBotCommands();

        public int setSelectorOne()
        {
            return selector;
        }

        public void runDeNestTest(int[] XID)
        {
            int[] xbot_ids = XID;
            selector = 4;
            Console.Clear();
            Console.WriteLine(" System tests");
            Console.WriteLine("0    Return");
            Console.WriteLine("1    ");
            Console.WriteLine("2    Demo");
            Console.WriteLine("3    Step by step");
            Console.WriteLine("4    ");
            Console.WriteLine("5    ");
            Console.WriteLine("6    ");
            Console.WriteLine("7    ");
            Console.WriteLine("8    Rotation program for unit Carrier");
            Console.WriteLine("9    ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.KeyChar)
            {

                case '0':
                    selector = 1;
                    break;

                case '1':

                    break;

                case '2':
                    //Demo program
                    break;

                case '3':
                    selector = 50;
                    deNestingStepByStep(XID);
                    break;

                case '8':
                    selector = 52;
                    break;
                
            }
        }
        public void deNestingStepByStep(int[] XID)
        {
            
            int[] xbot_ids = XID;
            selector = 50;
            Console.Clear();
            Console.WriteLine(" De-nesting step by step");
            Console.WriteLine("0    Return ");
            Console.WriteLine("1    Start position for scissor lift");
            Console.WriteLine("2    Grasp nest");
            Console.WriteLine("3    Start position Line pusher");
            Console.WriteLine("4    Move line pusher forward");
            Console.WriteLine("5    Lower scissor lift");
            Console.WriteLine("6    Start postion line de-nester");
            Console.WriteLine("7    Lift vials");
            Console.WriteLine("8    ");
            Console.WriteLine("9    ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.KeyChar)
            {

                case '0':
                    selector = 5;
                    break;

                case '1':
                    double[] scissor_inital = { 0.400, 0.780 - 0.015, 0.4, 0.630 + 0.015, 0.650, 0.780 - 0.015, 0.650, 0.630 + 0.015 };

                    _xbotCommand.LinearMotionSI(0, XID[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_inital[0], scissor_inital[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_inital[2], scissor_inital[3], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_inital[4], scissor_inital[5], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_inital[6], scissor_inital[7], 0, 0.1, 0.1);                    
                    break;

                case '2':
                    MoveOpposite(0, XID[2], XID[0], 0.005, Movement.DIRECTION.X, 0.01, 0.01);
                    MoveOpposite(0, XID[3], XID[1], 0.005, Movement.DIRECTION.X, 0.01, 0.01);                    
                    break;

                case '3':
                    double[] linepusher_initial = { 0.525, 0.490 };
                    _xbotCommand.LinearMotionSI(0, XID[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linepusher_initial[0], linepusher_initial[1], 0, 0.1, 0.1);
                    
                    break;

                case '4':
                    double[] linepusher_end = { 0.525, 0.682 };
                    _xbotCommand.LinearMotionSI(0, XID[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linepusher_end[0], linepusher_end[1], 0, 0.1, 0.1);
                    break;

                case '5':
                    _xbotCommand.LinearMotionSI(0, XID[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
                    break;

                case '6':
                    double[] linedenester_initial = { 0.527, 0.410, 0.527, 0.285 };
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_initial[0], linedenester_initial[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_initial[2], linedenester_initial[3], 0, 0.1, 0.1);
                    break;

                case '7':
                    double[] linedenester_end = { 0.527, 0.455, 0.527, 0.330 };
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_end[0], linedenester_end[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_end[2], linedenester_end[3], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.005, 0, 0.01, 0.01);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.005, 0, 0.01, 0.01);
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.003, 0, 0.01, 0.01);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.014, 0, 0.01, 0.01);
                    break;
                

                
            }
        }
        public void unitCarriereRotation(int[] XID)
        {
            int[] xbot_ids = XID;
            selector = 51;
            Console.Clear();
            Console.WriteLine(" De-nesting step by step");
            Console.WriteLine("0    Return ");
            Console.WriteLine("1    Start position for scissor lift");
            Console.WriteLine("2    Grasp nest");
            Console.WriteLine("3    Start position Line pusher");
            Console.WriteLine("4    Move line pusher forward");
            Console.WriteLine("5    Lower scissor lift");
            Console.WriteLine("6    Start postion line de-nester");
            Console.WriteLine("7    Lift vials");
            Console.WriteLine("8    Move to start postion unit carrier");
            Console.WriteLine("9    Rotate unit carrier 90 deg");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.KeyChar)
            {

                case '0':
                    selector = 5;
                    break;

                case '1':
                    double[] pos_unit_carrier_initial1 = { 0.120, 0.120 };
                    _xbotCommand.LinearMotionSI(0, XID[7], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, pos_unit_carrier_initial1[0], pos_unit_carrier_initial1[1], 0, 0.1, 0.1);
                    _xbotCommand.RotaryMotionP2P(0, XID[7], ROTATIONMODE.WRAP_TO_2PI_CCW, 0, 5, 1);
                    _xbotCommand.LinearMotionSI(2, XID[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.360, 0.480, 0, 0.1, 0.1);
                    //_xbotCommand.ShortAxesMotionSI(0, XID[7], POSITIONMODE.ABSOLUTE, SHORTAXESCENTERMODE.XBOT_CENTER, 0, 0, 0.001, 0, 0, 0, 0.01, 0, 0, 5);
                    break;

                case '2':
                    WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

                    //_xbotCommand.RotaryMotionP2P(0, XID[7], ROTATIONMODE.WRAP_TO_2PI_CCW, 1.570, 5, 2, POSITIONMODE.RELATIVE);
                    double[] pos_unit_carrier_initial = { 0.120, 0.600 };
                    _xbotCommand.LinearMotionSI(0, xbot_ids[7], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, pos_unit_carrier_initial[0], pos_unit_carrier_initial[1], 0, 0.1, 0.1);
                    _xbotCommand.RotaryMotionP2P(1, xbot_ids[7], ROTATIONMODE.WRAP_TO_2PI_CCW, 1.5707, 5, 2);

                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params.triggerXbotID = xbot_ids[7];
                    CMD_params.triggerCmdLabel = 1;

                    _xbotCommand.WaitUntil(4, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                    _xbotCommand.LinearMotionSI(2, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.360, 0.120, 0, 0.1, 0.1);

                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params.triggerXbotID = xbot_ids[4];
                    CMD_params.triggerCmdLabel = 2;

                    _xbotCommand.WaitUntil(4, xbot_ids[7], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                    _xbotCommand.LinearMotionSI(0, xbot_ids[7], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.120, 0.120, 0, 0.1, 0.1);
                    _xbotCommand.RotaryMotionP2P(2, xbot_ids[7], ROTATIONMODE.WRAP_TO_2PI_CCW, 0, 5, 1);

                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params.triggerXbotID = xbot_ids[7];
                    CMD_params.triggerCmdLabel = 2;

                    _xbotCommand.WaitUntil(4, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                    _xbotCommand.LinearMotionSI(2, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.360, 0.480, 0, 0.1, 0.1);

                    break;


            }
        }
    }
}
