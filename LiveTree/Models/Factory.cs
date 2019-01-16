using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveTree.Models
{
    public class Factory
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public int NumberOfNodes { get; set; }
    }

}