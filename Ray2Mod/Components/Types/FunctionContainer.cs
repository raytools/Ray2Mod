namespace Ray2Mod.Components.Types
{
    public class FunctionContainer
    {
        protected FunctionContainer(RemoteInterface remoteInterface)
        {
            Interface = remoteInterface;
        }

        protected RemoteInterface Interface { get; }
    }
}