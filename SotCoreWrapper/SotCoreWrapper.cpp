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
        return gcnew UE4Actor(m_Instance->getNameFromIDmem(m_Instance->getLocalPlayer().GetID()), -1);
    }

    CameraManager^ SotCore::GetCameraManager()
    {
        if (!cameraManager)
            cameraManager = gcnew CameraManager(m_Instance->GetCameraManager());
        cameraManager->UpdateInstance(&m_Instance->GetCameraManager());
        return cameraManager;
    }

    array<UE4Actor^>^ SotCore::GetActors()
    {
        auto actors = m_Instance->getActors();
        array<UE4Actor^>^ list = gcnew array< UE4Actor^ >(actors.size());

        for (int i = 0; i < actors.size(); i++)
        {
            SoT::UE4Actor^ act = gcnew SoT::UE4Actor(m_Instance->getNameFromIDmem(actors[i].GetID()), i);
            list[i] = act;
        }
        return list;
    }
}