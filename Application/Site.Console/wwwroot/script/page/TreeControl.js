
//初始化树控件
var CTreeControl = function CTreeControl(options) {
    var thisObj = this;
    var defaultOptions = {
        treeSetting: {
            view: {
                nameIsHTML: true,
            },
            callback: {
                onClick: function (event, treeId, treeNode) {
                    if (thisObj.settings.data.onClick) {
                        thisObj.settings.data.onClick(event, treeId, treeNode);
                    }
                },
                beforeExpand: function (treeId, treeNode) {
                    if (!treeNode || treeNode.loadData) {
                        return true;
                    }
                    var zTree = $.fn.zTree.getZTreeObj(treeId);
                    treeNode.icon = thisObj.settings.data.loadingIcoUrl;
                    zTree.updateNode(treeNode);

                    var data = {};
                    if (thisObj.settings.data.where) {
                        data = $.extend(true, {}, data, thisObj.settings.data.where(treeId, treeNode));
                    }

                    AjaxDataRequest(thisObj.settings.data.method, thisObj.settings.data.url, data, function (res) {
                        var nodes = thisObj.settings.data.parseDataToNode(res);
                        zTree.addNodes(treeNode, -1, nodes);
                        treeNode.loadData = true;
                        treeNode.icon = "";
                        zTree.updateNode(treeNode);
                        zTree.expandNode(treeNode, true);
                    });
                    return false;
                }
            },
            check: {
                enable: false,
                chkboxType: { "Y": "", "N": "" },
                autoCheckTrigger: false
            },
            edit: {
            }
        },
        data: {
            url: '',
            method: 'post',
            parseDataToNode: null,
            treeEleId: 'data_tree',
            keyField: 'Id',
            nameField: 'Name',
            parentField: 'Parent',
            loadingIcoUrl: EZNEW_TreeLoadingIcoUrl,
            where: null,
            initDataCondition: { level: 1 },
            parseDataToNode: null,
            selectAllBtnId: 'btn_selectall',
            cancelAllBtnId: 'btn_cancelall',
            confirmSelectBtnId: 'btn_confirmselect',
            closeBtnId: 'btn_closeselect',
            dataTag: '',
            onClick: null
        }
    };

    //默认数据转换
    if (!options.data.parseDataToNode) {
        options.data.parseDataToNode = function (datas) {
            if (!datas || datas < 1) {
                return [];
            }
            var nodes = [];
            for (var d in datas) {
                var nowData = datas[d];
                eval('nodes.push({ id: nowData.' + options.data.keyField + ', name: nowData.' + options.data.nameField + ', isParent: true });');
            }
            return nodes;
        }
    }

    //默认条件
    if (!options.data.where) {
        options.data.where = function (treeId, treeNode) {
            var data = {};
            if (treeNode) {
                eval('data.' + options.data.parentField + ' = treeNode.id;');
            }
            return data;
        }
    }

    this.settings = options = $.extend(true, {}, defaultOptions, options);
    this.AllDatas = {};

    //初始化
    this.Init = function () {
        var data = thisObj.settings.data.initDataCondition;
        if (thisObj.settings.data.where) {
            data = $.extend(true, {}, data, thisObj.settings.data.where(null, null));
        }
        AjaxDataRequest(thisObj.settings.data.method, thisObj.settings.data.url, data, function (res) {
            if (!res) {
                return;
            }
            var nodes = thisObj.settings.data.parseDataToNode(res);
            for (var d in res) {
                var nowData = res[d];
                eval('thisObj.AllDatas[nowData.' + thisObj.settings.data.keyField + ']=nowData');
            }
            $.fn.zTree.init($("#" + thisObj.settings.data.treeEleId), thisObj.settings.treeSetting, nodes);
        });

        //全选事件
        $("#" + thisObj.settings.data.selectAllBtnId).click(function () {
            var zTree = $.fn.zTree.getZTreeObj(thisObj.settings.data.treeEleId);
            zTree.checkAllNodes(true);
        });

        //取消全选事件
        $("#" + thisObj.settings.data.cancelAllBtnId).click(function () {
            var zTree = $.fn.zTree.getZTreeObj(thisObj.settings.data.treeEleId);
            zTree.checkAllNodes(false);
        });

        //确认选择事件
        $("#" + thisObj.settings.data.confirmSelectBtnId).click(function () {
            DialogOpener().EZNEW_SelectCallback({
                type: thisObj.settings.treeSetting.check.chkStyle == 'radio' ? 1 : 2,
                objectTag: thisObj.settings.data.dataTag,
                data: {
                    nodes: thisObj.GetCheckedNodes(),
                    datas: thisObj.GetCheckedDatas(),
                    keys: thisObj.GetCheckedKeys()
                }
            });
            CloseCurrentDialogPage();
        });

        //关闭事件
        $("#" + thisObj.settings.data.closeBtnId).click(function () {
            CloseCurrentDialogPage();
        });
    }

    //返回选择数据
    this.GetCheckedDatas = function () {
        var checkedNodes = thisObj.GetCheckedNodes();
        var datas = [];
        for (var n in checkedNodes) {
            var nowNode = checkedNodes[n];
            var nowData = thisObj.AllDatas[nowNode.id];
            datas.push(nowData);
        }
        return datas;
    }

    //获取选择键值
    this.GetCheckedKeys = function () {
        var checkedNodes = thisObj.GetCheckedNodes();
        var datas = [];
        for (var n in checkedNodes) {
            var nowNode = checkedNodes[n];
            datas.push(nowNode.id);
        }
        return datas;
    }

    //返回选择的节点
    this.GetCheckedNodes = function () {
        var zTree = $.fn.zTree.getZTreeObj(thisObj.settings.data.treeEleId);
        return zTree.getCheckedNodes(true);
    }

    //取消所有节点选择
    this.CancelSelectedNodes = function () {
        var zTree = $.fn.zTree.getZTreeObj(thisObj.settings.data.treeEleId);
        zTree.cancelSelectedNode();
    }

    //展开/关闭所有节点
    this.ExpandAll = function (status) {
        var zTree = $.fn.zTree.getZTreeObj(thisObj.settings.data.treeEleId);
        zTree.expandAll(status);
    }

    this.Init();
    return this;
}