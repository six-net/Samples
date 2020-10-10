var EZNEW_DefaultGroupMultSelectOptions = {
    objectTag: "",
    confirmBtnEleId: 'btn_confirmgroupmultselect',
    closeBtnEleId: 'btn_closegroupmultselect',
    groupData: {
        eleId: "groupData",
        loadingIconUrl: EZNEW_TreeLoadingIcoUrl,
        dataUrl: "",
        requestMethod: "post",
        where: null,
        initDataCondition: { levelOne: true },
        groupDataToNodeFunc: null,
        keyField: "Id",
        nameField: "Name",
        parentField: 'Parent'
    },
    treeSetting: {
        view: {
            nameIsHTML: true
        },
        callback: {
            onClick: function (event, treeId, treeNode) {
                nowSelectedNode = treeNode;
                EZNEW_GroupMultSelectLoadTableData();
            },
            beforeExpand: EZNEW_GroupMultSelectTreeBeforeExpand,
        }
    },
    dataTable: {
        eleId: "data_list",
        pagerEleId: "data_pager",
        toolBarId: "data_toobar",
        requestMethod: "post",
        dataUrl: '',
        where: null,
        requestVerify: null,
        dataField: "Id",
        groupField: 'Group',
        searchBtnId: 'search_btn',
        cols: [[]] //数据标题
    },
    selectTable: {
        eleId: "select_list",
        toolBarId: "select_toobar",
    },
    navTab: {
        eleId: "data_tab"
    }
};
var EZNEW_GroupMultSelectOptions = EZNEW_DefaultGroupMultSelectOptions;
var EZNEW_GroupMultSelectedDatas = {};//已选择数据
var EZNEW_NowGroupMultSelectedNode = null;//当前选择节点

