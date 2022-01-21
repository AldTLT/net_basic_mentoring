using PerformanceCounterHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gates.WebApp.Infrastructure
{
    [PerformanceCounterCategory("CommonCounters", System.Diagnostics.PerformanceCounterCategoryType.SingleInstance, "")]
    public enum Counters
    {
        [PerformanceCounter("Authentication", "", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
        Authentication,
        [PerformanceCounter("Authorization", "", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
        Authorization,
        [PerformanceCounter("TasksCreation", "", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
        TasksCreation,
        [PerformanceCounter("ProjectCreation", "", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
        ProjectCreation        
    }

    public static class Counter
    {
        public static readonly CounterHelper<Counters> Counters;
        static Counter()
        {
            Counters = PerformanceHelper.CreateCounterHelper<Counters>();
        }
    }
}