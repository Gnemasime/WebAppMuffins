using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //Primary key
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppMuffins.Models
{
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int ItemNo { get; set; }
        public virtual MuffinItems MuffinItems { get; set; } //foreign key
        //public datatype variable {get;set;}
        public string Role { get; set; }

        public int NumMuffins { get; set; }

        public double basicCost { get; set; }
        public double Discamount { get; set; }
        public double VatAmount { get; set; }
        public double TotalCost { get; set; }

        //3
        public double Pullprice()
        {
            AppDbContext db = new AppDbContext();
            var pr = (from c in db.muffinItems
                      where c.ItemNo == ItemNo
                      select c.Price).FirstOrDefault();
            return pr;
        }

        public double PuulDiscrate()
        {
            AppDbContext database = new AppDbContext();
            var gh = (from t in database.muffinItems
                      where t.ItemNo == ItemNo
                      select t.DiscRate).Single();
            return gh;
        }

        public double PullVatrate()
        {
            AppDbContext db = new AppDbContext();
            var vat = (from v in db.muffinItems
                       where v.ItemNo == ItemNo
                       select v.VatRate).Single();
            return vat;
        }

        //4
        public double CalcBasicCost()
        {
            return Pullprice() * NumMuffins;
        }

        //5
        public double CalcDiscount()
        {
            if(Role=="Staff" && NumMuffins >= 5)
            {
                return ((PuulDiscrate() * (1 / 4)) + PuulDiscrate()) * CalcBasicCost();
            }
            else if(Role=="Student" && NumMuffins >= 3)
            {
                return ((PuulDiscrate() * 0.5) + PuulDiscrate()) * CalcBasicCost();
            }
            else
            {
                return PuulDiscrate() * CalcBasicCost();
            }
        }

        //6
        public double Calcvat()
        {
            return CalcBasicCost() * PullVatrate();
        }

        //7
        public double Calctotal()
        {
            return (CalcBasicCost() - CalcDiscount()) + Calcvat();
        }
    }
}