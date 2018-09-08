using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Win32.Toolhelp32;
namespace Toolhelp32Tests
{
    [TestClass]
    public class Toolhelp32SnapshotTest
    {
        [TestMethod]
        public void Toolhelp32SnapshotTest1()
        {
            using (var toolhelp = new Toolhelp32Snapshot())
            {
                Assert.AreEqual(true, toolhelp.IsInvalid);
                Assert.AreEqual(false, toolhelp.IsClosed);
            }
        }
        static IEnumerable<object[]> Toolhelp32SnapshotTest2Data
        {
            get
            {
                yield return new object[] { SnapshotFlags.Process, 0u };
                yield return new object[] { SnapshotFlags.All, 0u };
            }
        }
        [TestMethod,DynamicData(nameof(Toolhelp32SnapshotTest2Data))]
        public void Toolhelp32SnapshotTest2(SnapshotFlags Flags, uint ProcessID)
        {
            using (var toolhelp = new Toolhelp32Snapshot(Flags, ProcessID))
            {
                Assert.AreEqual(false, toolhelp.IsInvalid);
                Assert.AreEqual(false, toolhelp.IsClosed);
            }
        }
        static IEnumerable<object[]> GetProcess32TestData
        {
            get
            {
                yield return new object[] { SnapshotFlags.Process, 0u };
            }
        }
        [TestMethod,DynamicData(nameof(GetProcess32TestData))]
        public void GetProcess32Test(SnapshotFlags Flags, uint ProcessID)
        {
            using (var toolhelp = new Toolhelp32Snapshot(Flags, ProcessID))
            {
                foreach (var Process in toolhelp.GetProcess32())
                    Trace.WriteLine(Process);
            }
        }
        static IEnumerable<object[]> GetModule32TestData
        {
            get
            {
                yield return new object[] { SnapshotFlags.Module, 0u };
                yield return new object[] { SnapshotFlags.Module32, 0u };
            }
        }
        [TestMethod, DynamicData(nameof(GetModule32TestData))]
        public void GetModule32Test(SnapshotFlags Flags, uint ProcessID)
        {
            using (var toolhelp = new Toolhelp32Snapshot(Flags, ProcessID))
            {
                foreach (var Module in toolhelp.GetModule32())
                    Trace.WriteLine(Module);
            }
        }
        static IEnumerable<object[]> GetHeaplistTestData
        {
            get
            {
                yield return new object[] { SnapshotFlags.HeapList, 0u };
            }
        }
        [TestMethod, DynamicData(nameof(GetHeaplistTestData))]
        public void GetHeaplistTest(SnapshotFlags Flags, uint ProcessID)
        {
            using (var toolhelp = new Toolhelp32Snapshot(Flags, ProcessID))
            {
                foreach (var Heaplist in toolhelp.GetHeaplist32())
                {
                    Trace.WriteLine(Heaplist);
                    foreach (var Heap in Heaplist.GetHeap32())
                        Trace.WriteLine(Heap);
                }
            }
        }
    }
}
