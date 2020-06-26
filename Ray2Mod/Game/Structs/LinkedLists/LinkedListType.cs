namespace Ray2Mod.Game.Structs.LinkedLists
{

    public abstract partial class LinkedList
    {

        [System.AttributeUsage(System.AttributeTargets.Field)]
        public class Type : System.Attribute
        {
            public System.Type type;

            public Type(System.Type type)
            {
                this.type = type;
            }
        }
    }
}
