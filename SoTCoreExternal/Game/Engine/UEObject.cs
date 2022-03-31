using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game.Engine
{
    public class UEObject
    {
        String _className;
        public String ClassName
        {
            get
            {
                if (_className != null) return _className;
                _className = SotCore.Instance.Engine.GetFullName(ClassAddr);
                return _className;
            }
        }

        String _parentClassName;
        public String ParentClassName
        {
            get
            {
                if (_parentClassName != null) return _parentClassName;
                _parentClassName = SotCore.Instance.Engine.GetFullName(ParentClassAddr);
                return _className;
            }
        }

        static ConcurrentDictionary<UInt64, UInt64> ObjToClass = new ConcurrentDictionary<UInt64, UInt64>();
        UInt64 _classAddr = UInt64.MaxValue;
        public UInt64 ClassAddr
        {
            get
            {
                if (_classAddr != UInt64.MaxValue) return _classAddr;
                if (ObjToClass.ContainsKey(Address))
                {
                    // _classAddr = ObjToClass[Address];
                    // return _classAddr;
                }
                _classAddr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(Address + 0x10);
                //ObjToClass[Address] = _classAddr;
                return _classAddr;
            }
        }

        UInt64 _ParentclassAddr = UInt64.MaxValue;
        public UInt64 ParentClassAddr
        {
            get
            {
                if (_ParentclassAddr != UInt64.MaxValue) return _ParentclassAddr;
                _ParentclassAddr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(Address + 0x30);
                return _ParentclassAddr;
            }
        }

        public UEObject(UInt64 address)
        {
            Address = address;
        }
        public Boolean IsA(String className)
        {
            return SotCore.Instance.Engine.IsA(ClassAddr, SotCore.Instance.Engine.FindClass(className));
        }
        public UInt32 FieldOffset;
        public Byte[] Data;
        public UInt64 _value = UInt64.MaxValue;
        public UInt64 Value
        {
            get
            {
                if (_value != UInt64.MaxValue) return _value;
                _value = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(Address);
                return _value;
            }
            set
            {
                SotCore.Instance.Memory.WriteProcessMemory(Address, value);
            }
        }
        public UInt64 Address;
        public UEObject this[String key]
        {
            get
            {
                var fieldAddr = SotCore.Instance.Engine.GetFieldAddr(ClassAddr, ClassAddr, key);
                var fieldType = SotCore.Instance.Engine.GetFieldType(fieldAddr);
                var offset = (UInt32)SotCore.Instance.Engine.GetFieldOffset(fieldAddr);
                UEObject obj;
                if (fieldType == "ObjectProperty" || fieldType == "ScriptStruct")
                    obj = new UEObject(SotCore.Instance.Memory.ReadProcessMemory<UInt64>(Address + offset)) { FieldOffset = offset };
                else if (fieldType == "ArrayProperty")
                {
                    obj = new UEObject(Address + offset);
                    obj._classAddr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(fieldAddr + 0x10);
                }
                else if (fieldType.Contains("Bool"))
                {
                    obj = new UEObject(Address + offset);
                    obj._classAddr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(fieldAddr + 0x10);
                    var boolMask = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(fieldAddr + 0x70);
                    boolMask = (boolMask >> 16) & 0xff;
                    var fullVal = SotCore.Instance.Memory.ReadProcessMemory<Byte>(Address + offset);
                    obj._value = ((fullVal & boolMask) == boolMask) ? 1u : 0;
                }
                else if (fieldType.Contains("Function"))
                {
                    obj = new UEObject(fieldAddr);
                    obj.BaseObjAddr = Address;
                }
                else
                {
                    obj = new UEObject(Address + offset);
                    obj._classAddr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(fieldAddr + 0x70);
                }
                if (obj.Address == 0)
                {
                    return null;
                    //var classInfo = Engine.Instance.DumpClass(ClassAddr);
                    //throw new Exception("bad addr");
                }
                return obj;
            }
        }
        public UInt32 Num { get { return SotCore.Instance.Memory.ReadProcessMemory<UInt32>(Address + 8); } }
        public UEObject this[UInt32 index]
        {
            get
            {
                return new UEObject(SotCore.Instance.Memory.ReadProcessMemory<UInt64>(SotCore.Instance.Memory.ReadProcessMemory<UInt64>(Address) + index * 8));
            }
        }
        UInt64 BaseObjAddr;
        static UInt32 _vTableVFunc = UInt32.MaxValue;
        UInt32 VTableVFunc
        {
            get
            {
                if (_vTableVFunc != UInt32.MaxValue) return _vTableVFunc;
                var v = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(BaseObjAddr);
                for (var i = 60u; i < 75; i++)
                {
                    var s = (IntPtr)SotCore.Instance.Memory.ReadProcessMemory<UInt64>(v + i * 8);
                    var sig = (UInt64)SotCore.Instance.Memory.FindPattern("48 8B EA 48 8B D9 FF 90 ? ? ? ? 48 85 C0", s, 0x200);
                    if (sig != 0)
                    {
                        _vTableVFunc = i;
                        return _vTableVFunc;
                    }
                }
                throw new Exception("not found");
            }
        }
        public T Invoke<T>(params Object[] args)
        {
            var vTableFunc = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(BaseObjAddr) + VTableVFunc * 8;
            vTableFunc = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(vTableFunc);
            return SotCore.Instance.Memory.Execute<T>((IntPtr)vTableFunc, (IntPtr)BaseObjAddr, (IntPtr)Address, args);
        }

        public static bool operator ==(UEObject lhs, UEObject rhs)
        {
            return lhs.Address == rhs.Address;
        }
        public static bool operator !=(UEObject lhs, UEObject rhs)
        {
            return lhs.Address != rhs.Address;
        }
    }
}
