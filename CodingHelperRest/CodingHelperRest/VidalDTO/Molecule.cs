using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtomTester
{
    class Molecule
    {
        public readonly int id;
        public readonly String name;
        
        public Molecule(int id, String name){
            this.id=id;
            this.name = name;
        }
    }
}
