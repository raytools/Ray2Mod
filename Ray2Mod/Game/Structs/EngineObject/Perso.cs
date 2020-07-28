using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Ray2Mod.Game.Functions;
using Ray2Mod.Game.Structs.AI;
using Ray2Mod.Game.Structs.Dynamics;
using Ray2Mod.Game.Structs.LinkedLists;
using Ray2Mod.Game.Structs.MathStructs;
using Ray2Mod.Game.Structs.SPO;
using Ray2Mod.Utils;

namespace Ray2Mod.Game.Structs.EngineObject
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Perso
    {
        public Perso3dData* p3dData;
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
            List<DsgVarInfoAndValues> result = new List<DsgVarInfoAndValues>();
            if (brain == null || brain->mind == null || brain->mind->dsgMem == null || brain->mind->dsgMem->dsgVar == null)
            {
                return result;
            }

            DsgMem* dsgMem = brain->mind->dsgMem;
            DsgVar* dsgVar = *(dsgMem->dsgVar);

            for (int i = 0; i < dsgVar->dsgVarInfosLength; i++)
            {
                DsgVarInfo info = dsgVar->dsgVarInfos[i];
                result.Add(new DsgVarInfoAndValues()
                {
                    info = info,
                    valuePtrCurrent = new IntPtr(dsgMem->memoryBufferCurrent + info.offsetInBuffer),
                    valuePtrInitial = new IntPtr(dsgMem->memoryBufferInitial + info.offsetInBuffer)
                });
            }

            return result;
        }

        public string GetFamilyName(World w)
        {
            return GetName(ObjectSet.Family, stdGamePtr->familyID, w);
        }

        public string GetModelName(World w)
        {
            return GetName(ObjectSet.Model, stdGamePtr->modelID, w);
        }

        public string GetInstanceName(World w)
        {
            return GetName(ObjectSet.Instance, stdGamePtr->instanceID, w);
        }

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

            Matrix ogMatrix1 = (*ogSPO->matrixPtr);
            Matrix ogMatrix2 = (*ogSPO->matrixPtr2);

            newSPO->matrixPtr = (*ogSPO->matrixPtr).ToUnmanaged(); // Copy
            newSPO->matrixPtr2 = (*ogSPO->matrixPtr2).ToUnmanaged(); // Copy

            Matrix newMatrix1 = (*newSPO->matrixPtr);
            Matrix newMatrix2 = (*newSPO->matrixPtr2);

            newSPO->boundingVolume = (*ogSPO->boundingVolume).ToUnmanaged(); // Copy

            return newSPO;
        }

        public static Perso* CopyPerso(Perso* ogPerso, SuperObject* newSuperObject)
        {
            Perso* newPerso = new Perso().ToUnmanaged();
            newSuperObject->PersoData = newPerso;

            StandardGame* standardGame = (*ogPerso->stdGamePtr).ToUnmanaged(); ;  // Copy StandardGame
            standardGame->superObjectPtr = newSuperObject;
            standardGame->instanceID = 99999;
            newPerso->stdGamePtr = standardGame;

            newPerso->brain = new Brain().ToUnmanaged();
            newPerso->brain->mind = new Mind() { aiModel = ogPerso->brain->mind->aiModel }.ToUnmanaged();
            newPerso->p3dData = new Perso3dData().ToUnmanaged();

            if (ogPerso->brain != null)
            {
                EngineFunctions.fn_vBrainCopyClone.Call(newPerso, ogPerso);
            }

            if (ogPerso->p3dData != null)
            {
                EngineFunctions.fn_v3dDataCopyClone.Call(newPerso, ogPerso);
            }

            EngineFunctions.fn_vInitOneObject.Call(newPerso, 0);

            return newPerso;
        }

        public int GetStateIndex()
        {
            return GetStateIndex(p3dData->stateCurrent);
        }

        public int GetStateIndex(LinkedList.ListElement_HHP<States.State>* state)
        {
            LinkedList.ListElement_HHP<States.State>*[] states = p3dData->family->Element.states.Read();

            for (int i = 0; i < states.Length; i++)
            {
                if (states[i] == state)
                {
                    return i;
                }
            }
            return -1;
        }

        public void SetState(int animationIndex, bool force = true, bool withEvents = true, bool setAnimation = true)
        {
            LinkedList.ListElement_HHP<States.State>*[] states = p3dData->family->Element.states.Read();
            if (animationIndex < states.Length)
            {
                EngineFunctions.PLA_fn_bSetNewState.Call(stdGamePtr->superObjectPtr, states[animationIndex], force ? (char)1 : (char)0, withEvents ? (char)1 : (char)0, setAnimation ? (char)1 : (char)0);
            }
            else
            {
                throw new Exception("Invalid State Number " + animationIndex + ", number of states is " + states.Length);
            }
        }

        public int NormalBehaviourIndex
        {
            get
            {
                Behavior* currentBehaviourNormal = brain->mind->intelligenceNormal->behavior;
                Behavior*[] normalBehaviours = brain->mind->aiModel->behaviorsListNormal->Behaviors;

                for (int i = 0; i < normalBehaviours.Length; i++)
                {
                    if (normalBehaviours[i] == currentBehaviourNormal)
                    {
                        return i;
                    }
                }
                return -1;
            }

            set
            {
                Behavior*[] normalBehaviours = brain->mind->aiModel->behaviorsListNormal->Behaviors;
                if (value < normalBehaviours.Length)
                {
                    brain->mind->intelligenceNormal->behavior = normalBehaviours[value];
                }
                else
                {
                    throw new Exception("Invalid Normal Behaviour Number " + value + ", number of states is " + normalBehaviours.Length);
                }
            }
        }

        public int ReflexBehaviourIndex
        {
            get
            {
                Behavior* currentBehaviourReflex = brain->mind->intelligenceReflex->behavior;
                Behavior*[] reflexBehaviours = brain->mind->aiModel->behaviorsListReflex->Behaviors;

                for (int i = 0; i < reflexBehaviours.Length; i++)
                {
                    if (reflexBehaviours[i] == currentBehaviourReflex)
                    {
                        return i;
                    }
                }
                return -1;
            }

            set
            {
                Behavior*[] reflexBehaviours = brain->mind->aiModel->behaviorsListReflex->Behaviors;
                if (value < reflexBehaviours.Length)
                {
                    brain->mind->intelligenceReflex->behavior = reflexBehaviours[value];
                }
                else
                {
                    throw new Exception("Invalid Reflex Behaviour Number " + value + ", number of states is " + reflexBehaviours.Length);
                }
            }
        }
    }
}
