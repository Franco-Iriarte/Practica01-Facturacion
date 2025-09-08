using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act._Practica_01_Iriarte_Franco.Data
{
    public class ParametroSP
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public ParametroSP() { }

        public ParametroSP(string name, object value) 
        {
            Name = name;
            Value = value;
        }
    }
}
