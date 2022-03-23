<template>
  <el-container style="height: 100%; border: 1px solid #eee">
    <el-header>
      <vab-query-form>
        <vab-query-form-left-panel :span="5">
          <el-button
            icon="el-icon-plus"
            type="success"
            :disabled="pageStatus.isDisabled"
            @click="handleAddOperationGroup"
          >
            添加分组
          </el-button>
          <el-button
            icon="el-icon-delete"
            type="danger"
            :disabled="pageStatus.isDisabled"
            @click="handleBatchDeleteOperationGroup"
          >
            批量删除分组
          </el-button>
        </vab-query-form-left-panel>
        <vab-query-form-left-panel v-if="showOperationPanel" :span="3">
          <el-form :inline="true" @submit.native.prevent>
            <el-form-item>
              <span>【{{ currentGroupName }}】下的功能</span>
            </el-form-item>
          </el-form>
        </vab-query-form-left-panel>
        <vab-query-form-right-panel v-if="showOperationPanel" :span="16">
          <el-form :inline="true" :model="queryForm" @submit.native.prevent>
            <el-form-item>
              <el-input
                v-model="queryForm.operationMateKey"
                placeholder="名称/action/controller"
              />
            </el-form-item>
            <el-form-item>
              <el-button
                icon="el-icon-search"
                type="primary"
                native-type="submit"
                :disabled="pageStatus.isDisabled"
                @click="handleQueryOperation({ initPage: true })"
              >
                查询
              </el-button>
            </el-form-item>
            <el-form-item>
              <el-button
                icon="el-icon-plus"
                type="primary"
                :disabled="pageStatus.isDisabled"
                @click="handleAddOperation"
              >
                添加
              </el-button>
            </el-form-item>
            <el-form-item>
              <el-button
                icon="el-icon-delete"
                type="danger"
                :disabled="pageStatus.isDisabled"
                @click="handleBulkDeleteOperation"
              >
                删除
              </el-button>
            </el-form-item>
          </el-form>
        </vab-query-form-right-panel>
      </vab-query-form>
    </el-header>
    <el-main>
      <el-container>
        <el-aside width="300px">
          <el-container>
            <el-main>
              <el-tree
                ref="group-tree"
                :data="groupTreeData"
                show-checkbox
                node-key="id"
                default-expand-all
                :expand-on-click-node="false"
                :check-strictly="true"
              >
                <span slot-scope="{ node, data }" class="custom-tree-node">
                  <span>{{ node.label }}</span>
                  &nbsp;&nbsp;
                  <span>
                    <el-button
                      type="text"
                      size="mini"
                      @click.stop="() => handleShowOperationGroupDetail(data)"
                    >
                      详情
                    </el-button>
                    <el-button
                      type="text"
                      size="mini"
                      @click="() => handleEditOperationGroup(data)"
                    >
                      编辑
                    </el-button>
                    <el-button
                      type="text"
                      size="mini"
                      @click="() => handleDeleteOperationGroupNode(node, data)"
                    >
                      删除
                    </el-button>
                  </span>
                </span>
              </el-tree>
            </el-main>
          </el-container>
        </el-aside>
        <el-container>
          <el-main v-if="showOperationPanel">
            <div class="table-container">
              <el-table
                ref="tableSort"
                v-loading="pageStatus.isLoading"
                :data="operationData"
                :element-loading-text="pageStatus.loadingText"
                :height="tableHeight"
                @selection-change="handleSetSelectedOperation"
              >
                <el-table-column
                  show-overflow-tooltip
                  type="selection"
                  width="50"
                ></el-table-column>
                <el-table-column show-overflow-tooltip label="序号" width="95">
                  <template #default="scope">
                    {{ scope.$index + 1 }}
                  </template>
                </el-table-column>
                <el-table-column
                  show-overflow-tooltip
                  prop="name"
                  label="名称"
                  width="150"
                ></el-table-column>
                <el-table-column
                  show-overflow-tooltip
                  prop="controller"
                  label="控制器"
                  width="150"
                ></el-table-column>
                <el-table-column
                  show-overflow-tooltip
                  prop="action"
                  label="方法"
                  width="150"
                ></el-table-column>
                <el-table-column
                  show-overflow-tooltip
                  prop="path"
                  label="地址"
                  width="150"
                ></el-table-column>
                <el-table-column label="状态" width="100">
                  <template #default="{ row }">
                    <span>
                      {{ getOperationStatusDisplayText(row.status) }}
                    </span>
                  </template>
                </el-table-column>
                <el-table-column
                  show-overflow-tooltip
                  prop="remark"
                  label="备注"
                ></el-table-column>
                <el-table-column
                  show-overflow-tooltip
                  label="操作"
                  align="center"
                  width="120px"
                >
                  <template #default="{ row }">
                    <el-button type="text" @click="handleEditOperation(row)">
                      编辑
                    </el-button>
                    <el-button
                      type="text"
                      @click="handleOperationPermission(row)"
                    >
                      授权
                    </el-button>
                    <el-button
                      type="text"
                      @click="handleDeleteOperationRow(row)"
                    >
                      删除
                    </el-button>
                  </template>
                </el-table-column>
              </el-table>
              <ez-pagination :pagination-info="paginationInfo"></ez-pagination>
            </div>
          </el-main>
        </el-container>
      </el-container>
    </el-main>
    <edit-operation-group
      ref="edit-operation-group"
      @edit-operation-group-callback="handleEditOperationGroupCallback"
    ></edit-operation-group>
    <edit-operation
      ref="edit-operation"
      @fetch-operation="handleQueryOperation"
    ></edit-operation>
    <operation-permission ref="operation-permission"></operation-permission>
  </el-container>
