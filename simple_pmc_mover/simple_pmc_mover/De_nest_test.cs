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
            Console.WriteLine("8    Move line de-nester to initial end");
            Console.WriteLine("9    Next page");
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
                    //double[] linedenester_initial = { 0.527, 0.410, 0.527, 0.251 };
                    //_xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_initial[0], linedenester_initial[1], 0, 0.1, 0.1);
                    //_xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_initial[2], linedenester_initial[3], 0, 0.1, 0.1);

                    double[] linedenester_initial = { 0.527, 0.410, 0.527, 0.263 };
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_initial[0], linedenester_initial[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_initial[2], linedenester_initial[3], 0, 0.1, 0.1);
                    break;

                case '7':
                    //double[] linedenester_end = { 0.527, 0.455, 0.527, 0.296 };
                    //_xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_end[0], linedenester_end[1], 0, 0.1, 0.1);
                    //_xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_end[2], linedenester_end[3], 0, 0.1, 0.1);
                    //_xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.005, 0, 0.01, 0.01);
                    //_xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.005, 0, 0.01, 0.01);
                    //_xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.003, 0, 0.01, 0.01);
                    //_xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.009, 0, 0.01, 0.01);

                    double[] linedenester_end = { 0.527, 0.475, 0.527, 0.328 };
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_end[0], linedenester_end[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_end[2], linedenester_end[3], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.005, 0, 0.01, 0.01);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.005, 0, 0.01, 0.01);
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.008, 0, 0.01, 0.01);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.040, 0, 0.01, 0.01);
                    break;

                case '8':
                    /*
                    WaitUntilTriggerParams waitforlift = new WaitUntilTriggerParams();
                    waitforlift.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    waitforlift.triggerXbotID = XID[5];
                    waitforlift.triggerCmdLabel = 2;
                    
                    _xbotCommand.ShortAxesMotionSI(2, XID[5], POSITIONMODE.ABSOLUTE, 0.003, 0, 0, 0, 0.05, 0, 0, 0);
                    _xbotCommand.WaitUntil(0, XID[6], TRIGGERSOURCE.CMD_LABEL, waitforlift);
                    double[] linedenester_back = { 0.527, 0.360, 0.527, 0.194};
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back[0], linedenester_back[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back[2], linedenester_back[3], 0, 0.1, 0.1);

                    double[] linedenester_totheside = { 0.445, 0.360, 0.445, 0.194};
                    //_xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside[0], linedenester_totheside[1], 0, 0.1, 0.1);
                    //_xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_totheside[2], linedenester_totheside[3], 0, 0.1, 0.1);
                    */

                    
                    double[] linedenester_back = { 0.527, 0.360, 0.527, 0.179};
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back[0], linedenester_back[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back[2], linedenester_back[3], 0, 0.1, 0.1);

                    double[] linedenester_totheside = { 0.445, 0.360, 0.445, 0.179 };
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside[0], linedenester_totheside[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_totheside[2], linedenester_totheside[3], 0, 0.1, 0.1);

                    break;

                case '9':
                    unitCarriereRotation(XID);
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
            Console.WriteLine("1    Turn line de_nester");
            Console.WriteLine("2    Move line de_nester to unit carrier");
            Console.WriteLine("3    Place into unit carrier");
            Console.WriteLine("4    Lower ínto unit carrier");
            Console.WriteLine("5    Move away from nest");
            Console.WriteLine("6    ");
            Console.WriteLine("7    ");
            Console.WriteLine("8    ");
            Console.WriteLine("9    ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.KeyChar)
            {

                case '0':
                    selector = 50;
                    break;

                case '1':
                    //_xbotCommand.ArcMotionMetersRadians(0, XID[6], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.450, 0.360, 0, 0.05, 0.1, 0.156, 1.570796);
                    _xbotCommand.ArcMotionMetersRadians(0, XID[6], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.05, 0.1, 0.179, 1.65);
                    _xbotCommand.ArcMotionMetersRadians(0, XID[6], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.05, 0.1, 0.179, 1.65-1.570796);

                    break;

                case '2':
                    double[] linedenester_back = { 0.364, 0.120, 0.554, 0.120};
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.240, 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.240, 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back[0], linedenester_back[1], 0, 0.12, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back[2], linedenester_back[3], 0, 0.1, 0.1);
                    
                    break;

                case '3':
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.035, 0, 0, 0.01, 0.05);
                    break;

                case '4':
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.005, 0, 0, 0.01, 0.05);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.030, 0, 0, 0.01, 0.05);
                    break;

                case '5':
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.05, 0, 0, 0.1, 0.05);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.05, 0, 0, 0.1, 0.05);
                    break;
            }
        }
    }
}
