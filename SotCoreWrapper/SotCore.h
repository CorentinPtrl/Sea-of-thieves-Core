#pragma once
#include <Process.h>
#include <dwmapi.h>
#include <iostream>
#include <sstream>
#include <thread>
#include <chrono>
#include "process_manager.h"


namespace Core
{
	class SotCore
	{
	public:
		SotCore();

		bool Prepare();

	};
}
