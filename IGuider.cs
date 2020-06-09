using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polarizelab.Blazor.GuidePopup
{
    public interface IGuider
    {
        event EventHandler OnClosed;
        ValueTask<object> Show (ElementReference element, string content, GuidePosition guidePosition = GuidePosition.Right);
        ValueTask<object> Show (string elementId, string content, GuidePosition guidePosition = GuidePosition.Right);
        ValueTask<object> Show (double x, double y, string content, GuidePosition guidePosition = GuidePosition.Right);
        void InvokeClosed();
        IGuider Make(ElementReference element, string content, GuidePosition guidePosition = GuidePosition.Right);
        IGuider Make(string elementId, string content, GuidePosition guidePosition = GuidePosition.Right);
        IGuider Make(double x, double y, string content, GuidePosition guidePosition = GuidePosition.Right);
        Task ShowAll();
        Task Start();
    }
    public enum GuidePosition
    {
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left
    }
}
