#include "pch.h"
#include "SOTStuff.h"
#include "SotCoreWrapper.h"
namespace SoT
{  

    SotCore::SotCore()
        : ManagedObject(new Core::SotCore())
    {
    }

    bool SotCore::Prepare(System::Boolean^ IsSteam)
    {
        if (IsSteam)
        {
            return m_Instance->Prepare(true);
        }
        else
        {
            return m_Instance->Prepare(false);
        }
    }

    UE4Actor^ SotCore::GetLocalPlayer()
    {
        return m_Instance->ActorToManaged(-1, m_Instance->getLocalPlayer());
    }

    array<UE4Actor^>^ SotCore::GetActors()
    {
        auto actors = m_Instance->getActors();
        array<UE4Actor^>^ list = gcnew array< UE4Actor^ >(actors.size());

        for (int i = 0; i < actors.size(); i++)
        {
            list[i] = m_Instance->ActorToManaged(i, actors[i]);
        }
        return list;
    }
}