#include "pch.h"

#include "SotCoreWrapper.h"
namespace CLI
{
    SotCore::SotCore()
        : ManagedObject(new Core::SotCore())
    {
        Console::WriteLine("Creating a new Entity-wrapper object!");
    }
    bool SotCore::Prepare()
    {
        Console::WriteLine("The Move method from the Wrapper was called!");
        return m_Instance->Prepare();
    }
}