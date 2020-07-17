using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ray2Mod.Game.Structs.LinkedLists;

namespace UnitTests.LinkedListTests {

    [TestClass]
    public class HasHeaderPointers_ElementPointerFirstTests {
            
        [TestMethod]
        public unsafe void Test1_Add()
        {
            LinkedList.HasHeaderPointers_ElementPointerFirst<int> list = new LinkedList.HasHeaderPointers_ElementPointerFirst<int>();
            var firstItem = list.Add(1);
            var secondItem = list.Add(2);
            var thirdItem = list.Add(3);

            var items_1 = list.Read();

            Assert.AreEqual(3, list.Count);

            Assert.AreEqual((int)items_1[0], (int)list.Head);
            Assert.AreEqual((int)items_1[2], (int)list.Tail);

            Assert.AreEqual(1, items_1[0]->Element);
            Assert.AreEqual(2, items_1[1]->Element);
            Assert.AreEqual(3, items_1[2]->Element);

            Assert.AreEqual((int)firstItem, (int)items_1[0]);
            Assert.AreEqual((int)secondItem, (int)items_1[1]);
            Assert.AreEqual((int)thirdItem, (int)items_1[2]);

            Assert.AreEqual((int)items_1[1], (int)items_1[2]->Previous);
            Assert.AreEqual((int)items_1[1], (int)items_1[0]->Next);
        }

        [TestMethod]
        public unsafe void Test2_AddAndRemove()
        {
            LinkedList.HasHeaderPointers_ElementPointerFirst<int> list = new LinkedList.HasHeaderPointers_ElementPointerFirst<int>();
            var firstItem = list.Add(1);
            var secondItem = list.Add(2);
            var thirdItem = list.Add(3);

            list.Remove(firstItem);
            list.Remove(thirdItem);

            var items_2 = list.Read();
            Assert.AreEqual(2, items_2[0]->Element);
            Assert.AreEqual((int)items_2[0], (int)list.Head);
            Assert.AreEqual((int)list.Head, (int)list.Tail);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public unsafe void Test3_Write()
        {
            LinkedList.HasHeaderPointers_ElementPointerFirst<int> list = new LinkedList.HasHeaderPointers_ElementPointerFirst<int>();

            list.Write(new int[] { 4, 5, 6, 7 });
            var items_3 = list.Read();

            Assert.AreEqual(4, items_3[0]->Element);
            Assert.AreEqual(5, items_3[1]->Element);
            Assert.AreEqual(6, items_3[2]->Element);
            Assert.AreEqual(7, items_3[3]->Element);

            Assert.AreEqual(4, list.Count);

        }
    }
}
