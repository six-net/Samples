var ajaxPro = 0;
var EZNEW_TreeLoadingIcoUrl = '/script/ztree/img/loading.gif';//树控件加载数据的时候图标地址

//获取元素高度，若高度值不合法返回0
function GetElementHeight(selector) {
    if (!selector) {
        return 0;
    }
    var height = $(selector).outerHeight();
    if (isNaN(height)) {
        height = 0;
    }
    return height;
}

//获取元素宽度，若宽度值不合法返回0
function GetElementWeight(selector) {
    if (!selector) {
        return 0;
    }
    var width = $(selector).outerWidth();
    if (isNaN(width)) {
        width = 0;
    }
    return width;
}


//数据请求
function AjaxDataRequest(method, url, where, callback) {
    if (method.toUpperCase() == "GET") {
        $.get(url, where, function (res) {
            if (callback) {
                callback(res);
            }
        });
    } else {
        $.post(url, where, function (res) {
            if (callback) {
                callback(res);
            }
        });
    }
}

//显示Ajax等待框
function ShowLoading() {
    try {
        layer.load(2);
    } catch (e) {

    }
}

//关闭Ajax等待框
function HideLoading() {
    try {
        layer.closeAll('loading');
    } catch (e) {

    }
}

//Ajax全局设置
$.ajaxSetup({
    global: false,
    beforeSend: function (xhr, o) {
        AjaxBeforeSend(xhr, this);
    },
    complete: function () {
        AjaxComplete();
    }
});

//Ajax发送前事件
function AjaxBeforeSend(xhr, options) {
    ajaxPro++;
    xhr.setRequestHeader("Http-Request-Type", "ajax-request");
    if (options && options.data && (options.data.NotShowLoading || options.data.indexOf("NotShowLoading=true") >= 0)) {

    } else {
        window.ShowLoading();
    }
}

//Ajax完成回调
function AjaxComplete() {
    ajaxPro--;
    if (ajaxPro <= 0) {
        window.HideLoading();
    }
}

//成功消息
function SuccessMsg(msg) {
    TipMsg(msg, 1);
};

//失败消息
function ErrorMsg(msg) {
    TipMsg(msg, 2);
};

//结果消息
function ResultMsg(res) {
    if (!res) {
        return;
    }
    TipMsg(res.Message, res.Success ? 1 : 2);
    if (!res.success && res.NeedAuthentication) {
        window.top.RedirectLoginPage();
    }
};

//显示消息
function TipMsg(msg, type) {
    var style = "success";
    switch (type) {
        case 2:
            style = "error";
            break;
    }
    $.message({
        message: msg,
        type: style,
        time: '3000',
        location:'tc'
    });
};

//询问框
function ConfirmMsg(msg, fun) {
    return layer.confirm(msg, {
        btn: ['确定', '取消'], //按钮,
        shade: 0.2
    }, function (index) {
        fun();
        layer.close(index);
    }, function () { });
}

//创建一个指定Class的Div
function GetDivByClass(className, parentElement) {
    return GetElementByClass("div", className, parentElement);
}

//创建一个指定Id的div
function GetDivById(id, parentElement) {
    return GetElementById("div", id, parentElement);
}

//创建一个指定Class的a标签
function GetLinkByClass(href, className, parentElement) {
    var linkElement = GetElementByClass("a", className, parentElement);
    linkElement.href = href;
    return linkElement;
}

//创建一个指定Id的a标签
function GetLinkById(href, id, parentElement) {
    var linkElement = GetElementById("a", id, parentElement);
    linkElement.href = href;
    return linkElement;
}

//创建一个指定Class的Img标签
function GetImgByClass(src, className, parentElement) {
    var imgElement = GetElementByClass("img", className, parentElement);
    imgElement.src = src;
    return imgElement;
}

//创建一个指定Id的img标签
function GetImgById(src, id, parentElement) {
    var imgElement = GetElementById("img", id, parentElement);
    imgElement.src = src;
    return imgElement;
}

//用指定的类名创建一个指定类型的元素对象
function GetElementByClass(tagName, className, parentElement) {
    if (!tagName) {
        return;
    }
    var elementObject = document.createElement(tagName);
    elementObject.className = className;
    if (parentElement) {
        try {
            parentElement.appendChild(elementObject);
        } catch (e) {

        }
    }
    return elementObject;
}

//使用指定的ID创建一个元素对象
function GetElementById(tagName, id, parentElement) {
    if (!tagName) {
        return;
    }
    var elementObject = document.createElement(tagName);
    elementObject.id = id;
    if (parentElement) {
        try {
            parentElement.appendChild(elementObject);
        } catch (e) {

        }
    }
    return elementObject;
}

//深度拷贝
function DeepClone(data) {
    if (!data) {
        return data;
    }
    return JSON.parse(JSON.stringify(data));
}

//时间格式转换
Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,                 //月份 
        "d+": this.getDate(),                    //日 
        "h+": this.getHours(),                   //小时 
        "m+": this.getMinutes(),                 //分 
        "s+": this.getSeconds(),                 //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds()             //毫秒 
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}
