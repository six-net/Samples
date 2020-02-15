var DataObjectTag = "";

//表单成功回调
function EZNEW_SuccessCallback(res) {
    window.HideLoading();
    if (!res) {
        return;
    }
    if (res.Success) {
        if (res.SuccessRefresh) {
            window.location.href = window.location.href;
        }
        else if (res.SuccessClose) {
            var opener = DialogOpener();
            opener.SuccessMsg(res.Message);
            if (opener.EZNEW_EditCallback) {
                opener.EZNEW_EditCallback({
                    objectTag: DataObjectTag,
                    data: res.Data
                });
            }
            CloseCurrentDialogPage();
        }
        else if (res.NeedAuthentication) {
            window.top.RedirectLoginPage();
        } else {
            SuccessMsg(res.Message);
        }
    } else {
        ErrorMsg(res.Message);
    }
}

//表单失败回调
function EZNEW_FailedCallback(res) {
    window.HideLoading();
    ErrorMsg("数据提交失败");
}

//表单验证成功事件
function ValidSuccess(label, element) {
    var eleVal = $(element).val();
    if (eleVal == "") {
        return;
    }
    var tipEle = $('span[data-valmsg-for="' + $(element).attr('name') + '"]');
    tipEle.removeClass("error").removeClass("prompt").removeClass("ajax").addClass("ok form-validate-msg micon").html("");
}

//表单验证失败事件
function ValidError(label, element) {
    var tipEle = $('span[data-valmsg-for="' + $(element).attr('name') + '"]');
    tipEle.removeClass("ok").removeClass("prompt").removeClass("ajax").addClass("error form-validate-msg micon");
    tipEle.html(label.html());
}

//绑定表单提交事件
function BindFormEvent(options) {
    if (!options || !options.formId) {
        return;
    }
    var submitFun = function () {
        $("#" + options.formId).submit();
    };
    if (options.enter) {
        BindEnterEvent(submitFun);
    }
    if (options.submitBtnId) {
        document.getElementById(options.submitBtnId).addEventListener('click', submitFun, false);
    }
    if (options.resetBtnId) {
        document.getElementById(options.resetBtnId).addEventListener('click', function () {
            document.getElementById(options.formId).reset();
        }, false);
    }
}

//绑定默认表单提交事件
function BindDefaultFormSubmitEnterEvent() {
    BindFormEvent({
        enter: true,
        submitBtnId: "btn_submit",
        resetBtnId: "btn_reset",
        formId: "default-form"
    });
}