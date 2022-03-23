<template>
  <el-container style="height: 100%; border: 1px solid #eee">
    <el-header>
      <vab-query-form>
        <vab-query-form-left-panel :span="5">
          <el-button
            icon="el-icon-plus"
            type="success"
            :disabled="pageStatus.isDisabled"
            @click="handleAddPermissionGroup"
          >
            添加分组
          </el-button>
          <el-button
            icon="el-icon-delete"
            type="danger"
            :disabled="pageStatus.isDisabled"
            @click="handleBatchDeletePermissionGroup"
          >
            删除选中分组
          </el-button>
        </vab-query-form-left-panel>
        <vab-query-form-left-panel v-if="showPermissionPanel" :span="3">
          <el-form :inline="true" @submit.native.prevent>
            <el-form-item>
              <span>【{{ currentGroupName }}】下的权限</span>
            </el-form-item>
          </el-form>
        </vab-query-form-left-panel>
        <vab-query-form-right-panel v-if="showPermissionPanel" :span="16">
          <el-form :inline="true" :model="queryForm" @submit.native.prevent>
            <el-form-item>
              <el-input
                v-model="queryForm.nameCodeMateKey"
                placeholder="名称/编码"
              />
            </el-form-item>
            <el-form-item>
              <el-button
                icon="el-icon-search"
                type="primary"
                native-type="submit"
                :disabled="pageStatus.isDisabled"
                @click="handleQueryPermission({ initPage: true })"
              >
                查询
              </el-button>
            </el-form-item>
            <el-form-item>
              <el-button
                icon="el-icon-plus"
                type="success"
                :disabled="pageStatus.isDisabled"
                @click="handleAddPermission"
              >
                添加
              </el-button>
            </el-form-item>
            <el-form-item>
              <el-button
                icon="el-icon-delete"
                type="danger"
                :disabled="pageStatus.isDisabled"
                @click="handleBulkDeletePermission"
              >
                删除选中
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
                      @click.stop="() => handleShowPermissionGroupDetail(data)"
                    >
                      详情
                    </el-button>
                    <el-button
                      type="text"
                      size="mini"
                      @click.stop="() => handleEditPermissionGroup(data)"
                    >
                      编辑
                    </el-button>
                    <el-button
                      type="text"
                      size="mini"
                      @click.stop="
                        () => handleDeletePermissionGroupNode(node, data)
                      "
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
          <el-main v-if="showPermissionPanel">
            <div class="table-container">
              <el-table
                ref="tableSort"
                v-loading="pageStatus.isLoading"
                :data="permissionData"
                :element-loading-text="pageStatus.loadingText"
                :height="tableHeight"
                @selection-change="handleSetSelectedPermission"
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
                  prop="code"
                  label="编码"
                  width="150"
                ></el-table-column>
                <el-table-column label="状态" width="100">
                  <template #default="{ row }">
                    <span>
                      {{ getPermissionStatusDisplayText(row.status) }}
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
                  width="100px"
                >
                  <template #default="{ row }">
                    <el-button type="text" @click="handleEditPermission(row)">
                      编辑
                    </el-button>
                    <el-button
                      type="text"
                      @click="handleDeletePermissionRow(row)"
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
    <edit-permission-group
      ref="edit-permission-group"
      @save-success-callback="handleSavePermissionGroupCallback"
    ></edit-permission-group>
    <edit-permission
      ref="edit-permission"
      @fetch-permission="handleQueryPermission"
    ></edit-permission>
  </el-container>
</template>

