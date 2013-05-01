namespace ProjectAI.RouteFinding
{
    public class SearchResult
    {
        public NodeAbstract TraceNode { get; set; }
	    public int Iterations { get; set;}

	    public SearchResult(NodeAbstract traceNode, int iterations)
	    {
	        this.TraceNode = traceNode;
	        this.Iterations = iterations;
	    }
    }
}