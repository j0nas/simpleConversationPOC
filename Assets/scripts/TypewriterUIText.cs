using Assets.scripts;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TypewriterUIText : MonoBehaviour
{
    public Text DialogueText;
    public TextAsset ConversationXml;

    private TypewriterEffectTextAppender _textAppender;
    private DialogueLine[] _lines;
    private int _currentLineIndex;

    void Start()
    {
        var dialogueCollection = DialogueLoader.LoadFromText(ConversationXml.text);
        _lines = dialogueCollection.DialogueLines;
        if (_lines[0].Text.Length == 0)
        {
            Debug.Log("Failed to load dialogues!");
            return;
        }

        Click();
    }

    public void Click()
    {
        var dialogueLine = _lines[_currentLineIndex].Name + ": " + _lines[_currentLineIndex].Text;
        _textAppender = new TypewriterEffectTextAppender(DialogueText, dialogueLine, Time.time);
        _currentLineIndex++;
    }

    void Update()
    {
        if (!_textAppender.DoneAppending)
        {
            _textAppender.UpdateText();
        }
    }
}
