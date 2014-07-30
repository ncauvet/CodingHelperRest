using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtomTester
{
    class GenericGroup
    {
       private int id;
       private String name;
       private String type;
       private Uri genTypeLink;

       public GenericGroup(int id, String name, String type, Uri genTypeLink)
       {
           this.id = id;
           this.name = name;
           this.type = type;
           this.genTypeLink = genTypeLink;
       }

    }
}
