using Assets.scripts;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterUIText : MonoBehaviour
{
    public Text DialogueText;
    public TextAsset ConversationXml;

    private const int MaxCharsPerLine = 30;

    private TypewriterEffectTextAppender _textAppender;
    private DialogueLine[] _lines;
    private int _currentLineIndex;
    private StringPartitioner _partitioner;

    void Start()
    {
        var dialogueCollection = DialogueLoader.LoadFromText(ConversationXml.text);
        _lines = dialogueCollection.DialogueLines;
        if (string.IsNullOrEmpty(_lines[0].Text))
        {
            Debug.Log("Failed to load dialogues!");
            return;
        }

        _partitioner = new StringPartitioner(MaxCharsPerLine);

        Click();
    }

    public void Click()
    {
        if (_currentLineIndex >= _lines.Length) return;
        var dialogueLine = _lines[_currentLineIndex].Name + ": " + _lines[_currentLineIndex].Text;
        dialogueLine = _partitioner.PartitionWithLineBreaks(dialogueLine);

        _textAppender = new TypewriterEffectTextAppender(dialogueLine, Time.time);
        _currentLineIndex++;
    }

    void Update()
    {
        if (_textAppender.UpdateTextIfAble(Time.time))
            DialogueText.text = _textAppender.Text;
    }
}
