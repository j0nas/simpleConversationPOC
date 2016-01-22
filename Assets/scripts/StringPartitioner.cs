namespace Assets.scripts
{
    public class StringPartitioner
    {
        private readonly float _maxChars;

        public StringPartitioner(float maxChars)
        {
            _maxChars = maxChars;
        }

        public string PartitionWithLineBreaks(string sentence)
        {
            var result  = "";
            var currentCharacterCount = 0;
            var words = sentence.Split(' ');

            foreach (var s1 in words)
            {
                currentCharacterCount += s1.Length;
                result += (currentCharacterCount > _maxChars ? '\n' : ' ') + s1;
                if (currentCharacterCount > _maxChars)
                    currentCharacterCount = 0;
            }

            return result;
        }
    }
}