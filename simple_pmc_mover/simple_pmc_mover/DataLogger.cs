using PMCLIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace simple_pmc_mover
{
    internal class DataLogger
    {
        private static SystemCommands _systemCommand = new SystemCommands();
        //this class contains a collection of xbot commands, such as discover xbots, mobility control, linear motion, etc.
        private static XBotCommands _xbotCommand = new XBotCommands();

        

      
        String filename_xbot1 = "positions_xbot_1.CSV";

        String filename_xbot2 = "positions_xbot_2.CSV";
        public bool stopLoop = false;
        public void sampler(object state)//DataTable table1, DataTable table2, XBotStatus status, int[] xbots_to_sample, double[,] positions)
        {


            Params _params = (Params)state;
            
            _params.status = _xbotCommand.GetXbotStatus(_params.xbots_to_sample[0]);



            for (int i = 0; i < 6; i++)
            {
                _params.positions[0, i] = i;// status.FeedbackPositionSI[i];
                

            }

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("sampling1");
                _params.positions[1, i] = i;//status.FeedbackPositionSI[i];
            }


            _params.table1.Rows.Add(_params.positions[0, 0], _params.positions[0, 1], _params.positions[0, 2], _params.positions[0, 3], _params.positions[0, 4], _params.positions[0, 5]);
            _params.table2.Rows.Add(_params.positions[1, 0], _params.positions[1, 1], _params.positions[1, 2], _params.positions[1, 3], _params.positions[1, 4], _params.positions[1, 5]);
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar == 'H' || key.KeyChar == 'h')
            {
                Console.WriteLine("Saving");
                DataTableToCsv(_params.table1, filename_xbot1);
                DataTableToCsv(_params.table2, filename_xbot2);
                
                stopLoop = true;
                return;
            }

        }

        public void performSampling()
        {
            String filename_xbot1 = "positions_xbot_1.CSV";

            String filename_xbot2 = "positions_xbot_2.CSV";

            int[] xbots_to_sample = { 1, 2 };

            double[,] positions = new double[xbots_to_sample.Length, 6];

            DataTable table1 = new DataTable();
            DataTable table2 = new DataTable();

            int samplingInterval = 50;

            XBotStatus status = new XBotStatus();

            //table1.Columns.Add("Ts", typeof(long));
            table1.Columns.Add("X", typeof(double));
            table1.Columns.Add("Y", typeof(double));
            table1.Columns.Add("Z", typeof(double));
            table1.Columns.Add("Rx", typeof(double));
            table1.Columns.Add("Ry", typeof(double));
            table1.Columns.Add("Rz", typeof(double));

            //table1.Columns.Add("Ts", typeof(long));
            table2.Columns.Add("X", typeof(double));
            table2.Columns.Add("Y", typeof(double));
            table2.Columns.Add("Z", typeof(double));
            table2.Columns.Add("Rx", typeof(double));
            table2.Columns.Add("Ry", typeof(double));
            table2.Columns.Add("Rz", typeof(double));

            

            var _params = new Params
            {
                table1 = table1,
                table2 = table2,
                status = status,
                xbots_to_sample = xbots_to_sample,
                positions = positions,
            };

            
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar == 'R' || key.KeyChar == 'r' && stopLoop ==false)
                {
                    Console.WriteLine("sampling time");
                    var sampleTimer = new System.Threading.Timer(sampler, _params, 0, 50);
                    stopLoop = true;
                    
                }

                
                else if (key.KeyChar == 'H' || key.KeyChar == 'e')
                {
                    Console.Clear();
                    Console.WriteLine("Saving");
                    
                    DataTableToCsv(table1,filename_xbot1);
                    DataTableToCsv(table2, filename_xbot2);
                }
                Console.Clear();
                Console.WriteLine("returned");

            }
        }

        



        static void DataTableToCsv(DataTable dataTable, string filename)
        {
            string[] paths = { Directory.GetCurrentDirectory(), filename };
            String filePath = Path.Combine(paths);
            // Write the DataTable to a CSV file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the column headers
                foreach (DataColumn column in dataTable.Columns)
                {
                    writer.Write(column.ColumnName);
                    writer.Write(",");
                }
                writer.WriteLine(); // Move to the next line after writing headers

                // Write the rows
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString());
                        if (i < dataTable.Columns.Count - 1)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine(); // Move to the next line after writing a row
                }
            }
        }
    }
}
