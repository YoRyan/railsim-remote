using System;
using System.Runtime.InteropServices;

namespace RailSimRemote.Models
{
    public class StringReturnMarshaler : ICustomMarshaler
    {
        private static StringReturnMarshaler static_instance;
        public void CleanUpManagedData(object ManagedObj)
        {
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
        }

        public int GetNativeDataSize()
        {
            return -1;
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            return (IntPtr)0;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return Marshal.PtrToStringAnsi(pNativeData);
        }
        static ICustomMarshaler GetInstance(string pstrCookie)
        {
            if (static_instance == null)
            {
                static_instance = new StringReturnMarshaler();
            }
            return static_instance;
        }
    }
}