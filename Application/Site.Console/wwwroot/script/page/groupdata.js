
var EZNEW_GroupDataOptions = {};//分组数据管理配置项
var EZNEW_NowSelectedNode = null;//当前选择节点
var EZNEW_NowSelectedGroupData = null;//当前选择数据
var EZNEW_AllGroupData = {};//所有分组数据


//初始化分组数据管理
function InitGroupDataManage(options) {
    //默认配置
    var defaultGroupDataOptions = {
        groupData: {
            loadDataUrl: '',
            deleteUrl: '',
            sortUrl: '',
            editUrl: '',
            loadMethod: 'post',
            keyField: 'Id',
            nameField: 'Name',
            parentField: 'Parent',
            sortField: 'Sort',
            levelField: 'Level',
            groupName: '',
            treeEleId: 'group_tree',
            addBtnEleId: 'btn_addgroup',
            editBtnEleId: 'btn_editgroup',
            delBtnEleId: 'btn_delgroup',
            groupDataToNodeFunc: null,
            editWindowSize: ['600px', '400px'],
            objectTag: '',
            loadingIcoUrl: EZNEW_TreeLoadingIcoUrl,
            where: null,
            initDataCondition: { levelOne: true },
            selectCallback: null,
            nodeSelectToggle: null,
            treeSettings: {
                view: {
                    nameIsHTML: true
                },
                check: {
                    enable: true,
                    chkboxType: { "Y": "", "N": "" },
                    autoCheckTrigger: false
                },
                edit: {
                    enable: true,
                    showRemoveBtn: false,
                    showRenameBtn: false,
                    drag: {
                        isMove: true,
                        inner: false,
                        prev: true,
                        next: true
                    }
                },
                callback: {
                    onClick: function (event, treeId, treeNode) {
                        EZNEW_GroupDataClick(event, treeId, treeNode);
                    },
                    beforeExpand: function (treeId, treeNode) {
                        if (!treeNode || treeNode.loadData) {
                            return true;
                        }
                        var zTree = $.fn.zTree.getZTreeObj(treeId);
                        treeNode.icon = EZNEW_GroupDataOptions.groupData.loadingIcoUrl;
                        zTree.updateNode(treeNode);
                        //数据条件
                        var data = {};
                        if (EZNEW_GroupDataOptions.groupData.where) {
                            data = $.extend(true, {}, data, EZNEW_GroupDataOptions.groupData.where(treeId, treeNode));
                        }
                        AjaxDataRequest(EZNEW_GroupDataOptions.groupData.loadMethod, EZNEW_GroupDataOptions.groupData.loadDataUrl, data, function (res) {
                            var nodes = EZNEW_GroupDataOptions.groupData.groupDataToNodeFunc(res);
                            for (var d in res) {
                                var nowData = res[d];
                                eval('EZNEW_AllGroupData[nowData.' + EZNEW_GroupDataOptions.groupData.keyField + ']=nowData');
                            }
                            zTree.addNodes(treeNode, -1, nodes);
                            treeNode.loadData = true;
                            treeNode.icon = "";
                            zTree.updateNode(treeNode);
                            zTree.expandNode(treeNode, true);
                        });
                        return false;
                    },
                    beforeDrag: function (treeId, treeNodes) {
                        for (var i = 0, l = treeNodes.length; i < l; i++) {
                            if (treeNodes[i].drag === false) {
                                return false;
                            }
                        }
                        return true;
                    },
                    beforeDrop: function (treeId, treeNodes, targetNode, moveType) {
                        if (!treeNodes || !targetNode || treeNodes.length <= 0) {
                            return false;
                        }
                        var firstNode = treeNodes[0];
                        return EZNEW_GroupDataMoveNode(firstNode, targetNode, moveType);
                    },
                }
            }
        },
        dataTable: {
            eleId: 'group_table_data',
            pagerEleId: 'group_table_data_pager',
            cols: [[]],
            searchBtnEleId: 'btn_search_group_table_data',
            where: null,
            loadDataUrl: '',
            removeDataUrl: '',
            clearDataUrl: '',
            addDataUrl: '',
            addCallbackUrl: '',
            addBtnEleId: 'btn_addgrouptabledata',
            delBtnEleId: 'btn_delgrouptabledata',
            clearBtnEleId: 'btn_cleargrouptabledata',
            loadDataMethod: 'post',
            toolBarEvent: null,
            tableDataKeyField: 'Id',
            editWindowSize: ['700px', '500px'],
            dataTag: '',
            dataName: '',
            toolbar: null,
            defaultToolbar: null,
            groupArgName: 'group',
            dataArgName: 'datas'
        },
        detailContainer: {
            eleId: 'detail_container',
            showDataDetailFunc: null,
            resetCallback: null
        },
        tab: {
            eleId: 'group_data_tab',
            firstTabId: 'group_table_data_tab',
            tabEventCallback: null
        }
    };

    options = $.extend(true, {}, defaultGroupDataOptions, options);

    //默认数据转换方式
    if (!options.groupData.groupDataToNodeFunc) {
        options.groupData.groupDataToNodeFunc = function (datas) {
            if (!datas || datas < 1) {
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

    EZNEW_GroupDataOptions = options;

    EZNEW_InitGroupTree();//初始化分组数据控件

    //编辑数据
    $("#" + EZNEW_GroupDataOptions.groupData.editBtnEleId).click(function () {
        if (!EZNEW_NowSelectedNode || !EZNEW_NowSelectedNode.id) {
            ErrorMsg('请选择要编辑的数据');
            return;
        }
        OpenDialogPage({
            title: '编辑' + EZNEW_GroupDataOptions.groupData.groupName,
            content: EZNEW_GroupDataOptions.groupData.editUrl + '?' + EZNEW_GroupDataOptions.groupData.keyField + '=' + EZNEW_NowSelectedNode.id,
            area: EZNEW_GroupDataOptions.groupData.editWindowSize
        });
    });

    //添加数据
    $("#" + EZNEW_GroupDataOptions.groupData.addBtnEleId).click(function () {
        OpenDialogPage({
            title: '添加' + EZNEW_GroupDataOptions.groupData.groupName,
            content: EZNEW_GroupDataOptions.groupData.editUrl,
            area: EZNEW_GroupDataOptions.groupData.editWindowSize
        });
    });

    //删除数据
    $("#" + EZNEW_GroupDataOptions.groupData.delBtnEleId).click(function () {
        var checkNodes = $.fn.zTree.getZTreeObj(EZNEW_GroupDataOptions.groupData.treeEleId).getCheckedNodes(true);
        var values = new Array();
        for (var i = 0; i < checkNodes.length; i++) {
            values.push(checkNodes[i].id);
        }
        EZNEW_DeleteGroupData(values);
    });

    //初始化表格数据
    InitTable({
        elem: EZNEW_GroupDataOptions.dataTable.eleId,
        cols: EZNEW_GroupDataOptions.dataTable.cols,
        toolbar: EZNEW_GroupDataOptions.dataTable.toolbar,
        defaultToolbar: EZNEW_GroupDataOptions.dataTable.defaultToolbar
    });

    //监听数据表格事件
    ListenTableEvent(EZNEW_GroupDataOptions.dataTable.eleId, function (obj, btn) {
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
        var tr = obj.tr; //获得当前行 tr 的 DOM 对象（如果有的话）
        switch (layEvent) {
            case "remove":
                EZNEW_RemoveGroupTableDatas([data]);
                break;
            default:
                if (EZNEW_GroupDataOptions.dataTable.toolBarEvent) {
                    EZNEW_GroupDataOptions.dataTable.toolBarEvent(obj, btn);
                }
                break;
        }
    });

    //添加表格数据
    $("#" + EZNEW_GroupDataOptions.dataTable.addBtnEleId).click(function () {
        OpenDialogPage({
            title: '选择数据',
            content: EZNEW_GroupDataOptions.dataTable.addDataUrl.replace('_groupid', EZNEW_NowSelectedNode.id),
            area: EZNEW_GroupDataOptions.dataTable.editWindowSize
        });
    });

    //移除选中表格数据
    $("#" + EZNEW_GroupDataOptions.dataTable.delBtnEleId).click(function () {
        var removeDatas = GetTableCheckData(EZNEW_GroupDataOptions.dataTable.eleId).data;
        EZNEW_RemoveGroupTableDatas(removeDatas);
    });

    //清除表格数据
    $("#" + EZNEW_GroupDataOptions.dataTable.clearBtnEleId).click(function () {
        EZNEW_ClearGroupTableDatas();
    });

    //搜索表格数据
    $("#" + EZNEW_GroupDataOptions.dataTable.searchBtnEleId).click(function () {
        EZNEW_LoadGroupTableData();
    });

    //tab事件监听
    ListenTabEvent(options.tab.eleId, function (data) {
        if (EZNEW_GroupDataOptions.tab.tabEventCallback) {
            EZNEW_GroupDataOptions.tab.tabEventCallback(data);
        }
    });
};

//初始化分组数据
function EZNEW_InitGroupTree() {

    //数据条件
    var data = EZNEW_GroupDataOptions.groupData.initDataCondition;
    if (EZNEW_GroupDataOptions.groupData.where) {
        data = $.extend(true, {}, data, EZNEW_GroupDataOptions.groupData.where(null, null));
    }

    AjaxDataRequest(EZNEW_GroupDataOptions.groupData.loadMethod, EZNEW_GroupDataOptions.groupData.loadDataUrl, data, function (res) {
        if (res && EZNEW_GroupDataOptions.groupData.groupDataToNodeFunc) {
            var nodes = EZNEW_GroupDataOptions.groupData.groupDataToNodeFunc(res);
            for (var d in res) {
                var nowData = res[d];
                eval('EZNEW_AllGroupData[nowData.' + EZNEW_GroupDataOptions.groupData.keyField + ']=nowData');
            }
            $.fn.zTree.init($("#" + EZNEW_GroupDataOptions.groupData.treeEleId), EZNEW_GroupDataOptions.groupData.treeSettings, nodes);
        } else {
            ErrorMsg(EZNEW_GroupDataOptions.groupData.groupName + '数据加载失败');
        }
    });
}

//分组数据点击
function EZNEW_GroupDataClick(event, treeId, treeNode) {
    if (EZNEW_NowSelectedNode && EZNEW_NowSelectedNode.id == treeNode.id) {
        return;
    }
    if (EZNEW_NowSelectedNode && EZNEW_GroupDataOptions.groupData.nodeSelectToggle) {
        EZNEW_GroupDataOptions.groupData.nodeSelectToggle(EZNEW_NowSelectedNode, treeNode);
    }
    EZNEW_NowSelectedNode = treeNode;
    EZNEW_NowSelectedGroupData = EZNEW_AllGroupData[treeNode.id];
    if (!EZNEW_NowSelectedGroupData) {
        return;
    }
    EZNEW_ShowGroupDataDetail();//显示详情
    EZNEW_LoadGroupTableData();//加载关联数据
    $("#" + EZNEW_GroupDataOptions.detailContainer.eleId).removeClass('fiterhide');
}

//显示分组数据详情
function EZNEW_ShowGroupDataDetail() {
    if (EZNEW_GroupDataOptions.detailContainer.showDataDetailFunc) {
        EZNEW_GroupDataOptions.detailContainer.showDataDetailFunc(EZNEW_NowSelectedGroupData);
    }
}

//加载分组数据关联数据
function EZNEW_LoadGroupTableData() {
    if (!EZNEW_NowSelectedGroupData) {
        ErrorMsg('请选择' + EZNEW_GroupDataOptions.groupData.groupName);
        return;
    }
    var condition = {};
    if (EZNEW_GroupDataOptions.dataTable.where) {
        condition = EZNEW_GroupDataOptions.dataTable.where(EZNEW_NowSelectedGroupData);
    }
    PageSearch({
        url: EZNEW_GroupDataOptions.dataTable.loadDataUrl,
        method: EZNEW_GroupDataOptions.dataTable.loadDataMethod,
        listEle: EZNEW_GroupDataOptions.dataTable.eleId,
        pagerEle: EZNEW_GroupDataOptions.dataTable.pagerEleId,
        data: condition
    });
}

//清除分组表格数据
function EZNEW_ClearGroupTableDatas() {
    if (!EZNEW_NowSelectedGroupData) {
        return;
    }
    ConfirmMsg('确认要清除所有的' + EZNEW_GroupDataOptions.dataTable.dataName + '数据吗?', function () {
        var data = {};
        eval('data.' + EZNEW_GroupDataOptions.dataTable.groupArgName + '=EZNEW_NowSelectedNode.id;');
        $.post(EZNEW_GroupDataOptions.dataTable.clearDataUrl, data, function (res) {
            ResultMsg(res);
            if (res.Success) {
                EZNEW_LoadGroupTableData();
            }
        });
    });
}

//移除分组表格数据
function EZNEW_RemoveGroupTableDatas(datas) {
    if (!datas || datas.length < 1 || !EZNEW_NowSelectedGroupData) {
        ErrorMsg('没有指定要移除的' + EZNEW_GroupDataOptions.dataTable.dataName)
        return;
    }

    ConfirmMsg('确认要删除' + EZNEW_GroupDataOptions.dataTable.dataName + '数据吗?', function () {
        var dataIds = [];
        for (var d in datas) {
            eval('dataIds.push(datas[d].' + EZNEW_GroupDataOptions.dataTable.tableDataKeyField + ');');
        }
        var data = {};
        eval('data.' + EZNEW_GroupDataOptions.dataTable.groupArgName + '=EZNEW_NowSelectedNode.id;');
        eval('data.' + EZNEW_GroupDataOptions.dataTable.dataArgName + '=dataIds;');
        $.post(EZNEW_GroupDataOptions.dataTable.removeDataUrl, data, function (res) {
            ResultMsg(res);
            if (res.Success) {
                EZNEW_LoadGroupTableData();
            }
        });
    });
}

//删除分组数据
function EZNEW_DeleteGroupData(ids) {
    if (!ids || ids.length <= 0) {
        ErrorMsg('没有选择要删除的' + EZNEW_GroupDataOptions.groupData.groupName);
        return;
    }
    ConfirmMsg('删除' + EZNEW_GroupDataOptions.groupData.groupName + '数据将同时删除该' + EZNEW_GroupDataOptions.groupData.groupName + '下的所有下级' + EZNEW_GroupDataOptions.groupData.groupName + ',确认删除吗?', function (res) {
        $.post(EZNEW_GroupDataOptions.groupData.deleteUrl, { ids: ids }, function (res) {
            ResultMsg(res);
            if (res.Success) {
                var groupTree = $.fn.zTree.getZTreeObj(EZNEW_GroupDataOptions.groupData.treeEleId);
                var checkNodes = groupTree.getCheckedNodes(true);
                for (var n in checkNodes) {
                    var nowNode = checkNodes[n];
                    groupTree.removeNode(nowNode);
                    if (EZNEW_NowSelectedNode && EZNEW_NowSelectedNode.id == nowNode.id) {
                        EZNEW_ResetGroupDetailPage();
                    }
                }
            }
        });
    });
}

//重置详情数据
function EZNEW_ResetGroupDetailPage() {
    $("#" + EZNEW_GroupDataOptions.detailContainer.eleId).addClass("fiterhide");//隐藏内容面板
    EZNEW_NowSelectedGroupData = null;//当前选择数据
    EZNEW_NowSelectedNode = null;//当前选择节点
    EZNEW_TabToGroupTableData();//切换到分组表格数据
    ClearTableData(EZNEW_GroupDataOptions.dataTable.eleId);//清除表格数据
    $("#" + EZNEW_GroupDataOptions.dataTable.pagerEleId).html("");//清除表格数据分页控件
    if (EZNEW_GroupDataOptions.detailContainer.resetCallback) {
        EZNEW_GroupDataOptions.detailContainer.resetCallback();
    }
}

//切换表格数据
function EZNEW_TabToGroupTableData() {
    TabChange(EZNEW_GroupDataOptions.tab.eleId, EZNEW_GroupDataOptions.tab.firstTabId);
}

//页面编辑数据回调
function EZNEW_EditCallback(data) {
    if (!data) {
        return;
    }
    switch (data.objectTag) {
        case EZNEW_GroupDataOptions.groupData.objectTag:
            EZNEW_GroupDataEditCallback(data.data);
            break;
        case EZNEW_GroupDataOptions.dataTable.dataTag:
            EZNEW_LoadGroupTableData();
            break;
    }
}

//分组数据编辑回调
function EZNEW_GroupDataEditCallback(data) {
    if (!data) {
        return;
    }
    var nowTree = $.fn.zTree.getZTreeObj(EZNEW_GroupDataOptions.groupData.treeEleId);
    eval('var nowData = EZNEW_AllGroupData[data.' + EZNEW_GroupDataOptions.groupData.keyField + '];');
    eval('var nowNode = nowTree.getNodeByParam("id", data.' + EZNEW_GroupDataOptions.groupData.keyField + ');EZNEW_AllGroupData[data.' + EZNEW_GroupDataOptions.groupData.keyField + '] = data;');
    var newNode = EZNEW_GroupDataOptions.groupData.groupDataToNodeFunc([data])[0];
    var parentNode = null;
    if (nowNode) {//修改
        if (!nowData) {
            return;
        }
        //新数据父节点
        eval('var parent=data.' + EZNEW_GroupDataOptions.groupData.parentField + ';');
        eval('parentNode =parent==null?null:nowTree.getNodeByParam("id", parent.' + EZNEW_GroupDataOptions.groupData.keyField + ');');
        //原始数据父节点
        eval('var oldParent=nowData.' + EZNEW_GroupDataOptions.groupData.parentField + ';');
        eval('var oldParentNode = oldParent==null?null:nowTree.getNodeByParam("id", oldParent.' + EZNEW_GroupDataOptions.groupData.keyField + ');');

        var parentId = "EZNEW1000";
        if (parent) {
            eval('parentId=parent.' + EZNEW_GroupDataOptions.groupData.keyField + ';');
        }
        var oldParentId = "EZNEW1000";
        if (oldParent) {
            eval('oldParentId=oldParent.' + EZNEW_GroupDataOptions.groupData.keyField + ';');
        }
        if (parentId != oldParentId) {
            eval('var nowLevel=data.' + EZNEW_GroupDataOptions.groupData.levelField + ';');
            if (nowLevel < 2 || (parentNode && parentNode.loadData)) {
                nowTree.moveNode(parentNode, nowNode, 'inner', true);
                nowTree.expandNode(parentNode, true);
            }
            else {
                nowTree.removeNode(nowNode);
                EZNEW_ResetGroupDetailPage();
            }
            if (oldParentNode) {
                oldParentNode.isParent = true;
                nowTree.updateNode(oldParentNode);
            }
        }

        nowNode.name = newNode.name;
        nowTree.updateNode(nowNode);
        EZNEW_NowSelectedGroupData = data;
        EZNEW_ShowGroupDataDetail();
    } else {//新增
        if (data.Parent) {
            eval('parentNode = nowTree.getNodeByParam("id", data.Parent.' + EZNEW_GroupDataOptions.groupData.keyField + ');');
            if (parentNode && !parentNode.loadData) {
                return;
            }
        }
        nowTree.addNodes(parentNode, {
            id: newNode.id,
            name: newNode.name,
            isParent: true
        });
    }
}

//数据选择回调
function EZNEW_SelectCallback(res) {
    if (!res) {
        return;
    }
    switch (res.objectTag) {
        case EZNEW_GroupDataOptions.dataTable.dataTag:
            EZNEW_GroupTableDataSelectCallback(res.data);
            break;
        default:
            if (EZNEW_GroupDataOptions.groupData.selectCallback) {
                EZNEW_GroupDataOptions.groupData.selectCallback(res);
            }
            break;
    }
}

//分组表格数据回调
function EZNEW_GroupTableDataSelectCallback(datas) {
    if (!datas || datas.length < 1) {
        return;
    }
    var dataIds = new Array();
    for (var u in datas) {
        eval('dataIds.push(datas[u].' + EZNEW_GroupDataOptions.dataTable.tableDataKeyField + ');');
    }
    var data = {};
    eval('data.' + EZNEW_GroupDataOptions.dataTable.groupArgName + '=EZNEW_NowSelectedNode.id;');
    eval('data.' + EZNEW_GroupDataOptions.dataTable.dataArgName + '=dataIds;');
    $.post(EZNEW_GroupDataOptions.dataTable.addCallbackUrl, data, function (res) {
        ResultMsg(res);
        if (res.Success) {
            EZNEW_LoadGroupTableData();
        }
    });
}

//移动节点
function EZNEW_GroupDataMoveNode(treeNode, targetNode, moveType) {
    if (treeNode.tId == targetNode.tId) {
        return false;
    }
    var sort = 0;
    if (moveType == "inner") {
        if (targetNode.tId != treeNode.parentTId) {
            return false;
        }
        sort = 1;
    } else {
        if (treeNode.parentTId != targetNode.parentTId) {
            return false;
        }
        var targetId = targetNode.id;
        var targetData = EZNEW_AllGroupData[targetId];
        if (!targetData) {
            return false;
        }
        eval('sort = moveType == "prev" ? targetData.' + EZNEW_GroupDataOptions.groupData.sortField + ' : targetData.' + EZNEW_GroupDataOptions.groupData.sortField + ' + 1;');
    }
    var nowId = treeNode.id;
    var data = {};
    eval('data.' + EZNEW_GroupDataOptions.groupData.keyField + ' = nowId;');
    eval('data.' + EZNEW_GroupDataOptions.groupData.sortField + ' = sort;');
    $.post(EZNEW_GroupDataOptions.groupData.sortUrl, data, function (res) {
        if (res.Success) {
            $.fn.zTree.getZTreeObj(EZNEW_GroupDataOptions.groupData.treeEleId).moveNode(targetNode, treeNode, moveType, true);
        }
    })
    return false;
}