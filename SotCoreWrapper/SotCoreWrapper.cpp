#include "pch.h"
#include "SotCoreWrapper.h"
namespace SoT
{  

    SotCore::SotCore()
        : ManagedObject(new Core::SotCore())
    {
    }
    bool SotCore::Prepare()
    {
        return m_Instance->Prepare();
    }

    UE4Actor^ SotCore::GetLocalPlayer()
    {
        auto actor = m_Instance->GetLocalPlayer();
        return actor.ActorToManagedActor();
    }


    array<UE4Actor^>^ SotCore::GetActors()
    {
        auto actors = m_Instance->getActors();
        array<UE4Actor^>^ list = gcnew array< UE4Actor^ >(actors.size());

        for (int i = 0; i < actors.size(); i++)
        {
            list[i] = actors[i].ActorToManagedActor();
        }
        return list;
    }
}