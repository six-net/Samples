var layTable = layui.table;
var pageTables = {};
var searchOptionsDic = new Object();
var defaultTableOptions = {
    height: 'full-0',
    useParentContainer: true,
    method: 'post',
    loading: false,
    data: [],
    height: 'full-0',
    request: {
        pageName: 'page' //页码的参数名称，默认：page
        , limitName: 'pagesize' //每页数据量的参数名，默认：limit
    },
    response: {
        statusName: 'code' //规定数据状态的字段名称，默认：code
        , statusCode: 0 //规定成功的状态码，默认：0
        , msgName: '' //规定状态信息的字段名称，默认：msg
        , countName: 'TotalCount' //规定数据总数的字段名称，默认：count
        , dataName: 'Datas' //规定数据列表的字段名称，默认：data
    }
}

//初始化一个表格
function InitTable(options, initCallback) {
    if (!options) {
        return;
    }
    options = $.extend({}, defaultTableOptions, options);
    if ($.trim(options.id) == "") {
        options.id = options.elem;
    }
    options.elem = "#" + options.elem;
    var newTableObj = layTable.render(options);
    var tableId = newTableObj.config.id;
    pageTables[tableId] = newTableObj;
    if (initCallback) {
        initCallback();
    }
}

//根据页面高度自动计算表格差值
function GetPageDefaulttableHeightGap() {
    var headHeight = $("#page-head").outerHeight();
    var footHeight = $("#page-foot").outerHeight();
    if (isNaN(headHeight)) {
        headHeight = 0;
    }
    if (isNaN(footHeight)) {
        footHeight = 0;
    }
    return headHeight + footHeight;
}

//所有表格重新绘制尺寸
function ResizeAllTable() {
    for (var t in pageTables) {
        pageTables[t].resize();
    }
}

//重新绘制尺寸
function ResizeTable(id) {
    pageTables[id].resize()
}

//重新加载查询数据
function SetTableData(option) {
    if (!option) {
        return;
    }
    if (isNaN(option.page) || option.page < 1) {
        option.page = 1;
    }
    var nowTable = pageTables[option.id];
    nowTable.renderData({
        Datas: option.data
    }, option.page, 0);
}

//清除表格数据
function ClearTableData(id) {
    SetTableData({
        id: id,
        page: 1,
        count: 0,
        data: []
    });
}

//刷新表格配置
function RefreshTableOptions(options) {
    if (!options) {
        return;
    }
    var nowTable = pageTables[options.id];
    nowTable.refreshOptions(options);
}

//获取表格选择值
function GetTableCheckData(id) {
    var checkDatas = layTable.checkStatus(id);
    return checkDatas;
}

//监听表格事件
function ListenTableEvent(tableFilter, func) {
    layTable.on('tool(' + tableFilter + ')', function (obj) {
        func(obj, this);
    });
}

//分页搜索
function PageSearch(options) {
    if (!options) {
        return;
    }
    var defaults = {
        url: '',
        data: { page: 1, pageSize: 20 },
        listEle: "tabe_data",
        pagerEle: "page-foot",
        selectPage: false,
        callback: undefined,
        init: true,
        showPageNum: true,
        showFirstLastBtn: true,
        method: 'POST'
    };
    var pageListId = !options.listEle ? defaults.listEle : options.listEle;
    var searchOptions = searchOptionsDic[pageListId];
    searchOptions = $.extend(true, {}, defaults, options);
    if (searchOptions.init) {
        searchOptions.data.page = 1;
    }
    if (!searchOptions.url || $.trim(searchOptions.url) == "") {
        return;
    }
    searchOptionsDic[pageListId] = searchOptions;
    if (searchOptions.method.toUpperCase() == "GET") {
        $.get(searchOptions.url, searchOptions.data, function (res) {
            SetTableData({
                data: res.Datas,
                id: searchOptions.listEle,
                count: res.TotalCount
            });
            CreatePageControl(res.TotalCount, searchOptions.data.page, searchOptions.data.pageSize, pageListId);
            if (searchOptions.callback) {
                searchOptions.callback(res);
            }
        })
    } else {
        $.post(searchOptions.url, searchOptions.data, function (res) {
            SetTableData({
                data: res.Datas,
                id: searchOptions.listEle,
                count: res.TotalCount
            });
            CreatePageControl(res.TotalCount, searchOptions.data.page, searchOptions.data.pageSize, pageListId);
            if (searchOptions.callback) {
                searchOptions.callback(res);
            }
        })
    }
}

