﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Homework5
{
    [Serializable]
    public class OrderService
    {

        public List<Order> orderList = new List<Order>();


        public bool AddOrder(Order order)
        {

            if (this.TrackOrder(order.OrderID) == null)
            {
                this.orderList.Add(order);
                return true;
            }
            else
            {
                return false;
            }

        }

        public void DeleteOrder(int id)
        {

            this.orderList.Remove(TrackOrder(id));
            //删除失败抛出异常？？？ 怎么处理
        }

        public void DeleteOrder(int cid,int a) 
            //通过客户ID删除 a为冗余参数  一个客户可能有多个订单
        {
            foreach(Order or in TrackOrder(cid, 1))
            {
                this.orderList.Remove(or);
            }

            
        }



        public void ModifyOrder()
        {


        }


        public Order TrackOrder(int id) 
            //重载查询方法，用来添加，修改，删除的时候查看是否含有要操作的订单
        {
            //通过订单id来查询
            var query = orderList.Where(order => order.OrderID == id);
            return query.FirstOrDefault();

        }

        public List<Order> TrackOrder(int cid, int a) 
            //通过客户ID查询 a为冗余参数  一个客户可能有多个订单
        {
            //通过客户id来查询  按照订单总金额排序
            var query = orderList.Where(order => order.CustomerID == cid).
                OrderBy(order => order.OrderAmount);

            return query.ToList();

        }

        public List<Order> Sort()
        {
            //按照订单ID排序
            return orderList.Where(order => order.CustomerID > 0).
                OrderBy(order => order.OrderID).ToList();
        }

        public List<Order> Sort(int way)
        {
            if(way==1)
            {
                //按照订单ID排序
                return orderList.Where(order => order.CustomerID > 0).
                    OrderBy(order => order.OrderID).ToList();
            }
            else
                return orderList.Where(order => order.CustomerID > 0).
                    OrderBy(order => order.OrderAmount).ToList();
        }



        public void CalAmount()//计算订单总金额
        {
            foreach (Order order in orderList)
            {
                foreach (OrderItem ot in order.orderItems)
                {
                    order.OrderAmount += ot.ProductPrice * ot.ProductNum;
                }
            }
        }

        public String printOrder(List<Order> list)
        {
            String Orderstr = null;
            foreach(Order ord in list)
            {
                Orderstr += ord + "\n";
            }
            return Orderstr;
        }

        public String Export(String fileName) //fileName传递文件名
        {
            //将所有订单序列化为XML文件
            
            XmlSerializer xmlserializer = new XmlSerializer(typeof(Order[]));
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                xmlserializer.Serialize(fs, orderList.ToArray());
            }
            return File.ReadAllText(fileName);
        }

        public List<Order> Import(String fileName)
        {
            XmlSerializer xmlserializer = new XmlSerializer(typeof(Order[]));
            //从XML文件中载入订单 
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                Order[] orderlist2 = (Order[])xmlserializer.Deserialize(fs);
                foreach(Order o in orderlist2) //导入订单
                {
                    this.AddOrder(o);
                }
                return orderlist2.ToList();
            }
            

        }
    }
}
