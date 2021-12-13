#include "pch.h"
#include "SotCore.h"
#include "MemoryManager.h"
#include <codecvt>
#include <utility>
#include <locale>
#include <vector>
#include "SOTStuff.h"
using namespace System;
namespace Core
{
	SotCore::SotCore()
	{
	}

	bool SotCore::Prepare()
	{
		if (!ProcessManager->setWindow("Sea of Thieves"))
		{
			MessageBoxA(NULL, "Failed To Find The Window", "Failed To Find The Window", MB_OK);
			return false;
		}

		if (!ProcessManager->attachProcess("SoTGame.exe"))
		{
			MessageBoxA(NULL, "Failed To Find The Process", "Failed To Find The Process", MB_OK);
			return false;
		}

		if (!Core::baseModule)
		{
			Core::baseModule = MemoryManager->baseModuleAddress("SoTGame.exe");
			Core::baseModuleSize = MemoryManager->baseModuleSize("SoTGame.exe");
		}

		uintptr_t address = 0;

		if (!Core::UWorld)
		{
			address = MemoryManager->FindSignature(Core::baseModule, Core::baseModuleSize,
				(BYTE*)("\x48\x8B\x05\x00\x00\x00\x00\x48\x8B\x88\x00\x00\x00\x00\x48\x85\xC9\x74\x06\x48\x8B\x49\x70"),
				(char*)"xxx????xxx????xxxxxxxxx");

			auto uworldoffset = MemoryManager->Read<int32_t>(address + 3);
			Core::UWorld = address + uworldoffset + 7;
		}

		if (!Core::GNames)
		{
			address = MemoryManager->FindSignature(Core::baseModule, Core::baseModuleSize,
				(BYTE*)"\x48\x8B\x1D\x00\x00\x00\x00\x48\x85\x00\x75\x3A", (char*)"xxx????xx?xx");
			auto gnamesoffset = MemoryManager->Read<int32_t>(address + 3);
			Core::GNames = MemoryManager->Read<uintptr_t>(address + gnamesoffset + 7);
		}

		if (!Core::GObjects)
		{
			address = MemoryManager->FindSignature(Core::baseModule, Core::baseModuleSize,
				(BYTE*)"\x48\x8B\x15\x00\x00\x00\x00\x3B\x42\x1C", (char*)"xxx????xxx");
			auto gobjectsoffset = MemoryManager->Read<int32_t>(address + 3);
			auto offset = gobjectsoffset + 7;
			Core::GObjects = MemoryManager->Read<uintptr_t>(address + gobjectsoffset + 7);
		}

		if (!Core::baseThreadID)
			Core::baseThreadID = GetCurrentThreadId();

		///-> Get all access
		GetWindowThreadProcessId(Core::tWnd, &Core::baseProcessID);
		Core::hGame = OpenProcess(PROCESS_ALL_ACCESS, FALSE, Core::baseProcessID);
		return true;
	}

	std::string wstringToString(std::wstring wstring)
	{
		using convert_type = std::codecvt_utf8<wchar_t>;
		std::wstring_convert<convert_type, wchar_t> converter;
		return converter.to_bytes(wstring);
	}

	std::string getNameFromIDmem(int ID)
	{
		try
		{
			DWORD_PTR fNamePtr = MemoryManager->Read<uintptr_t>(Core::GNames + int(ID / 0x4000) * 0x8);
			DWORD_PTR fName = MemoryManager->Read<uintptr_t>(fNamePtr + 0x8 * int(ID % 0x4000));
			return MemoryManager->Read<text>(fName + 0x10).word;
		}
		catch (int e)
		{
			return std::string("");
		}
	}

	SotCore::UE4Actor SotCore::GetLocalPlayer()
	{
		auto gameWorld = MemoryManager->Read<cUWorld>(MemoryManager->Read<uintptr_t>(Core::UWorld));
		auto LP = gameWorld.GetGameInstance().GetLocalPlayer();
		auto LPController = LP.GetPlayerController();
		auto pos = LPController.GetActor().GetRootComponent().GetPosition();
		auto objectName = getNameFromIDmem(LPController.GetActor().GetID());
		Core::SotCore::Vector ActorPos;
		ActorPos.x = pos.x;
		ActorPos.y = pos.y;
		ActorPos.z = pos.z;
		UE4Actor actor;
		actor.name = objectName;
		actor.pos = ActorPos;
		return actor;
	}

	std::vector<SotCore::UE4Actor> SotCore::getActors()
	{
		auto gameWorld = MemoryManager->Read<cUWorld>(MemoryManager->Read<uintptr_t>(Core::UWorld));
		auto LP = gameWorld.GetGameInstance().GetLocalPlayer();
		auto LPController = LP.GetPlayerController();
		auto LPCameraManager = LPController.GetCameraManager();

		SOT->localPlayer.name = wstringToString(LPController.GetActor().GetPlayerState().GetName());

		SOT->localPlayer.position = LPController.GetActor().GetRootComponent().GetPosition();

		SOT->localCamera.fov = LPCameraManager.GetCameraFOV();
		SOT->localCamera.angles = LPCameraManager.GetCameraRotation();
		SOT->localCamera.position = LPCameraManager.GetCameraPosition();

		auto worldLevel = gameWorld.GetLevel();
		TArray<Chunk*> worldActors = worldLevel.GetActors();
		std::vector<SotCore::UE4Actor> actors;

		for (int i = 0; i < worldActors.Length(); ++i)
		{
			auto objectActor = *reinterpret_cast<AActor*>(&worldActors[i]);
			auto objectID = objectActor.GetID();
			auto pos = objectActor.GetRootComponent().GetPosition();
			auto objectName = getNameFromIDmem(objectID);
			Core::SotCore::Vector ActorPos;
			ActorPos.x = pos.x;
			ActorPos.y = pos.y;
			ActorPos.z = pos.z;
			UE4Actor actor;
			actor.name = objectName;
			actor.pos = ActorPos;
			actors.push_back(actor);
		}
		return actors;
	}
}