namespace YourNamespace.Controllers
{
    internal class CompletionRequest
    {
        public string Prompt { get; set; }
        public int MaxTokens { get; set; }
        public double Temperature { get; set; }
        public double TopP { get; set; }
    }
}