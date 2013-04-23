namespace ProjectAI.RouteFinding
{
    public class SearchResult
    {
	public NodeRoutefinding TraceNode { get; set;}
	public int Iterations { get; set;}

	public SearchResult(NodeRoutefinding traceNode, int iterations)
	{
	    this.TraceNode = traceNode;
	    this.Iterations = iterations;
	}
    }
}