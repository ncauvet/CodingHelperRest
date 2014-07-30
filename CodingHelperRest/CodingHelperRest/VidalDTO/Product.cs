using System;

public class Product
{
    public readonly int id;
    public readonly String name;
    public readonly String beCareful;
    public readonly String genericType;
    public readonly String companyName;
    public readonly String dispensationPlace;
    public readonly String marketStatus;
    public readonly String refundRate;
    public readonly String liste;
    public readonly Uri productRelativeUri;
    
    //only in the detail
    public String vigilance;
    public String vmp;

    public Product(Uri productRelativeUri, int id, String name, String beCareful,String genericType, String companyName, String dispensationPlace, String marketStatus, String refundRate,String liste)
    {
        this.productRelativeUri = productRelativeUri;
        this.id = id;
        this.name = name;
        this.beCareful = beCareful;
        this.companyName = companyName;
        this.dispensationPlace = dispensationPlace;
        this.genericType = genericType;
        this.marketStatus = marketStatus;
        this.refundRate = refundRate;
        this.liste = liste;

    }

    public Product(Uri productRelativeUri, int id, String name, String beCareful, String genericType, String companyName, String dispensationPlace, String marketStatus, String refundRate, String liste, String vigilance, String vmp)
    {
        this.productRelativeUri = productRelativeUri;
        this.id = id;
        this.name = name;
        this.beCareful = beCareful;
        this.companyName = companyName;
        this.dispensationPlace = dispensationPlace;
        this.genericType = genericType;
        this.marketStatus = marketStatus;
        this.refundRate = refundRate;
        this.liste = liste;
        this.vigilance = vigilance;
        this.vmp = vmp;

    }
    

    public int Id
    {
        get { return id; }
    }
    public  String Name
    {
        get { return name; }
    }

    public  String BeCareful
    {
        get { return beCareful; }
    }
    public  String GenericType
{
        get { return genericType; }
    }
    public  String CompanyName
{
        get { return companyName; }
    }
    public  String DispensationPlace
{
        get { return dispensationPlace; }
    }
    public  String MarketStatus
{
        get { return marketStatus; }
    }
    public  String RefundRate
{
        get { return refundRate; }
    }
    public  Uri ProductRelativeUri
{
    get { return productRelativeUri; }
    }
    public String Liste
    {
        get { return liste; }
    }

    public override string ToString()
    {

        return this.id +" : " +this.name;
    }

}