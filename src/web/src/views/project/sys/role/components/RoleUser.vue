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
              <el-input v-model="queryForm.nameMateKey" placeholder="名称" />
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
                @click="handleCleanUser"
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
          prop="realName"
          label="姓名"
        ></el-table-column>
        <el-table-column
          show-overflow-tooltip
          prop="userName"
          label="登录名"
          width="150"
        ></el-table-column>
        <el-table-column label="状态" width="80">
          <template #default="{ row }">
            <span>
              {{ getUserStatusDisplayText(row.status) }}
            </span>
          </template>
        </el-table-column>
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
    <user-multi-select
      ref="select-user"
      @confirm-selected-user="confirmSelectedUserCallback"
    ></user-multi-select>
  </el-drawer>
</template>

<script>
  import { createNamespacedHelpers } from 'vuex'
  import UserMultiSelect from '../../user/components/UserMultiSelect'
  import { doQueryUser } from '@/api/sys/user'
  import {
    doAddUserRole,
    doDeleteUserRole,
    doClearByRole,
  } from '@/api/sys/userRole'
  import EzPagination from '@/components/EzPagination'
  const { mapActions, mapGetters } = createNamespacedHelpers('sys/user')

  export default {
    name: 'RoleUser',

    components: { UserMultiSelect, EzPagination },

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
          nameMateKey: '',
        },
        //主数据
        primaryData: {
          name: '',
        },
      }
    },
    computed: {
      ...mapGetters(['getUserStatusDisplayText']),
      //获取标题
      getTitle() {
        if (
          this.primaryData &&
          this.primaryData.name &&
          this.primaryData.name != ''
        ) {
          return `角色【${this.primaryData.name}】绑定的用户`
        }
        return '角色账户'
      },
      //获取表格高度
      tableHeight() {
        return this.$baseTableHeight()
      },
    },
    created() {},
    methods: {
      ...mapActions(['initUserConfig']),
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
        this.pageStatus.showPageLoading('正在加载用户...')
        var queryUserResult = await doQueryUser(
          this.paginationInfo.getQueryForm(this.queryForm)
        )
        if (queryUserResult && queryUserResult.success) {
          this.dataList = queryUserResult.data.items
          this.paginationInfo.setTotal(queryUserResult.data.totalCount)
        }
        this.pageStatus.closePageLoading()
      },
      //显示页面
      async showRoleUser(data) {
        this.initialize()
        if (data) {
          this.primaryData = Object.assign({}, data)
          this.queryForm.roleFilter = { ids: [data.id] }
          this.drawer = true
          this.pageStatus.showPageLoading('正在加载角色用户...')
          await this.initUserConfig()
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
            await this.deleteUserData(dataKeys)
          })
        } else {
          if (this.selectRows && this.selectRows.length > 0) {
            dataKeys = this.selectRows.map((item) => item.id)
            this.$baseConfirm('你确定要移除选中项吗', null, async () => {
              await this.deleteUserData(dataKeys)
            })
          } else {
            this.$baseMessage('未选中任何行', 'error')
          }
        }
      },
      //删除角色用户数据项
      async deleteUserData(userIds) {
        let roleId = this.queryForm.roleFilter.ids[0]
        this.pageStatus.showPageLoading('正在移除角色用户...')
        let removeResult = await doDeleteUserRole(
          userIds.map(function (uid) {
            return {
              userId: uid,
              roleId: roleId,
            }
          })
        )
        this.pageStatus.closePageLoading()
        if (removeResult.success) {
          this.$baseMessage('删除成功', 'success')
          this.handleQuery()
        } else {
          this.$baseMessage(removeResult.msg, 'error')
        }
        return removeResult
      },
      //清空
      async handleCleanUser() {
        this.$baseConfirm('确认要清空角色用户吗?', null, async () => {
          this.pageStatus.showPageLoading('正在清空角色用户...')
          const { success, msg } = await doClearByRole([
            this.queryForm.roleFilter.ids[0],
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
      //用户选择回调
      async confirmSelectedUserCallback($event) {
        if ($event && $event.length > 0) {
          let roleId = this.queryForm.roleFilter.ids[0]
          this.pageStatus.showPageLoading('正在保存角色用户...')
          const { msg, success } = await doAddUserRole(
            $event.map(function (uid) {
              return {
                userId: uid,
                roleId: roleId,
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
      handleAdd() {
        this.$refs['select-user'].showPage()
      },
    },
  }
</script>
