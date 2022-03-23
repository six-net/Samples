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
                @click="handleCleanRole"
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
          prop="name"
          label="名称"
          width="150"
        ></el-table-column>
        <el-table-column label="状态" width="80">
          <template #default="{ row }">
            <span>
              {{ getRoleStatusDisplayText(row.status) }}
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
    <role-multi-select
      ref="select-role"
      @confirm-selected-role="confirmSelectedRoleCallback"
    ></role-multi-select>
  </el-drawer>
</template>

<script>
  import { createNamespacedHelpers } from 'vuex'
  import RoleMultiSelect from '../../role/components/RoleMultiSelect'
  import { doQueryRole } from '@/api/sys/role'
  import EzPagination from '@/components/EzPagination'
  import {
    doAddUserRole,
    doDeleteUserRole,
    doClearByUser,
  } from '@/api/sys/userRole'
  const { mapActions, mapGetters } = createNamespacedHelpers('sys/role')

  export default {
    name: 'UserRole',
    components: { RoleMultiSelect, EzPagination },
    data() {
      return {
        //是否显示页面
        drawer: false,
        //数据列表
        dataList: [],
        //分页信息
        paginationInfo: {},
        //已选择的行
        selectedRows: {},
        //查询表单
        queryForm: {
          nameMateKey: '',
        },
        //当前用户
        user: {},
        //页面状态
        pageStatus: {},
      }
    },
    computed: {
      ...mapGetters(['getRoleStatusDisplayText']),
      //获取标题
      getTitle() {
        if (this.user) {
          return `【${this.user.realName}】绑定的角色`
        }
        return '账户角色'
      },
      //获取表格高度
      tableHeight() {
        return this.$baseTableHeight()
      },
    },
    created() {},
    methods: {
      ...mapActions(['initRoleConfig']),
      //页面初始化
      initialize() {
        Object.assign(this.$data, this.$options.data())
        this.paginationInfo = this.$getPaginationInfo?.({
          handleQueryData: this.handleQuery,
        })
        this.pageStatus = this.$getPageStatus?.()
      },
      //设置选择行数据
      setSelectedRows(rows) {
        this.selectRows = rows
      },
      //查询数据
      async handleQuery(options) {
        this.pageStatus.showPageLoading('正在加载用户角色...')
        if (options && options.initPage) {
          this.queryForm.page = 1
        }
        var searchResult = await doQueryRole(
          this.paginationInfo.getQueryForm(this.queryForm)
        )
        if (searchResult && searchResult.success) {
          this.dataList = searchResult.data.items
          this.paginationInfo.setTotal(searchResult.data.totalCount)
        }
        this.pageStatus.closePageLoading()
      },
      //显示页面
      async showUserRole(user) {
        this.initialize()
        if (user) {
          this.user = user
          this.queryForm.userFilter = { ids: [user.id] }
          this.drawer = true
          this.pageStatus.showPageLoading('正在加载用户角色...')
          await this.initRoleConfig()
          await this.handleQuery()
          this.pageStatus.closePageLoading()
        }
      },
      //删除
      async handleDelete(row) {
        var dataKeys = []
        if (row.id) {
          this.$baseConfirm('你确定要移除当前项吗', null, async () => {
            dataKeys.push(row.id)
            await this.deleteRoleData(dataKeys)
          })
        } else {
          if (this.selectRows && this.selectRows.length > 0) {
            dataKeys = this.selectRows.map((item) => item.id)
            this.$baseConfirm('你确定要移除选中项吗', null, async () => {
              await this.deleteRoleData(dataKeys)
            })
          } else {
            this.$baseMessage('未选中任何行', 'error')
          }
        }
      },
      //删除角色用户数据项
      async deleteRoleData(roleIds) {
        let userId = this.queryForm.userFilter.ids[0]
        this.pageStatus.showPageLoading('正在移除用户角色...')
        let removeResult = await doDeleteUserRole(
          roleIds.map(function (rid) {
            return {
              userId: userId,
              roleId: rid,
            }
          })
        )
        this.pageStatus.closePageLoading()
        if (removeResult.success) {
          this.$baseMessage('移除成功', 'success')
          this.handleQuery()
        } else {
          this.$baseMessage(removeResult.msg, 'error')
        }
        return removeResult
      },
      //清空
      async handleCleanRole() {
        this.$baseConfirm('确认要清空用户角色吗?', null, async () => {
          this.pageStatus.showPageLoading('正在清空用户角色...')
          const { success, msg } = await doClearByUser([
            this.queryForm.userFilter.ids[0],
          ])
          this.pageStatus.closePageLoading()
          if (success) {
            this.$baseMessage('清空成功', 'success')
            this.handleQuery({ initPage: true })
          } else {
            this.$baseMessage(msg, 'error')
          }
        })
      },
      //用户选择回调
      async confirmSelectedRoleCallback($event) {
        if ($event && $event.length > 0) {
          let userId = this.queryForm.userFilter.ids[0]
          this.pageStatus.showPageLoading('正在保存用户角色...')
          const { msg, success } = await doAddUserRole(
            $event.map(function (rid) {
              return {
                userId: userId,
                roleId: rid,
              }
            })
          )
          this.pageStatus.closePageLoading()
          if (success) {
            this.$baseMessage(msg, 'success')
            await this.handleQuery()
          } else {
            this.$baseMessage(msg, 'error')
          }
        }
      },
      //添加
      handleAdd() {
        this.$refs['select-role'].showPage()
      },
    },
  }
</script>
