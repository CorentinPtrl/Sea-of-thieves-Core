#pragma once
#include "globals.h"

#include <string>

class cProcessManager
{
public:
	HWND myWindow;
public:
	bool attachProcess(std::string Process);
	bool setWindow(std::string Window);
private:
	HWND targetWindow;
	HANDLE targetProcess;
};

extern cProcessManager* ProcessManager;