<template>
  <div class="table-container">
    <vab-query-form>
      <vab-query-form-left-panel>
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
          </el-form-item>
        </el-form>
        <el-button
          icon="el-icon-plus"
          type="success"
          :disabled="pageStatus.isDisabled"
          @click="handleAdd"
        >
          添加
        </el-button>
        <el-button
          icon="el-icon-delete"
          type="danger"
          :disabled="pageStatus.isDisabled"
          @click="handleDelete"
        >
          删除选中
        </el-button>
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
      <el-table-column show-overflow-tooltip label="序号" width="50">
        <template #default="scope">{{ scope.$index + 1 }}</template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="name"
        label="名称"
        width="150"
      ></el-table-column>
      <el-table-column label="状态" width="100">
        <template #default="{ row }">
          <span>
            {{ getRoleStatusDisplayText(row.status) }}
          </span>
        </template>
      </el-table-column>
      <el-table-column show-overflow-tooltip label="添加时间" width="160">
        <template #default="{ row }">
          <span>{{ formatDateTime(row.creationTime) }}</span>
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
        width="180px"
      >
        <template #default="{ row }">
          <el-button type="text" @click="handleEdit(row)">编辑</el-button>
          <el-button type="text" @click="handleRoleUser(row)">账户</el-button>
          <el-button type="text" @click="handleRolePermission(row)">
            授权
          </el-button>
          <el-button type="text" @click="handleDelete(row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <ez-pagination :pagination-info="paginationInfo"></ez-pagination>
    <edit-role ref="edit" @fetch-data="editCallback"></edit-role>
    <role-user ref="role-user"></role-user>
    <role-permission ref="role-permission"></role-permission>
  </div>
</template>

<script>
  import { createNamespacedHelpers } from 'vuex'
  import { doQueryRole, doDeleteRole } from '@/api/sys/role'
  import moment from 'moment'
  import EditRole from './components/EditRole'
  import RoleUser from './components/RoleUser'
  import RolePermission from './components/RolePermission'
  import EzPagination from '@/components/EzPagination'
  const { mapActions, mapGetters } = createNamespacedHelpers('sys/role')

  export default {
    name: 'Role',

    components: {
      EditRole,
      RoleUser,
      RolePermission,
      EzPagination,
    },

    data() {
      return {
        //数据列表
        dataList: [],
        //分页信息
        paginationInfo: {},
        //已选择的行
        selectedRows: '',
        //页面状态
        pageStatus: {},
        //查询表单
        queryForm: {
          nameMateKey: '',
        },
      }
    },

    computed: {
      //获取表格高度
      tableHeight() {
        return this.$baseTableHeight()
      },
      //格式化时间数据
      formatDateTime: () => (dateTime) => {
        return moment(dateTime).format('YYYY-MM-DD HH:mm:ss')
      },
      ...mapGetters(['getRoleStatusDisplayText']),
    },

    //组件创建后事件
    async created() {},

    //组件销毁前事件
    beforeDestroy() {},

    //绑定到元素后事件
    async mounted() {
      this.initialize()
      this.pageStatus.showPageLoading('正在加载角色...')
      await this.initRoleConfig()
      await this.handleQuery()
      this.pageStatus.closePageLoading()
    },

    //方法
    methods: {
      ...mapActions(['initRoleConfig']),
      //页面初始化
      initialize() {
        this.paginationInfo = this.$getPaginationInfo?.({
          handleQueryData: this.handleQuery,
        })
        this.pageStatus = this.$getPageStatus?.()
      },
      //设置选择行数据
      setSelectedRows(rows) {
        this.selectRows = rows
      },
      //添加
      handleAdd() {
        this.$refs['edit'].showEdit()
      },
      //编辑
      handleEdit(row) {
        this.$refs['edit'].showEdit(row)
      },
      //删除
      handleDelete(row) {
        var dataKeys = []
        var vm = this
        if (row.id) {
          this.$baseConfirm('你确定要删除当前项吗', null, async () => {
            dataKeys.push(row.id)
            await vm.handleDeleteRoleData(dataKeys)
          })
        } else {
          if (this.selectRows && this.selectRows.length > 0) {
            this.$baseConfirm('你确定要删除选中项吗', null, async () => {
              dataKeys = vm.selectRows.map((item) => item.id)
              await vm.handleDeleteRoleData(dataKeys)
            })
          } else {
            this.$baseMessage('未选中任何行', 'error')
            return false
          }
        }
      },
      //删除角色数据
      async handleDeleteRoleData(dataKeys) {
        if (dataKeys.length > 0) {
          this.pageStatus.showPageLoading('正在删除角色...')
          const { success, msg } = await doDeleteRole(dataKeys)
          if (success) {
            this.$baseMessage('删除成功', 'success')
            this.handleQuery()
          } else {
            this.$baseMessage(msg, 'error')
          }
          this.pageStatus.closePageLoading()
        }
      },
      //角色账户
      handleRoleUser(row) {
        var data = Object.assign({}, row)
        this.$refs['role-user'].showRoleUser(data)
      },
      //数据查询
      async handleQuery(options) {
        this.pageStatus.showPageLoading('正在加载角色...')
        if (options && options.initPage) {
          this.paginationInfo.initPage()
        }
        const roleResult = await doQueryRole(
          this.paginationInfo.getQueryForm(this.queryForm)
        )
        if (roleResult && roleResult.success) {
          const { items, totalCount } = roleResult.data
          this.dataList = items
          this.paginationInfo.setTotal(totalCount)
        }
        this.pageStatus.closePageLoading()
      },
      //编辑回调
      async editCallback() {
        this.handleQuery()
      },
      //角色授权
      handleRolePermission(data) {
        this.$refs['role-permission'].showRolePermission(data)
      },
    },
  }
</script>
