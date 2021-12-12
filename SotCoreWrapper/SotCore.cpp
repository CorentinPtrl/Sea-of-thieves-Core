#include "pch.h"
#include "SotCore.h"
#include "MemoryManager.h"
using namespace System;

Core::SotCore::SotCore()
{

}

bool Core::SotCore::Prepare()
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