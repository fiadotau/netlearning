using PerformanceCounterHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Infrastructure
{
	[PerformanceCounterCategory("MvcMusicStore", System.Diagnostics.PerformanceCounterCategoryType.MultiInstance, "MvcMusicStore")]
	public enum Counters
	{
		[PerformanceCounter("Go to home count", "Go to home", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
		GoToHome,
		[PerformanceCounter("Success Login", "Login", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
		LogIn,
		[PerformanceCounter("Success Log off", "Log off", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
		LogOff
	}
}