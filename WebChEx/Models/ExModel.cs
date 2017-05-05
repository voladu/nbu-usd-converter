using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChEx.Models
{
    public class ExModel
    {
        public string ExDate { get; set; }
        public string ExRealDate { get; set; }        
        public string ExSum { get; set; }

        public ExModel()
        {
            ExDate = DateTime.Now.ToString("yyyyMMdd");
            ExRealDate = DateTime.Now.ToString("dd.MM.yyyy");
            ExSum = "100";
        }
    }
}
