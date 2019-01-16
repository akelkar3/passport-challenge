using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveTree.Models
{
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //foreign key
        public int FactoryId { get; set; }
        //navigation objects
        //public Factory Factory { get; set; }
    }   
}