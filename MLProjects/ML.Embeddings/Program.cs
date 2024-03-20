using MiniLM;

var assetsRelativePath = @"../../../../../data";
string assetsPath = GetAbsolutePath(assetsRelativePath);

var sentenceEncoder = new SentenceEncoder();
//var embeddings = new Dictionary<string, float[][]>();
var embeddings = new Dictionary<string, EncodedChunk[]>();

foreach (var f in Directory.GetFiles(assetsPath).Where(f => Path.GetExtension(f) == ".md")
    .Select(f => new { Path = Path.GetFullPath(f), Name = Path.GetFileName(f) }))
{
    Console.WriteLine($"Processing {f.Path}");
    try
    {
        //var lines = File.ReadAllLines(f.Path);
        //var encodedLines = sentenceEncoder.Encode(lines); // there is a good chance we will miss things because of model limitation
        //embeddings.Add(f.Name, encodedLines);
        var longSentence = File.ReadAllText(f.Path);
        var chunks = sentenceEncoder.ChunkAndEncode(longSentence);
        embeddings.Add(f.Name, chunks);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\tFailed with {ex.Message}");
    }
}

while(true)
{
    Console.Write("What's your question? [q] to exit\t");
    var input = Console.ReadLine();
    if (input == "q") break;

    var questions = new[] { input };
    var questionEmbeddings = sentenceEncoder.Encode(new[] { input });

    foreach (var (question, questionVector) in questions.Zip(questionEmbeddings))
    {

        var answers = embeddings.SelectMany(e => e.Value.Select(c => (k: e.Key, chunk: c)));
        var weightedAnswers = answers.Select(c => (file: c.k, chunk: c.chunk,
            score: 1f - HNSW.Net.CosineDistance.NonOptimized(c.chunk.Vector, questionVector)));
        var bestAnswer = weightedAnswers.OrderByDescending(d => d.score).First();

        Console.WriteLine($"Answer: [{bestAnswer.score}:{bestAnswer.file}] {bestAnswer.chunk.Text}\n\n------------------------\n");
    }
}

string GetAbsolutePath(string relativePath)
{
    FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
    string assemblyFolderPath = _dataRoot.Directory.FullName;

    string fullPath = Path.Combine(assemblyFolderPath, relativePath);

    return fullPath;
}