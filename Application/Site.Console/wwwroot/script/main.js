var pageTables = {};

//分割面板默认配置项
var verticalSplitDefaultOptions = {
    disabled: false,
    height: '100%',
    orientation: 'vertical',
    //panels:[{ size: '30%',collapsible: false}],
    resizable: true,
    splitBarSize: 5,
    showSplitBar: true,
    theme: 'metro',
    width: '100%'
};
var horizontalSplitDefaultOptions = {
    disabled: false,
    height: '100%',
    orientation: 'horizontal',
    panels: [{ size: '30%' }],
    resizable: true,
    splitBarSize: 5,
    showSplitBar: true,
    theme: 'metro',
    width: '100%'
};
var splitCollection = new Array();

$(function () {
    Layout();
    InitPageSplit();
    InitContentPanel();
    BindEvent();
});

//绑定页面事件
function BindEvent() {
    window.onresize = function () {
        setTimeout(function () {
            Layout();
            InitContentPanel();
            //ResizeAllTable();
        }, 0);
    }

    //遮罩事件
    $("body").on('click', '.body_shade', function () {
        CloseRightSlide($('.right_slide_container'));
    });
}

//页面布局
function Layout() {
    var winHeight = $(window).height();
    var headHeight = $("#page-head").outerHeight();
    var footHeight = $("#page-foot").outerHeight();
    if (isNaN(headHeight)) {
        headHeight = 0;
    }
    if (isNaN(footHeight)) {
        footHeight = 0;
    }
    var nowBodyHeight = $("#page-body").height();
    var newBodyHeight = winHeight - headHeight - footHeight;
    $("#page-body").height(newBodyHeight);
    $("#page-foot").removeClass('hidev');
}

//初始化页面分割
function InitPageSplit() {
    var splitArray = $('.split-container');
    splitArray.each(function (i, e) {
        var container = $(e);
        var containerOptions = container.attr('split-options');
        if ($.trim(containerOptions) == "") {
            containerOptions = "{}";
        }
        InitSplit(container, JSON.parse(containerOptions));
    });
}

//初始化一个面板
function InitSplit(ele, options) {
    if (!ele) {
        return;
    }
    if (options.orientation && options.orientation == 'horizontal') {
        options = $.extend({}, horizontalSplitDefaultOptions, options);
    }
    else {
        options = $.extend({}, verticalSplitDefaultOptions, options);
    }
    $(ele).eznewSplitter(options);
    $(ele).on('resize', function (event) {
        InitContentPanel();
        setTimeout(ResizeAllTable, 0);
        event.stopPropagation();
    });
}

//面板计算
function InitContentPanel() {
    var contentArray = $('.content-panel');
    contentArray.each(function (i, e) {
        var container = $(e);
        if (container.is(":hidden")) {
            return;
        }
        var containerHeight = container.height();
        var headHeight = container.children('.content-panel-head').outerHeight();
        var footHeight = container.children('.content-panel-foot').outerHeight();
        if (isNaN(headHeight)) {
            headHeight = 0;
        }
        if (isNaN(footHeight)) {
            footHeight = 0;
        }
        container.children('.content-panel-body').height(containerHeight - headHeight - footHeight);
    });
}



//打开右侧滑动面板
function OpenRightSlide(ele) {
    var boxEle = $(ele);
    var positionClass = "right-0";
    boxEle.attr('class', 'right_slide_container ' + positionClass);
    if (boxEle.is('[show-shade]')) {
        OpenShade();
    }
};


//关闭右侧滑动面板
function CloseRightSlide(ele) {
    var boxEle = $(ele);
    boxEle.attr('class', 'right_slide_container');
    CloseShade();
};

//打开遮罩
function OpenShade() {
    var shadeEles = $('.body_shade');
    if (shadeEles.length < 1) {
        var newShade = $('<div class="body_shade"></div>')
        $('body').append(newShade);
    }
}

//关闭遮罩
function CloseShade() {
    $('.body_shade').remove();
}

//绑定回车事件
function BindEnterEvent(func) {
    if (!func) {
        return;
    }
    $(window).keydown(function (e) {
        if (e.keyCode == 13) {
            func();
        }
    });
}
