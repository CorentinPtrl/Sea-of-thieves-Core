#include "pch.h"
#include "LevelWrapper.h"
namespace SoT
{
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

    SotLevel::SotLevel()
        : ManagedObject(new Core::Level())
    {
        Console::WriteLine("Creating a new Entity-wrapper object!");
    }
    array<UE4ActorWrapper^>^ SotLevel::getActors()
    {
        Console::WriteLine("The Move method from the Wrapper was called!");
        auto actors = m_Instance->getActors();
        array<UE4ActorWrapper^>^ list = gcnew array< UE4ActorWrapper^ >(actors.size());

        for (int i = 0; i < actors.size(); i++)
        {
            list[i] = gcnew UE4ActorWrapper;
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