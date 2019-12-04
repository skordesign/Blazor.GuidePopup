using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.GuidePopup
{
    public static class GuidePopupExtension
    {
        public static void AddGuider(this IServiceCollection services)
        {
            services.AddScoped<IGuider, Guider>();
        }
        public static void AddGuider(this IServiceCollection services, Action<GuiderSetting> options)
        {
            services.AddScoped<IGuider>(f=> new Guider(f.GetService<IJSRuntime>()));
        }
    }
}
