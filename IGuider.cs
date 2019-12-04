using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.GuidePopup
{
    public interface IGuider
    {
        event EventHandler OnClosed;
        Task Show(ElementRef element, string content, GuidePosition guidePosition = GuidePosition.Right);
        Task Show(string elementId, string content, GuidePosition guidePosition = GuidePosition.Right);
        Task Show(double x, double y, string content, GuidePosition guidePosition = GuidePosition.Right);
        void InvokeClosed();
        IGuider Make(ElementRef element, string content, GuidePosition guidePosition = GuidePosition.Right);
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
