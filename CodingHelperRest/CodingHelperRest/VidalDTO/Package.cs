using System;

public class Package
{
    public readonly int id;
    public readonly String name;
    public readonly String companyName;
    public readonly String marketStatus;
    public readonly String lppr;

    public readonly Uri packageRelativeUri;
    

    public Package(Uri packageRelativeUri, int id, String name, String companyName,String marketStatus, String lppr)
    {
        this.packageRelativeUri = packageRelativeUri;
        this.id = id;
        this.name = name;
        this.companyName = companyName;
        this.lppr = lppr;

    }

    

    public int Id
    {
        get { return id; }
    }
    public  String Name
    {
        get { return name; }
    }

    public  String CompanyName
{
        get { return companyName; }
    }
    public  String MarketStatus
{
        get { return marketStatus; }
    }
    public  Uri PackageRelativeUri
{
    get { return packageRelativeUri; }
    }
    public String Lppr
    {
        get { return lppr; }
    }

    public override string ToString()
    {

        return this.id +" : " +this.name;
    }

}