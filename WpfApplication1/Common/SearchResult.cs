namespace Heureka.Common
{
    public class SearchResult
    {
        public bool Succeeded { get; set; }
        public AbstractNode TraceNode { get; set; }
        public int Iterations { get; set; }

        public SearchResult(AbstractNode traceNode, int iterations, bool succeeded)
        {
            this.Succeeded = succeeded;
            this.TraceNode = traceNode;
            this.Iterations = iterations;
        }
    }
}