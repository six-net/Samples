var CTreeSelect = function (options) {
    this.defaultOptions = {
        containerEle: '.ctreeselect-container',//容器元素
        treeConfig: {
            view: {
                nameIsHTML: true
            },
            check: {
                enable: false,
                chkboxType: { "Y": "", "N": "" },
                autoCheckTrigger: false
            },
            callback: {
            }
        },
        multiSelect: false,//是否启用多选
        width: 200,//最大宽度
        height: 300,//最大高度
        selectCallback: null,//选择回调
        placeText: '请选择',//默认提示信息
        inputText: '',//默认文本框信息
        selectedValue: null,//默认选择值
        defaultNode: null,//默认选项节点
        dataUrl: '',//数据加载地址
        keyField: 'Id',//值字段
        nameField: 'Name',//名称字段
        parseDataToNodeFunc: null,//数据转换为节点方法
        requestMethod: "post",//数据请求方式,
        where: null,
        editMode: false,//编辑模式,
        editKeyValue: null,//当前编辑值
        loadingIcoUrl: EZNEW_TreeLoadingIcoUrl
    };

    this.settings = $.extend(true, {}, this.defaultOptions, options);
    var thisObj = this;
    this.Container = null;
    this.TreeObject = null;
    this.TreeEle = null;

    //默认条件
    if (!this.settings.where && !this.settings.multiSelect) {
        this.settings.where = function (node, editValue) {
            var data = {};
            if (node) {
                data.parent = node.id;
            }
            if (thisObj.settings.editMode && thisObj.settings.editKeyValue) {
                data.excludeIds = [thisObj.settings.editKeyValue]
            }
            return data;
        }
    }

    //默认数据转换
    if (!this.settings.parseDataToNodeFunc) {
        this.settings.parseDataToNodeFunc = function (datas) {
            if (!datas || datas.length < 1) {
                return [];
            }
            var nodes = [];
            for (var d in datas) {
                var nowData = datas[d];
                eval('nodes.push({ id: nowData.' + thisObj.settings.keyField + ', name: nowData.' + thisObj.settings.nameField + ', isParent: true });');
            }
            return nodes;
        }
    }

    //初始化
    this.Init = function () {
        var container = this.Container = $(this.settings.containerEle);
        if (container.length <= 0) {
            return;
        }
        this.settings.treeConfig.check = {
            enable: this.settings.multiSelect
        };
        if (!this.settings.multiSelect) {
            if (this.settings.treeConfig.callback) {
                this.settings.treeConfig.callback.onClick = thisObj.TreeNodeClick;
                this.settings.treeConfig.callback.beforeExpand = thisObj.BeforeExpand;
            }
            else {
                this.settings.treeConfig.callback = {
                    onClick: thisObj.TreeNodeClick
                }
            }
        } else {
            if (this.settings.treeConfig.callback) {
                this.settings.treeConfig.callback.onCheck = thisObj.TreeNodeCheck;
            }
            else {
                this.settings.treeConfig.callback = {
                    onCheck: thisObj.TreeNodeCheck
                }
            }
            this.settings.treeConfig.check.chkboxType = { 'Y': '', 'N': '' }
        }

        container.addClass('ztree-container');
        container.css('position', 'relative');
        container.css('z-index', '99');
        //创建下拉框
        var txtContainer = $('<div class="ctreeselect-text-container"><i class="ctreeselect-text-ico micon micon-down"></i><input type="text" class="form-control ctreeselect-text-input" placeholder="' + this.settings.placeText + '" readonly="readonly" value="' + this.settings.inputText + '"/></div>')
        txtContainer.width(this.settings.width);
        txtContainer.css('position', 'absolute').css("left", '0px').css('top', '0px');
        container.append(txtContainer);
        txtContainer.find('.ctreeselect-text-input').outerWidth(txtContainer.width() - txtContainer.find('.ctreeselect-text-ico').width()).outerHeight(txtContainer.height());
        txtContainer.css('line-height', txtContainer.height() + 'px');

        var treeContainer = $('<div class="ctreeselect-tree-container"></div>')
        treeContainer.width(this.settings.width);
        treeContainer.css('position', 'absolute').css("left", '0px').css('top', txtContainer.outerHeight() - 1);
        container.append(treeContainer);

        var treeList = $('<div class="ctreeselect-data-container"></div>');
        treeList.css('max-height', this.settings.height);
        var id = 'treedata_' + this.settings.containerEle.substring(1);
        var treeData = $('<ul class="ctreeselect-date-list ztree" id="' + id + '"></ul>');
        treeContainer.append(treeList);
        treeList.append(treeData);
        this.TreeEle = treeData;
        this.InitTree();

        container.click(function (e) {
            e.stopPropagation();
        });

        txtContainer.find('.ctreeselect-text-input').on('click', function (e) {
            thisObj.ToggleShow();
            e.stopPropagation();
        });

        txtContainer.find('.ctreeselect-text-ico').click(function (e) {
            thisObj.ToggleShow();
            e.stopPropagation();
        });

        $('body').click(function () {
            thisObj.ClosePanel();
        });

        if (this.settings.multiSelect) {
            var footTool = $('<div class="ctreeselect-foot"><button type="button" class="ctreeselect-selectallbtn layui-btn layui-btn-sm layui-btn-gsuccess">全选</button> <button type="button" class="ctreeselect-cacheallbtn layui-btn layui-btn-sm layui-btn-primary">清空</button> <button type="button" class="ctreeselect-cachebtn layui-btn layui-btn-sm layui-btn-danger">关闭</button></div>');
            treeContainer.append(footTool);
            footTool.find('.ctreeselect-cachebtn').click(function () {
                thisObj.ToggleShow();
            });
            footTool.find('.ctreeselect-selectallbtn').click(function () {
                thisObj.TreeObject.checkAllNodes(true);
                thisObj.TreeNodeCheck();
            });
            footTool.find('.ctreeselect-cacheallbtn').click(function () {
                thisObj.TreeObject.checkAllNodes(false);
                thisObj.TreeNodeCheck();
            });
        }
    }

    //显示切换
    this.ToggleShow = function () {
        if (thisObj.Container.hasClass('open')) {
            this.ClosePanel();
        } else {
            this.ShowPanel();
        }
    }

    //显示面板
    this.ShowPanel = function () {
        thisObj.Container.addClass('open');
        thisObj.Container.find('.ctreeselect-text-ico').addClass('micon-up').removeClass('micon-down');
    }

    //关闭面板
    this.ClosePanel = function () {
        thisObj.Container.removeClass('open');
        thisObj.Container.find('.ctreeselect-text-ico').removeClass('micon-up').addClass('micon-down');
    }

    //节点选择
    this.TreeNodeCheck = function (event, treeId, treeNode) {
        var names = thisObj.GetCheckedNames();
        thisObj.Container.find('.ctreeselect-text-input').val(names.join(','));
        var nodes = thisObj.TreeObject.getCheckedNodes();
        if (thisObj.settings.selectCallback) {
            thisObj.settings.selectCallback(nodes);
        }
    }

    //节点点击
    this.TreeNodeClick = function (event, treeId, treeNode) {
        if (thisObj.settings.multiSelect) {
            return;
        }
        thisObj.SelectNode(treeNode);
        thisObj.ToggleShow();
    }

    //选择数据
    this.SelectNode = function (treeNode) {
        var txt = thisObj.JoinParentNames(treeNode);
        thisObj.Container.find('.ctreeselect-text-input').val(txt);
        if (thisObj.settings.selectCallback) {
            thisObj.settings.selectCallback([treeNode]);
        }
    }

    //获取已选择的名称
    this.GetCheckedNames = function () {
        var checkedNodes = thisObj.TreeObject.getCheckedNodes();
        var names = new Array();
        for (var n in checkedNodes) {
            var node = checkedNodes[n];
            names.push(node.name);
        }
        return names;
    }

    //拼接父级名称
    this.JoinParentNames = function (treeNode) {
        var parentNodes = new Array();
        var nowNode = treeNode;
        var parentNodeNames = '';
        parentNodeNames += treeNode.name;
        return parentNodeNames;
    }

    //初始化选择值
    this.InitSelectedValue = function () {
        if (!thisObj.settings.selectedValue || thisObj.settings.selectedValue.length <= 0) {
            return;
        }
        if (thisObj.settings.multiSelect) {

        } else {
            var firVal = thisObj.settings.selectedValue[0];
            var node = thisObj.TreeObject.getNodeByParam("id", firVal);
            if (node) {
                thisObj.SelectNode(node);
            }
        }
    }

    //初始化树控件
    this.InitTree = function () {
        var data = {
            levelOne: true
        };
        if (thisObj.settings.where) {
            data = $.extend(true, {}, data, thisObj.settings.where(null, thisObj.settings.editKeyValue));
        }
        AjaxDataRequest(thisObj.settings.requestMethod, thisObj.settings.dataUrl, data, function (res) {
            var nodes = thisObj.settings.parseDataToNodeFunc(res);
            if (thisObj.settings.defaultNode) {
                nodes = [thisObj.settings.defaultNode].concat(nodes);
            }
            thisObj.TreeObject = $.fn.zTree.init(thisObj.TreeEle, thisObj.settings.treeConfig, nodes);
            //初始化选中值
            thisObj.InitSelectedValue();
        });
    }

    //隐藏大于或等于指定等级的节点
    this.HideNodesByLevel = function (startLevel) {
        thisObj.InitTree();
        var nodes = thisObj.TreeObject.getNodesByFilter(function (node) {
            return node.level >= startLevel;
        }, false);
        thisObj.TreeObject.hideNodes(nodes);
    }

    //展开事件
    this.BeforeExpand = function (treeId, treeNode) {
        if (thisObj.settings.defaultNode && treeNode.id == thisObj.settings.defaultNode.id) {
            return;
        }
        if (!treeNode || treeNode.loadData) {
            return true;
        }
        var zTree = $.fn.zTree.getZTreeObj(treeId);
        treeNode.icon = thisObj.settings.loadingIcoUrl;
        zTree.updateNode(treeNode);

        //数据条件
        var data = {};
        if (thisObj.settings.where) {
            data = $.extend(true, {}, data, thisObj.settings.where(treeNode, thisObj.settings.editKeyValue));
        }
        AjaxDataRequest(thisObj.settings.requestMethod, thisObj.settings.dataUrl, data, function (res) {
            var nodes = thisObj.settings.parseDataToNodeFunc(res);
            zTree.addNodes(treeNode, -1, nodes);
            treeNode.loadData = true;
            treeNode.icon = "";
            zTree.updateNode(treeNode);
            zTree.expandNode(treeNode, true);
        });
        return false;
    }

    //初始化
    this.Init();
}
