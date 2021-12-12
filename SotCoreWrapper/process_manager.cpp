#include "pch.h"
#include "process_manager.h"
#include <TlHelp32.h>

cProcessManager* ProcessManager = new cProcessManager();

bool cProcessManager::attachProcess(std::string Process)
{

	while (!Core::memoryProcess)
	{
		HANDLE hPID = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, NULL);
		PROCESSENTRY32 ProcEntry;
		ProcEntry.dwSize = sizeof(ProcEntry);

		do
			if (!strcmp(ProcEntry.szExeFile, Process.c_str()))
			{
				Core::memoryProcessID = ProcEntry.th32ProcessID;
				CloseHandle(hPID);

				return Core::memoryProcess = OpenProcess(PROCESS_ALL_ACCESS, FALSE, Core::memoryProcessID);

			}
		while (Process32Next(hPID, &ProcEntry));
	}

	return true;
}

bool cProcessManager::setWindow(std::string Window)
{
	targetWindow = FindWindow(NULL, Window.c_str());

	if (!targetWindow)
		return false;

	return true;
}