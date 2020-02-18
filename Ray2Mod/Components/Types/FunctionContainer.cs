namespace Ray2Mod.Components.Types
{
    public class FunctionContainer
    {
        protected FunctionContainer(EntryPoint entryPoint)
        {
            Interface = entryPoint.Interface;
        }

        protected RemoteInterface Interface { get; }
    }
}