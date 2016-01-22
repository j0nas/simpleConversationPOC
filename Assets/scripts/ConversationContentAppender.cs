using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Assets.scripts
{
    public class TypewriterEffectTextAppender
    {
        public bool DoneAppending { get; private set; }
        private readonly Text _textComponent;

        private const float TextSpeedDelaySeconds = 0.05f;
        private const float TextSpeedRandomnessVariance = 0.1f;
        private double _currentVariance = 0;
        private readonly Random _random = new Random();

        private readonly string _dialogueLine;
        private int _currentAppendingCharacterPosition;
        private float _lastCharAppendTimeStamp;
        
        public TypewriterEffectTextAppender(Text textComponent, string dialogueLine, float lastCharAppendTimeStamp)
        {
            _textComponent = textComponent;
            _dialogueLine = dialogueLine;
            _lastCharAppendTimeStamp = lastCharAppendTimeStamp;

            // TODO _line length check if (_lines.Length == _currentLineIndex) return;
            // TODO loosen time.time dependency?
            // TODO loosen dependency from Text component?
            _textComponent.text = "";
            DoneAppending = false;
        }

        public void UpdateText()
        {
            var enoughTimePassedSinceLastCharAppend = Time.time - _lastCharAppendTimeStamp >
                                                      (TextSpeedDelaySeconds + _currentVariance);
            if (DoneAppending || !enoughTimePassedSinceLastCharAppend) return;

            _textComponent.text += _dialogueLine[_currentAppendingCharacterPosition++];
            _lastCharAppendTimeStamp = Time.time;
            DoneAppending = _currentAppendingCharacterPosition == _dialogueLine.Length;
            _currentVariance = _random.NextDouble()*TextSpeedRandomnessVariance;
        }
    }
}