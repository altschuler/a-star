namespace ProjectAI.RouteFinding
{
    public class SearchResult
    {
        public bool Succeeded { get; set; }
        public NodeAbstract TraceNode { get; set; }
	    public int Iterations { get; set;}

	    public SearchResult(NodeAbstract traceNode, int iterations, bool succeeded)
	    {
	        this.Succeeded = succeeded;
	        this.TraceNode = traceNode;
	        this.Iterations = iterations;
	    }
    }
}