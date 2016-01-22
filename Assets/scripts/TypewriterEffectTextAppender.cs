using System;
using Random = System.Random;

namespace Assets.scripts
{
    public class TypewriterEffectTextAppender
    {
        public bool Done { get; private set; }
        public string Text { get; private set; }

        private const float TextSpeedDelaySeconds = 0.05f;
        private const float TextSpeedRandomnessVariance = 0.1f;

        private readonly Random _random = new Random();
        private readonly string _textToAppend;

        private double _currentVariance;
        private int _currentAppendingCharacterPosition;
        private float _lastAppendTimeStamp;

        public TypewriterEffectTextAppender(string textToAppend, float timeStamp)
        {
            if (string.IsNullOrEmpty(textToAppend)) throw new ArgumentException();
            _textToAppend = textToAppend;
            _lastAppendTimeStamp = timeStamp;
        }

        public bool CanUpdateText(float time)
        {
            return !Done && time - _lastAppendTimeStamp > (TextSpeedDelaySeconds + _currentVariance);
        }

        public bool UpdateTextIfAble(float time)
        {
            if (!CanUpdateText(time)) return false;
            _lastAppendTimeStamp = time;
            _currentVariance = _random.NextDouble()*TextSpeedRandomnessVariance;
            Text += _textToAppend[_currentAppendingCharacterPosition++];
            Done = _currentAppendingCharacterPosition == _textToAppend.Length;
            return true;
        }
    }
}