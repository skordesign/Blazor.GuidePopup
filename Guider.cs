using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SkorBlazor.GuidePopup
{
    public class Guider : IGuider
    {
        private GuiderSetting Setting;
        private string Id { get; } = Guid.NewGuid().ToString();
        private readonly IJSRuntime _jSRuntime;
        public Guider(IJSRuntime jSRuntime)
        {
            Setting = new GuiderSetting();
            _jSRuntime = jSRuntime;
        }
        public Guider(IJSRuntime jSRuntime, Action<GuiderSetting> options) : this(jSRuntime)
        {
            options(Setting);
        }
        public event EventHandler OnClosed;

        public Task Show(ElementRef element, string content, GuidePosition guidePosition = GuidePosition.Right)
        {
            return _jSRuntime.InvokeAsync<object>("guiderJsFunctions.showWithElementRef", Setting, Id, element, content, guidePosition, new DotNetObjectRef(this));
        }

        public Task Show(string elementId, string content, GuidePosition guidePosition = GuidePosition.Right)
        {
            return _jSRuntime.InvokeAsync<object>("guiderJsFunctions.showWithElementId", Setting, Id, elementId, content, guidePosition, new DotNetObjectRef(this));
        }

        public Task Show(double x, double y, string content, GuidePosition guidePosition = GuidePosition.Right)
        {
            return _jSRuntime.InvokeAsync<object>("guiderJsFunctions.showWithXY", Setting, Id, x, y, content, guidePosition, new DotNetObjectRef(this));
        }
        [JSInvokable]
        public void InvokeClosed()
        {
            OnClosed?.Invoke(this, null);
        }
    }
}
