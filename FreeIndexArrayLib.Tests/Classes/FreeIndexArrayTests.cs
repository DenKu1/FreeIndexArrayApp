using Microsoft.VisualStudio.TestTools.UnitTesting;
using FreeIndexArrayLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FreeIndexArrayLib.Classes.Tests
{
    [TestClass()]
    public class FreeIndexArrayTests
    {
        FreeIndexArray<string> array;

        [TestInitialize]
        public void TestInitialize()
        {
            array = new FreeIndexArray<string>(-3, 2);
        }

        [ExpectedException(typeof(FreeIndexArrayIndexValuesException))]
        [TestMethod()]
        public void FreeIndexArray_StopIndexLessStartIndex_ThrowFreeIndexArrayIndexValuesException()
        {
            array = new FreeIndexArray<string>(10, 0);
        }

        [TestMethod()]
        public void FreeIndexArray_IndexesMinusFiveAndSeven_MaxLengthIs13()
        {
            int maxLength;

            array = new FreeIndexArray<string>(-5, 7);
            maxLength = array.MaxLength;

            Assert.AreEqual(13, maxLength);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod()]
        public void Add_ItemIsNull_ThrowNullReferenceException()
        {
            array.Add(null);
        }

        [ExpectedException(typeof(FreeIndexArrayIsFullException))]
        [TestMethod()]
        public void Add_ArrayIsFull_ThrowFreeIndexArrayIsFullException()
        {
            string firstItem = "A";
            string secondItem = "B";

            array = new FreeIndexArray<string>(0, 0);
            array.Add(firstItem);
            array.Add(secondItem);
        }

        [TestMethod()]
        public void Add_AddFiveItems_StoredItemsHasProperSequence()
        {
            string firstItem = "A";
            string secondItem = "B";
            string thirdItem = "C";
            string fourthItem = "D";
            string fifthItem = "E";
            List<string> expected = new List<string>
            {
                 firstItem,
                 secondItem,
                 thirdItem,
                 fourthItem,
                 fifthItem
            };        
           
            array.Add(firstItem);
            array.Add(secondItem);
            array.Add(thirdItem);
            array.Add(fourthItem);
            array.Add(fifthItem);
            var actual = (ICollection)array.ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Remove_ArrayIsEmpty_ReturnFalse()
        {
            bool isRemoved = array.Remove("Item");

            Assert.IsFalse(isRemoved);
        }

        [TestMethod()]
        public void Remove_ArrayNotContainsThisItem_ReturnFalse()
        {
            string firstItem = "Some item1";
            string secondItem = "Some item2";
            string thirdItem = "Some item3";

            array.Add(firstItem);
            array.Add(secondItem);
            array.Add(thirdItem);
            bool isRemoved = array.Remove("Item");

            Assert.IsFalse(isRemoved);
        }

        [TestMethod()]
        public void Remove_ArrayContainsThisItem_ReturnTrueAndNotContainsThisItem()
        {
            string firstItem = "Some item1";
            string secondItem = "Some item2";
            string thirdItem = "Some item3";
            string itemToRemove = "Item to remove";

            array.Add(firstItem);
            array.Add(secondItem);
            array.Add(thirdItem);
            array.Add(itemToRemove);
            bool isRemoved = array.Remove(itemToRemove);
            bool arrayContainsValue = array.Contains(itemToRemove);

            Assert.IsTrue(isRemoved);
            Assert.IsFalse(arrayContainsValue);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod()]
        public void Remove_ItemIsNull_ThrowNullReferenceException()
        {
            array.Remove(null);
        }

        [TestMethod()]
        public void Clear_ClearNotEmptyArray_ArrayHasZeroItems()
        {        
            string firstItem = "Item1";
            string secondItem = "Item2";
            string thirdItem = "Item3";
            int itemsCount;

            array.Add(firstItem);
            array.Add(secondItem);
            array.Add(thirdItem);
            array.Clear();
            itemsCount = array.Count;
           
            Assert.AreEqual(0, itemsCount);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod()]
        public void Contains_ItemIsNull_ThrowNullReferenceException()
        {
            array.Contains(null);
        }

        [TestMethod()]
        public void Contains_EmptyArray_ReturnFalse()
        {
            bool contains = array.Contains("Item");

            Assert.IsFalse(contains);
        }

        [TestMethod()]
        public void Contains_ArrayContainsThisItem_ReturnTrue()
        {
            string item = "Item";

            array.Add(item);
            bool contains = array.Contains("Item");

            Assert.IsTrue(contains);
        }

        [TestMethod()]
        public void Contains_ArrayNotContainsThisItem_ReturnFalse()
        {
            string item1 = "Some Item1";
            string item2 = "Some Item2";

            array.Add(item1);
            array.Add(item2);
            bool contains = array.Contains("Item");

            Assert.IsFalse(contains);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod()]
        public void CopyTo_TargetArrayIsNull_ThrowArgumentNullException()
        {
            array.CopyTo(null, 0);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod()]
        public void CopyTo_ArrayIndexUnderZero_ThrowArgumentOutOfRangeException()
        {
            string[] targetArray = new string[5];

            array.CopyTo(targetArray, -1);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void CopyTo_TargetArrayNotHasEnoughSpace_ThrowArgumentException()
        {
            string[] targetArray = new string[1];
            string item1 = "Some Item1";
            string item2 = "Some Item2";

            array.Add(item1);
            array.Add(item2);
            array.CopyTo(targetArray, 0);
        }

        [TestMethod()]
        public void CopyTo_CopyTwoItemsToTargetArray_TargetArrayHasTwoItems()
        {
            string[] targetArray = new string[5];
            string item1 = "Some Item1";
            string item2 = "Some Item2";            

            array.Add(item1);
            array.Add(item2);
            array.CopyTo(targetArray, 0);         

            Assert.AreEqual(item1, targetArray[0]);
            Assert.AreEqual(item2, targetArray[1]);
        }

        [TestMethod()]
        public void GetEnumerator_ArrayIsEmpty_EnumeratorNotHasNextValue()
        {
            var enumerator = array.GetEnumerator();
            bool hasNextElement = enumerator.MoveNext();

            Assert.IsFalse(hasNextElement);
        }

        [TestMethod()]
        public void GetEnumerator_ArrayHasFiveValues_EnumeratorHasFiveValues()
        {
            int counter = 0;
            string firstValue = "A";
            string secondValue = "B";
            string thirdValue = "C";
            string fourthValue = "D";
            string fifthValue = "E";

            array.Add(firstValue);
            array.Add(secondValue);
            array.Add(thirdValue);
            array.Add(fourthValue);
            array.Add(fifthValue);
            foreach (var value in array)
                counter++;

            Assert.AreEqual(5, counter);        
        }
    }
}