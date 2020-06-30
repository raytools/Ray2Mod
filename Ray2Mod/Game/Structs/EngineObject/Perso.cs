using Ray2Mod.Game.Functions;
using Ray2Mod.Game.Structs.AI;
using Ray2Mod.Game.Structs.SPO;
using Ray2Mod.Game.Structs.Dynamics;
using Ray2Mod.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.EngineObject
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Perso
    {
        public int p3dData;
        public StandardGame* stdGamePtr;
        public Dynam* dynam;
        public Brain* brain;
        public int* off_camera;
        public int* off_collSet;
        public int* off_msWay;
        public int* off_msLight;
        public SectInfo* sectInfo;

        public List<DsgVarInfoAndValues> GetDsgVarList()
        {
            var result = new List<DsgVarInfoAndValues>();
            if (brain == null || brain->mind == null || brain->mind->dsgMem == null || brain->mind->dsgMem->dsgVar == null)
            {
                return result;
            }

            DsgMem* dsgMem = brain->mind->dsgMem;
            DsgVar* dsgVar = *(dsgMem->dsgVar);

            for (int i = 0; i < dsgVar->dsgVarInfosLength; i++)
            {
                var info = dsgVar->dsgVarInfos[i];
                result.Add(new DsgVarInfoAndValues()
                {
                    info = info,
                    valuePtrCurrent = new IntPtr(dsgMem->memoryBufferCurrent + info.offsetInBuffer),
                    valuePtrInitial = new IntPtr(dsgMem->memoryBufferInitial + info.offsetInBuffer)
                });
            }

            return result;
        }

        public string GetFamilyName(World w) => GetName(ObjectSet.Family, stdGamePtr->familyID, w);

        public string GetModelName(World w) => GetName(ObjectSet.Model, stdGamePtr->modelID, w);

        public string GetInstanceName(World w) => GetName(ObjectSet.Instance, stdGamePtr->instanceID, w);

        private string GetName(ObjectSet set, int id, World w)
        {
            string[] names = w.ObjectNames[set];
            return ((id >= 0 && id < names.Length) ? names[id] : id.ToString());
        }

        /// <summary>
        /// Copies a full SuperObject including Perso Data
        /// </summary>
        /// <param name="ogSPO">The Perso SuperObject to clone</param>
        /// <returns>The new SuperObject</returns>
        public static SuperObject* CopyPersoSPO(SuperObject* ogSPO)
        {
            SuperObject* newSPO = new SuperObject().ToUnmanaged();
            newSPO->type = SuperObjectType.Perso;
            newSPO->PersoData = Perso.CopyPerso(ogSPO->PersoData, newSPO);

            var ogMatrix1 = (*ogSPO->matrixPtr);
            var ogMatrix2 = (*ogSPO->matrixPtr2);

            newSPO->matrixPtr = (*ogSPO->matrixPtr).ToUnmanaged(); // Copy
            newSPO->matrixPtr2 = (*ogSPO->matrixPtr2).ToUnmanaged(); // Copy

            var newMatrix1 = (*newSPO->matrixPtr);
            var newMatrix2 = (*newSPO->matrixPtr2);

            newSPO->boundingVolume = (*ogSPO->boundingVolume).ToUnmanaged(); // Copy

            return newSPO;
        }

        public static Perso* CopyPerso(Perso* ogPerso, SuperObject* newSuperObject)
        {
            Perso* newPerso = new Perso().ToUnmanaged();
            newSuperObject->PersoData = newPerso;

            newPerso->p3dData = ogPerso->p3dData;
            var standardGame = (*ogPerso->stdGamePtr).ToUnmanaged(); ;  // Copy StandardGame
            standardGame->superObjectPtr = newSuperObject;
            standardGame->instanceID = 99999;
            newPerso->stdGamePtr = standardGame;

            EngineFunctions.fn_vInitOneObject.Call(newPerso, 0);

            return newPerso;
        }
    }
}