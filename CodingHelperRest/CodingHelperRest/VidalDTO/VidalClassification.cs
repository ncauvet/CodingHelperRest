using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtomTester
{
   public class VidalClassification
    {
        private int id;
        private String name;
        private Uri parentLink;

        public VidalClassification(int id, String name, Uri parentLink)
        {
            this.id = id;
            this.name = name;
            this.parentLink = parentLink;
        }
        public override string ToString()
        {

            return this.id + " : " + this.name;
        }
    }
}