</template>

<script>
  import { createNamespacedHelpers } from 'vuex'
  import {
    doQueryOperationGroup,
    doDeleteOperationGroup,
  } from '@/api/sys/operationGroup'
  import { doQueryOperation, doDeleteOperation } from '@/api/sys/operation'
  import EditOperationGroup from './components/EditOperationGroup'
  import EditOperation from './components/EditOperation'
  import OperationPermission from './components/OperationPermission'
  import EzPagination from '@/components/EzPagination'
  const { mapGetters: mapOperationGetters, mapActions: mapOperationActions } =
    createNamespacedHelpers('sys/operation')
  export default {
    components: {
      EditOperationGroup,
      EditOperation,
      OperationPermission,
      EzPagination,
    },

    data() {
      return {
        //分组数据
        groupDataDict: {},
        //分组节点数据
        groupTreeData: [],
        queryForm: {
          operationMateKey: '',
          group: '',
        },
        //权限数据
        operationData: [],
        //分页信息
        paginationInfo: {},
        //已选择权限行
        selectedOperationRows: [],
        //页面状态
        pageStatus: {},
        //当权选中分组名称
        currentGroupName: '',
      }
    },
    computed: {
      ...mapOperationGetters(['getOperationStatusDisplayText']),
      //获取表格高度
      tableHeight() {
        return this.$baseTableHeight()
      },
      //是否显示功能页面
      showOperationPanel() {
        return this.queryForm.group && this.queryForm.group != ''
      },
    },
    created() {
      this.initialize()
      this.handleQueryOperationGroup()
    },
    methods: {
      ...mapOperationActions(['initOperationConfig']),
      //页面初始化
      initialize() {
        this.paginationInfo = this.$getPaginationInfo?.({
          handleQueryData: this.handleQueryOperation,
        })
        this.pageStatus = this.$getPageStatus?.()
      },
      //添加分组
      async handleAddOperationGroup() {
        await this.$refs['edit-operation-group'].showEditOperationGroup()
      },
      //查询分组
      async handleQueryOperationGroup() {
        this.pageStatus.showPageLoading('正在加载权限分组...')
        this.groupDataDict = {}
        var queryResult = await doQueryOperationGroup(this.queryForm)
        if (queryResult && queryResult.success) {
          this.groupTreeData = this.convertToTreeData(queryResult.data, '0')
        }
        this.pageStatus.closePageLoading()
      },
      //转换分组数据
      convertToTreeData(groupDatas, parentId) {
        let treeDatas = []
        if (groupDatas && groupDatas.length > 0) {
          let vm = this
          var topGroupDatas = groupDatas.filter((item) => {
            vm.groupDataDict[item.id] = item
            return item.parent == parentId
          })
          topGroupDatas.map((gd) => {
            let nodeData = {
              id: gd.id,
              label: gd.name,
            }
            let childDatas = this.convertToTreeData(groupDatas, gd.id)
            nodeData.children = childDatas
            treeDatas.push(nodeData)
          })
        }
        return treeDatas
      },
      //编辑分组
      async handleEditOperationGroup(data) {
        await this.$refs['edit-operation-group'].showEditOperationGroup(
          this.groupDataDict[data.id]
        )
      },
      //删除分组节点
      handleDeleteOperationGroupNode(node, data) {
        let vm = this
        this.$baseConfirm('你确定要删除当前分组吗', null, async () => {
          await vm.handleDeleteOperationGroupData([data.id])
        })
      },
      //批量删除
      handleBatchDeleteOperationGroup() {
        let checkedKeys = this.$refs['group-tree'].getCheckedKeys()
        if (checkedKeys?.length > 0) {
          let vm = this
          this.$baseConfirm('你确定要删除选择的分组吗', null, async () => {
            await vm.handleDeleteOperationGroupData(checkedKeys)
          })
        } else {
          this.$baseMessage('未选择任何数据', 'error')
        }
      },
      //删除分组数据
      async handleDeleteOperationGroupData(dataKeys) {
        const { success, msg } = await doDeleteOperationGroup(dataKeys)
        if (success) {
          this.$baseMessage('删除成功', 'success')
          await this.handleQueryOperationGroup()
          if (dataKeys.includes(this.queryForm.group)) {
            this.queryForm.group = ''
          }
        } else {
          this.$baseMessage(msg, 'error')
        }
      },
      //分组节点点击
      async handleShowOperationGroupDetail(data, node, el) {
        if (data.id == this.queryForm.group) {
          return
        }
        this.queryForm.group = data.id
        this.currentGroupName = data.label
        await this.initOperationConfig()
        await this.handleQueryOperation()
      },
      //查询权限数据
      async handleQueryOperation(options) {
        this.pageStatus.showPageLoading('正在加载权限...')
        if (options && options.initPage) {
          this.paginationInfo.initPage()
        }
        this.operationData = []
        this.total = 0
        const { success, msg, data } = await doQueryOperation(
          this.paginationInfo.getQueryForm(this.queryForm)
        )
        if (success) {
          const { items, totalCount } = data
          this.operationData = items
          this.paginationInfo.setTotal(totalCount)
        }
        this.pageStatus.closePageLoading()
      },
      //操作授权
      handleOperationPermission(data) {
        this.$refs['operation-permission'].showOperationPermission(data)
      },
      //添加权限
      handleAddOperation() {
        this.$refs['edit-operation'].showEditOperation()
      },
      //编辑权限
      handleEditOperation(data) {
        this.$refs['edit-operation'].showEditOperation(data)
      },
      //设置选择的权限
      handleSetSelectedOperation(rows) {
        this.selectedOperationRows = rows
      },
      //删除权限
      async handleDeleteOperationRow(row) {
        if (row) {
          let vm = this
          this.$baseConfirm('你确定要删除当前权限吗?', null, async () => {
            await vm.handleDeleteOperationData([row.id])
          })
        }
      },
      //批量删除
      async handleBulkDeleteOperation() {
        let vm = this
        if (
          this.selectedOperationRows &&
          this.selectedOperationRows.length > 0
        ) {
          this.$baseConfirm('你确定要删除选中的权限吗?', null, async () => {
            await vm.handleDeleteOperationData(
              vm.selectedOperationRows.map((item) => item.id)
            )
          })
        }
      },
      async handleDeleteOperationData(dataKeys) {
        if (dataKeys && dataKeys.length > 0) {
          this.pageStatus.showPageLoading('正在删除权限...')
          const { success, msg } = await doDeleteOperation(dataKeys)
          if (success) {
            this.$baseMessage('删除成功', 'success')
            await this.handleQueryOperation()
          } else {
            this.$baseMessage(msg, 'error')
          }
        }
      },
      //分组编辑回调
      async handleEditOperationGroupCallback() {
        await this.handleQueryOperationGroup()
      },
    },
  }
</script>

<style>
  .el-header {
    color: #333;
  }

  .el-aside {
    color: #333;
  }
  .el-main {
    padding: 0px;
  }
</style>
