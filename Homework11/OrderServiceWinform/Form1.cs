﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrderServiceWinform;

namespace OrderServiceWinform
{
    public partial class Form1 : Form
    {
        public string createOrderID { get; set; }
        public string createCustomerID { get; set; }
        public string createDate { get; set; }
        public string createProductName { get; set; }
        public string createProductNum { get; set; }
        
        public List<Order> orderList = new List<Order>();
        public string queryid { get; set; }
        public string customerid { get; set; }
        OrderService os = new OrderService();
        public Form1()
        {
            InitializeComponent();
            orderBindingSource.DataSource = OrderService.TrackAllOrders();
            //绑定查询条件
            QueryBox.DataBindings.Add("Text", this, "queryid");
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void CreateOrder_Click(object sender, EventArgs e)
        {
            Form form2 = new Form2();
            form2.ShowDialog();
        }

        private void QuaryButton_Click(object sender, EventArgs e)
        {
            /*if((ItemBox.Text==null ||ItemBox.Text==""|| queryid == null|| queryid == "")) //&& (customerid==null||customerid==""))
            {
                orderBindingSource.DataSource = os.orderList.ToList();
            }
            else
            {
                if(ItemBox.Text=="订单ID")
                {
                    orderBindingSource.DataSource = os.orderList.Where(
                    order => order.OrderID.ToString() == queryid
                    ).ToList();
                }
                else
                {
                   orderBindingSource.DataSource = os.orderList.Where(
                   order => order.CustomerID.ToString() == queryid
                   ).ToList();
                }*/
                 
                //当不存在order满足时，会出现bug!!!!   
                //加上toList()  已解决
                
            

        }
        

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ModifyOrder_Click(object sender, EventArgs e)
        {
            this.orderBindingSource.ResetBindings(false);
            this.ItembindingSource1.ResetBindings(false);
        }

        private void ImportOrder_Click(object sender, EventArgs e)
        {
            /*openFileDialog1.InitialDirectory = "G:\\VS\\repos\\Homework5" +
                "\\Homework5\\bin\\Debug";
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                os.Import(openFileDialog1.FileName);
                this.orderBindingSource.ResetBindings(false);
                using (var db = new OrderContext())
                {
                    foreach(Order order in os.orderList)
                    {
                        db.Orders.Add(order);
                    }
                    db.SaveChanges();

                }
            }*/
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void OutputOrder_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                saveFileDialog1.InitialDirectory = "G:\\VS\\repos\\" +
                    "Homework5\\Homework5\\bin\\Debug";
                OrderService.Export(saveFileDialog1.FileName);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //int oid = (int)dataGridView1.SelectedRows[0].Cells[0].Value; //获取当前选中行的 订单id
            //MessageBox.Show(""+a);
            Order order = orderBindingSource.Current as Order;
            if (order == null)
            {
                MessageBox.Show("请选择一个订单进行删除");
                return;
            }
            OrderService.DeleteOrder(order.OrderID);
            QueryAll();

        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            orderBindingSource.DataSource = new List<Order>();
            this.orderBindingSource.ResetBindings(false);
        }

        private void mysqlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //从数据库导入订单
            using (var db = new OrderContext())
            {
                foreach (Order order in db.Orders)
                {
                    OrderService.AddOrder(order);
                }
                QueryAll();
                this.ItembindingSource1.ResetBindings(false);
               
            }
        }

        private void computerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //从xml文件导入订单
            openFileDialog1.InitialDirectory = "G:\\VS\\repos\\Homework5" +
                "\\Homework5\\bin\\Debug";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OrderService.Import(openFileDialog1.FileName);
                QueryAll();
                
            }
        }
        private void QueryAll()
        {
            orderBindingSource.DataSource = OrderService.TrackAllOrders();
            orderBindingSource.ResetBindings(false);
        }
    }
}