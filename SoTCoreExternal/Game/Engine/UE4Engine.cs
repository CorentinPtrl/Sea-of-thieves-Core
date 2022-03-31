using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game.Engine
{
    public class UE4Engine
    {
        public String GetName(Int32 i)
        {
            var fNamePtr = SotCore.Instance.Memory.ReadProcessMemory<ulong>(SotCore.Instance.GNames + ((UInt64)i / 0x4000) * 8);
            var fName2 = SotCore.Instance.Memory.ReadProcessMemory<ulong>(fNamePtr + (8 * ((UInt64)i % 0x4000)));
            var fName3 = SotCore.Instance.Memory.ReadProcessMemory<String>(fName2 + 0x10); // 0x10 on some version?
            if (fName3.Contains("/")) return fName3.Substring(fName3.LastIndexOf("/") + 1);
            return fName3;
        }
        ConcurrentDictionary<UInt64, String> AddrToClass = new ConcurrentDictionary<UInt64, String>();
        public String GetFullName(UInt64 entityAddr)
        {
            if (AddrToClass.ContainsKey(entityAddr)) return AddrToClass[entityAddr];
            var classPtr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(entityAddr + 0x10);
            var classNameIndex = SotCore.Instance.Memory.ReadProcessMemory<Int32>(classPtr + 0x18);
            var name = GetName(classNameIndex);
            UInt64 outerEntityAddr = entityAddr;
            var parentName = "";
            //while ((outerEntityAddr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(outerEntityAddr + 0x20)) > 0)
            while (true)
            {
                var tempOuterEntityAddr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(outerEntityAddr + 0x20);
                if (tempOuterEntityAddr == outerEntityAddr || tempOuterEntityAddr == 0) break;
                outerEntityAddr = tempOuterEntityAddr;
                var outerNameIndex = SotCore.Instance.Memory.ReadProcessMemory<Int32>(outerEntityAddr + 0x18);
                var tempName = GetName(outerNameIndex);
                if (tempName == "") break;
                if (tempName == "None") break;
                parentName = tempName + "." + parentName;
            }
            name += " " + parentName;
            var nameIndex = SotCore.Instance.Memory.ReadProcessMemory<Int32>(entityAddr + 0x18);
            name += GetName(nameIndex);
            AddrToClass[entityAddr] = name;
            return name;
        }
        ConcurrentDictionary<String, Boolean> ClassIsSubClass = new ConcurrentDictionary<String, Boolean>();
        public Boolean IsA(UInt64 entityClassAddr, UInt64 targetClassAddr)
        {
            var key = entityClassAddr + ":" + targetClassAddr;
            if (ClassIsSubClass.ContainsKey(key)) return ClassIsSubClass[key];
            if (entityClassAddr == targetClassAddr) return true;
            while (true)
            {
                var tempEntityClassAddr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(entityClassAddr + 0x40);
                if (entityClassAddr == tempEntityClassAddr || tempEntityClassAddr == 0)
                    break;
                entityClassAddr = tempEntityClassAddr;
                if (entityClassAddr == targetClassAddr)
                {
                    ClassIsSubClass[key] = true;
                    return true;
                }
            }
            ClassIsSubClass[key] = false;
            return false;
        }
        ConcurrentDictionary<String, UInt64> ClassToAddr = new ConcurrentDictionary<String, UInt64>();
        public UInt64 FindClass(String className)
        {
            if (ClassToAddr.ContainsKey(className)) return ClassToAddr[className];
            var masterEntityList = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(SotCore.Instance.Memory.BaseAddress + SotCore.Instance.GObjects);
            var num = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(SotCore.Instance.Memory.BaseAddress + SotCore.Instance.GObjects + 0x14);
            var entityChunk = 0u;
            var entityList = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(masterEntityList);
            for (var i = 0u; i < num; i++)
            {
                if ((i / 0x10000) != entityChunk)
                {
                    entityChunk = (UInt32)(i / 0x10000);
                    entityList = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(masterEntityList + 8 * entityChunk);
                }
                var entityAddr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(entityList + 24 * (i % 0x10000));
                if (entityAddr == 0) continue;
                var name = GetFullName(entityAddr);
                if (name == className)
                {
                    ClassToAddr[className] = entityAddr;
                    return entityAddr;
                }
            }
            return 0;
        }
        public Int32 FieldIsClass(String className, String fieldName)
        {
            var classAddr = FindClass(className);
            var fieldAddr = GetFieldAddr(classAddr, classAddr, fieldName);
            var offset = GetFieldOffset(fieldAddr);
            return offset;
        }
        ConcurrentDictionary<UInt64, ConcurrentDictionary<String, UInt64>> ClassFieldToAddr = new ConcurrentDictionary<UInt64, ConcurrentDictionary<String, UInt64>>();
        public UInt64 GetFieldAddr(UInt64 origClassAddr, UInt64 classAddr, String fieldName)
        {
            if (ClassFieldToAddr.ContainsKey(origClassAddr) && ClassFieldToAddr[origClassAddr].ContainsKey(fieldName)) return ClassFieldToAddr[origClassAddr][fieldName];
            var field = classAddr + 0x10; // 0x10 on some versions?
            while ((field = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(field + 0x28)) > 0)
            {
                var fName = GetName(SotCore.Instance.Memory.ReadProcessMemory<Int32>(field + 0x18));
                if (fName == fieldName)
                {
                    //var offset = SotCore.Instance.Memory.ReadProcessMemory<Int32>(field + 0x44);
                    if (!ClassFieldToAddr.ContainsKey(origClassAddr))
                        ClassFieldToAddr[origClassAddr] = new ConcurrentDictionary<String, UInt64>();
                    ClassFieldToAddr[origClassAddr][fieldName] = field;
                    return field;
                }
            }
            var parentClass = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(classAddr + 0x30); // 0x30 on some versions
            var c = GetFullName(classAddr);
            var pc = GetFullName(parentClass);
            if (parentClass == classAddr) throw new Exception("parent is me");
            if (parentClass == 0) throw new Exception("bad field");
            return GetFieldAddr(origClassAddr, parentClass, fieldName);
        }
        ConcurrentDictionary<UInt64, Int32> FieldAddrToOffset = new ConcurrentDictionary<UInt64, Int32>();
        public Int32 GetFieldOffset(UInt64 fieldAddr)
        {
            if (FieldAddrToOffset.ContainsKey(fieldAddr)) return FieldAddrToOffset[fieldAddr];
            var offset = SotCore.Instance.Memory.ReadProcessMemory<Int32>(fieldAddr + 0x44);
            FieldAddrToOffset[fieldAddr] = offset;
            return offset;
        }
        ConcurrentDictionary<UInt64, String> FieldAddrToType = new ConcurrentDictionary<UInt64, String>();
        public String GetFieldType(UInt64 fieldAddr)
        {
            if (FieldAddrToType.ContainsKey(fieldAddr)) return FieldAddrToType[fieldAddr];
            var fieldType = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(fieldAddr + 0x10);
            var name = GetName(SotCore.Instance.Memory.ReadProcessMemory<Int32>(fieldType + 0x18));
            FieldAddrToType[fieldAddr] = name;
            return name;
        }
        public String DumpClass(String className)
        {
            var classAddr = FindClass(className);
            return DumpClass(classAddr);
        }
        public String DumpClass(UInt64 classAddr)
        {
            var sb = new StringBuilder();
            var name = GetFullName(classAddr);
            sb.Append(classAddr.ToString("X") + " : " + name);
            var pcAddr = classAddr;
            var c = 0;
            while ((pcAddr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(pcAddr + 0x40)) > 0 && c++ < 20)
            {
                var super = GetFullName(pcAddr);
                sb.Append(" : " + super);
            }
            sb.AppendLine();

            pcAddr = classAddr;
            //while ((pcAddr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(pcAddr + 0x40)) > 0)
            while (true)
            {
                var field = pcAddr + 0x40;
                while (true)
                {
                    var nextField = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(field + 0x28);
                    if (nextField == field) break;
                    field = nextField;
                    if (field == 0) break;
                    var fieldName = GetFullName(field);
                    var f = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(field + 0x70);
                    var fType = GetName(SotCore.Instance.Memory.ReadProcessMemory<Int32>(f + 0x18));
                    var fName = GetName(SotCore.Instance.Memory.ReadProcessMemory<Int32>(field + 0x18));
                    var offset = SotCore.Instance.Memory.ReadProcessMemory<Int32>(field + 0x44);
                    if (fType == "None" && String.IsNullOrEmpty(fName)) break;
                    var fieldObj = new UEObject(field);
                    if (fieldObj.ClassName == "Class CoreUObject.Function")
                    {
                        sb.AppendLine("  " + fType + " " + fName + "(" + fieldObj.ClassName + ") : 0x" + offset.ToString("X"));
                        var funcParams = field + 0x20u;
                        while ((funcParams = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(funcParams + 0x28u)) > 0)
                        {
                            sb.AppendLine("      " + GetFullName(funcParams));
                        }
                    }
                    else
                        sb.AppendLine("  " + fType + " " + fName + "(" + fieldObj.ClassName + "Field Name " + fieldName + ") : 0x" + offset.ToString("X"));
                }
                pcAddr = SotCore.Instance.Memory.ReadProcessMemory<UInt64>(pcAddr + 0x40);
                if (pcAddr == 0) break;
            }
            return sb.ToString();
        }
    }
}
