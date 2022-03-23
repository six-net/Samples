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
              :disabled="pageStatus.isDisabled"
              native-type="submit"
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
          :disabled="pageStatus.isDisabled"
          type="danger"
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
        :selectable="handleAllowSelect"
        width="50"
      ></el-table-column>
      <el-table-column show-overflow-tooltip label="序号" width="50">
        <template #default="scope">{{ scope.$index + 1 }}</template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="userName"
        label="登录名"
        width="150"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="realName"
        label="姓名"
        width="150"
      ></el-table-column>
      <el-table-column label="状态" width="100">
        <template #default="{ row }">
          <span>
            {{ getUserStatusDisplayText(row.status) }}
          </span>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="mobile"
        label="手机"
        width="150"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="email"
        label="邮箱"
      ></el-table-column>
      <el-table-column show-overflow-tooltip label="添加时间" width="160">
        <template #default="{ row }">
          <span>{{ formatDateTime(row.creationTime) }}</span>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="操作"
        align="center"
        width="180px"
      >
        <template #default="{ row }">
          <el-button type="text" @click="handleEdit(row)">编辑</el-button>
          <el-button
            type="text"
            :disabled="row.superUser"
            @click="handleUserRole(row)"
          >
            角色
          </el-button>
          <el-button
            type="text"
            :disabled="row.superUser"
            @click="handleUserPermission(row)"
          >
            授权
          </el-button>
          <el-button
            type="text"
            :disabled="row.superUser"
            @click="handleDelete(row)"
          >
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <ez-pagination :pagination-info="paginationInfo"></ez-pagination>
    <edit-user ref="edit" @fetch-data="editCallback"></edit-user>
    <user-role ref="user-role"></user-role>
    <user-permission ref="user-permission"></user-permission>
  </div>
</template>

<script>
  import { createNamespacedHelpers } from 'vuex'
  import moment from 'moment'
  import EditUser from './components/EditUser'
  import UserRole from './components/UserRole'
  import UserPermission from './components/UserPermission'
  import { doQueryUser, doDeleteUser } from '@/api/sys/user'
  import EzPagination from '@/components/EzPagination'
  const { mapActions: mapUserActions, mapGetters: mapUserGetters } =
    createNamespacedHelpers('sys/user')

  export default {
    name: 'User',
    components: {
      EditUser,
      UserRole,
      UserPermission,
      EzPagination,
    },
    data() {
      return {
        //数据列表
        dataList: [],
        //已选择的行
        selectedRows: [],
        //页面状态
        pageStatus: {},
        //分页信息
        paginationInfo: {},
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
      ...mapUserGetters(['getUserStatusDisplayText']),
    },

    //组件创建后事件
    created() {},

    //组件销毁前事件
    beforeDestroy() {},

    //绑定到元素后事件
    async mounted() {
      this.initialize()
      this.pageStatus.showPageLoading('正在加载用户...')
      await this.initUserConfig()
      await this.handleQuery()
      this.pageStatus.closePageLoading()
    },

    methods: {
      ...mapUserActions(['initUserConfig']),
      //页面初始化
      initialize() {
        this.paginationInfo = this.$getPaginationInfo?.({
          handleQueryData: this.handleQuery,
        })
        this.pageStatus = this.$getPageStatus?.()
      },
      //设置选择行数据
      setSelectedRows(rows) {
        this.selectedRows = rows
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
        let vm = this
        if (row.id) {
          this.$baseConfirm('你确定要删除当前项吗', null, async () => {
            dataKeys.push(row.id)
            await vm.handleDeleteUserData(dataKeys)
          })
        } else {
          if (this.selectedRows.length > 0) {
            this.$baseConfirm('你确定要删除选中项吗', null, async () => {
              dataKeys = vm.selectedRows.map((item) => item.id)
              await vm.handleDeleteUserData(dataKeys)
            })
          } else {
            this.$baseMessage('未选中任何行', 'error')
            return false
          }
        }
      },
      //删除用户数据
      async handleDeleteUserData(dataKeys) {
        if (dataKeys.length > 0) {
          this.pageStatus.showPageLoading('正在删除用户...')
          const { success, msg } = await doDeleteUser(dataKeys)
          if (success) {
            this.$baseMessage('删除成功', 'success')
            await this.handleQuery()
          } else {
            this.$baseMessage(msg, 'error')
          }
          this.pageStatus.closePageLoading()
        }
      },
      //账户角色
      handleUserRole(row) {
        var data = Object.assign({}, row)
        this.$refs['user-role'].showUserRole(data)
      },
      //数据查询
      async handleQuery(options) {
        this.pageStatus.showPageLoading('正在加载用户...')
        if (options && options.initPage) {
          this.paginationInfo.initPage()
        }
        const searchResult = await doQueryUser(
          this.paginationInfo.getQueryForm(this.queryForm)
        )
        if (searchResult && searchResult.success) {
          const { items, totalCount } = searchResult.data
          this.dataList = items
          this.paginationInfo.setTotal(totalCount)
        }
        this.pageStatus.closePageLoading()
      },
      //编辑回调
      async editCallback() {
        this.handleQuery()
      },
      //是否允许选中
      handleAllowSelect(row, index) {
        return !row.superUser
      },
      //用户授权
      handleUserPermission(data) {
        this.$refs['user-permission'].showUserPermission(data)
      },
    },
  }
</script>
