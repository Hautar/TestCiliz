using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestCaseCiliz
{
    public class TestRunner
    {
       private int       intValue;
       private List<int> intArray;
       private string    stringValue;
       private DateTime  dateValue;
       private TestClass testClass;

       private IMonitor<int>       intMonitor;
       private List<IMonitor<int>> intArrayMonitors;
       private IMonitor<string>    stringMonitor;
       private IMonitor<DateTime>  dateMonitor;
       private IMonitor<TestClass> testClassMonitor;

       public void Run()
       {
           RunTest1();
           RunTest2();
           RunTest3();
       }

       private void RunTest1()
       {
           Initialize();

           SetupMonitorsForTest1();
           CheckTest1Asserts();

           UpdateValues();
           CheckTest1Asserts();
       }

       private void RunTest2()
       {
           Initialize();

           SetupMonitorsForTest2();
           CheckTest2Asserts();

           UpdateValues();
           CheckTest2Asserts();
       }

       private void RunTest3()
       {
           Initialize();

           SetupMonitorsForTest3();
           CheckTest3Asserts();

           UpdateValues();
           CheckTest3Asserts();
       }

       //TODO: write your code here - setup observers
       //use cycle to initialize observers for intArray
       private void SetupMonitorsForTest1()
       {
           intMonitor = new Monitor1<int>(() => intValue);
           intArrayMonitors = new List<IMonitor<int>>();
           for (int i = 0; i < intArray.Count; i++)
           {
               var i1 = i;
               intArrayMonitors.Add(new Monitor1<int>(() => intArray[i1]));
           }
           stringMonitor = new Monitor1<string>(() => stringValue);
           dateMonitor = new Monitor1<DateTime>(() => dateValue);
           testClassMonitor = new Monitor1<TestClass>(() => testClass);
       }

       //TODO: write your code here - setup observers
       //use cycle to initialize observers for intArray
       private void SetupMonitorsForTest2()
       {
           intMonitor = new Monitor2And3<int>(intValue);
           intArrayMonitors = new List<IMonitor<int>>();
           for (int i = 0; i < intArray.Count; i++)
           {
               intArrayMonitors.Add(new Monitor2And3<int>(intArray[i]));
           }
           stringMonitor = new Monitor2And3<string>(stringValue);
           dateMonitor = new Monitor2And3<DateTime>(dateValue);
           testClassMonitor = new Monitor2And3<TestClass>(testClass);
       }

       //TODO: write your code here - setup observers
       private void SetupMonitorsForTest3()
       {
           testClassMonitor = new Monitor2And3<TestClass>(new TestClass
           {
               IntValue = testClass.IntValue,
               StringValue = testClass.StringValue,
           });
       }

       private void Initialize()
       {
           intValue    = 9;
           intArray    = new List<int> { 8, 7, 6, 5, 4 };
           stringValue = "TestString";
           dateValue   = DateTime.MinValue;
           testClass   = new TestClass() { IntValue = 1, StringValue = "ReferenceType" };

           intMonitor       = null;
           intArrayMonitors = null;
           stringMonitor    = null;
           dateMonitor      = null;
           testClassMonitor = null;
       }

       private void UpdateValues()
       {
           intValue++;
           for (int i = 0; i < intArray.Count; i++)
           {
               intArray[i]++;
           }

           stringValue += "1";
           dateValue = dateValue.AddDays(1);
           testClass.IntValue++;
       }

       private void CheckTest1Asserts()
       {
           Debug.Assert(intMonitor.Value == intValue);
           Debug.Assert(intArrayMonitors.Count == intArray.Count);
           for (int i = 0; i < intArray.Count; i++)
           {
               Debug.Assert(intArrayMonitors[i].Value == intArray[i]);
           }

           Debug.Assert(stringMonitor.Value == stringValue);
           Debug.Assert(dateMonitor.Value == dateValue);
           Debug.Assert(testClassMonitor.Value == testClass);
       }

       private void CheckTest2Asserts()
       {
           Debug.Assert(intMonitor.Value == 9);
           Debug.Assert(intArrayMonitors.Count == intArray.Count);
           var expectedArray = new int[] { 8, 7, 6, 5, 4 };
           for (int i = 0; i < intArray.Count; i++)
           {
               Debug.Assert(intArrayMonitors[i].Value == expectedArray[i]);
           }

           Debug.Assert(stringMonitor.Value == "TestString");
           Debug.Assert(dateMonitor.Value == DateTime.MinValue);
           Debug.Assert(testClassMonitor.Value == testClass);
       }

       private void CheckTest3Asserts()
       {
           //TODO: explain the difference between '==' operator and 'Equals' method
           // 1) '==' and 'Equals'
           // '==' compares references to objects if we compare reference data types 
           // in this particular case 'Equals' compares both fields of TestClass: 'StringValue' and 'IntValue'
           // because we compare both times testClassMonitor.Value 'StringValue' and 'IntValue' to 1 and "ReferenceType",
           // it means that we shouldn't update testClassMonitor.Value. One of the approaches is to pass new class
           // instance (make a deep copy, or just create new class)
           Debug.Assert(testClassMonitor.Value.Equals(new TestClass() { IntValue = 1, StringValue = "ReferenceType" }));
       }
   }
}