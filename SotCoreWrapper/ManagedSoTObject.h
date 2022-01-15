//ManagedSoTObject.h
#pragma once
using namespace System;
namespace SoT {

    template<class T>
    public ref class ManagedSoTObject
    {
    protected:
        T* m_Instance;
    public:
        ManagedSoTObject(T* instance)
            : m_Instance(instance)
        {
        }
        
        T* GetInstance()
        {
            return m_Instance;
        }

        void UpdateInstance(T* newInstance)
        {
            if ((*(uintptr_t*)m_Instance) != *(uintptr_t*)newInstance)
            {
                m_Instance = newInstance;
            }
        }
    };
}