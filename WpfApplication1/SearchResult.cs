namespace ProjectAI.RouteFinding
{
    public class SearchResult
    {
	public Node TraceNode { get; set;}
	public int Iterations { get; set;}

	public SearchResult(Node traceNode, int iterations)
	{
	    this.TraceNode = traceNode;
	    this.Iterations = iterations;
	}
    }
}