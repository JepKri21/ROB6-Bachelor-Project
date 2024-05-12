using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Xml.Serialization;
using System.Diagnostics;
using PMCLIB;

namespace simple_pmc_mover
{
    internal class capping : Movement
    {

        //this class contains a collection of system commands such as connecting to the PMC, gain mastership, etc.
        private static SystemCommands _systemCommand = new SystemCommands();
        //this class contains a collection of xbot commands, such as discover xbots, mobility control, linear motion, etc.
        private static XBotCommands _xbotCommand = new XBotCommands();

        char SOT = '<';
        char EOT = '>';
        int sides_to_fill = 4;
        int syringes_to_fill = 10;
        string PIN = "2";

        double[] start_position = { 0.111, 0.293 };

        bool finished_cap = false;
        bool receiving = false;
        string received_data = "";
        SerialPort _serialPort;



        public void PerformCapping(int[] XID)
        {
            // first we set up the communication
            Console.WriteLine("capping thread running");

            int[] xbot_ids = XID;
            _serialPort = new SerialPort();
            _serialPort.PortName = "COM7";
            _serialPort.BaudRate = 115200;
            XBotStatus status = _xbotCommand.GetXbotStatus(xbot_ids[4]);
            
            while (true)
            {
                while (_serialPort.IsOpen == false)
                {
                    _serialPort.Open();
                }

               

                // the xbot is now under the capper
                // move syringe under the capper

                for (int i = 0; i < sides_to_fill; i++)
                {
                    DetectCapping(xbot_ids);
                    for (int j = 0; j < syringes_to_fill; j++)
                    {
                        

                        //Console.WriteLine("the gates are open");
                        _serialPort.WriteLine(SOT.ToString());
                        _serialPort.WriteLine(PIN);
                        _serialPort.WriteLine(EOT.ToString());

                        //Console.WriteLine("eot send");

                        while (finished_cap == false)
                        {
                            // do nothing
                            //Console.WriteLine("entered receive loop");
                            finished_cap = ReceiveData(_serialPort);

                        }
                        finished_cap = false;
                        //Console.WriteLine("syringe capped, moving to next");

                        

                        status = _xbotCommand.GetXbotStatus(xbot_ids[4]);
                        while (status.XBOTState != XBOTSTATE.XBOT_IDLE)
                        {
                            status = _xbotCommand.GetXbotStatus(xbot_ids[4]);
                        }
                        _xbotCommand.LinearMotionSI(4, xbot_ids[4], POSITIONMODE.RELATIVE, LINEARPATHTYPE.DIRECT, 0, 0.015, 0, 0.5, 1);

                        
                    }

                    if (i < 3)
                    {
                        _xbotCommand.LinearMotionSI(4, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, 0.120, 0.120, 0, 0.5, 1);
                        _xbotCommand.RotaryMotionP2P(0, xbot_ids[4], ROTATIONMODE.WRAP_TO_2PI_CW, 1.570796, 3, 6, POSITIONMODE.RELATIVE);
                        _xbotCommand.LinearMotionSI(4, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, start_position[0], start_position[1], 0, 0.5, 1);
                        //rotate the syringe holder
                    }
                    else
                    {
                        _xbotCommand.LinearMotionSI(4, xbot_ids[4], POSITIONMODE.ABSOLUTE, LINEARPATHTYPE.DIRECT, 0.120, 0.120, 0, 0.5, 1);
                    }
                }
                _serialPort.Close();
                
            }
        }


        bool ReceiveData(SerialPort _serialport)
        {
            _serialPort.Dispose();
            bool end = false;
            _serialPort.Open();
            while (end == false) {

                //Console.WriteLine("reading string ");
                string incoming_string = _serialPort.ReadLine();
                //Console.WriteLine("received string is: "+ incoming_string);

                if (incoming_string[0] == SOT)
                {
                    received_data = "";
                    receiving = true;
                    //Console.WriteLine("SOT received");

                }
                else if (incoming_string[0] == EOT) {
                    receiving = false;
                    
                    //Console.WriteLine("EOT received");
                    return true;
                }
                else if (receiving)
                {
                    //Console.WriteLine("Entered receiving");
                    received_data += incoming_string;
                }
            }
            //hopefully we should not reach this point
            return false;
        }


        private void port_DataReceived(object sender,SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer in the output window
            Debug.WriteLine("data : " + _serialPort.ReadExisting());
        }

        void DetectCapping(int[] XID)
        {
            int[] xbot_ids = XID;

            XBotStatus status = _xbotCommand.GetXbotStatus(xbot_ids[4]);
            double[] position = status.FeedbackPositionSI;
            

            while (!IsObjectWithinThreshold(position[0], position[1],0.0001))
            {
                status = _xbotCommand.GetXbotStatus(xbot_ids[4]);
                position = status.FeedbackPositionSI;
                
            }
            //Console.WriteLine("detected lets go");
        }
        static bool IsObjectWithinThreshold(double x, double y, double threshold)
        {
            // Replace this logic with your own condition to check if the object is within the threshold
            // For example, check if the distance between (x, y) and a reference point is less than the threshold
            double referenceX = 0.111;
            double referenceY = 0.293;
            double distance = Math.Sqrt(Math.Pow(x - referenceX, 2) + Math.Pow(y - referenceY, 2));
            return distance < threshold;
        }

    }

}
