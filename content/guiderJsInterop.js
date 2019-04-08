// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API
window.guiderJsFunctions = {
    showWithElementId: function (setting, id, elementId, content, position, dotnetHelper) {
        let element = document.getElementById(elementId);
        if (!element)
            return;
        guiderJsFunctions.show(setting, id, element.offsetLeft, element.offsetTop, element.offsetWidth, element.offsetHeight, content, position, dotnetHelper);
    },
    showWithElementRef: function (setting, id, element, content, position) {
        guiderJsFunctions.show(setting, id, element.offsetLeft, element.offsetTop, element.offsetWidth, element.offsetHeight, content, position, dotnetHelper);
    },
    showWithXY: function (setting, id, x, y, content, position, dotnetHelper) {
        guiderJsFunctions.show(setting, id, x, y, 0, 0, content, position, dotnetHelper);
    },
    show: function (setting, id, x, y, w, h, content, position, dotnetHelper) {
        let overlay = document.createElement('div');
        overlay.setAttribute('id', id);
        overlay.setAttribute('class', setting.overlayClassName);
        overlay.addEventListener('click', function () {
            guiderJsFunctions.remove(id);
            dotnetHelper.invokeMethodAsync("InvokeClosed");
        });
        let contentEle = document.createElement('div');
        contentEle.setAttribute('class', setting.popupClassName);
        contentEle.appendChild(document.createTextNode(content));
        overlay.appendChild(contentEle);
        let body = document.getElementsByTagName('body')[0];

        let highlight = document.createElement('div');
        highlight.setAttribute('class', setting.roundedClassName);
        overlay.appendChild(highlight);
        body.appendChild(overlay);
        // Compute size
        let positionXY = getPosition(x, y, w, h, contentEle, position);
        contentEle.style.left = positionXY.X + 'px';
        contentEle.style.top = positionXY.Y + 'px';

        highlight.style.left = x - 4 + 'px';
        highlight.style.top = y - 4 + 'px';
        highlight.style.width = w + 8 + 'px';
        highlight.style.height = h + 8 + 'px';
        function getPosition(x, y, w, h, popup, position) {
            switch (position) {
                // TL
                case 0: return {
                    X: x - popup.offsetWidth - 8,
                    Y: y - popup.offsetHeight - 8
                };
                // T
                case 1: return {
                    X: x + w / 2 - popup.offsetWidth / 2,
                    Y: y - popup.offsetHeight - 8
                };
                // TR
                case 2: return {
                    X: x + w + 8,
                    Y: y - popup.offsetHeight - 8
                };
                // R
                case 3: return {
                    X: x + w + 8,
                    Y: y + h / 2 - popup.offsetHeight / 2
                };
                // BR
                case 4: return {
                    X: x + w + 8,
                    Y: y + h + 8
                };
                // B
                case 5: return {
                    X: x + w / 2 - popup.offsetWidth / 2,
                    Y: y + h + 8
                };
                // BL
                case 6: return {
                    X: x - popup.offsetWidth - 8,
                    Y: y + h + 8
                };
                // L
                default: return {
                    X: x - popup.offsetWidth - 8,
                    Y: y + h / 2 - popup.offsetHeight / 2
                };
            }
        }
    },
    remove: function (id) {
        let guider = document.getElementById(id);
        if (guider)
            guider.parentElement.removeChild(guider);
    }
};
