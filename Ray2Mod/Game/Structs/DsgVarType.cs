using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs {
    public enum DsgVarType {

        Boolean,
        Byte,
        UByte,
        Short,
        UShort,
        Int,
        UInt,
        Float,
        WayPoint,
        Perso,
        List,
        Vector,
        Comport,
        Action,
        Text,
        GameMaterial,
        Caps,
        Graph,
        PersoArray,
        VectorArray,
        FloatArray,
        IntegerArray,
        WayPointArray,
        TextArray,
        SuperObject
    }

    public static class DsgVarTypeMap {

        public static Dictionary<DsgVarType, Type> typeMap = new Dictionary<DsgVarType, Type>()
        {
            {DsgVarType.Boolean,        typeof(bool) },
            {DsgVarType.Byte,           typeof(sbyte) },
            {DsgVarType.UByte,          typeof(byte) },
            {DsgVarType.Short,          typeof(short) },
            {DsgVarType.UShort,         typeof(ushort) },
            {DsgVarType.Int,            typeof(int) },
            {DsgVarType.UInt,           typeof(uint) },
            {DsgVarType.Float,          typeof(float) },
            {DsgVarType.WayPoint,       typeof(int*) },
            {DsgVarType.Perso,          typeof(Perso*) },
            {DsgVarType.List,           typeof(int*) },
            {DsgVarType.Vector,         typeof(Vector3) },
            {DsgVarType.Comport,        typeof(Behavior*) },
            {DsgVarType.Action,         typeof(int*) },
            {DsgVarType.Text,           typeof(int*) },
            {DsgVarType.GameMaterial,   typeof(int*) },
            {DsgVarType.Caps,           typeof(int*) },
            {DsgVarType.Graph,          typeof(int*) },
            {DsgVarType.PersoArray,     typeof(Perso*[]) },
            {DsgVarType.VectorArray,    typeof(Vector3[]) },
            {DsgVarType.FloatArray,     typeof(float[]) },
            {DsgVarType.IntegerArray,   typeof(int[]) },
            {DsgVarType.WayPointArray,  typeof(int*[]) },
            {DsgVarType.TextArray,      typeof(int*[]) },
            {DsgVarType.SuperObject,    typeof(SuperObject*) },
        };
    }

    
}
