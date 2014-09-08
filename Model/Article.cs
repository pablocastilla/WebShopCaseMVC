﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
       
    public class Article
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double TotalWithoutVAT { get; set; }

        public double TotalWithVAT { get; set; }
    }
}
