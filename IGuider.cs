using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkorBlazor.GuidePopup
{
    public interface IGuider
    {
        event EventHandler OnClosed;
        Task Show(ElementRef element, string content, GuidePosition guidePosition = GuidePosition.Right);
        Task Show(string elementId, string content, GuidePosition guidePosition = GuidePosition.Right);
        Task Show(double x, double y, string content, GuidePosition guidePosition = GuidePosition.Right);
        void InvokeClosed();
        IGuider Make(GuideStep guideStep);
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
