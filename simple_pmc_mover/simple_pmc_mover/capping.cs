using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Xml.Serialization;
using System.Diagnostics;
using PMCLIB;
using System.Security.Cryptography;
using System.Data;
using System.Reflection;
using System.Threading;

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
        public int carrierIndex = -1;

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

            int[] cariier_xbotIDs = { xbot_ids[4], xbot_ids[5], xbot_ids[6], xbot_ids[7] };
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

                for (int i = 0;i<40;i++ ) 
                {
                    DetectCapping(cariier_xbotIDs);

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

                    
                }
                _serialPort.Close();
                carrierIndex = -1;

                //Console.WriteLine("Serial port close" + "carrier index" + carrierIndex);

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

            double[] targetPosition = { 0.111, 0.293 };
            int[] cariier_xbotIDs = XID;
            while (carrierIndex == -1)
            {
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
                            //Console.WriteLine("hej");

                            break;
                        case 1:
                            currentPosition = position2;
                            //Console.WriteLine("hej");
                            break;
                        case 2:
                            currentPosition = position3;
                            //Console.WriteLine("hej");
                            break;
                        case 3:
                            currentPosition = position4;
                            //Console.WriteLine("hej");
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
            }
            XBotStatus status = _xbotCommand.GetXbotStatus(cariier_xbotIDs[carrierIndex]);
            double[] position = status.FeedbackPositionSI;

            while (!IsObjectWithinThreshold1(position[0], position[1],0.0001))
            {
                status = _xbotCommand.GetXbotStatus(cariier_xbotIDs[carrierIndex]);
                position = status.FeedbackPositionSI;
                
            }
            position[0] = Math.Round(position[0], 3);
            position[1] = Math.Round(position[1], 3);
            if (position[0] == 0.360 && position[1] == 0.120)
            {
                carrierIndex = -1; // Store the index of the matching carrier

            }

            //Console.WriteLine("detected lets go");
        }
        static bool IsObjectWithinThreshold1(double x, double y, double threshold)
        {
            double distance = double.MaxValue;

            double referenceX = 0.111;
            double[] referenceY = { 0.293, 0.308, 0.323, 0.338, 0.353, 0.368, 0.383, 0.398, 0.413, 0.428 };
            for (int i = 0; i < 10; i++)
            {
                double currentDistance = Math.Sqrt(Math.Pow(x - referenceX, 2) + Math.Pow(y - referenceY[i], 2));

                if (currentDistance < threshold)
                {
                    distance = currentDistance; // Update distance if within threshold
                    break;
                }
            }

            return distance < threshold;
        }

        void DetectCapping2(int[] XID)
        {
            int[] xbot_ids = XID;
            double referenceX = 0.111;
            double[] referenceY = { 0.293, 0.308, 0.323, 0.338, 0.353, 0.368, 0.383, 0.398, 0.413, 0.428 };
            double threshold = 0.0001;

            foreach (var xbot_id in xbot_ids)
            {
                bool isWithinThreshold = false;

                while (!isWithinThreshold)
                {
                    XBotStatus status = _xbotCommand.GetXbotStatus(xbot_id);
                    double[] position = status.FeedbackPositionSI;

                    foreach (var refY in referenceY)
                    {
                        if (IsObjectWithinThreshold(position[0], position[1], referenceX, refY, threshold))
                        {
                            isWithinThreshold = true;
                            Console.WriteLine($"Xbot {xbot_id} detected at reference position (X: {referenceX}, Y: {refY}).");
                            break;
                        }
                    }

                    if (!isWithinThreshold)
                    {
                        // Optionally, add a small delay here to avoid a tight loop
                        //System.Threading.Thread.Sleep(100); // Sleep for 100ms
                    }
                }
            }
        }


        static bool IsObjectWithinThreshold(double x, double y, double referenceX, double referenceY, double threshold)
        {
            double distance = Math.Sqrt(Math.Pow(x - referenceX, 2) + Math.Pow(y - referenceY, 2));
            return distance < threshold;
        }

    }

}
