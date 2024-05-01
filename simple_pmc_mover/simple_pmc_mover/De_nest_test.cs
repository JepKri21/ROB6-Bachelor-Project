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
                    double[] scissor_inital = { 0.4, 0.880, 0.4, 0.730, 0.650, 0.880, 0.650, 0.730 };

                    _xbotCommand.LinearMotionSI(0, XID[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, scissor_inital[0], scissor_inital[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, scissor_inital[2], scissor_inital[3], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, scissor_inital[4], scissor_inital[5], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, scissor_inital[6], scissor_inital[7], 0, 0.1, 0.1);
                    break;

                case '2':
                    MoveOpposite(0, XID[0], XID[1], 0.005, Movement.DIRECTION.X, 0.01, 0.01);
                    MoveOpposite(0, XID[2], XID[3], 0.005, Movement.DIRECTION.X, 0.01, 0.01);
                    break;
            }
        }
    }
}
