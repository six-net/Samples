var defaultMultSelectOptions = {
    objectTag: "",
    dataTable: {
        eleId: "data_list",
        pagerEleId: "data_pager",
        toolBarId: "data_toobar",
        requestMethod: "post",
        dataUrl: '',
        where: null,
        dataField: "SysNo",
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
var selectOptions = defaultMultSelectOptions;
var multSelectedDatas = {};//已选择数据

//初始化分组选择
function InitMultSelect(options) {
    selectOptions = options = $.extend(true, {}, defaultMultSelectOptions, options);
    //数据表格工具模板
    OutPutTemplateScript(options.dataTable.toolBarId, '{{#  if(multSelectedDatas[d.' + options.dataTable.dataField + ']){ }}<button type="button" class="layui-btn layui-btn-xs layui-btn-gdanger dbtn{{d.' + options.dataTable.dataField + '}}" lay_event="disselect"><i class="icon-remove"></i> 取消</button>{{# }else{ }}<button type="button" class="layui-btn layui-btn-xs layui-btn-gsuccess dbtn{{d.' + options.dataTable.dataField + '}}" lay_event="select"><i class="icon-check"></i> 选择</button>{{# } }}');
    OutPutTemplateScript(options.selectTable.toolBarId, '<button type="button" class="layui-btn layui-btn-xs layui-btn-gdanger" lay_event="disselect"><i class="icon-remove"></i> 取消</button>');

    //初始化数据表格
    var cols = options.dataTable.cols;
    var dataCols = DeepClone(cols);
    dataCols[0].push({ width: 80, align: 'center', fixed: 'right', toolbar: '#' + options.dataTable.toolBarId });
    var selectCols = DeepClone(cols);
    selectCols[0].push({ width: 80, align: 'center', fixed: 'right', toolbar: '#' + options.selectTable.toolBarId });
    InitTable({
        elem: options.dataTable.eleId,
        id: options.dataTable.eleId,
        even: true,
        useParentContainer: true,
        cols: dataCols
    }, LoadTableData);

    InitTable({
        elem: options.selectTable.eleId,
        id: options.selectTable.eleId,
        even: true,
        useParentContainer: true,
        cols: selectCols
    });

    //表格事件监听
    ListenTableEvent(options.dataTable.eleId, function (obj, btn) {
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay_event 对应的值（也可以是表头的 event 参数对应的值）
        var tr = obj.tr; //获得当前行 tr 的 DOM 对象（如果有的话）
        switch (layEvent) {
            case "select":
                eval('multSelectedDatas[data.' + selectOptions.dataTable.dataField + ']=data');
                BtnCheckToCancel(btn);
                break;
            case "disselect":
                eval('multSelectedDatas[data.' + selectOptions.dataTable.dataField + ']=null');
                BtnCancelToCheck(btn);
                break;
        }
        RefreshSelectTable();
    });

    ListenTableEvent(options.selectTable.eleId, function (obj, btn) {
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay_event 对应的值（也可以是表头的 event 参数对应的值）
        switch (layEvent) {
            case "disselect":
                eval('multSelectedDatas[data.' + selectOptions.dataTable.dataField + ']=null');
                eval('var dataKey=data.' + selectOptions.dataTable.dataField);
                var dataBtn = $('.dbtn' + dataKey);
                BtnCancelToCheck(dataBtn);
                break;
        }
        RefreshSelectTable();
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
        LoadTableData();
    });
}

//加载表格数据
function LoadTableData() {
    var where = {
    };
    if (selectOptions.dataTable.where) {
        where = $.extend(true, {}, where, selectOptions.dataTable.where());
    }
    PageSearch({
        url: selectOptions.dataTable.dataUrl,
        method: selectOptions.dataTable.requestMethod,
        listEle: selectOptions.dataTable.eleId,
        pagerEle: selectOptions.dataTable.pagerEleId,
        showFirstLastBtn: false,
        data: where
    });
}

//选择按钮转换为取消按钮
function BtnCheckToCancel(btn) {
    $(btn).attr('lay_event', 'disselect').removeClass('layui-btn-gsuccess').addClass('layui-btn-gdanger');
    $(btn).html('<i class="icon-remove"></i> 取消');
}

//取消按钮转换为选择按钮
function BtnCancelToCheck(btn) {
    $(btn).attr('lay_event', 'select').removeClass('layui-btn-gdanger').addClass('layui-btn-gsuccess');
    $(btn).html('<i class="icon-check"></i> 选择');
}

//刷新选择数据表格
function RefreshSelectTable() {
    var datas = GetCheckedData();
    SetTableData({
        data: datas,
        page: 1,
        count: 0,
        id: selectOptions.selectTable.eleId
    });
}

//获取选择数据
function GetCheckedData() {
    var datas = [];
    for (var k in multSelectedDatas) {
        if (multSelectedDatas[k]) {
            datas.push(multSelectedDatas[k]);
        }
    }
    return datas;
}

//确认选择
function SelectData() {
    parent.EZNEW_SelectCallback({
        type: 2,
        objectTag: selectOptions.objectTag,
        data: GetCheckedData()
    });
    ClosePage();
}

//关闭
function ClosePage() {
    CloseCurrentDialogPage();
}
