/*
 Validate charecter v1.0.0
 (c) iSabaya Co Ltd.
 License: Free
*/
function FillterNumber(e) {
    var code = ((e.keyCode) ? e.keyCode : e.which)

    // Exception Key
    if (e.ctrlKey) { return true; }
    if (code == 8 || code == 9) { return true; }

    if (48 <= code && code <= 57) { return true; }
    else {
        alert("กรุณาระบุเป็นตัวเลข");
        return false;
    }
}
function FillterNumberOnPaste(e) {
    var pastedText = undefined;
    // IE
    if (window.clipboardData && window.clipboardData.getData) {
        pastedText = window.clipboardData.getData('Text');
    }
    else if (e.clipboardData && e.clipboardData.getData) {
        pastedText = e.clipboardData.getData('text/plain');
    }

    if (pastedText) {
        var isPass = true;
        for (var i = 0; i < pastedText.length; i++) {
            if (48 <= pastedText.charCodeAt(i) && pastedText.charCodeAt(i) <= 57) {
                isPass = isPass && true;
            }
            else {
                isPass = isPass && false;
                break;
            }
        }
        if (!isPass) { alert("กรุณาระบุเป็นตัวเลข"); }
        return isPass;
    }

    alert("กรุณาระบุเป็นตัวเลข");
    return false;
}
function FillterAlphabet(e) {
    var code = ((e.keyCode) ? e.keyCode : e.which)

    // Exception Key
    if (e.ctrlKey) { return true; }
    if (code == 8 || code == 9) { return true; }

    if ((65 <= code && code <= 90) || (97 <= code && code <= 122)) { return true; }
    else {
        alert("กรุณาระบุเป็นภาษาอังกฤษ");
        return false;
    }
}
function FillterAlphabetOnPaste(e) {
    var pastedText = undefined;
    // IE
    if (window.clipboardData && window.clipboardData.getData) {
        pastedText = window.clipboardData.getData('Text');
    }
    else if (e.clipboardData && e.clipboardData.getData) {
        pastedText = e.clipboardData.getData('text/plain');
    }

    if (pastedText) {
        var isPass = true;
        for (var i = 0; i < pastedText.length; i++) {
            if ((65 <= pastedText.charCodeAt(i) && pastedText.charCodeAt(i) <= 90)
				|| (97 <= pastedText.charCodeAt(i) && pastedText.charCodeAt(i) <= 122)) {
                isPass = isPass && true;
            }
            else {
                isPass = isPass && false;
                break;
            }
        }
        if (!isPass) { alert("กรุณาระบุเป็นภาษาอังกฤษ"); }
        return isPass;
    }

    alert("กรุณาระบุเป็นภาษาอังกฤษ");
    return false;
}
function FillterThaiAlphabet(e) {
    debugger;
    var code = ((e.keyCode) ? e.keyCode : e.which)

    // Exception Key
    if (e.ctrlKey) { return true; }
    if (code == 8) { return true; }
    if (code == 8 || code == 9) { return true; }

    if ((3585 <= code && code <= 3642) || (3648 <= code && code <= 3663)
		 || 3674 == code || 3675 == code) { return true; }
    else {
        alert("กรุณาระบุเป็นภาษาไทย");
        return false;
    }
}
function FillterThaiAlphabetOnPaste(e) {
    var pastedText = undefined;
    // IE
    if (window.clipboardData && window.clipboardData.getData) {
        pastedText = window.clipboardData.getData('Text');
    }
    else if (e.clipboardData && e.clipboardData.getData) {
        pastedText = e.clipboardData.getData('text/plain');
    }

    if (pastedText) {
        var isPass = true;
        for (var i = 0; i < pastedText.length; i++) {
            if ((3585 <= pastedText.charCodeAt(i) && pastedText.charCodeAt(i) <= 3642)
				|| (3648 <= pastedText.charCodeAt(i) && pastedText.charCodeAt(i) <= 3663)
				|| 3674 == pastedText.charCodeAt(i) || 3675 == pastedText.charCodeAt(i)) {
                isPass = isPass && true;
            }
            else {
                isPass = isPass && false;
                break;
            }
        }

        if (!isPass) { alert("กรุณาระบุเป็นภาษาไทย"); }
        return isPass;
    }

    alert("กรุณาระบุเป็นภาษาไทย");
    return false;
}