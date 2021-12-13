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

    System::String^ StdStringToUTF16(std::string s)
    {

        cli::array<System::Byte>^ a = gcnew cli::array<System::Byte>(s.length());
        int i = s.length();
        while (i-- > 0)
        {
            a[i] = s[i];
        }

        return System::Text::Encoding::UTF8->GetString(a);
    }

    UE4Actor^ SotCore::GetLocalPlayer()
    {
        auto actor = m_Instance->GetLocalPlayer();
        UE4Actor^ actorWrapper = gcnew UE4Actor;
        VectorUE4^ pos = gcnew VectorUE4;
        pos->x = actor.pos.x;
        pos->y = actor.pos.y;
        pos->z = actor.pos.z;
        actorWrapper->pos = pos;
        actorWrapper->name = StdStringToUTF16(actor.name);
        return actorWrapper;
    }


    array<UE4Actor^>^ SotCore::GetActors()
    {
        auto actors = m_Instance->getActors();
        array<UE4Actor^>^ list = gcnew array< UE4Actor^ >(actors.size());

        for (int i = 0; i < actors.size(); i++)
        {
            list[i] = gcnew UE4Actor;
            VectorUE4^ pos = gcnew VectorUE4;
            pos->x = actors[i].pos.x;
            pos->y = actors[i].pos.y;
            pos->z = actors[i].pos.z;
            list[i]->pos = pos;
            list[i]->name = StdStringToUTF16(actors[i].name);


        }
        return list;
    }
}