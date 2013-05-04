namespace Heureka.Testing
{
    public class Test
    {
        public string Name { get; set; }
        public string File { get; set; }
        public string Target { get; set; }
        public bool Exepected { get; set; }

        public Test(string name, string file, string target, bool expected)
        {
            this.Name = name;
            this.File = file;
            this.Target = target;
            this.Exepected = expected;
        }
    }
}