//初始化分组选择
function InitGroupMultSelect(options) {
    options = $.extend(true, {}, EZNEW_DefaultGroupMultSelectOptions, options);

    //默认数据转换
    if (!options.groupData.groupDataToNodeFunc) {
        options.groupData.groupDataToNodeFunc = function (datas) {
            if (!datas || datas.length < 1) {
                return [];
            }
            var nodes = [];
            for (var d in datas) {
                var nowData = datas[d];
                eval('nodes.push({ id: nowData.' + options.groupData.keyField + ', name: nowData.' + options.groupData.nameField + ', isParent: true });');
            }
            return nodes;
        }
    }

    //默认条件
    if (!options.groupData.where) {
        options.groupData.where = function (treeId, treeNode) {
            var data = {};
            if (treeNode) {
                eval('data.' + options.groupData.parentField + ' = treeNode.id;');
            }
            return data;
        }
    }

    EZNEW_GroupMultSelectOptions = options;
    //数据表格工具模板
    OutPutTemplateScript(options.dataTable.toolBarId, '{{#  if(EZNEW_GroupMultSelectedDatas[d.' + options.dataTable.dataField + ']){ }}<button type="button" class="layui-btn layui-btn-xs layui-btn-gdanger dbtn{{d.' + options.dataTable.dataField + '}}" lay_event="disselect"><i class="icon-remove"></i> 取消</button>{{# }else{ }}<button type="button" class="layui-btn layui-btn-xs layui-btn-gsuccess dbtn{{d.' + options.dataTable.dataField + '}}" lay_event="select"><i class="icon-check"></i> 选择</button>{{# } }}');
    OutPutTemplateScript(options.selectTable.toolBarId, '<button type="button" class="layui-btn layui-btn-xs layui-btn-gdanger" lay_event="disselect"><i class="icon-remove"></i> 取消</button>');

    EZNEW_InitGroupMultSelectTree();//初始化分组数据

    //初始化数据表格
    var cols = options.dataTable.cols;
    var dataCols = DeepClone(cols);
    dataCols[0].push({ width: 80, align: 'center', fixed: 'right', toolbar: '#' + options.dataTable.toolBarId });
    var selectCols = DeepClone(cols);
    selectCols[0].push({ width: 80, align: 'center', fixed: 'right', toolbar: '#' + options.selectTable.toolBarId });
    InitTable({
        elem: options.dataTable.eleId,
        cols: dataCols
    });

    InitTable({
        elem: options.selectTable.eleId,
        cols: selectCols
    });

    //表格事件监听
    ListenTableEvent(options.dataTable.eleId, function (obj, btn) {
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay_event 对应的值（也可以是表头的 event 参数对应的值）
        var tr = obj.tr; //获得当前行 tr 的 DOM 对象（如果有的话）
        switch (layEvent) {
            case "select":
                eval('EZNEW_GroupMultSelectedDatas[data.' + options.dataTable.dataField + ']=data');
                EZNEW_GroupMultSelectBtnCheckToCancel(btn);
                break;
            case "disselect":
                eval('EZNEW_GroupMultSelectedDatas[data.' + options.dataTable.dataField + ']=null');
                EZNEW_GroupMultSelectBtnCancelToCheck(btn);
                break;
        }
        EZNEW_GroupMultSelectRefreshSelectTable();
        event.stopPropagation();
    });

    ListenTableEvent(options.selectTable.eleId, function (obj, btn) {
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay_event 对应的值（也可以是表头的 event 参数对应的值）
        switch (layEvent) {
            case "disselect":
                eval('EZNEW_GroupMultSelectedDatas[data.' + options.dataTable.dataField + ']=null');
                eval('var dataBtn = $(".dbtn" + data.' + options.dataTable.dataField + ');');
                EZNEW_GroupMultSelectBtnCancelToCheck(dataBtn);
                break;
        }
        EZNEW_GroupMultSelectRefreshSelectTable();
    });

    //Tab事件监听
    ListenTabEvent(options.navTab.eleId, function (data) {
        if (data.index == 1) {
            InitContentPanel();
            ResizeTable(options.selectTable.eleId);
        }
    });

    //搜索按钮事件
    $('#' + options.dataTable.searchBtnId).click(function () {
        EZNEW_GroupMultSelectLoadTableData();
    });

    //确认选择
    $("#" + options.confirmBtnEleId).click(function () {
        EZNEW_GroupMultSelectSelectData();
    });

    //关闭页面
    $("#" + options.closeBtnEleId).click(function () {
        EZNEW_GroupMultSelectClosePage();
    });
}

//初始化树控件
function EZNEW_InitGroupMultSelectTree() {
    var data = EZNEW_GroupMultSelectOptions.groupData.initDataCondition;
    if (EZNEW_GroupMultSelectOptions.groupData.where) {
        data = $.extend(true, {}, data, EZNEW_GroupMultSelectOptions.groupData.where(null, null));
    }
    AjaxDataRequest(
        EZNEW_GroupMultSelectOptions.groupData.requestMethod
        , EZNEW_GroupMultSelectOptions.groupData.dataUrl
        , data
        , function (res) {
            if (res && EZNEW_GroupMultSelectOptions.groupData.groupDataToNodeFunc) {
                var nodes = EZNEW_GroupMultSelectOptions.groupData.groupDataToNodeFunc(res);
                $.fn.zTree.init($("#" + EZNEW_GroupMultSelectOptions.groupData.eleId), EZNEW_GroupMultSelectOptions.treeSetting, nodes);
            } else {
                ErrorMsg('数据加载失败');
            }
        });
}

