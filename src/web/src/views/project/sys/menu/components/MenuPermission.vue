<template>
  <el-drawer
    :title="getTitle"
    :visible.sync="drawer"
    direction="rtl"
    size="670px"
    :wrapper-closable="false"
  >
    <div class="table-container">
      <vab-query-form>
        <vab-query-form-left-panel :span="24">
          <el-form
            ref="form"
            :model="queryForm"
            :inline="true"
            @submit.native.prevent
          >
            <el-form-item>
              <el-input
                v-model="queryForm.nameCodeMateKey"
                placeholder="名称"
              />
            </el-form-item>
            <el-form-item>
              <el-button
                icon="el-icon-search"
                type="primary"
                native-type="submit"
                :disabled="pageStatus.isDisabled"
                @click="handleQuery({ initPage: true })"
              >
                查询
              </el-button>
              <el-button
                icon="el-icon-plus"
                :disabled="pageStatus.isDisabled"
                type="success"
                @click="handleAdd"
              >
                添加
              </el-button>
              <el-button
                icon="el-icon-delete"
                :disabled="pageStatus.isDisabled"
                type="danger"
                @click="handleDelete"
              >
                移除选中
              </el-button>
              <el-button
                icon="el-icon-delete"
                :disabled="pageStatus.isDisabled"
                type="danger"
                @click="handleClearPermission"
              >
                清空
              </el-button>
            </el-form-item>
          </el-form>
        </vab-query-form-left-panel>
      </vab-query-form>

      <el-table
        ref="tableSort"
        v-loading="pageStatus.isLoading"
        :data="dataList"
        :element-loading-text="pageStatus.loadingText"
        :height="tableHeight"
        @selection-change="setSelectedRows"
      >
        <el-table-column
          show-overflow-tooltip
          type="selection"
          width="50"
        ></el-table-column>
        <el-table-column
          show-overflow-tooltip
          label="序号"
          align="left"
          width="50"
        >
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
          width="80px"
        >
          <template #default="{ row }">
            <el-button type="text" @click="handleDelete(row)">移除</el-button>
          </template>
        </el-table-column>
      </el-table>

      <ez-pagination :pagination-info="paginationInfo"></ez-pagination>
    </div>
    <permission-multi-select
      ref="select-permission"
      @confirm-selected-permission="confirmSelectedPermissionCallback"
    ></permission-multi-select>
  </el-drawer>
</template>

<script>
  import { createNamespacedHelpers } from 'vuex'
  import PermissionMultiSelect from '../../permission/components/PermissionMultiSelect'
  import { doQueryPermission } from '@/api/sys/permission'
  import EzPagination from '@/components/EzPagination'
  import {
    doAddMenuPermission,
    doDeleteMenuPermission,
    doClearByMenu,
  } from '@/api/sys/menuPermission'
  const { mapActions, mapGetters } = createNamespacedHelpers('sys/permission')

  export default {
    name: 'MenuPermission',
    components: { PermissionMultiSelect, EzPagination },
    data() {
      return {
        //是否显示页面
        drawer: false,
        //数据列表
        dataList: [],
        //分页信息
        paginationInfo: {},
        //已选择的行
        selectedRows: [],
        pageStatus: {},
        //查询表单
        queryForm: {
          nameCodeMateKey: '',
        },
      }
    },
    computed: {
      ...mapGetters(['getPermissionStatusDisplayText']),
      //获取标题
      getTitle() {
        if (this.menu) {
          return `【${this.menu.title}】授予的权限`
        }
        return '菜单权限'
      },
      //获取表格高度
      tableHeight() {
        return this.$baseTableHeight()
      },
    },
    created() {},
    methods: {
      ...mapActions(['initPermissionConfig']),
      //初始化
      initialize() {
        Object.assign(this.$data, this.$options.data())
        //分页信息
        this.paginationInfo = this.$getLwtPaginationInfo?.({
          handleQueryData: this.handleQuery,
        })
        //页面状态
        this.pageStatus = this.$getPageStatus?.() ?? {}
      },
      //设置选择行数据
      setSelectedRows(rows) {
        this.selectRows = rows
      },
      //查询数据
      async handleQuery(options) {
        if (options && options.initPage) {
          this.paginationInfo.initPage()
        }
        this.pageStatus.showPageLoading('正在加载菜单权限...')
        var queryResult = await doQueryPermission(
          this.paginationInfo.getQueryForm(this.queryForm)
        )
        if (queryResult && queryResult.success) {
          this.dataList = queryResult.data.items
          this.paginationInfo.setTotal(queryResult.data.totalCount)
        }
        this.pageStatus.closePageLoading()
      },
      //显示页面
      async showMenuPermission(menu) {
        if (menu) {
          this.initialize()
          this.queryForm.menuFilter = { ids: [menu.id] }
          this.drawer = true
          this.pageStatus.showPageLoading('正在加载菜单权限...')
          await this.initPermissionConfig()
          await this.handleQuery({ initPage: true })
          this.pageStatus.closePageLoading()
        }
      },
      //删除
      async handleDelete(row) {
        var dataKeys = []
        if (row.id) {
          this.$baseConfirm('你确定要移除当前项吗', null, async () => {
            dataKeys.push(row.id)
            await this.deletePermissionData(dataKeys)
          })
        } else {
          if (this.selectRows && this.selectRows.length > 0) {
            dataKeys = this.selectRows.map((item) => item.id)
            this.$baseConfirm('你确定要移除选中项吗', null, async () => {
              await this.deletePermissionData(dataKeys)
            })
          } else {
            this.$baseMessage('未选中任何行', 'error')
          }
        }
      },
      //删除角色用户数据项
      async deletePermissionData(dataKeys) {
        let menuId = this.queryForm.menuFilter.ids[0]
        this.pageStatus.showPageLoading('正在移除菜单权限...')
        let deleteResult = await doDeleteMenuPermission(
          dataKeys.map(function (pid) {
            return {
              MenuId: menuId,
              PermissionId: pid,
            }
          })
        )
        this.pageStatus.closePageLoading()
        if (deleteResult.success) {
          this.$baseMessage('删除成功', 'success')
          this.handleQuery()
        } else {
          this.$baseMessage(deleteResult.msg, 'error')
        }
        return deleteResult
      },
      //清空
      async handleClearPermission() {
        this.$baseConfirm('确认要清空菜单权限吗?', null, async () => {
          this.pageStatus.showPageLoading('正在清空菜单权限...')
          const { success, msg } = await doClearByMenu([
            this.queryForm.menuFilter.ids[0],
          ])
          this.pageStatus.closePageLoading()
          if (success) {
            this.$baseMessage('删除成功', 'success')
            this.handleQuery()
          } else {
            this.$baseMessage(msg, 'error')
          }
        })
      },
      //选择回调
      async confirmSelectedPermissionCallback($event) {
        if ($event && $event.length > 0) {
          let menuId = this.queryForm.menuFilter.ids[0]
          this.pageStatus.showPageLoading('正在保存菜单权限...')
          const { msg, success } = await doAddMenuPermission(
            $event.map(function (pid) {
              return {
                MenuId: menuId,
                PermissionId: pid,
              }
            })
          )
          this.pageStatus.closePageLoading()
          if (success) {
            this.$baseMessage('保存成功', 'success')
            await this.handleQuery()
          } else {
            this.$baseMessage(msg, 'error')
          }
        }
      },
      //添加
      async handleAdd() {
        await this.$refs['select-permission'].showPage()
      },
    },
  }
</script>