<script>
  import { createNamespacedHelpers } from 'vuex'
  import {
    doQueryPermissionGroup,
    doDeletePermissionGroup,
  } from '@/api/sys/permissionGroup'
  import { doQueryPermission, doDeletePermission } from '@/api/sys/permission'
  import EditPermissionGroup from './components/EditPermissionGroup'
  import EditPermission from './components/EditPermission'
  import EzPagination from '@/components/EzPagination'
  const { mapGetters: mapPermissionGetters, mapActions: mapPermissionActions } =
    createNamespacedHelpers('sys/permission')
  export default {
    components: {
      EditPermissionGroup,
      EditPermission,
      EzPagination,
    },

    data() {
      return {
        //分组数据
        groupDataDict: {},
        //分组节点数据
        groupTreeData: [],
        queryForm: {
          nameCodeMateKey: '',
          group: '',
        },
        //权限数据
        permissionData: [],
        //已选择权限行
        selectedPermissionRows: [],
        //页面状态
        pageStatus: {},
        //分页信息
        paginationInfo: {},
        //当权选中分组名称
        currentGroupName: '',
      }
    },
    computed: {
      ...mapPermissionGetters(['getPermissionStatusDisplayText']),
      //获取表格高度
      tableHeight() {
        return this.$baseTableHeight()
      },
      //是否显示权限页面
      showPermissionPanel() {
        return this.queryForm.group && this.queryForm.group != ''
      },
    },
    created() {
      this.initialize()
      this.handleQueryPermissionGroup()
    },
    methods: {
      ...mapPermissionActions(['initPermissionConfig']),
      //页面初始化
      initialize() {
        this.paginationInfo = this.$getPaginationInfo?.({
          handleQueryData: this.handleQuery,
        })
        this.pageStatus = this.$getPageStatus?.()
      },
      //添加分组
      async handleAddPermissionGroup() {
        await this.$refs['edit-permission-group'].showEditPermissionGroup()
      },
      //查询分组
      async handleQueryPermissionGroup() {
        this.pageStatus.showPageLoading('正在加载权限分组...')
        this.groupDataDict = {}
        var queryResult = await doQueryPermissionGroup(this.queryForm)
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
      async handleEditPermissionGroup(data) {
        await this.$refs['edit-permission-group'].showEditPermissionGroup(
          this.groupDataDict[data.id]
        )
      },
      //删除分组节点
      handleDeletePermissionGroupNode(node, data) {
        let vm = this
        this.$baseConfirm('你确定要删除当前分组吗', null, async () => {
          await vm.handleDeletePermissionGroupData([data.id])
        })
      },
      //批量删除
      handleBatchDeletePermissionGroup() {
        let checkedKeys = this.$refs['group-tree'].getCheckedKeys()
        if (checkedKeys?.length > 0) {
          let vm = this
          this.$baseConfirm('你确定要删除选择的分组吗', null, async () => {
            await vm.handleDeletePermissionGroupData(checkedKeys)
          })
        } else {
          this.$baseMessage('未选择任何数据', 'error')
        }
      },
      //删除分组数据
      async handleDeletePermissionGroupData(dataKeys) {
        const { success, msg } = await doDeletePermissionGroup(dataKeys)
        if (success) {
          this.$baseMessage('删除成功', 'success')
          await this.handleQueryPermissionGroup()
          if (dataKeys.includes(this.queryForm.group)) {
            this.queryForm.group = ''
          }
        } else {
          this.$baseMessage(msg, 'error')
        }
      },
      //分组节点点击
      async handleShowPermissionGroupDetail(data) {
        if (data.id == this.queryForm.group) {
          return
        }
        this.queryForm.group = data.id
        this.currentGroupName = data.label
        await this.initPermissionConfig()
        await this.handleQueryPermission()
      },
      //查询权限数据
      async handleQueryPermission(options) {
        this.pageStatus.showPageLoading('正在加载权限...')
        if (options && options.initPage) {
          this.paginationInfo.initPage()
        }
        this.permissionData = []
        this.total = 0
        const { success, msg, data } = await doQueryPermission(
          this.paginationInfo.getQueryForm(this.queryForm)
        )
        if (success) {
          const { items, totalCount } = data
          this.permissionData = items
          this.paginationInfo.setTotal(totalCount)
        }
        this.pageStatus.closePageLoading()
      },
      //添加权限
      handleAddPermission() {
        this.$refs['edit-permission'].showEditPermission({
          group: this.queryForm.group,
        })
      },
      //编辑权限
      handleEditPermission(data) {
        this.$refs['edit-permission'].showEditPermission(data, true)
      },
      //设置选择的权限
      handleSetSelectedPermission(rows) {
        this.selectedPermissionRows = rows
      },
      //删除权限
      async handleDeletePermissionRow(row) {
        if (row) {
          let vm = this
          this.$baseConfirm('你确定要删除当前权限吗?', null, async () => {
            await vm.handleDeletePermissionData([row.id])
          })
        }
      },
      //批量删除权限
      async handleBulkDeletePermission() {
        let vm = this
        if (
          this.selectedPermissionRows &&
          this.selectedPermissionRows.length > 0
        ) {
          this.$baseConfirm('你确定要删除选中的权限吗?', null, async () => {
            await vm.handleDeletePermissionData(
              vm.selectedPermissionRows.map((item) => item.id)
            )
          })
        }
      },
      //执行删除权限数据
      async handleDeletePermissionData(dataKeys) {
        if (dataKeys && dataKeys.length > 0) {
          this.pageStatus.showPageLoading('正在删除权限...')
          const { success, msg } = await doDeletePermission(dataKeys)
          if (success) {
            this.$baseMessage('删除成功', 'success')
            await this.handleQueryPermission()
          } else {
            this.$baseMessage(msg, 'error')
          }
        }
      },
      //编辑权限分组回调
      async handleSavePermissionGroupCallback() {
        await this.handleQueryPermissionGroup()
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
