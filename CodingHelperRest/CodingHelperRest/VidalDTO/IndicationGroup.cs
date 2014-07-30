using System;
using System.ServiceModel.Syndication;
using System.Collections.ObjectModel;
using CodingHelperRest.VidalDTO;
using AtomTester;

public class IndicationGroup
{
    public  int id;
    public  String name;
    public  Collection<VidalLink> links;
    public Collection<Reco> recos;
    public Collection<VidalClassification> cims;

    public IndicationGroup(int id, String name, Collection<VidalLink> links)
    {
        this.id=id;
        this.name=name;
        this.links = links;

    }

   

    public int Id
    {
        get { return id; }
    }
    public  String Name
    {
        get { return name; }
    }
    
   
    public override string ToString()
    {

        return this.id +" : " +this.name;
    }

}