using System.Xml.Serialization;

public class DialogueLine
{
    [XmlAttribute("name")]
    public string Name;

    [XmlText]
    public string Text;
}
