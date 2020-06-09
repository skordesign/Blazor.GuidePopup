# Polarizelab.Blazor.GuidePopup
Guidelines popup for Blazor
### Usage
#### 1.Installation
Nuget: [Blazor.GuidePopup](https://www.nuget.org/packages/Polarizelab.Blazor.GuidePopup)
#### 2.Add service
```csharp
builder.Services.AddGuider();
// also use with options
// builder.Services.AddGuider(options => {
// options.PopupClassName = "your-class";
///...
//})
```
and in index.html
```html
    ...
    <link href="_content/Polarizelab.Blazor.GuidePopup/styles.css" rel="stylesheet"/>
    ...

    ...
    <script src="_content/Polarizelab.Blazor.GuidePopup/Guider.js"></script>
    ...
```

#### 3.Inject service and use it
```csharp
@page "/"
@inject Polarizelab.Blazor.GuidePopup.IGuider Guider
<div ref="showPopupNearMe"></div>
@functions{
    ElementReference showPopupNearMe;
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Guider.Show("elementId", "Content", GuidePosition.Right);
        // you can use it with ElementRef or X,Y
        // Guider.Show(showPopupNearMe, "Content", GuidePosition.Bottom);
        // Guider.Show(200, 400, "Content", GuidePosition.TopLeft);
        Guider.OnClosed += OnClosed;
    }
    private void OnClosed(object sender, System.EventArgs args)
    {
        Console.WriteLine("Closed");
    }
}
```
#### Use GuideLines
```csharp
@inject Polarizelab.Blazor.GuidePopup.IGuider Guider
<div ref="showPopupNearMe"></div>
@code{
    ElementReference showPopupNearMe;
    private void ShowGuidline(){
        Guider.Make("elementId", "Content", GuidePosition.Right)
              .Make(showPopupNearMe, "Content", GuidePosition.Bottom)
              .Make(300, 300, "Test 3")
              .Start();
	}
}
```

##### Note: The Guider will create new element and add it to inside of `body` tag, when close it will removed. If you have any idea for this package,feel free create new issue on this repository.
#### Demo
![Demo](Demo.gif)