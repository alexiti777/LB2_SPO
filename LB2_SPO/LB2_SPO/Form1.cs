using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using System.Linq.Expressions;
using System.Management;

namespace LB2_SPO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            load();
            //GetSystemInfo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        [DllImport("kernel32.dll")]
        
        public static extern IntPtr GlobalAlloc(int con, int size);
        [DllImport("kernel32.dll")]
        
        public static extern int GlobalFree(IntPtr start);
        [DllImport("kernel32.dll")]
        public static extern void GlobalMemoryStatus(ref MEMORYSTATUS lpBuffer);
        //
        public struct MEMORYSTATUS
        {
            public UInt32 dwLength;               
            public UInt32 dwMemoryLoad;           
            public UInt32 dwTotalPhys;            
            public UInt32 dwAvailPhys;            
            public UInt32 dwTotalPageFile;        
            public UInt32 dwAvailPageFile;        
            public UInt32 dwTotalVirtual;         
            public UInt32 dwAvailVirtual;         
            public UInt32 dwAvailExtendedVirtual;
        }
        public void load()
        {
            
            MEMORYSTATUS memStatus = new MEMORYSTATUS();
         
            GlobalMemoryStatus(ref memStatus);


            dataGridView1.Rows.Add("Размер структуры", memStatus.dwLength.ToString());
            dataGridView1.Rows.Add("Процент занятой памяти", memStatus.dwMemoryLoad.ToString() + " %");
            dataGridView1.Rows.Add("Общее кол-во физической памяти", memStatus.dwTotalPhys.ToString() + " byte");
            dataGridView1.Rows.Add("Свободное кол-во физической памяти", memStatus.dwAvailPhys.ToString() + " byte");
            dataGridView1.Rows.Add("Предел памяти для системи", memStatus.dwTotalPageFile.ToString() + " byte");
            dataGridView1.Rows.Add("Максимальный объем памяти", memStatus.dwAvailPageFile.ToString() + " byte");
            dataGridView1.Rows.Add("Общее количество виртуальной памяти", memStatus.dwTotalVirtual.ToString() + " byte");
            dataGridView1.Rows.Add("Свободное количество виртуальной памяти", memStatus.dwAvailVirtual.ToString() + " byte");

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            load();
        }

        void GetSystemInfo()
        {
            ObjectQuery winQuery = new ObjectQuery("SELECT * FROM Win32_LogicalMemoryConfiguration");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(winQuery);
            foreach (ManagementObject item in searcher.Get())
            {
                label2.Text = item["TotalPhysicalMemory"].ToString();
                label4.Text = item["TotalVirtualMemory"].ToString();
                label6.Text = item["TotalPageFileSpace"].ToString();
                label8.Text = item["AvailableVirtualMemory"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
