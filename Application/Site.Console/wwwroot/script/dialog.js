layer.config({
    //skin: 'layui-layer-molv'
})

var EZNEW_DialogPages = {};
var EZNEW_DialogTopContainer = false;

//打开一个新页面
function OpenDialogPage(options) {
    var container = GetDialogContainer();
    var defaultOps = {
        title: '新页面',
        area: ['800px', '500px'],
        shade: 0.2,
        shadeClose: false,
        time: 0,
        resize: true,
        type: 2,
        opener: window
    };
    defaultOps = $.extend(defaultOps, options);
    var dialogPage = container.layer.open(defaultOps);
    container.EZNEW_DialogPages[dialogPage.index] = dialogPage;
}

//获取对话框容器页面
function GetDialogContainer() {
    if (window.self === window.top) {
        return window;
    }
    var nowWin = window;
    var parentWin = nowWin.parent;
    while (parentWin) {
        if (parentWin === top) {
            return EZNEW_DialogTopContainer == true ? top : nowWin;
        }
        nowWin = parentWin;
        parentWin = nowWin.parent;
    }
    return nowWin;
}

//关闭当前弹出页
function CloseCurrentDialogPage() {
    var pageContainer = GetDialogContainer();
    var index = pageContainer.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
    pageContainer.layer.close(index); //再执行关闭   
}

//获取打开对话框的对象
function DialogOpener() {
    var pageContainer = GetDialogContainer();
    var index = pageContainer.layer.getFrameIndex(window.name);
    return pageContainer.EZNEW_DialogPages[index].config.opener;
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
        time: '3000'
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