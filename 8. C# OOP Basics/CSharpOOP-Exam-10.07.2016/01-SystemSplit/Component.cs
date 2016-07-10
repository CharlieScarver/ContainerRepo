namespace _01_SystemSplit
{
    public abstract class Component
    {
        private readonly string name;

        protected Component(string name)
        {
            this.name = name;
        }

        public string Name => this.name;
    }
}