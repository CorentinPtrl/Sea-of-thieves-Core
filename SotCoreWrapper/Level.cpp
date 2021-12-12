#include "pch.h"
#include "Level.h"
#include <codecvt>
#include <utility>
#include <locale>
#include <vector>
using namespace System;

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

std::vector<Core::Level::UE4Actor> Core::Level::getActors()
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
	std::vector<Core::Level::UE4Actor> actors;

	for (int i = 0; i < worldActors.Length(); ++i)
	{
		auto objectActor = *reinterpret_cast<AActor*>(&worldActors[i]);
		auto objectID = objectActor.GetID();
		auto pos = objectActor.GetRootComponent().GetPosition();
		auto objectName = getNameFromIDmem(objectID);
		Core::Level::Vector ActorPos;
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