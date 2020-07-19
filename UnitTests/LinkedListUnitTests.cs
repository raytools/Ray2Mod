using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ray2Mod.Game.Structs.LinkedLists;

namespace UnitTests {
    [TestClass]
    public class LinkedListUnitTests {
        [TestMethod]
        public unsafe void LinkedListTest_EPF()
        {
            LinkedList.ElementPointerFirst<int> list = new LinkedList.ElementPointerFirst<int>();
            var firstItem = list.Add(1);
            var secondItem = list.Add(2);
            var thirdItem = list.Add(3);

            var items = list.Read();

            Assert.AreEqual(list.Count, 3);

            Assert.AreEqual((int)list.Head, (int)items[0]);
            Assert.AreEqual((int)list.Tail, (int)items[2]);
            Assert.AreEqual((int)list.Tail, (int)items[2]);

            Assert.AreEqual(list.Count, 3);
        }
    }
}
