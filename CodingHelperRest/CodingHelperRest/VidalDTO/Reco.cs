using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtomTester
{
   public class Reco
    {
        private int id;
        private String name;
        private Uri recolink;

        public Reco(int id, String name, Uri recolink)
        {
            this.id = id;
            this.name = name;
            this.recolink = recolink;
         }
        public override string ToString()
        {

            return this.id + " : " + this.name;
        }
    }
}
