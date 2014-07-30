using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingHelperRest.VidalDTO
{
    public class VidalLink
    {
        public String type;
        public Uri url;
        public String title;

        public VidalLink(String type, Uri url, String title)
        {
            this.title = title;
            this.type = type;
            this.url = url;
        }
        public override string ToString()
        {

            return this.title + " : " + this.type;
        }
    }
}
