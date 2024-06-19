using PMCLIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
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
            Console.WriteLine("4    De-nesting Demo");
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

                case '4':
                    selector = 52;
                    deNestingDemo(XID);
                    break;

                case '8':
                    selector = 51;
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
                    double[] linedenester_initial = { 0.527, 0.410, 0.527, 0.263 };
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_initial[0], linedenester_initial[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_initial[2], linedenester_initial[3], 0, 0.1, 0.1);
                    break;

                case '7':
                    double[] linedenester_end = { 0.527, 0.475, 0.527, 0.328 };
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_end[0], linedenester_end[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_end[2], linedenester_end[3], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.005, 0, 0.01, 0.01);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.005, 0, 0.01, 0.01);
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.008, 0, 0.01, 0.01);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.040, 0, 0.01, 0.01);
                    break;

                case '8':
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

        public WaitUntilTriggerParams closingScissorlift(int[] scissorLiftIDs, ushort command_label)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            //Scissor lifts close 
            MoveOpposite(command_label, scissorLiftIDs[2], scissorLiftIDs[0], 0.005, Movement.DIRECTION.X, 0.01, 0.01);
            MoveOpposite(0, scissorLiftIDs[3], scissorLiftIDs[1], 0.005, Movement.DIRECTION.X, 0.01, 0.01);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = scissorLiftIDs[2];
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }

        public WaitUntilTriggerParams linePusherMoves(int line_pusherID, int line_number, WaitUntilTriggerParams wait_params, ushort command_label)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, line_pusherID, TRIGGERSOURCE.CMD_LABEL, wait_params);

            double[] linepusher_end = { 0.525, 0.682 };
            double line_pusher_increment = 0.0127;
            _xbotCommand.LinearMotionSI(command_label, line_pusherID, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linepusher_end[0], linepusher_end[1] + (line_number * line_pusher_increment), 0, 0.5, 0.5);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = line_pusherID;
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }

        public WaitUntilTriggerParams scissorLiftLowers(int[] scissorLiftIDs, WaitUntilTriggerParams wait_params, ushort command_label)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, scissorLiftIDs[0], TRIGGERSOURCE.CMD_LABEL, wait_params);
            _xbotCommand.WaitUntil(0, scissorLiftIDs[2], TRIGGERSOURCE.CMD_LABEL, wait_params);

            _xbotCommand.LinearMotionSI(0, scissorLiftIDs[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
            _xbotCommand.LinearMotionSI(command_label, scissorLiftIDs[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = scissorLiftIDs[2];
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }

        public WaitUntilTriggerParams lineDenesterMovesForward(int[] lineDenesterIDs, WaitUntilTriggerParams wait_params, ushort command_label, bool shifted)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, lineDenesterIDs[0], TRIGGERSOURCE.CMD_LABEL, wait_params);
            _xbotCommand.WaitUntil(0, lineDenesterIDs[1], TRIGGERSOURCE.CMD_LABEL, wait_params);

            double line_shift = 0.0152 / 2;

            /*
            if (shifted == true)
            {
                double[] linedenester_init = { 0.527 - line_shift, 0.410, 0.527 - line_shift, 0.263 };

                _xbotCommand.LinearMotionSI(command_label, lineDenesterIDs[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_init[2], linedenester_init[3], 0, 0.1, 0.1);
                _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_init[0], linedenester_init[1], 0, 0.1, 0.1);

            }
            
            else
            {
                double[] linedenester_init = { 0.527, 0.410, 0.527, 0.263 };
                _xbotCommand.LinearMotionSI(command_label, lineDenesterIDs[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_init[2], linedenester_init[3], 0, 0.1, 0.1);
                _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_init[0], linedenester_init[1], 0, 0.1, 0.1);
            }
            */
            //double[] linedenester_init = { 0.525, 0.460, 0.525, 0.323 };
            double[] linedenester_init = { 0.525, 0.410, 0.525, 0.263 };
            _xbotCommand.LinearMotionSI(command_label, lineDenesterIDs[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_init[2], linedenester_init[3], 0, 0.1, 0.1);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_init[0], linedenester_init[1], 0, 0.1, 0.1);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = lineDenesterIDs[1];
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }

        public WaitUntilTriggerParams lineDenesterGrips(int[] lineDenesterIDs, int line_number,  WaitUntilTriggerParams wait_params, ushort command_label, bool shifted, bool second_run)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams internal_params = new WaitUntilTriggerParams();


            //Some of these wait commands might need to be only for one the de-nest xbots, as one has to
            //wait on the other since they don't move the same amount

            if (second_run == false)
            {
                _xbotCommand.WaitUntil(0, lineDenesterIDs[0], TRIGGERSOURCE.CMD_LABEL, wait_params);
            }
            else if (second_run == true)
            {
                _xbotCommand.WaitUntil(678, lineDenesterIDs[0], TRIGGERSOURCE.CMD_LABEL, wait_params);

                internal_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                internal_params.triggerXbotID = lineDenesterIDs[0];
                internal_params.triggerCmdLabel = 678;


                _xbotCommand.WaitUntil(0, lineDenesterIDs[1], TRIGGERSOURCE.CMD_LABEL, internal_params);
            }

            //Denester moves closer
            double line_pusher_increment = 0.0123;
            //double[] linedenester_end = { 0.527, 0.475, 0.527, 0.328 };
            double[] linedenester_end = { 0.525, 0.495, 0.525, 0.350 };
            double line_shift = 0.0152 / 2;

            /*
            if (shifted == false)
            {
                linedenester_end[0] = 0.527 - line_shift;
                linedenester_end[2] = 0.527 - line_shift;
            }
            */
          
            // turn these into relative movements and add shifted-------------------------------------------------------------------//
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_end[0], linedenester_end[1] + (line_number * line_pusher_increment), 0, 0.1, 0.1);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_end[2], linedenester_end[3] + (line_number * line_pusher_increment), 0, 0.1, 0.1);

            //De-nester lifts syringes
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.005, 0, 0.1, 0.01);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.005, 0, 0.1, 0.01);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.008, 0, 0.1, 0.01);
            _xbotCommand.LinearMotionSI(command_label, lineDenesterIDs[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.045, 0, 0.1, 0.01);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = lineDenesterIDs[1];
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }

        public WaitUntilTriggerParams lineDenesterMovesToCarrier(int[] lineDenesterIDs, WaitUntilTriggerParams wait_params, ushort command_label)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams internal_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, lineDenesterIDs[0], TRIGGERSOURCE.CMD_LABEL, wait_params);
            _xbotCommand.WaitUntil(0, lineDenesterIDs[1], TRIGGERSOURCE.CMD_LABEL, wait_params);

            //moves back
            double[] linedenester_back = { 0.527, 0.360, 0.527, 0.179 };
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back[0], linedenester_back[1], 0, 0.2, 0.2);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back[2], linedenester_back[3], 0, 0.2, 0.2);

            double[] linedenester_totheside = { 0.445, 0.360, 0.445, 0.179 };
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside[0], linedenester_totheside[1], 0, 0.2, 0.2);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside[2], linedenester_totheside[3], 0, 0.2, 0.2);
            
            //rotates
            _xbotCommand.ArcMotionMetersRadians(0, lineDenesterIDs[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.1, 0.1, 0.179, 1.60);
            _xbotCommand.ArcMotionMetersRadians(111, lineDenesterIDs[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.1, 0.1, 0.179, 1.60 - 1.570796);
            
            //Moves to carrier
            internal_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            internal_params.triggerXbotID = lineDenesterIDs[1];
            internal_params.triggerCmdLabel = 111;

            _xbotCommand.WaitUntil(0, lineDenesterIDs[0], TRIGGERSOURCE.CMD_LABEL, internal_params);

            //double[] linedenester_to_unitcarrier = { 0.364, 0.120, 0.558, 0.120 };
            double[] linedenester_to_unitcarrier = { 0.341, 0.120, 0.535, 0.120 };
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.240, 0, 0.2, 0.2);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.240, 0, 0.2, 0.2);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_to_unitcarrier[0], linedenester_to_unitcarrier[1], 0, 0.12, 0.1);
            _xbotCommand.LinearMotionSI(command_label, lineDenesterIDs[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_to_unitcarrier[2], linedenester_to_unitcarrier[3], 0, 0.1, 0.1);

            

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = lineDenesterIDs[1];
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }

        public WaitUntilTriggerParams lowerSyringesIntoCarrier(int[] lineDenesterIDs, WaitUntilTriggerParams wait_params, ushort command_label)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams internal_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, lineDenesterIDs[0], TRIGGERSOURCE.CMD_LABEL, wait_params);
            //_xbotCommand.WaitUntil(0, lineDenesterIDs[1], TRIGGERSOURCE.CMD_LABEL, wait_params);

            time_params.delaySecs = 1;

            _xbotCommand.WaitUntil(0, lineDenesterIDs[1], TRIGGERSOURCE.TIME_DELAY, time_params);
            _xbotCommand.WaitUntil(0, lineDenesterIDs[0], TRIGGERSOURCE.TIME_DELAY, time_params);

            // lower syringes into unit carrier first step
            _xbotCommand.LinearMotionSI(99, lineDenesterIDs[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.033, 0, 0, 0.01, 0.05);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.005, 0, 0, 0.005, 0.05);
            //Lower syringes into unit carrier
            internal_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            internal_params.triggerXbotID = lineDenesterIDs[1];
            internal_params.triggerCmdLabel = 99;

            _xbotCommand.WaitUntil(0, lineDenesterIDs[0], TRIGGERSOURCE.CMD_LABEL, internal_params);

            _xbotCommand.LinearMotionSI(888, lineDenesterIDs[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.003, 0, 0, 0.01, 0.05);

            //Brief shake
            internal_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            internal_params.triggerXbotID = lineDenesterIDs[0];
            internal_params.triggerCmdLabel = 888;

            _xbotCommand.WaitUntil(0, lineDenesterIDs[1], TRIGGERSOURCE.CMD_LABEL, internal_params);
            /*
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.002, 0, 1, 2);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.002, 0, 1, 2);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.004, 0, 1, 2);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.004, 0, 1, 2);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.002, 0, 1, 2);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.002, 0, 1, 2);
            */

            //before moving them down
            _xbotCommand.LinearMotionSI(999, lineDenesterIDs[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.030, 0, 0, 0.01, 0.05);

            // Move line de-nest away from unit carrier
            internal_params.triggerXbotID = lineDenesterIDs[1];
            internal_params.triggerCmdLabel = 999;

            _xbotCommand.WaitUntil(0, lineDenesterIDs[0], TRIGGERSOURCE.CMD_LABEL, internal_params);

            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.05, 0, 0, 0.1, 0.05);
            _xbotCommand.LinearMotionSI(command_label, lineDenesterIDs[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.05, 0, 0, 0.1, 0.05);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = lineDenesterIDs[1];
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }

        public WaitUntilTriggerParams de_nester_back_to_nest(int[] lineDenesterIDs, WaitUntilTriggerParams wait_params, ushort command_label)
        {

            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            _xbotCommand.WaitUntil(0, lineDenesterIDs[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);

            double[] linedenester_coor = { 0.445, 0.360, 0.626, 0.360 };
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_coor[0], linedenester_coor[1], 0, 0.5, 0.5);
            _xbotCommand.LinearMotionSI(0, lineDenesterIDs[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_coor[2], linedenester_coor[3], 0, 0.5, 0.5);


            // rotate line de-nester
            _xbotCommand.ArcMotionMetersRadians(0, lineDenesterIDs[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.5, 0.5, 0.179, 1.60);
            _xbotCommand.ArcMotionMetersRadians(command_label, lineDenesterIDs[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.05, 0.1, 0.179, 1.60 - 1.570796);
            
            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = lineDenesterIDs[1];
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;

        }

        public WaitUntilTriggerParams liftingScissorLift(int[] scissorLiftIDs, WaitUntilTriggerParams wait_params, ushort command_label)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, scissorLiftIDs[0], TRIGGERSOURCE.CMD_LABEL, wait_params);
            _xbotCommand.WaitUntil(0, scissorLiftIDs[1], TRIGGERSOURCE.CMD_LABEL, wait_params);
            _xbotCommand.WaitUntil(0, scissorLiftIDs[2], TRIGGERSOURCE.CMD_LABEL, wait_params);
            _xbotCommand.WaitUntil(0, scissorLiftIDs[3], TRIGGERSOURCE.CMD_LABEL, wait_params);

            double[] scissor_lift = { 0.405, 0.780 - 0.015, 0.405, 0.630 + 0.015, 0.645, 0.780 - 0.015, 0.645, 0.630 + 0.015 };

            _xbotCommand.LinearMotionSI(0, scissorLiftIDs[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_lift[0], scissor_lift[1], 0, 0.1, 0.1);
            _xbotCommand.LinearMotionSI(0, scissorLiftIDs[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_lift[2], scissor_lift[3], 0, 0.1, 0.1);
            _xbotCommand.LinearMotionSI(0, scissorLiftIDs[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_lift[4], scissor_lift[5], 0, 0.1, 0.1);
            _xbotCommand.LinearMotionSI(command_label, scissorLiftIDs[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_lift[6], scissor_lift[7], 0, 0.1, 0.1);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = scissorLiftIDs[3];
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }

        public WaitUntilTriggerParams rotateUnitCarrier(int[] unitCarrierIDs, int unitCarrier , WaitUntilTriggerParams wait_params, ushort command_label)
        {
            double[] carrier_inital = { 0.120, 0.120 };
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, unitCarrierIDs[unitCarrier], TRIGGERSOURCE.CMD_LABEL, wait_params);
            _xbotCommand.LinearMotionSI(0, unitCarrierIDs[unitCarrier], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, carrier_inital[0], carrier_inital[1], 0, 0.1, 0.1);
            _xbotCommand.RotaryMotionP2P(command_label, unitCarrierIDs[unitCarrier], ROTATIONMODE.WRAP_TO_2PI_CW, 1.57076, 2, 2, POSITIONMODE.RELATIVE);

            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = unitCarrierIDs[unitCarrier];
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }


        public WaitUntilTriggerParams switchCarriers(int fullcarrier , int emptycarrier, WaitUntilTriggerParams wait_params, ushort command_label)
        {
            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();
            WaitUntilTriggerParams internal_params = new WaitUntilTriggerParams();

            _xbotCommand.WaitUntil(0, fullcarrier, TRIGGERSOURCE.CMD_LABEL, wait_params);
            _xbotCommand.WaitUntil(0, emptycarrier, TRIGGERSOURCE.CMD_LABEL, wait_params);

            _xbotCommand.LinearMotionSI(333, emptycarrier, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.360, 0.360, 0, 0.5, 0.5);

            internal_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            internal_params.triggerXbotID = emptycarrier;
            internal_params.triggerCmdLabel = 333;

            _xbotCommand.WaitUntil(0, fullcarrier, TRIGGERSOURCE.CMD_LABEL, internal_params);
            _xbotCommand.LinearMotionSI(0, fullcarrier, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, 0.120, 0.840 , 0, 0.5, 0.5);

            _xbotCommand.LinearMotionSI(command_label, emptycarrier, POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.120, 0.120, 0, 0.3, 0.3);



            CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
            CMD_params.triggerXbotID = emptycarrier;
            CMD_params.triggerCmdLabel = command_label;

            return CMD_params;
        }



        public void deNestingDemo(int[] XID)
        {
            int[] xbot_ids = XID;
            selector = 52;
            Console.Clear();
            Console.WriteLine(" De-nesting Demo");
            Console.WriteLine("0    Return ");
            Console.WriteLine("1    Preperation Phase");
            Console.WriteLine("2    Start Demo");
            Console.WriteLine("3    Function based DEMO");
            Console.WriteLine("4    Renesting");
            Console.WriteLine("5    ");
            Console.WriteLine("6    ");
            Console.WriteLine("7    ");
            Console.WriteLine("8    ");
            Console.WriteLine("9    ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            WaitUntilTriggerParams CMD_params = new WaitUntilTriggerParams();

            switch (keyInfo.KeyChar)
            {


                case '0':
                    selector = 51;
                    break;

                case '1':

                    //Inital position for scissor lifts
                    double[] scissor_inital = { 0.400, 0.780 - 0.015, 0.4, 0.630 + 0.015, 0.650, 0.780 - 0.015, 0.650, 0.630 + 0.015 };

                    _xbotCommand.LinearMotionSI(0, XID[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_inital[0], scissor_inital[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_inital[2], scissor_inital[3], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_inital[4], scissor_inital[5], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(30, XID[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_inital[6], scissor_inital[7], 0, 0.1, 0.1);

                    //Inital position for line-pusher
                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params.triggerXbotID = XID[3];
                    CMD_params.triggerCmdLabel = 30;

                    _xbotCommand.WaitUntil(0, XID[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                    double[] linepusher_initial = { 0.525, 0.490 };
                    _xbotCommand.LinearMotionSI(0, XID[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linepusher_initial[0], linepusher_initial[1], 0, 0.1, 0.1);

                    //Initial position for de-nester
                    double[] linedenester_initial = { 0.527, 0.267, 0.527, 0.120 };
                    _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_initial[0], linedenester_initial[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_initial[2], linedenester_initial[3], 0, 0.1, 0.1);

                    //Initial position for carrier
                    double[] carrier_inital = { 0.120, 0.120 };
                    _xbotCommand.LinearMotionSI(0, XID[7], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, carrier_inital[0], carrier_inital[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, XID[8], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, 0.120, 0.840, 0, 0.1, 0.1);


                    break;


                case '2':
                    //Scissor lifts close 
                    MoveOpposite(1, XID[2], XID[0], 0.005, Movement.DIRECTION.X, 0.01, 0.01);
                    MoveOpposite(0, XID[3], XID[1], 0.005, Movement.DIRECTION.X, 0.01, 0.01);
                    CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params.triggerXbotID = XID[2];
                    CMD_params.triggerCmdLabel = 1;

                    bool shifted = false;
                    double line_pusher_increment = 0.0127;
                    double line_shift = 0.0152 / 2;

                    for (int i = 0; i < 4; i++)
                    {
                        //Line-pusher moves under scissorlift
                        if (i == 0)
                        {
                            _xbotCommand.WaitUntil(0, XID[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        }
                        else
                        {
                            CMD_params.triggerXbotID = XID[6];
                            CMD_params.triggerCmdLabel = 11;
                            _xbotCommand.WaitUntil(0, XID[4], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        }

                        double[] linepusher_end = { 0.525, 0.682 };
                        _xbotCommand.LinearMotionSI(2, XID[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linepusher_end[0], linepusher_end[1] + (i * line_pusher_increment), 0, 0.1, 0.1);

                        //Scissor lift lowers 
                        CMD_params.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                        CMD_params.triggerXbotID = XID[4];
                        CMD_params.triggerCmdLabel = 2;

                        _xbotCommand.WaitUntil(0, XID[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, XID[2], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        _xbotCommand.LinearMotionSI(0, XID[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
                        _xbotCommand.LinearMotionSI(3, XID[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);


                        // Move line de-nester forward
                        CMD_params.triggerXbotID = XID[2];
                        CMD_params.triggerCmdLabel = 3;

                        _xbotCommand.WaitUntil(0, XID[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, XID[6], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        if (shifted == true)
                        {
                            double[] linedenester_init = { 0.527 - line_shift, 0.410, 0.527 - line_shift, 0.263 };

                            _xbotCommand.LinearMotionSI(4, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_init[2], linedenester_init[3], 0, 0.1, 0.1);
                            _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_init[0], linedenester_init[1], 0, 0.1, 0.1);

                            shifted = false;
                        }
                        else
                        {
                            double[] linedenester_init = { 0.527, 0.410, 0.527, 0.263 };
                            _xbotCommand.LinearMotionSI(4, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_init[2], linedenester_init[3], 0, 0.1, 0.1);
                            _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_init[0], linedenester_init[1], 0, 0.1, 0.1);
                            shifted = true;
                        }



                        // De-nester grip syringes

                        CMD_params.triggerXbotID = XID[6];
                        CMD_params.triggerCmdLabel = 4;
                        _xbotCommand.WaitUntil(0, XID[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);


                        double[] linedenester_end = { 0.527, 0.475, 0.527, 0.328 };
                        _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_end[0], linedenester_end[1] + (i * line_pusher_increment), 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester_end[2], linedenester_end[3] + (i * line_pusher_increment), 0, 0.1, 0.1);



                        _xbotCommand.LinearMotionSI(40, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.005, 0, 0.1, 0.01);
                        _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.005, 0, 0.1, 0.01);
                        _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.008, 0, 0.1, 0.01);
                        _xbotCommand.LinearMotionSI(5, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.040, 0, 0.1, 0.01);




                        // Move de-nest back and to the side
                        CMD_params.triggerXbotID = XID[6];
                        CMD_params.triggerCmdLabel = 5;

                        _xbotCommand.WaitUntil(0, XID[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        double[] linedenester_back = { 0.527, 0.360, 0.527, 0.179 };
                        _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back[0], linedenester_back[1], 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back[2], linedenester_back[3], 0, 0.1, 0.1);

                        double[] linedenester_totheside = { 0.445, 0.360, 0.445, 0.179 };
                        _xbotCommand.LinearMotionSI(6, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside[0], linedenester_totheside[1], 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside[2], linedenester_totheside[3], 0, 0.1, 0.1);


                        //rotate the line de-nester and lift scissor lift
                        CMD_params.triggerXbotID = XID[5];
                        CMD_params.triggerCmdLabel = 6;

                        _xbotCommand.WaitUntil(0, XID[6], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        _xbotCommand.ArcMotionMetersRadians(0, XID[6], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.05, 0.1, 0.179, 1.65);
                        _xbotCommand.ArcMotionMetersRadians(7, XID[6], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.05, 0.1, 0.179, 1.65 - 1.570796);

                        //Move line de-nester to the side and forward to the unit carrier
                        CMD_params.triggerXbotID = XID[6];
                        CMD_params.triggerCmdLabel = 7;

                        _xbotCommand.WaitUntil(0, XID[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);


                        double[] linedenester_to_unitcarrier = { 0.364, 0.120, 0.554, 0.120 };
                        _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.240, 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.240, 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_to_unitcarrier[0], linedenester_to_unitcarrier[1], 0, 0.12, 0.1);
                        _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_to_unitcarrier[2], linedenester_to_unitcarrier[3], 0, 0.1, 0.1);

                        // lower syringes into unit carrier first step
                        _xbotCommand.LinearMotionSI(8, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.035, 0, 0, 0.01, 0.05);

                        //Lower syringes into unit carrier
                        CMD_params.triggerXbotID = XID[6];
                        CMD_params.triggerCmdLabel = 8;

                        _xbotCommand.WaitUntil(0, XID[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.005, 0, 0, 0.01, 0.05);
                        _xbotCommand.LinearMotionSI(9, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.030, 0, 0, 0.01, 0.05);

                        // Move line de-nest away from unit carrier
                        CMD_params.triggerXbotID = XID[6];
                        CMD_params.triggerCmdLabel = 9;

                        _xbotCommand.WaitUntil(0, XID[5], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        _xbotCommand.LinearMotionSI(10, XID[5], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.05, 0, 0, 0.1, 0.05);
                        _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.05, 0, 0, 0.1, 0.05);

                        // move line de-nester back to nest
                        CMD_params.triggerXbotID = XID[5];
                        CMD_params.triggerCmdLabel = 10;

                        _xbotCommand.WaitUntil(0, XID[6], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        double[] linedenester_coor = { 0.445, 0.360, 0.626, 0.360 };
                        _xbotCommand.LinearMotionSI(0, XID[5], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_coor[0], linedenester_coor[1], 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, XID[6], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_coor[2], linedenester_coor[3], 0, 0.1, 0.1);


                        // rotate line de-nester
                        _xbotCommand.ArcMotionMetersRadians(0, XID[6], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.05, 0.1, 0.179, 1.65);
                        _xbotCommand.ArcMotionMetersRadians(11, XID[6], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.05, 0.1, 0.179, 1.65 - 1.570796);

                        // lift scissor
                        CMD_params.triggerXbotID = XID[5];
                        CMD_params.triggerCmdLabel = 6;

                        _xbotCommand.WaitUntil(0, XID[0], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, XID[1], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, XID[2], TRIGGERSOURCE.CMD_LABEL, CMD_params);
                        _xbotCommand.WaitUntil(0, XID[3], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        double[] scissor_lift = { 0.405, 0.780 - 0.015, 0.405, 0.630 + 0.015, 0.645, 0.780 - 0.015, 0.645, 0.630 + 0.015 };

                        _xbotCommand.LinearMotionSI(0, XID[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_lift[0], scissor_lift[1], 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, XID[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_lift[2], scissor_lift[3], 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(0, XID[2], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_lift[4], scissor_lift[5], 0, 0.1, 0.1);
                        _xbotCommand.LinearMotionSI(30, XID[3], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, scissor_lift[6], scissor_lift[7], 0, 0.1, 0.1);



                        //Rotate unit-carrier 90
                        CMD_params.triggerXbotID = XID[6];
                        CMD_params.triggerCmdLabel = 11;

                        _xbotCommand.WaitUntil(0, XID[7], TRIGGERSOURCE.CMD_LABEL, CMD_params);

                        double[] carrier_init = { 0.120, 0.120 };
                        _xbotCommand.LinearMotionSI(0, XID[7], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, carrier_init[0], carrier_init[1], 0, 0.1, 0.1);
                        _xbotCommand.RotaryMotionP2P(0, XID[7], ROTATIONMODE.WRAP_TO_2PI_CCW, 1.570796, 1, 2, POSITIONMODE.RELATIVE);

                    }
                    break;
                case '3':

                    WaitUntilTriggerParams Closing_scissor_lift = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams Line_pusher_moves = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams Scissor_lift_lowers = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams De_nester_approach = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams De_nester_grips = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams De_nester_moves_to_carrier = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams Lowering_syringes = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams De_nester_moves_back_to_nest = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams unit_carrier_rotates = new WaitUntilTriggerParams();

                    WaitUntilTriggerParams time = new WaitUntilTriggerParams();

                    WaitUntilTriggerParams Switch_carriers = new WaitUntilTriggerParams();

                    WaitUntilTriggerParams Lift_scissor_lift = new WaitUntilTriggerParams();

                    _xbotCommand.MotionBufferControl(xbot_ids[0], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[1], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[2], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[3], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[4], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[5], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[6], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[7], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[8], MOTIONBUFFEROPTIONS.BLOCKBUFFER);


                    bool shifting = true;

                    int[] scissorLiftIDs = { xbot_ids[0], xbot_ids[1], xbot_ids[2], xbot_ids[3] };
                    int[] lineDeNesterIDs = { xbot_ids[5], xbot_ids[6] };
                    int[] unitCarrierIDs = { xbot_ids[7], xbot_ids[8] };
                    //int[] unitCarrierIDs = { xbot_ids[7], xbot_ids[8], xbot_ids[9] };

                    Closing_scissor_lift = closingScissorlift(scissorLiftIDs, 1000);

                    Line_pusher_moves = linePusherMoves(xbot_ids[4], 0, Closing_scissor_lift, 1010);

                    Scissor_lift_lowers = scissorLiftLowers(scissorLiftIDs, Line_pusher_moves, 1020);

                    De_nester_approach = lineDenesterMovesForward(lineDeNesterIDs, Scissor_lift_lowers, 1030, false);

                    ushort last_trigger_label = 1110;


                    for (int i = 0; i < 4; i++)
                    {

                        if (i % 2 == 0)
                        {
                            shifting = true;
                        }
                        else if (i % 2 == 1)
                        {
                            shifting = false;
                        }

                        if (i == 0) //This if-statement is just because the wait command are fucking with us
                        {
                            time.delaySecs = 10;
                            De_nester_grips = lineDenesterGrips(lineDeNesterIDs, i, time, 1040, shifting, false);
                        }
                        else
                        {
                            De_nester_grips = lineDenesterGrips(lineDeNesterIDs, i, De_nester_approach, 1040, shifting, false);
                        }

                        De_nester_moves_to_carrier = lineDenesterMovesToCarrier(lineDeNesterIDs, De_nester_grips, 1050);

                        Lowering_syringes = lowerSyringesIntoCarrier(lineDeNesterIDs, De_nester_moves_to_carrier, 1060);

                        De_nester_moves_back_to_nest = de_nester_back_to_nest(lineDeNesterIDs, Lowering_syringes, 1070);

                        Lift_scissor_lift = liftingScissorLift(scissorLiftIDs, De_nester_moves_to_carrier, 1080);

                        Line_pusher_moves = linePusherMoves(xbot_ids[4], i + 1, Lowering_syringes, 1090);

                        Scissor_lift_lowers = scissorLiftLowers(scissorLiftIDs, De_nester_moves_back_to_nest, 1100);

                        unit_carrier_rotates = rotateUnitCarrier(unitCarrierIDs, 0, Line_pusher_moves, 1105);

                        De_nester_approach = lineDenesterMovesForward(lineDeNesterIDs, Scissor_lift_lowers, last_trigger_label, shifting);

                        last_trigger_label++;

                    }

                    Switch_carriers = switchCarriers(unitCarrierIDs[0], unitCarrierIDs[1], De_nester_approach, 1120);
                    last_trigger_label++;

                    for (int i = 4; i < 8; i++)
                    {

                        if (i % 2 == 0)
                        {
                            shifting = true;
                        }
                        else if (i % 2 == 1)
                        {
                            shifting = false;
                        }

                        if (i == 4)
                        {

                            //The wait commands here are a bit iffy, I think it is something within the function itself which I made to avoid exactly this.
                            De_nester_grips = lineDenesterGrips(lineDeNesterIDs, i, Switch_carriers, 2040, shifting, true); //Something here doesn't work, the rest should

                        }
                        else
                        {
                            De_nester_grips = lineDenesterGrips(lineDeNesterIDs, i, De_nester_approach, 2045, shifting, false);
                        }

                        De_nester_moves_to_carrier = lineDenesterMovesToCarrier(lineDeNesterIDs, De_nester_grips, 2050);

                        Lowering_syringes = lowerSyringesIntoCarrier(lineDeNesterIDs, De_nester_moves_to_carrier, 2060);

                        De_nester_moves_back_to_nest = de_nester_back_to_nest(lineDeNesterIDs, Lowering_syringes, 2070);

                        Lift_scissor_lift = liftingScissorLift(scissorLiftIDs, De_nester_moves_to_carrier, 2080);

                        Line_pusher_moves = linePusherMoves(xbot_ids[4], i + 1, Lowering_syringes, 2090);

                        Scissor_lift_lowers = scissorLiftLowers(scissorLiftIDs, De_nester_moves_back_to_nest, 2100);

                        unit_carrier_rotates = rotateUnitCarrier(unitCarrierIDs, 1, Line_pusher_moves, 2105);
                        if (i < 7)
                        {
                            De_nester_approach = lineDenesterMovesForward(lineDeNesterIDs, Scissor_lift_lowers, last_trigger_label, shifting);
                        }
                        

                        last_trigger_label++;

                    }



                    _xbotCommand.MotionBufferControl(xbot_ids[0], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[1], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[2], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[3], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[4], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[5], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[6], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[7], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[8], MOTIONBUFFEROPTIONS.RELEASEBUFFER);


                    break;

                case '4':

                    _xbotCommand.MotionBufferControl(xbot_ids[0], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[1], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[2], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[3], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[4], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[5], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[6], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[7], MOTIONBUFFEROPTIONS.BLOCKBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[8], MOTIONBUFFEROPTIONS.BLOCKBUFFER);

                    WaitUntilTriggerParams time_params = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams CMD_params1 = new WaitUntilTriggerParams();
                    WaitUntilTriggerParams internal_params = new WaitUntilTriggerParams();

                    int[] scissorLiftIDs1 = { xbot_ids[0], xbot_ids[1], xbot_ids[2], xbot_ids[3] };
                    int[] lineDeNesterIDs1 = { xbot_ids[5], xbot_ids[6] };
                    int[] unitCarrierIDs1 = { xbot_ids[7], xbot_ids[8] };
                    time_params.delaySecs = 1;
                    //Closing_scissor_lift = closingScissorlift(scissorLiftIDs1, 1000);
                    //Closing_scissor_lift = closingScissorlift(scissorLiftIDs1, 1000);
                    //liftingScissorLift(scissorLiftIDs1, time_params, 0);
                    
                    double line_pusher_increment1 = 0.0127;
                    double[] linepusher_end1 = { 0.525, 0.682 };
                    _xbotCommand.LinearMotionSI(0, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linepusher_end1[0], linepusher_end1[1], 0, 0.5, 0.5);

                    //_xbotCommand.LinearMotionSI(0, scissorLiftIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
                    //_xbotCommand.LinearMotionSI(0, scissorLiftIDs1[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);

                    double[] linedenester_back1 = { 0.527, 0.360, 0.527, 0.179 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back1[0], linedenester_back1[1], 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back1[2], linedenester_back1[3], 0, 0.2, 0.2);


                    double[] linedenester_totheside1 = { 0.445, 0.360, 0.445, 0.179 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside1[0], linedenester_totheside1[1], 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside1[2], linedenester_totheside1[3], 0, 0.2, 0.2);

                    //rotates
                    _xbotCommand.ArcMotionMetersRadians(0, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.1, 0.1, 0.179, 1.60);
                    _xbotCommand.ArcMotionMetersRadians(1, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.1, 0.1, 0.179, 1.60 - 1.570796);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 1;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.240, 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(2, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.049, -0.240, 0, 0.2, 0.2);


                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 2;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);


                    double[] linedenester_to_unitcarrier1 = { 0.343, 0.120, 0.474, 0.120 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_to_unitcarrier1[0], linedenester_to_unitcarrier1[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_to_unitcarrier1[2], linedenester_to_unitcarrier1[3], 0, 0.1, 0.1);


                    double[] Lift_syringes = { 0.540, 0.120 };
                    _xbotCommand.LinearMotionSI(4, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, Lift_syringes[0], Lift_syringes[1], 0, 0.1, 0.1);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 4;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    double[] linedenester3 = { 0.445, 0.360, 0.626, 0.360 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester3[0], linedenester3[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester3[2], linedenester3[3], 0, 0.1, 0.1);

                    _xbotCommand.ArcMotionMetersRadians(0, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.5, 0.5, 0.179, 1.60);
                    _xbotCommand.ArcMotionMetersRadians(5, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.05, 0.1, 0.179, 1.60 - 1.570796);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 5;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    double[] linedenester4 = { 0.529, 0.493, 0.529, 0.302 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester4[2], linedenester4[3], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(6, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester4[0], linedenester4[1], 0, 0.11, 0.1);


                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[0];
                    CMD_params1.triggerCmdLabel = 6;


                    _xbotCommand.WaitUntil(0, unitCarrierIDs1[1], TRIGGERSOURCE.CMD_LABEL, CMD_params1);
                    _xbotCommand.LinearMotionSI(0, unitCarrierIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, 0.120, 0.120, 0, 0.1, 0.1);
                    _xbotCommand.RotaryMotionP2P(0, unitCarrierIDs1[1], ROTATIONMODE.WRAP_TO_2PI_CW, 1.57076, 2, 2, POSITIONMODE.RELATIVE);

                    //time_params.delaySecs = 1;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[1], TRIGGERSOURCE.TIME_DELAY, time_params);
                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.TIME_DELAY, time_params);

                    _xbotCommand.LinearMotionSI(7, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.529, 0.310, 0, 0.01, 0.01);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 7;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.002, 0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.002, 0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.004, -0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.004, -0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.002, 0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.002, 0, 0, 1, 2);

                    _xbotCommand.LinearMotionSI(8, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.529, 0.334, 0, 0.01, 0.01);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 8;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    //double[] linedenester_back1 = { 0.527, 0.360, 0.527, 0.179 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back1[0], linedenester_back1[1], 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(9, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back1[2], linedenester_back1[3], 0, 0.2, 0.2);


                    //First line complete ---------------------------------------------------------------
                    //Now moving scissorlift and 

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 9;

                    Lift_scissor_lift = liftingScissorLift(scissorLiftIDs1, CMD_params1, 1080);

                    _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, Lift_scissor_lift);

                    double[] linepusher_end2 = { 0.525, 0.682 + line_pusher_increment1 };
                    _xbotCommand.LinearMotionSI(10, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linepusher_end2[0], linepusher_end2[1], 0, 0.5, 0.5);


                    _xbotCommand.LinearMotionSI(0, scissorLiftIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
                    _xbotCommand.LinearMotionSI(0, scissorLiftIDs1[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
                    // Scissorlift has moved------------------------------------------------------------------------

                    //double[] linedenester_totheside1 = { 0.445, 0.360, 0.445, 0.179 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside1[0], linedenester_totheside1[1], 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside1[2], linedenester_totheside1[3], 0, 0.2, 0.2);

                    //rotates
                    _xbotCommand.ArcMotionMetersRadians(0, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.1, 0.1, 0.179, 1.60);
                    _xbotCommand.ArcMotionMetersRadians(1, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.1, 0.1, 0.179, 1.60 - 1.570796);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 1;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.240, 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(2, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.049, -0.240, 0, 0.2, 0.2);


                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 2;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);


                    //double[] linedenester_to_unitcarrier1 = { 0.343, 0.120, 0.474, 0.120 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_to_unitcarrier1[0], linedenester_to_unitcarrier1[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_to_unitcarrier1[2], linedenester_to_unitcarrier1[3], 0, 0.1, 0.1);


                    //double[] Lift_syringes = { 0.540, 0.120 };
                    _xbotCommand.LinearMotionSI(4, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, Lift_syringes[0], Lift_syringes[1], 0, 0.1, 0.1);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 4;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    //double[] linedenester3 = { 0.445, 0.360, 0.626, 0.360 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester3[0], linedenester3[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester3[2], linedenester3[3], 0, 0.1, 0.1);

                    _xbotCommand.ArcMotionMetersRadians(0, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.5, 0.5, 0.179, 1.60);
                    _xbotCommand.ArcMotionMetersRadians(5, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.05, 0.1, 0.179, 1.60 - 1.570796);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 5;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    double[] linedenester41 = {0.529 - 0.0076, 0.493 + line_pusher_increment1, 0.529 - 0.0076, 0.302 + line_pusher_increment1 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester41[2], linedenester41[3], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(6, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester41[0], linedenester41[1], 0, 0.11, 0.1);


                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[0];
                    CMD_params1.triggerCmdLabel = 6;


                    _xbotCommand.WaitUntil(0, unitCarrierIDs1[1], TRIGGERSOURCE.CMD_LABEL, CMD_params1);
                    _xbotCommand.LinearMotionSI(0, unitCarrierIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, 0.120, 0.120, 0, 0.1, 0.1);
                    _xbotCommand.RotaryMotionP2P(0, unitCarrierIDs1[1], ROTATIONMODE.WRAP_TO_2PI_CW, 1.57076, 2, 2, POSITIONMODE.RELATIVE);

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[1], TRIGGERSOURCE.TIME_DELAY, time_params);
                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.TIME_DELAY, time_params);

                    _xbotCommand.LinearMotionSI(7, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.529, 0.310+line_pusher_increment1, 0, 0.01, 0.01);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 7;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.002, 0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.002, 0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.004, -0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.004, -0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.002, 0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.002, 0, 0, 1, 2);

                    _xbotCommand.LinearMotionSI(8, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.529, 0.334+line_pusher_increment1, 0, 0.01, 0.01);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 8;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    //double[] linedenester_back1 = { 0.527, 0.360, 0.527, 0.179 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back1[0], linedenester_back1[1], 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(9, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back1[2], linedenester_back1[3], 0, 0.2, 0.2);


                    //First line complete ---------------------------------------------------------------
                    //Now moving scissorlift and 

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 9;

                    Lift_scissor_lift = liftingScissorLift(scissorLiftIDs1, CMD_params1, 1080);

                    _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, Lift_scissor_lift);

                    double[] linepusher_end3 = { 0.525, 0.682 + line_pusher_increment1*2 };
                    _xbotCommand.LinearMotionSI(10, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linepusher_end3[0], linepusher_end3[1], 0, 0.5, 0.5);


                    _xbotCommand.LinearMotionSI(0, scissorLiftIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
                    _xbotCommand.LinearMotionSI(0, scissorLiftIDs1[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
                    // Scissorlift has moved------------------------------------------------------------------------

                    //double[] linedenester_totheside1 = { 0.445, 0.360, 0.445, 0.179 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside1[0], linedenester_totheside1[1], 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside1[2], linedenester_totheside1[3], 0, 0.2, 0.2);
                    
                    //rotates
                    _xbotCommand.ArcMotionMetersRadians(0, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.1, 0.1, 0.179, 1.60);
                    _xbotCommand.ArcMotionMetersRadians(20, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.1, 0.1, 0.179, 1.60 - 1.570796);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 20;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.240, 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(21, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.049, -0.240, 0, 0.2, 0.2);


                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 21;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);


                    //double[] linedenester_to_unitcarrier1 = { 0.343, 0.120, 0.474, 0.120 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_to_unitcarrier1[0], linedenester_to_unitcarrier1[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_to_unitcarrier1[2], linedenester_to_unitcarrier1[3], 0, 0.1, 0.1);


                    //double[] Lift_syringes = { 0.540, 0.120 };
                    _xbotCommand.LinearMotionSI(22, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, Lift_syringes[0], Lift_syringes[1], 0, 0.1, 0.1);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 22;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    //double[] linedenester3 = { 0.445, 0.360, 0.626, 0.360 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester3[0], linedenester3[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester3[2], linedenester3[3], 0, 0.1, 0.1);

                    _xbotCommand.ArcMotionMetersRadians(0, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.5, 0.5, 0.179, 1.60);
                    _xbotCommand.ArcMotionMetersRadians(23, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.05, 0.1, 0.179, 1.60 - 1.570796);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 23;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    double[] linedenester42 = { 0.529, 0.493 + line_pusher_increment1* 2, 0.529, 0.302 + line_pusher_increment1* 2 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester42[2], linedenester42[3], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(24, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester42[0], linedenester42[1], 0, 0.11, 0.1);


                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[0];
                    CMD_params1.triggerCmdLabel = 24;

                    /* We only do it 3 times so we don't need to spin a third time.
                    _xbotCommand.WaitUntil(0, unitCarrierIDs1[1], TRIGGERSOURCE.CMD_LABEL, CMD_params1);
                    _xbotCommand.LinearMotionSI(0, unitCarrierIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, 0.120, 0.120, 0, 0.1, 0.1);
                    _xbotCommand.RotaryMotionP2P(0, unitCarrierIDs1[1], ROTATIONMODE.WRAP_TO_2PI_CW, 1.57076, 2, 2, POSITIONMODE.RELATIVE);
                    */

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[1], TRIGGERSOURCE.TIME_DELAY, time_params);
                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.TIME_DELAY, time_params);

                    _xbotCommand.LinearMotionSI(25, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.529, 0.310 + line_pusher_increment1 *2, 0, 0.01, 0.01);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 25;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.002, 0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.002, 0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.004, -0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.004, -0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.002, 0, 0, 1, 2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0.002, 0, 0, 1, 2);

                    _xbotCommand.LinearMotionSI(26, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.529, 0.334 + line_pusher_increment1* 2, 0, 0.01, 0.01);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 26;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    //double[] linedenester_back1 = { 0.527, 0.360, 0.527, 0.179 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back1[0], linedenester_back1[1], 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(9, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back1[2], linedenester_back1[3], 0, 0.2, 0.2);

                    //First line complete ---------------------------------------------------------------
                    //Now moving scissorlift and 

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 9;

                    Lift_scissor_lift = liftingScissorLift(scissorLiftIDs1, CMD_params1, 1080);

                    _xbotCommand.WaitUntil(0, xbot_ids[4], TRIGGERSOURCE.CMD_LABEL, Lift_scissor_lift);

                    double[] linepusher_end4 = { 0.525, 0.682 + line_pusher_increment1 *3 };
                    _xbotCommand.LinearMotionSI(10, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linepusher_end4[0], linepusher_end4[1], 0, 0.5, 0.5);


                    _xbotCommand.LinearMotionSI(0, scissorLiftIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
                    _xbotCommand.LinearMotionSI(0, scissorLiftIDs1[2], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, 0.04, 0, 0.05, 0.1);
                    // Scissorlift has moved------------------------------------------------------------------------

                    /*
                    //double[] linedenester_totheside1 = { 0.445, 0.360, 0.445, 0.179 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside1[0], linedenester_totheside1[1], 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_totheside1[2], linedenester_totheside1[3], 0, 0.2, 0.2);

                    //rotates
                    _xbotCommand.ArcMotionMetersRadians(0, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.1, 0.1, 0.179, 1.60);
                    _xbotCommand.ArcMotionMetersRadians(1, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.1, 0.1, 0.179, 1.60 - 1.570796);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 1;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, 0, -0.240, 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(2, lineDeNesterIDs1[1], POSITIONMODE.RELATIVE, LINEARPATHTYPE.YTHENX, -0.049, -0.240, 0, 0.2, 0.2);


                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 2;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);


                    //double[] linedenester_to_unitcarrier1 = { 0.343, 0.120, 0.474, 0.120 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_to_unitcarrier1[0], linedenester_to_unitcarrier1[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_to_unitcarrier1[2], linedenester_to_unitcarrier1[3], 0, 0.1, 0.1);


                    //double[] Lift_syringes = { 0.540, 0.120 };
                    _xbotCommand.LinearMotionSI(4, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, Lift_syringes[0], Lift_syringes[1], 0, 0.1, 0.1);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 4;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    //double[] linedenester3 = { 0.445, 0.360, 0.626, 0.360 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester3[0], linedenester3[1], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester3[2], linedenester3[3], 0, 0.1, 0.1);

                    _xbotCommand.ArcMotionMetersRadians(0, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.CLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.5, 0.5, 0.179, 1.60);
                    _xbotCommand.ArcMotionMetersRadians(5, lineDeNesterIDs1[1], ARCMODE.CENTERANGLE, ARCTYPE.MINORARC, ARCDIRECTION.COUNTERCLOCKWISE, POSITIONMODE.ABSOLUTE, 0.445, 0.360, 0, 0.05, 0.1, 0.179, 1.60 - 1.570796);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 5;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    double[] linedenester43 = { 0.529, 0.493 + line_pusher_increment1*3, 0.529, 0.302 + line_pusher_increment1*3 };
                    //double[] linedenester43 = { 0.524, 0.524, 0.529 - 0.0076, 0.328 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester42[2], linedenester43[3], 0, 0.1, 0.1);
                    _xbotCommand.LinearMotionSI(6, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, linedenester43[0], linedenester43[1], 0, 0.11, 0.1);

                    
                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[0];
                    CMD_params1.triggerCmdLabel = 6;


                    _xbotCommand.WaitUntil(0, unitCarrierIDs1[1], TRIGGERSOURCE.CMD_LABEL, CMD_params1);
                    _xbotCommand.LinearMotionSI(0, unitCarrierIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, 0.120, 0.120, 0, 0.1, 0.1);
                    _xbotCommand.RotaryMotionP2P(0, unitCarrierIDs1[1], ROTATIONMODE.WRAP_TO_2PI_CW, 1.57076, 2, 2, POSITIONMODE.RELATIVE);

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[1], TRIGGERSOURCE.TIME_DELAY, time_params);
                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.TIME_DELAY, time_params);

                    _xbotCommand.LinearMotionSI(7, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.524, 0.310 + line_pusher_increment1*3, 0, 0.01, 0.01);

                    
                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 7;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    _xbotCommand.LinearMotionSI(8, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.XTHENY, 0.529, 0.334 + line_pusher_increment1*3, 0, 0.01, 0.01);

                    CMD_params1.CmdLabelTriggerType = TRIGGERCMDLABELTYPE.CMD_FINISH;
                    CMD_params1.triggerXbotID = lineDeNesterIDs1[1];
                    CMD_params1.triggerCmdLabel = 8;

                    _xbotCommand.WaitUntil(0, lineDeNesterIDs1[0], TRIGGERSOURCE.CMD_LABEL, CMD_params1);

                    //double[] linedenester_back1 = { 0.527, 0.360, 0.527, 0.179 };
                    _xbotCommand.LinearMotionSI(0, lineDeNesterIDs1[0], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back1[0], linedenester_back1[1], 0, 0.2, 0.2);
                    _xbotCommand.LinearMotionSI(9, lineDeNesterIDs1[1], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.YTHENX, linedenester_back1[2], linedenester_back1[3], 0, 0.2, 0.2);
                    */


                    _xbotCommand.MotionBufferControl(xbot_ids[0], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[1], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[2], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[3], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[4], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[5], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[6], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[7], MOTIONBUFFEROPTIONS.RELEASEBUFFER);
                    _xbotCommand.MotionBufferControl(xbot_ids[8], MOTIONBUFFEROPTIONS.RELEASEBUFFER);






                    break;
            }
            }
    }
}
