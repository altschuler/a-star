namespace ProjectAI.RouteFinding
{
  public class SearchResultInference
  {
    public int Iterations { get; set;}
    public bool Succeeded { get; set;}

    public SearchResultInference(int iterations, bool succeeded)
    {
      this.Iterations = iterations;
      this.Succeeded = succeeded;
    }
  }
}