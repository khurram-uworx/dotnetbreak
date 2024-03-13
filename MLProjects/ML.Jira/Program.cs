
using ML;

var input = @"Today, I completed 5 hours of work on ticket XY-9876";
Console.WriteLine($"Input: {input}");

var sampleDataTicket = new WorkLogsModelTicket.ModelInput() { Input = input };
var sampleDataDuration = new WorkLogsModelDuration.ModelInput() { Input = input };

var sortedScoresWithLabelTicket = WorkLogsModelTicket.PredictAllLabels(sampleDataTicket);
var sortedScoresWithLabelDuration = WorkLogsModelDuration.PredictAllLabels(sampleDataDuration);

foreach (var score in sortedScoresWithLabelTicket.OrderByDescending(o => o.Value))
{
    if (input.Contains(score.Key))
    {
        Console.WriteLine($"Detected Ticket: {score.Key}");
        break;
    }
}

foreach (var score in sortedScoresWithLabelDuration.OrderByDescending(o => o.Value))
{
    if (input.Contains(score.Key))
    {
        Console.WriteLine($"Detected Duration: {score.Key} hours");
        break;
    }
}
