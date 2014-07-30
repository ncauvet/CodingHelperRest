using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using CodingHelperRest;
using System.Collections.ObjectModel;
using CodingHelperRest.VidalDTO;
using AtomTester;

public static class RestUtils
{

    public static Uri serverBaseUri;
    public static String vidalNameSpace = "http://api.vidal.net/-/spec/vidal-api/1.0/";

    public static void setServerParameters(String serverBaseUri)
    {
        RestUtils.serverBaseUri = new Uri(serverBaseUri);
    }

    public static Uri getAbsoluteUri(Uri uri)
    {
        return ((uri == null) || (uri.IsAbsoluteUri)) ? uri : new Uri(serverBaseUri, uri);
    }

    public static Uri getRelativeUri(Uri uri)
    {
        return ((uri == null) || !(uri.IsAbsoluteUri)) ? uri : new Uri(uri.PathAndQuery, UriKind.Relative);
    }

    

    public static SyndicationFeed AtomResultRequest(Uri uri)
    {
        try
        {
            return SyndicationFeed.Load(XmlReader.Create(getAbsoluteUri(uri).ToString()));
        }
        catch (WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                MessageBox.Show("A error have been raised.\r\n\r\n" + "Exception message:\r\n    " + e.Message + "\r\n\r\nException stack trace:\r\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Status == WebExceptionStatus.NameResolutionFailure)
            {
                MessageBox.Show("A error have been raised.\r\nPlease check you have put a valid server URL' .\r\n\r\n" + "Exception message:\r\n    " + e.Message + "\r\n\r\nException stack trace:\r\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("A error have been raised.\r\n\r\n" + "Exception message:\r\n    " + e.Message + "\r\n\r\nException stack trace:\r\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        return new SyndicationFeed();
    }

   
    public static SyndicationFeed getProductsFeedsByName(String name,int startedPage,int maxPerPage)
    {
        if (String.IsNullOrEmpty(name))
            return null;
        return AtomResultRequest(new Uri(serverBaseUri, "/rest/rest/api/products?q=" + name + "&start-page=" + startedPage + "&page-size=" + maxPerPage));
    }
    public static SyndicationFeed getPackageFeedsByName(String name, int startedPage, int maxPerPage,String type)
    {
        if (String.IsNullOrEmpty(name))
            return null;
        return AtomResultRequest(new Uri(serverBaseUri, "/rest/rest/api/packages?q=" + name +"&type="+type +"&start-page=" + startedPage + "&page-size=" + maxPerPage));
    }

   


    public static Uri getDocumentRelativeUriBySyndicationFeed(SyndicationFeed documentSyndicationFeed)
    {
        Uri documentUri = null;
        if (documentSyndicationFeed == null)
            return documentUri;
        //treatement of url
        if (documentSyndicationFeed.Items != null)
            //get only the first item of the collection    
            foreach (var documentSyndicationItem in documentSyndicationFeed.Items)
            {
                documentUri = getRelativeUri(documentSyndicationItem.Links[0].Uri);
                break;
            }
        return documentUri;
    }

    public static List<Product> getProductsBySyndicationFeed(SyndicationFeed productsFeed)
    {
        List<Product> products = new List<Product>();
        foreach (SyndicationItem productFeed in productsFeed.Items)
        {
        Product product = getProductByFeedItem(productFeed);
        products.Add(product);
        }    
        return products;
    }

    private static Product getProductByFeedItem(SyndicationItem productFeed)
    {
        String productName = productFeed.Title.Text;
        Uri productRelativeUri = RestUtils.getAbsoluteUri(productFeed.Links[0].Uri);
        int productId = productFeed.ElementExtensions.ReadElementExtensions<int>("id", vidalNameSpace).FirstOrDefault();
        String beCareful = productFeed.ElementExtensions.ReadElementExtensions<String>("beCareful", vidalNameSpace).FirstOrDefault();
        String genericType = productFeed.ElementExtensions.ReadElementExtensions<String>("genericType", vidalNameSpace).FirstOrDefault();
        String companyName = productFeed.ElementExtensions.ReadElementExtensions<String>("company", vidalNameSpace).FirstOrDefault();
        String dispensationPlace = productFeed.ElementExtensions.ReadElementExtensions<String>("dispensationPlace", vidalNameSpace).FirstOrDefault();
        String marketStatus = productFeed.ElementExtensions.ReadElementExtensions<String>("marketStatus", vidalNameSpace).FirstOrDefault();
        String refundRate = productFeed.ElementExtensions.ReadElementExtensions<String>("refundRate", vidalNameSpace).FirstOrDefault();
        String liste = productFeed.ElementExtensions.ReadElementExtensions<String>("list", vidalNameSpace).FirstOrDefault();

        Product product = new Product(productRelativeUri, productId, productName, beCareful, genericType, companyName, dispensationPlace, marketStatus, refundRate, liste);
        return product;
    }


    public static List<Package> getPackagesBySyndicationFeed(SyndicationFeed packagesFeed)
    {
         List<Package> packages = new List<Package>();
         foreach (SyndicationItem packageFeed in packagesFeed.Items)
         {
             String packageName = packageFeed.Title.Text;
             Uri packageRelativeUri = RestUtils.getAbsoluteUri(packageFeed.Links[0].Uri);
             int packageId = packageFeed.ElementExtensions.ReadElementExtensions<int>("id", vidalNameSpace).FirstOrDefault();
             String companyName = packageFeed.ElementExtensions.ReadElementExtensions<String>("company", vidalNameSpace).FirstOrDefault();
             String marketStatus = packageFeed.ElementExtensions.ReadElementExtensions<String>("marketStatus", vidalNameSpace).FirstOrDefault();
             String lppr = packageFeed.ElementExtensions.ReadElementExtensions<String>("lppr", vidalNameSpace).FirstOrDefault();
             Package pack = new Package(packageRelativeUri, packageId, packageName, companyName, marketStatus, lppr);
             packages.Add(pack);
         }    
        return packages;
    }


    internal static SyndicationFeed getFeedByUri(Uri uri)
    {
        if (uri == null)
            return null;
        return AtomResultRequest(uri);
    }

   

    private static SyndicationFeed getProductDetailAggregateFeeds(Uri uri)
    {
        if (uri == null)
            return null;
        return AtomResultRequest(new Uri(uri,"?aggregate=PACKAGES&aggregate=MOLECULES&aggregate=RECOS&aggregate=GENERIC_INFOS&aggregate=VIDAL_CLASSIFICATION"));
    }



    internal static Collection<VidalLink> getVidalRelatedLinks(System.Collections.ObjectModel.Collection<SyndicationLink> collection)
    {
        Collection<VidalLink> links = new Collection<VidalLink>();
        foreach (SyndicationLink link in collection)
        {
            if (link.RelationshipType == "related")
            {
                links.Add(new VidalLink(link.RelationshipType, link.Uri, link.Title));
            }
        }
        return links;
    }
    internal static Collection<Reco> getRecosInlineFeed(System.Collections.ObjectModel.Collection<SyndicationLink> collection,SyndicationFeed feed)
    {
        Collection<Reco> recos = new Collection<Reco>();
        foreach (SyndicationLink link in collection)
        {
            if (link.RelationshipType == "inline")
            {
                IEnumerable<SyndicationItem> entry = from p in feed.Items where p.Id == link.Uri.OriginalString select p;
                if (entry.ToArray()[0].Categories[0].Name == "RECO")
                {
                    recos.Add(new Reco(entry.ToArray()[0].ElementExtensions.ReadElementExtensions<int>("id", vidalNameSpace).FirstOrDefault(), entry.ToArray()[0].Title.Text, null));
                }
               // links.Add(new VidalLink(link.RelationshipType, link.Uri, link.Title));
            }
        }
        return recos;
    }
    internal static Collection<VidalClassification> getCim10InlineFeed(System.Collections.ObjectModel.Collection<SyndicationLink> collection, SyndicationFeed feed)
    {
        Collection<VidalClassification> cims = new Collection<VidalClassification>();
        foreach (SyndicationLink link in collection)
        {
            if (link.RelationshipType == "inline")
            {
                IEnumerable<SyndicationItem> entry = from p in feed.Items where p.Id == link.Uri.OriginalString select p;
                if (entry.ToArray()[0].Categories[0].Name == "CIM10")
                {
                    cims.Add(new VidalClassification(entry.ToArray()[0].ElementExtensions.ReadElementExtensions<int>("id", vidalNameSpace).FirstOrDefault(), entry.ToArray()[0].Title.Text, null));
                }
                // links.Add(new VidalLink(link.RelationshipType, link.Uri, link.Title));
            }
        }
        return cims;
    }
}