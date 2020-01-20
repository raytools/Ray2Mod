namespace Ray2Mod.Types
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