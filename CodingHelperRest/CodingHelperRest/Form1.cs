using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;
using System.Windows.Forms;
using CodingHelperRest.VidalDTO;
using AtomTester;

namespace CodingHelperRest
{
    public partial class Form1 : Form
    {
        static string baseUrl  = "http://apirest-dev.vidal.fr";
        static string credentials = "app_id=dbd540aa&app_key=8343650ea233a4716f524ab77dc24948";
        public static String vidalNameSpace = "http://api.vidal.net/-/spec/vidal-api/1.0/";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string aggregat = "";
            if (checkBox1.Checked)
            {
                aggregat += "&aggregate=CIM10";
            }
            if (checkBox2.Checked)
            {
                aggregat += "&aggregate=RECO";
            }
            SyndicationFeed giFeedSearcher = RestUtils.AtomResultRequest(new Uri(baseUrl + "/rest/api/coding-support/search-by-name?q=" + textBox1.Text + "&" + credentials + "&" + aggregat));
            List<IndicationGroup> groups = new List<IndicationGroup>();
            foreach (SyndicationItem item in giFeedSearcher.Items)
            {   if( item.Categories[0].Name=="INDICATION_GROUP"){
                int giId = item.ElementExtensions.ReadElementExtensions<int>("id", vidalNameSpace).FirstOrDefault();
                IndicationGroup gi = new IndicationGroup(giId, item.Title.Text, RestUtils.getVidalRelatedLinks(item.Links));
                gi.recos = RestUtils.getRecosInlineFeed(item.Links, giFeedSearcher);
                gi.cims = RestUtils.getCim10InlineFeed(item.Links, giFeedSearcher);
                groups.Add(gi);
                
            }
            
            }
            
            listBox1.DataSource =groups;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.DataSource = (((IndicationGroup)listBox1.SelectedItem).links).ToArray();
            listBox3.DataSource = (((IndicationGroup)listBox1.SelectedItem).recos);
            listBox4.DataSource = (((IndicationGroup)listBox1.SelectedItem).cims);
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            webBrowser1.Navigate(baseUrl + ((VidalLink)listBox2.SelectedItem).url.OriginalString+"?"+credentials);
        }
    }
}
