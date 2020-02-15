var defaultSingleSelectOptions = {
    objectTag: "",
    dataTable: {
        eleId: "data_list",
        pagerEleId: "data_pager",
        requestMethod: "post",
        dataUrl: '',
        where: null,
        dataField: "SysNo",
        searchBtnId: 'search_btn',
        cols: [[]] //数据标题
    }
};
var selectOptions = defaultSingleSelectOptions;

//初始化分组选择
function InitSingleSelect(options) {
    selectOptions = options = $.extend(true, {}, defaultSingleSelectOptions, options);

    //初始化数据表格
    var cols = options.dataTable.cols;
    InitTable({
        elem: '#' + options.dataTable.eleId,
        id: '#' + options.dataTable.eleId,
        even: true,
        useParentContainer: true,
        cols: cols
    }, LoadTableData);

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
        listEle: "#" + selectOptions.dataTable.eleId,
        pagerEle: "#" + selectOptions.dataTable.pagerEleId,
        showFirstLastBtn: false,
        data: where
    });
}

//确认选择
function SelectData() {
    var selectData = GetTableCheckData('#' + selectOptions.dataTable.eleId).data;
    parent.EZNEW_SelectCallback({
        type: 1,
        objectTag: selectOptions.objectTag,
        data: selectData
    });
    ClosePage();
}

//关闭
function ClosePage() {
    CloseCurrentDialogPage();
}
