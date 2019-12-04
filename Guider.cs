using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.GuidePopup
{
    public class Guider : IGuider
    {
        private Queue<GuideStep> GuideLines { get; set; }
        private GuiderSetting Setting;
        private string Id { get; } = Guid.NewGuid().ToString();
        private readonly IJSRuntime _jSRuntime;
        public Guider(IJSRuntime jSRuntime)
        {
            GuideLines = new Queue<GuideStep>();
            Setting = new GuiderSetting();
            _jSRuntime = jSRuntime;
        }
        public Guider(IJSRuntime jSRuntime, Action<GuiderSetting> options) : this(jSRuntime)
        {
            options(Setting);
        }
        public event EventHandler OnClosed;

        public ValueTask<object> Show(ElementReference element, string content, GuidePosition guidePosition = GuidePosition.Right)
        {
            return _jSRuntime.InvokeAsync<object>("guiderJsFunctions.showWithElementRef", Setting, Id, element, content, guidePosition, DotNetObjectReference.Create(this));
        }

        public ValueTask<object> Show (string elementId, string content, GuidePosition guidePosition = GuidePosition.Right)
        {
            return _jSRuntime.InvokeAsync<object>("guiderJsFunctions.showWithElementId", Setting, Id, elementId, content, guidePosition, DotNetObjectReference.Create(this));
        }

        public ValueTask<object> Show (double x, double y, string content, GuidePosition guidePosition = GuidePosition.Right)
        {
            return _jSRuntime.InvokeAsync<object>("guiderJsFunctions.showWithXY", Setting, Id, x, y, content, guidePosition, DotNetObjectReference.Create (this));
        }
        [JSInvokable]
        public void InvokeClosed()
        {
            OnClosed?.Invoke(this, null);
        }

        private IGuider Make(GuideStep guideStep)
        {
            GuideLines.Enqueue(guideStep);
            return this;
        }

        public async Task Start()
        {
            while (GuideLines.Count != 0)
            {
                bool closed = false;
                GuideStep step = GuideLines.Dequeue();
                this.OnClosed += (s, e) =>
                {
                    closed = true;
                };
                await ShowStep(step);
                while (!closed)
                    await Task.Delay(100);
            }
        }

        private ValueTask<object> ShowStep(GuideStep guideStep)
        {
            if (guideStep.GuideType == GuideType.Id)
                return Show(guideStep.ElementId, guideStep.Content, guideStep.GuidePosition);
            if (guideStep.GuideType == GuideType.Ref)
                return Show(guideStep.ElementRef, guideStep.Content, guideStep.GuidePosition);
            return Show(guideStep.X, guideStep.Y, guideStep.Content, guideStep.GuidePosition);
        }

        public IGuider Make(ElementReference element, string content, GuidePosition guidePosition = GuidePosition.Right)
        {
            return Make(new GuideStep(element, content, guidePosition));
        }

        public IGuider Make(string elementId, string content, GuidePosition guidePosition = GuidePosition.Right)
        {
            return Make(new GuideStep(elementId, content, guidePosition));
        }

        public IGuider Make(double x, double y, string content, GuidePosition guidePosition = GuidePosition.Right)
        {
            return Make(new GuideStep(x, y, content, guidePosition));
        }

        public async Task ShowAll()
        {
            if (GuideLines.Count == 0)
                return;
            await _jSRuntime.InvokeAsync<object>("guiderJsFunctions.showMany", Setting, Id, GuideLines.ToArray(), DotNetObjectReference.Create (this));
        }
    }
    public class GuideStep
    {
        private GuideStep(string content, GuidePosition guidePosition)
        {
            this.Content = content;
            this.GuidePosition = guidePosition;
        }
        public GuideStep(string elementId, string content, GuidePosition guidePosition = GuidePosition.Right) : this(content, guidePosition)
        {
            this.ElementId = elementId;
            this.GuideType = GuideType.Id;
        }
        public GuideStep(ElementReference elementRef, string content, GuidePosition guidePosition = GuidePosition.Right) : this(content, guidePosition)
        {
            this.ElementRef = elementRef;
            this.GuideType = GuideType.Ref;
        }
        public GuideStep(double x, double y, string content, GuidePosition guidePosition = GuidePosition.Right) : this(content, guidePosition)
        {
            this.X = x;
            this.Y = y;
            this.GuideType = GuideType.Coordination;
        }
        public GuideType GuideType { get; set; }
        public string Content { get; set; }
        public GuidePosition GuidePosition { get; set; }
        public string ElementId { get; set; }
        public ElementReference ElementRef { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
    public enum GuideType
    {
        Ref,
        Id,
        Coordination
    }
}
