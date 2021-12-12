#pragma once
#include "pch.h"
#include <dwmapi.h>
#include <iostream>
#include <sstream>
#include <thread>
#include <chrono>
using namespace System;

namespace Core
{
	extern int rWidth;
	extern int rHeight;

	extern int PMrWidth;
	extern int PMrHeight;

	extern HWND hWnd;
	extern HWND tWnd;
	extern HWND hMsg;

	extern HANDLE hGame;
	extern DWORD dwBase;

	extern bool MenuShown;
	extern int FramesPerSecond;

	extern DWORD baseThreadID;

	extern DWORD baseProcessID;
	extern uintptr_t memoryProcessID;
	extern HANDLE memoryProcess;

	extern uintptr_t baseModule;
	extern uintptr_t baseModuleSize;
	extern uintptr_t UWorld;
	extern uintptr_t GNames;
	extern uintptr_t GObjects;
};

namespace ThreadsManager
{
	extern DWORD hOverlayRefreshID;
	extern DWORD hOverlayPositionID;
	extern DWORD hDataShareID;
	extern DWORD hDebugDataShareID;
};
