using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Xml.Linq;
using MetroFramework;
using MetroFramework.Forms;



namespace DatabaseUI
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            /*
            metroStyleManager1.Theme = metroStyleManager1.Theme == MetroThemeStyle.Light ? MetroThemeStyle.Dark : MetroThemeStyle.Light;
            this.Theme = metroStyleManager1.Theme;
            this.Refresh();
            */
        }

        // #### GLOBAL  VARABLES ##############################################################################################################################################

        // connection string defaults you to the master database, enabling me to create and drop databases!
        public String Project_SQLConnect = "Server=localhost;Integrated security=SSPI;database=master";


        public static String Project_Database = "CsharpDB";
        public static String Project_SQLFolder = "D:\\_WORKING\\Databases";

        private SqlCommand SQL_Command = null;
        private SqlDataReader reader = null;

        private String SQL_Query = null;



        public String Project_DBConnect = "Server=localhost;Integrated security=SSPI;Initial Catalog=" + Project_Database;

        public String query_drop_database = @"ALTER DATABASE {DBNAME} SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{DBNAME}]";
        public String query_make_database = "CREATE DATABASE {DBNAME} ON PRIMARY " +
                                            "(NAME = {DBNAME}_Data, " +
                                            "FILENAME = '" + Project_SQLFolder + "\\{DBNAME}.mdf', " +
                                            "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
                                            "LOG ON (NAME = {DBNAME}_Log, " +
                                            "FILENAME = '" + Project_SQLFolder + "\\{DBNAME}_Log.ldf', " +
                                            "SIZE = 1MB, " +
                                            "MAXSIZE = 5MB, " +
                                            "FILEGROWTH = 10%)";




        // Called when you are done with the application 
        // Or from Close button  
        private void AppExit()
        {
            string msg = "App Exit at User Request";
            Console.WriteLine(msg);
            System.Diagnostics.Debug.WriteLine(msg);
            if (reader != null)
                reader.Close();
            /*if (conn.State == ConnectionState.Open)
                conn.Close();*/
        }





        // #### FUNCTIONS #####################################################################################################################################################


        private bool DB_Execution(String str, SqlConnection conn, String action = "DataBase Action ")
        {
            bool errorstate = false;
            string msg = "";

            try
            {
                SQL_Command = new SqlCommand(str, conn);
                SQL_Command.ExecuteNonQuery();

                msg = action + " Successfully";

            }
            catch (System.Exception ex)
            {
                msg = action + "Failed: " + ex.Message;
                errorstate = true; ;
            }
            finally
            {
                Console.WriteLine(msg);
                System.Diagnostics.Debug.WriteLine(msg);
                //MessageBox.Show(msg, "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            return errorstate;
        }




        /// <summary>
        /// DB Manager to Create and Delete Databases
        /// </summary>
        /// <param name="Dbname"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool SQL_Execution(String Dbname, String action)
        {
            bool errorstate = false;
            String str = "";
            string msg = "";

            switch (action)
            {
                case "create":
                    str = query_make_database.Replace("{DBNAME}", Dbname);
                    break;
                case "delete":
                    str = query_drop_database.Replace("{DBNAME}", Dbname);
                    break;
                default:
                    msg = "Unrecognized Request Received!";
                    Console.WriteLine(msg);
                    System.Diagnostics.Debug.WriteLine(msg);
                    return true;
            }

            using (SqlConnection conn = new SqlConnection(Project_SQLConnect))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("SQL Connection Successful");
                    SQL_Command = new SqlCommand(str, conn);
                    SQL_Command.ExecuteNonQuery();
                    msg = "DataBase " + action.ToUpper() + "D Successfully";
                }
                catch (System.Exception ex)
                {
                    msg = ("DataBase " + action.ToUpper() + " Failed. " + ex.ToString());
                    errorstate = true; ;
                }
                finally
                {
                    Console.WriteLine(msg);
                    System.Diagnostics.Debug.WriteLine(msg);
                    //MessageBox.Show(msg, "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }

            return errorstate;
        }

        private bool CreateTable(string tablename = null, string datacolumns = null, string headercolumns = null)
        {
            bool errorstate = false;

            headercolumns = (headercolumns == null) ? "(myId INTEGER CONSTRAINT PKeyMyId PRIMARY KEY," : headercolumns;
            tablename = (tablename == null) ? "myTable" : tablename;
            datacolumns = (datacolumns == null) ? "myName CHAR(50), myAddress CHAR(255), myBalance FLOAT)" : datacolumns;

            using (SqlConnection conn = new SqlConnection(Project_DBConnect))
            {
                conn.Open();
                string str = "CREATE TABLE " + tablename + headercolumns + datacolumns;

                DB_Execution(str, conn, "Added Table: " + tablename);
                conn.Close();
            }

            return errorstate;

        }

        private bool ImportRecords()
        {
            bool errorstate = false;

            String[] sqlcom = new string[4];
            sqlcom[0] = "INSERT INTO myTable(myId, myName, myAddress, myBalance) VALUES (1001, 'Puneet Nehra', 'A 449 Sect 19, DELHI', 23.98 ) ";
            sqlcom[1] = "INSERT INTO myTable(myId, myName, myAddress, myBalance) VALUES (1002, 'Anoop Singh', 'Lodi Road, DELHI', 353.64) ";
            sqlcom[2] = "INSERT INTO myTable(myId, myName, myAddress, myBalance) VALUES(1003, 'Rakesh M', 'Nag Chowk, Jabalpur M.P.', 43.43) ";
            sqlcom[3] = "INSERT INTO myTable(myId, myName, myAddress, myBalance) VALUES (1004, 'Madan Kesh', '4th Street, Lane 3, DELHI', 23.00) ";

            using (SqlConnection conn = new SqlConnection(Project_DBConnect))
            {
                conn.Open();
                int i = 1;
                float progress = 0;
                foreach (string str in sqlcom)
                {
                    DB_Execution(str, conn, "Added New Record: " + progress.ToString("0.##") + "%");
                    i++;
                    progress = 100 * i / (float)sqlcom.Length;
                }

                conn.Close();
            }

            return errorstate;

        }

        private bool CreateDBView(string viewName = null, string datacolumns = null, string tablename = null)
        {
            bool errorstate = false;

            viewName = (viewName == null) ? "myView" : viewName;
            tablename = (tablename == null) ? "myTable" : tablename;
            datacolumns = (datacolumns == null) ? "myName, myBalance" : datacolumns;

            using (SqlConnection conn = new SqlConnection(Project_DBConnect))
            {
                conn.Open();
                string str = "CREATE VIEW " + viewName + " AS SELECT " + datacolumns + " FROM " + tablename + " GO";

                DB_Execution(str, conn, "Added View: " + viewName + " into Table: " + tablename);
                conn.Close();
            }

            return errorstate;

        }

        private bool CreateProcedure(string procedureName = null, string datacolumns = null, string tablename = null)
        {
            bool errorstate = false;

            procedureName = (procedureName == null) ? "myProc" : procedureName;
            tablename = (tablename == null) ? "myTable" : tablename;
            datacolumns = (datacolumns == null) ? "myName" : datacolumns;

            using (SqlConnection conn = new SqlConnection(Project_DBConnect))
            {
                conn.Open();
                string str = "CREATE PROCEDURE " + procedureName + " AS SELECT " + datacolumns + " FROM " + tablename + " GO";

                DB_Execution(str, conn, "Added Procedure: " + procedureName + " into Table: " + tablename);
                conn.Close();
            }

            return errorstate;

        }

        private bool AlterTable(string tablename = null, string datacolumns = null)
        {
            bool errorstate = false;

            tablename = (tablename == null) ? "myTable" : tablename;
            datacolumns = (datacolumns == null) ? "myName CHAR(100) NOT NULL" : datacolumns;

            using (SqlConnection conn = new SqlConnection(Project_DBConnect))
            {
                conn.Open();
                string str = "ALTER TABLE " + tablename + " ALTER COLUMN " + datacolumns;

                DB_Execution(str, conn, "Altered Column(s): " + datacolumns + " | in Table: " + tablename);
                conn.Close();
            }

            return errorstate;

        }

        private bool DropTable(string tablename = null)
        {
            bool errorstate = false;

            tablename = (tablename == null) ? "myTable" : tablename;

            using (SqlConnection conn = new SqlConnection(Project_DBConnect))
            {
                conn.Open();
                string str = "DROP TABLE " + tablename;

                DB_Execution(str, conn, "Dropped Table: " + tablename);
                conn.Close();
            }

            return errorstate;

        }

        private bool ViewTable(string query)
        {
            bool errorstate = false;

            using (SqlConnection conn = new SqlConnection(Project_DBConnect))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);


                // Create DataSet, fill it and view in data grid  
                //DataSet ds = new DataSet("myTable");
                //da.Fill(ds, "myTable");
                // Create DataSet, fill it and view in data grid  
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];//.DefaultViewManager;
                conn.Close();
            }

            return errorstate;

        }


        /// <summary>
        /// Open's a connection to the SQL Server - note the default connection string connects me to the master DB - enabling creation and dropping of other Databases
        /// </summary>
        /// <returns></returns>
        private SqlConnection OpenSQLConnection()
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(Project_SQLConnect);
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();
                MessageBox.Show("SQL Connection Successful");
                Console.WriteLine("SQL Connection Successful");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return conn;
        }



        // #### UI CONTROLS #####################################################################################################################################################


        /// <summary>
        /// UI Button Controllers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button_createdb_Click(object sender, EventArgs e)
        {
            bool errorstate = SQL_Execution(Project_Database, "create");
        }

        private void button_dropdb_Click(object sender, EventArgs e)
        {
            bool errorstate = SQL_Execution(Project_Database, "delete");
        }

        private void button_table_make_Click(object sender, EventArgs e)
        {
            CreateTable();
        }

        private void button_import_data2DB_Click(object sender, EventArgs e)
        {
            ImportRecords();
        }

        private void button_create_procedure_Click(object sender, EventArgs e)
        {
            CreateProcedure();
        }

        private void button_create_dbview_Click(object sender, EventArgs e)
        {
            CreateDBView();
        }

        private void button_alter_table_Click(object sender, EventArgs e)
        {
            AlterTable();
        }

        private void button_drop_table_Click(object sender, EventArgs e)
        {
            DropTable();
        }

        private void button_view_dt_Click(object sender, EventArgs e)
        {
            ViewTable("SELECT * FROM myTable");
        }

        private void button_view_dt_procedure_Click(object sender, EventArgs e)
        {
            ViewTable("myProc");
        }

        private void view_dt_view_Click(object sender, EventArgs e)
        {
            ViewTable("SELECT * FROM myView");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
