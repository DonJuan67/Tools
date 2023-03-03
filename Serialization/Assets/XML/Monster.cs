using System.Xml;
using System.Xml.Serialization;

public class Monster
{
    [XmlAttribute("name")]
    public string Name;
    [XmlAttribute("hp")]
    public int Health;
}