//分页控件点击事件
function PagerBtnSearch(page, pageListId) {
    if (isNaN(page) || page <= 0 || !pageListId || $.trim(pageListId) == "") {
        return;
    }
    var searchOptions = searchOptionsDic[pageListId];
    if (!searchOptions || page == searchOptions.data.page) {
        return;
    }
    var newOptions = $.extend(true, {}, searchOptions, { data: { page: page }, init: false });
    PageSearch(newOptions);
}

//生成分页控件
function CreatePageControl(totalCount, currentPage, pageSize, pageListId) {
    var searchOptions = searchOptionsDic[pageListId];
    $("#" + searchOptions.pagerEle + " .pager-ctrol").remove();
    if (isNaN(totalCount) || totalCount <= 0) {
        $(searchOptions.listEle).parent().addClass("b_b_none");
        return;
    }
    $(searchOptions.listEle).parent().removeClass("b_b_none");
    currentPage = isNaN(currentPage) || currentPage < 1 ? 1 : currentPage;
    pageSize = isNaN(pageSize) || pageSize < 1 ? 1 : pageSize;
    var pageCount = Math.ceil(totalCount / pageSize);
    currentPage = currentPage > pageCount ? pageCount : currentPage;
    var isFirstPage = currentPage == 1;
    var isLastPage = currentPage == pageCount;
    var pagerConClass = searchOptions.selectPage ? "pager-ctrol select_pager" : "pager-ctrol";
    pagerConClass += ' pd-0 mg-0';
    var pagerCon = GetDivByClass(pagerConClass);
    var btnUrl = "javascript:void(0)";

    //首页，上一页
    if (searchOptions.showFirstLastBtn) {
        var firstBtn = GetLinkByClass(btnUrl, "", pagerCon);
        var prevBtn = GetLinkByClass(btnUrl, "", pagerCon);
        firstBtn.innerHTML = "首页";
        prevBtn.innerHTML = "上一页";
        if (isFirstPage) {
            firstBtn.className = "layui-disabled";
            prevBtn.className = "layui-disabled";
        } else {
            firstBtn.onclick = function () {
                PagerBtnSearch(1, pageListId);
            };
            prevBtn.onclick = function () {
                PagerBtnSearch(currentPage - 1, pageListId);
            };
        }
    }

    //页码
    if (searchOptions.showPageNum) {
        if (pageCount <= 10) {
            for (var p = 1; p <= pageCount; p++) {
                var btn = GetLinkByClass(btnUrl, currentPage == p ? "cur" : "", pagerCon);
                btn.innerHTML = p;
                btn.onclick = function () {
                    var npage = parseInt(this.innerHTML);
                    PagerBtnSearch(npage, pageListId);
                };
            }
        } else if (currentPage <= 5) {
            for (var p = 1; p <= 8; p++) {
                var btn = GetLinkByClass(btnUrl, currentPage == p ? "cur" : "", pagerCon);
                btn.innerHTML = p;
                btn.onclick = function () {
                    var npage = parseInt(this.innerHTML);
                    PagerBtnSearch(npage, pageListId);
                };
            }
            var btnPoint = GetLinkByClass(btnUrl, "", pagerCon);
            btnPoint.innerHTML = "...";
            btnPoint.onclick = function () {
                PagerBtnSearch(9, pageListId);
            };
            var lastBtn = GetLinkByClass(btnUrl, "", pagerCon);
            lastBtn.innerHTML = pageCount;
            lastBtn.onclick = function () {
                var npage = parseInt(this.innerHTML);
                PagerBtnSearch(npage, pageListId);
            };
        } else {
            var firstBtn = GetLinkByClass(btnUrl, "", pagerCon);
            firstBtn.innerHTML = "1";
            firstBtn.onclick = function () {
                var npage = parseInt(this.innerHTML);
                PagerBtnSearch(npage, pageListId);
            };
            var btnPoint = GetLinkByClass(btnUrl, "", pagerCon);
            btnPoint.innerHTML = "...";
            btnPoint.onclick = function () {
                PagerBtnSearch(2, pageListId);
            };
            var beginPage = currentPage - 3;
            var endPage = (currentPage + 4) > pageCount ? pageCount : currentPage + 3;
            for (var p = beginPage; p <= endPage; p++) {
                var btn = GetLinkByClass(btnUrl, currentPage == p ? "cur" : "", pagerCon);
                btn.innerHTML = p;
                btn.onclick = function () {
                    var npage = parseInt(this.innerHTML);
                    PagerBtnSearch(npage, pageListId);
                };
            }
            if (endPage < pageCount) {
                var btnPoint2 = GetLinkByClass(btnUrl, "", pagerCon);
                btnPoint2.innerHTML = "...";
                btnPoint2.onclick = function () {
                    PagerBtnSearch(endPage + 1, pageListId);
                };
                var lastBtn = GetLinkByClass(btnUrl, "", pagerCon);
                lastBtn.innerHTML = pageCount;
                lastBtn.onclick = function () {
                    PagerBtnSearch(pageCount, pageListId);
                };
            }
        }
    }

    //尾页
    if (searchOptions.showFirstLastBtn) {
        var nextBtn = GetLinkByClass(btnUrl, "", pagerCon);
        var lastBtn = GetLinkByClass(btnUrl, "", pagerCon);
        nextBtn.innerHTML = "下一页";
        lastBtn.innerHTML = "尾页";
        if (isLastPage) {
            nextBtn.className = "layui-disabled";
            lastBtn.className = "layui-disabled";
        } else {
            lastBtn.onclick = function () {
                PagerBtnSearch(pageCount, pageListId);
            };
            nextBtn.onclick = function () {
                PagerBtnSearch(currentPage + 1, pageListId);
            };
        }
    }

    $(pagerCon).append('共<span class="txt_num">' + totalCount + '</span>条数据<span class="txt-split"> | </span>每页显示');
    var selectCon = GetElementByClass("span", "page_select", null);
    var sizeSelect = GetElementByClass("select", "form-control", selectCon);
    var option10 = GetElementByClass("option", "", sizeSelect);
    option10.innerHTML = "10";
    option10.setAttribute("value", 10);

    var option20 = GetElementByClass("option", "", sizeSelect);
    option20.innerHTML = "20";
    option20.setAttribute("value", 20);

    var option50 = GetElementByClass("option", "", sizeSelect);
    option50.innerHTML = "50";
    option50.setAttribute("value", 50);

    $(pagerCon).append(selectCon);
    $(sizeSelect).val(pageSize);
    sizeSelect.onchange = function () {
        var npageSize = parseInt($(this).val());
        if (isNaN(npageSize) || npageSize <= 0) {
            return;
        }
        var newPageCount = Math.ceil(totalCount / npageSize);
        var cnPage = currentPage > newPageCount ? newPageCount : currentPage;
        var newOptions = $.extend(true, {}, searchOptions, { data: { page: cnPage, pageSize: npageSize } });
        PageSearch(newOptions);
    }
    $("#" + searchOptions.pagerEle).append(pagerCon);
    $(window).resize();
}