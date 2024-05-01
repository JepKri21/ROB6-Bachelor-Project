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
            Console.WriteLine("0    Return ");
            Console.WriteLine("1    Start position for scissor lift");
            Console.WriteLine("2    Grasp nest");
            Console.WriteLine("3    Start position Line pusher");
            Console.WriteLine("4    Move line pusher forward");
            Console.WriteLine("5    Lower scissor lift");
            Console.WriteLine("6    Start postion line de-nester");
            Console.WriteLine("7    Lift vials");
            Console.WriteLine("9    ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.KeyChar)
            {

                case '0':
                    selector = 1;
                    break;

                case '1':
                    double[] scissor_inital = { 0.400, 0.780 - 0.015, 0.4, 0.630 + 0.015, 0.650, 0.780 - 0.015, 0.650, 0.630 + 0.015 };

                    _xbotCommand.LinearMotionSI(0, XID[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, scissor_inital[0], scissor_inital[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, scissor_inital[2], scissor_inital[3], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, scissor_inital[4], scissor_inital[5], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, scissor_inital[6], scissor_inital[7], 0, 0.1, 0.1);
                    selector = 5;
                    break;

                case '2':
                    MoveOpposite(0, XID[2], XID[0], 0.005, Movement.DIRECTION.X, 0.01, 0.01);
                    MoveOpposite(0, XID[3], XID[1], 0.005, Movement.DIRECTION.X, 0.01, 0.01);
                    selector = 5;
                    break;

                case '3':
                    double[] linepusher_initial = { 0.525, 0.490 };
                    _xbotCommand.LinearMotionSI(0, XID[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linepusher_initial[0], linepusher_initial[1], 0, 0.1, 0.1);
                    selector = 5;
                    break;

                case '4':
                    double[] linepusher_end = { 0.525, 0.682 };
                    _xbotCommand.LinearMotionSI(0, XID[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linepusher_end[0], linepusher_end[1], 0, 0.1, 0.1);
                    selector = 5;
                    break;

                case '5':
                    _xbotCommand.LinearMotionSI(0, XID[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
                    selector = 5;
                    break;

                case '6':
                    double[] linedenester_initial = { 0.526, 0.410, 0.526, 0.270 };
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_initial[0], linedenester_initial[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_initial[2], linedenester_initial[3], 0, 0.1, 0.1);
                    selector = 5;
                    break;

                case '7':
                    double[] linedenester_end = { 0.526, 0.460, 0.526, 0.320 };
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_end[0], linedenester_end[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_end[2], linedenester_end[3], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.01, 0, 0.01, 0.01);
                    selector = 5;
                    break;

            }
        }
    }
}
