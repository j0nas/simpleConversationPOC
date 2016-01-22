using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TypewriterUIText : MonoBehaviour
{
    public Text DialogueText;
    public TextAsset ConversationXml;

    private const float TextSpeedDelaySeconds = 0.05f;
    private const float TextSpeedRandomnessVariance = 0.1f;

    private static readonly Random Random = new Random();
    private double _currentVariance = Random.NextDouble()*TextSpeedRandomnessVariance;

    private string _currentLine;
    private float _lastCharAppendTime;
    private int _charPos;
    private bool _doneAppending;
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

        LoadCurrentLine();
        _lastCharAppendTime = Time.time;
    }

    public void Click()
    {
        _currentLineIndex++;
        LoadCurrentLine();
    }

    private void LoadCurrentLine()
    {
        if (_lines.Length == _currentLineIndex) return;

        _currentLine = _lines[_currentLineIndex].Name + ": " + _lines[_currentLineIndex].Text;
        DialogueText.text = "";
        _charPos = 0;
        _doneAppending = false;
    }

    void Update()
    {
        var enoughTimePassedSinceLastCharAppend = Time.time - _lastCharAppendTime >
                                                  (TextSpeedDelaySeconds + _currentVariance);
        if (_doneAppending || !enoughTimePassedSinceLastCharAppend) return;

        DialogueText.text += _currentLine[_charPos++];
        _lastCharAppendTime = Time.time;
        _doneAppending = _charPos == _currentLine.Length;
        _currentVariance = Random.NextDouble()*TextSpeedRandomnessVariance;
    }
}