//树控件展开
function EZNEW_GroupMultSelectTreeBeforeExpand(treeId, treeNode) {
    if (!treeNode || treeNode.loadData) {
        return true;
    }
    var zTree = $.fn.zTree.getZTreeObj(treeId);
    treeNode.icon = EZNEW_GroupMultSelectOptions.groupData.loadingIconUrl;
    zTree.updateNode(treeNode);

    var where = $.extend(true, {}, EZNEW_GroupMultSelectOptions.groupData.where(treeId, treeNode));
    AjaxDataRequest(
        EZNEW_GroupMultSelectOptions.groupData.requestMethod
        , EZNEW_GroupMultSelectOptions.groupData.dataUrl
        , where
        , function (res) {
            if (!res) {
                return;
            }
            var nodes = EZNEW_GroupMultSelectOptions.groupData.groupDataToNodeFunc(res);
            zTree.addNodes(treeNode, -1, nodes);
            treeNode.loadData = true;
            treeNode.icon = "";
            zTree.updateNode(treeNode);
            zTree.expandNode(treeNode, true);
        });
    return false;
}

//加载表格数据
function EZNEW_GroupMultSelectLoadTableData() {
    if (EZNEW_GroupMultSelectOptions.dataTable.requestVerify && !EZNEW_GroupMultSelectOptions.dataTable.requestVerify(nowSelectedNode)) {
        return;
    }
    var where = {};
    eval('where.' + EZNEW_GroupMultSelectOptions.dataTable.groupField + ' = nowSelectedNode.id;');
    if (EZNEW_GroupMultSelectOptions.dataTable.where) {
        where = $.extend(true, {}, where, EZNEW_GroupMultSelectOptions.dataTable.where());
    }
    PageSearch({
        url: EZNEW_GroupMultSelectOptions.dataTable.dataUrl,
        method: EZNEW_GroupMultSelectOptions.dataTable.requestMethod,
        listEle: EZNEW_GroupMultSelectOptions.dataTable.eleId,
        pagerEle: EZNEW_GroupMultSelectOptions.dataTable.pagerEleId,
        showFirstLastBtn: false,
        data: where
    });
}

//选择按钮转换为取消按钮
function EZNEW_GroupMultSelectBtnCheckToCancel(btn) {
    $(btn).attr('lay_event', 'disselect').removeClass('layui-btn-gsuccess').addClass('layui-btn-gdanger');
    $(btn).html('<i class="icon-remove"></i> 取消');
}

//取消按钮转换为选择按钮
function EZNEW_GroupMultSelectBtnCancelToCheck(btn) {
    $(btn).attr('lay_event', 'select').removeClass('layui-btn-gdanger').addClass('layui-btn-gsuccess');
    $(btn).html('<i class="icon-check"></i> 选择');
}

//刷新选择数据表格
function EZNEW_GroupMultSelectRefreshSelectTable() {
    var datas = EZNEW_GroupMultSelectGetCheckedData();
    SetTableData({
        data: datas,
        page: 1,
        count: 0,
        id: EZNEW_GroupMultSelectOptions.selectTable.eleId
    });
}

//获取选择数据
function EZNEW_GroupMultSelectGetCheckedData() {
    var datas = [];
    for (var k in EZNEW_GroupMultSelectedDatas) {
        if (EZNEW_GroupMultSelectedDatas[k]) {
            datas.push(EZNEW_GroupMultSelectedDatas[k]);
        }
    }
    return datas;
}

//确认选择
function EZNEW_GroupMultSelectSelectData() {
    var data = {};
    var checkedDatas = EZNEW_GroupMultSelectGetCheckedData();
    var keys = [];
    if (checkedDatas) {
        for (var d in checkedDatas) {
            var nowData = checkedDatas[d];
            eval('keys.push(nowData.' + EZNEW_GroupMultSelectOptions.dataTable.dataField+');');
        }
    }
    DialogOpener().EZNEW_SelectCallback({
        type: 2,
        objectTag: EZNEW_GroupMultSelectOptions.objectTag,
        data: {
            datas: checkedDatas,
            keys: keys
        }
    });
    EZNEW_GroupMultSelectClosePage();
}

//关闭
function EZNEW_GroupMultSelectClosePage() {
    CloseCurrentDialogPage();
}
