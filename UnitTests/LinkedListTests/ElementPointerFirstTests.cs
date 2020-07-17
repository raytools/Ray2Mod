using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ray2Mod.Game.Structs.LinkedLists;

namespace UnitTests.LinkedListTests {

    [TestClass]
    public class ElementPointerFirstTests {
            
        [TestMethod]
        public unsafe void Test1_Add()
        {
            LinkedList.ElementPointerFirst<int> list = new LinkedList.ElementPointerFirst<int>();
            var firstItem = list.Add(1);
            var secondItem = list.Add(2);
            var thirdItem = list.Add(3);

            var items_1 = list.Read();

            Assert.AreEqual(list.Count, 3);

            Assert.AreEqual((int)list.Head, (int)items_1[0]);
            Assert.AreEqual((int)list.Tail, (int)items_1[2]);

            Assert.AreEqual(items_1[0]->Element, 1);
            Assert.AreEqual(items_1[1]->Element, 2);
            Assert.AreEqual(items_1[2]->Element, 3);

            Assert.AreEqual((int)items_1[0], (int)firstItem);
            Assert.AreEqual((int)items_1[1], (int)secondItem);
            Assert.AreEqual((int)items_1[2], (int)thirdItem);

            Assert.AreEqual((int)items_1[2]->Previous, (int)items_1[1]);
            Assert.AreEqual((int)items_1[0]->Next, (int)items_1[1]);
        }

        [TestMethod]
        public unsafe void Test2_AddAndRemove()
        {
            LinkedList.ElementPointerFirst<int> list = new LinkedList.ElementPointerFirst<int>();
            var firstItem = list.Add(1);
            var secondItem = list.Add(2);
            var thirdItem = list.Add(3);

            list.Remove(firstItem);
            list.Remove(thirdItem);

            var items_2 = list.Read();
            Assert.AreEqual(items_2[0]->Element, 2);
            Assert.AreEqual((int)list.Head, (int)items_2[0]);
            Assert.AreEqual((int)list.Tail, (int)list.Head);
            Assert.AreEqual(list.Count, 1);
        }

        [TestMethod]
        public unsafe void Test3_Write()
        {
            LinkedList.ElementPointerFirst<int> list = new LinkedList.ElementPointerFirst<int>();

            list.Write(new int[] { 4, 5, 6, 7 });
            var items_3 = list.Read();

            Assert.AreEqual(items_3[0]->Element, 4);
            Assert.AreEqual(items_3[1]->Element, 5);
            Assert.AreEqual(items_3[2]->Element, 6);
            Assert.AreEqual(items_3[3]->Element, 7);

            Assert.AreEqual(list.Count, 4);

        }
    }
}
