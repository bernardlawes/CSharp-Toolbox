using Microsoft.VisualBasic;

namespace learn.databases
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            AppExit();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            string msg = "App Disposing";
            Console.WriteLine(msg);
            System.Diagnostics.Debug.WriteLine(msg);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_createdb = new Button();
            button_dropdb = new Button();
            button_table_make = new Button();
            button_import_records = new Button();
            button_create_procedure = new Button();
            button_create_dbview = new Button();
            button_alter_table = new Button();
            button_drop_table = new Button();
            button_view_dt = new Button();
            button_view_dt_procedure = new Button();
            view_dt_view = new Button();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button_createdb
            // 
            button_createdb.Location = new Point(27, 95);
            button_createdb.Name = "button_createdb";
            button_createdb.Size = new Size(149, 37);
            button_createdb.TabIndex = 0;
            button_createdb.Text = "Create DB";
            button_createdb.UseVisualStyleBackColor = true;
            button_createdb.Click += button_createdb_Click;
            // 
            // button_dropdb
            // 
            button_dropdb.Location = new Point(27, 138);
            button_dropdb.Name = "button_dropdb";
            button_dropdb.Size = new Size(149, 37);
            button_dropdb.TabIndex = 1;
            button_dropdb.Text = "Drop DB";
            button_dropdb.UseVisualStyleBackColor = true;
            button_dropdb.Click += button_dropdb_Click;
            // 
            // button_table_make
            // 
            button_table_make.Location = new Point(27, 181);
            button_table_make.Name = "button_table_make";
            button_table_make.Size = new Size(149, 37);
            button_table_make.TabIndex = 2;
            button_table_make.Text = "Create Table";
            button_table_make.UseVisualStyleBackColor = true;
            button_table_make.Click += button_table_make_Click;
            // 
            // button_import_records
            // 
            button_import_records.Location = new Point(27, 224);
            button_import_records.Name = "button_import_records";
            button_import_records.Size = new Size(149, 37);
            button_import_records.TabIndex = 4;
            button_import_records.Text = "Import Records";
            button_import_records.UseVisualStyleBackColor = true;
            button_import_records.Click += button_import_data2DB_Click;
            // 
            // button_create_procedure
            // 
            button_create_procedure.Location = new Point(27, 267);
            button_create_procedure.Name = "button_create_procedure";
            button_create_procedure.Size = new Size(149, 37);
            button_create_procedure.TabIndex = 5;
            button_create_procedure.Text = "Create Procedure";
            button_create_procedure.UseVisualStyleBackColor = true;
            button_create_procedure.Click += button_create_procedure_Click;
            // 
            // button_create_dbview
            // 
            button_create_dbview.Location = new Point(27, 310);
            button_create_dbview.Name = "button_create_dbview";
            button_create_dbview.Size = new Size(149, 37);
            button_create_dbview.TabIndex = 6;
            button_create_dbview.Text = "Create DB View";
            button_create_dbview.UseVisualStyleBackColor = true;
            button_create_dbview.Click += button_create_dbview_Click;
            // 
            // button_alter_table
            // 
            button_alter_table.Location = new Point(27, 353);
            button_alter_table.Name = "button_alter_table";
            button_alter_table.Size = new Size(149, 37);
            button_alter_table.TabIndex = 7;
            button_alter_table.Text = "Alter Table";
            button_alter_table.UseVisualStyleBackColor = true;
            button_alter_table.Click += button_alter_table_Click;
            // 
            // button_drop_table
            // 
            button_drop_table.Location = new Point(27, 396);
            button_drop_table.Name = "button_drop_table";
            button_drop_table.Size = new Size(149, 37);
            button_drop_table.TabIndex = 8;
            button_drop_table.Text = "Drop Table";
            button_drop_table.UseVisualStyleBackColor = true;
            button_drop_table.Click += button_drop_table_Click;
            // 
            // button_view_dt
            // 
            button_view_dt.Location = new Point(202, 43);
            button_view_dt.Name = "button_view_dt";
            button_view_dt.Size = new Size(138, 39);
            button_view_dt.TabIndex = 9;
            button_view_dt.Text = "View by Datatable";
            button_view_dt.UseVisualStyleBackColor = true;
            button_view_dt.Click += button_view_dt_Click;
            // 
            // button_view_dt_procedure
            // 
            button_view_dt_procedure.Location = new Point(346, 43);
            button_view_dt_procedure.Name = "button_view_dt_procedure";
            button_view_dt_procedure.Size = new Size(138, 39);
            button_view_dt_procedure.TabIndex = 10;
            button_view_dt_procedure.Text = "View by Procedure";
            button_view_dt_procedure.UseVisualStyleBackColor = true;
            button_view_dt_procedure.Click += button_view_dt_procedure_Click;
            // 
            // view_dt_view
            // 
            view_dt_view.Location = new Point(490, 43);
            view_dt_view.Name = "view_dt_view";
            view_dt_view.Size = new Size(138, 39);
            view_dt_view.TabIndex = 11;
            view_dt_view.Text = "View by View";
            view_dt_view.UseVisualStyleBackColor = true;
            view_dt_view.Click += view_dt_view_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(202, 95);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(569, 338);
            dataGridView1.TabIndex = 12;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button1
            // 
            button1.Location = new Point(635, 43);
            button1.Name = "button1";
            button1.Size = new Size(138, 39);
            button1.TabIndex = 13;
            button1.Text = "Undefined";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(790, 450);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(view_dt_view);
            Controls.Add(button_view_dt_procedure);
            Controls.Add(button_view_dt);
            Controls.Add(button_drop_table);
            Controls.Add(button_alter_table);
            Controls.Add(button_create_dbview);
            Controls.Add(button_create_procedure);
            Controls.Add(button_import_records);
            Controls.Add(button_table_make);
            Controls.Add(button_dropdb);
            Controls.Add(button_createdb);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button_createdb;
        private Button button_dropdb;
        private Button button_table_make;
        private Button button_import_records;
        private Button button_create_procedure;
        private Button button_create_dbview;
        private Button button_alter_table;
        private Button button_drop_table;
        private Button button_view_dt;
        private Button button_view_dt_procedure;
        private Button view_dt_view;
        private DataGridView dataGridView1;
        private Button button1;
    }
}
