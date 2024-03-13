using Microsoft.ML.Data;

namespace ML.Classifier
{
    public class ModelInput
    {
        public ModelInput(string utterance)
        {
            Sentence = utterance;
        }

        [LoadColumn(0)]
        [ColumnName(@"Sentence")]
        public string Sentence { get; set; }

        [LoadColumn(1)]
        [ColumnName(@"Label")]
        public float Label { get; set; }
    }

    public class ModelOutput
    {
        [ColumnName(@"Sentence")]
        public string Sentence { get; set; }

        [ColumnName(@"Label")]
        public uint Label { get; set; }

        [ColumnName(@"PredictedLabel")]
        public float PredictedLabel { get; set; }

        [ColumnName(@"Score")]
        public float[] Score { get; set; }
    }
}
