using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("DialogueCollection")]
public class DialogueLoader
{
    [XmlArray("DialogueLines"), XmlArrayItem("Line")]
    public DialogueLine[] DialogueLines;

    public static DialogueLoader LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof (DialogueLoader));
        return serializer.Deserialize(new StringReader(text)) as DialogueLoader;
    }
}
