using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.LinkedLists {

    public abstract partial class LinkedList {

        [System.AttributeUsage(System.AttributeTargets.Field)]
        public class Type : System.Attribute {
            public System.Type type;

            public Type(System.Type type)
            {
                this.type = type;
            }
        }
    }
}
