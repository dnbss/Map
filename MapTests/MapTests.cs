using System;
using System.IO.Enumeration;
using Map;
using NUnit.Framework;

namespace Tests
{
    public class MapTests 
    {
        private Map<char, int> map;
        
        [SetUp]
        public void Setup()
        {
            map = new Map<char, int>();
        }
        
        [Test]
        public void InsertTest_ExistingKey_ExceptionExpect()
        {
            map.Insert('a',2);

            try
            {
                map.Insert('a', 10);
                Assert.Fail();      // the exception didn't work
            }
            catch (Exception e)
            {
                Assert.AreEqual("An element with this key already exists", e.Message);
            }
        }

        [Test]
        public void InsertTest_InsertTwoElements_CorrectInsert()
        {
            map.Insert('a',1);
            map.Insert('b', 2);

            string actual = map.PrintToString();

            string expected = "Key: a | Data: 1\nKey: b | Data: 2\n";
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemoveTest_NonExistingElement_ExceptionExpect()
        {
            try
            {
                map.Remove('a');
                Assert.Fail();      // the exception didn't work
            }
            catch (Exception e)
            {
                Assert.AreEqual("Item not found!", e.Message);
            }
        }

        [Test]
        public void RemoveTest_InsertThreeElementsAndRemoveTwoElements_CorrectRemove()
        {
            map.Insert('b',5);
            map.Insert('a',1);
            map.Insert('r', 8);
            
            map.Remove('a');
            map.Remove('r');
            
            string actual = map.PrintToString();

            string expected = "Key: b | Data: 5\n";
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PrintTest_EmptyMap_EmptyOutput()
        {
            string actual = map.PrintToString();

            string expected = "";
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PrintTest_InsertTwoElements_CorrectOutput()
        {
            map.Insert('g', 8);
            map.Insert('b', 6);

            string actual = map.PrintToString();

            string expected = "Key: b | Data: 6\nKey: g | Data: 8\n";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ClearTest_InsertTwoElements_EmptyMap()
        {
            map.Insert('g', 8);
            map.Insert('b', 6);

            map.Clear();
            
            string actual = map.PrintToString();

            string expected = "";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetKeysTest_EmptyMap_EmptyGetKeys()
        {
            Queue<char> keys = map.GetKeys();
            
            Assert.AreEqual(true, keys.IsEmpty());
        }
        
        [Test]
        public void GetKeysTest_InsertTwoElements_GetTwoKeys()
        {
            map.Insert('g', 8);
            map.Insert('b', 6);
            
            Queue<char> actual = map.GetKeys();

            Queue<char> expected = new Queue<char>();
            expected.Push('b');
            expected.Push('g');

            while (!actual.IsEmpty())
            {
                if (actual.Pop() != expected.Pop())
                {
                    Assert.Fail();
                }
            }
            
        }
        
        [Test]
        public void GetKeysTest_EmptyMap_GetEmptyKeys()
        {
            Assert.True(map.GetKeys().IsEmpty());
        }
        
        [Test]
        public void GetValuesTest_InsertTwoElements_GetTwoKeys()
        {
            map.Insert('g', 8);
            map.Insert('b', 6);
            
            Queue<int> actual = map.GetValues();

            Queue<int> expected = new Queue<int>();
            expected.Push(6);
            expected.Push(8);

            while (!actual.IsEmpty())
            {
                if (actual.Pop() != expected.Pop())
                {
                    Assert.Fail();
                }
            }
            
        }
        
        [Test]
        public void GetValuesTest_EmptyMap_GetEmptyKeys()
        {
            Assert.True(map.GetValues().IsEmpty());
        }

        [Test]
        public void FindTest_NonExistingElement_ExceptionExpect()
        {
            map.Insert('b',1);
            
            try
            {
                map.Find('a');
                Assert.Fail();      // the exception didn't work
            }
            catch (Exception e)
            {
                Assert.AreEqual("Item not found!", e.Message);
            }
        }
        
        [Test]
        public void FindTest_TwoElement_CorrectFind()
        {
            map.Insert('b',1);
            map.Insert('a',7);
            
            Assert.AreEqual(7, map.Find('a'));
            
        }
    }
}