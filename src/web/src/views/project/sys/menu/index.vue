<template>
  <div class="table-container">
    <vab-query-form>
      <vab-query-form-top-panel :span="12">
        <el-button
          icon="el-icon-refresh"
          type="primary"
          :disabled="pageStatus.isDisabled"
          @click="handleQueryMenu()"
        >
          刷新
        </el-button>
        <el-button
          icon="el-icon-plus"
          type="success"
          :disabled="pageStatus.isDisabled"
          @click="handleAddMenu"
        >
          添加
        </el-button>
      </vab-query-form-top-panel>
    </vab-query-form>
    <el-table
      v-loading="pageStatus.isLoading"
      :data="treeTableData"
      :element-loading-text="pageStatus.loadingText"
      row-key="id"
      border
      :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
    >
      <el-table-column
        show-overflow-tooltip
        prop="meta.title"
        label="标题"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="name"
        label="名称"
        width="100px"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="path"
        label="路径"
        width="150px"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="component"
        label="Vue组件"
        width="150px"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="redirect"
        label="重定向"
        width="100px"
      ></el-table-column>
      <el-table-column show-overflow-tooltip width="100px" label="提示">
        <template #default="{ row }">
          <span v-if="row.meta">
            {{ row.meta.badge }}
          </span>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        width="80px"
        align="center"
        label="菜单图标"
      >
        <template #default="{ row }">
          <span v-if="row.meta">
            <vab-icon
              v-if="row.meta.icon"
              :icon="['fas', row.meta.icon]"
            ></vab-icon>
          </span>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        width="80px"
        align="center"
        label="路由图标"
      >
        <template #default="{ row }">
          <span v-if="row.meta">
            <vab-icon
              v-if="row.meta.remixIcon"
              :icon="['fas', row.meta.remixIcon]"
            ></vab-icon>
          </span>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="类型"
        align="center"
        width="70px"
      >
        <template #default="{ row }">
          <span>
            {{ getMenuUsageDisplayText(row.usage) }}
          </span>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="状态"
        align="center"
        width="70px"
      >
        <template #default="{ row }">
          <span>
            {{ getMenuStatusDisplayText(row.status) }}
          </span>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        align="center"
        width="80px"
        label="显示根"
      >
        <template #default="{ row }">
          <span>
            {{ row.alwaysShow ? '是' : '否' }}
          </span>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        width="50px"
        align="center"
        label="固定"
      >
        <template #default="{ row }">
          <span v-if="row.meta">
            {{ row.meta.affix ? '是' : '否' }}
          </span>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        align="center"
        width="70px"
        label="不缓存"
      >
        <template #default="{ row }">
          <span v-if="row.meta">
            {{ row.meta.noKeepAlive ? '是' : '否' }}
          </span>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="操作"
        align="center"
        width="120"
      >
        <template #default="{ row }">
          <el-button type="text" @click="handleEditMenu(row)">编辑</el-button>
          <el-button type="text" @click="handleMenuPermission(row)">
            授权
          </el-button>
          <el-button type="text" @click="handleDeleteMenu(row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <edit-menu ref="edit-menu" @fetch-data="handleQueryMenu"></edit-menu>
    <menu-permission ref="menu-permission"></menu-permission>
  </div>
</template>

<script>
  import { createNamespacedHelpers } from 'vuex'
  import { doQueryMenu, doDeleteMenu } from '@/api/sys/menu'
  import EditMenu from './components/EditMenu'
  import MenuPermission from './components/MenuPermission'
  const { mapGetters: mapMenuGetters, mapActions: mapMenuActions } =
    createNamespacedHelpers('sys/menu')

  export default {
    name: 'Menu',
    components: { EditMenu, MenuPermission },
    data() {
      return {
        //菜单字典数据
        menuDataDict: {},
        //菜单表格数据
        menuTableData: [],
        //页面状态
        pageStatus: this.$getPageStatus?.(),
        //表格数据
        treeTableData: [],
      }
    },
    computed: {
      ...mapMenuGetters([
        'getMenuStatusDisplayText',
        'getMenuUsageDisplayText',
      ]),
    },
    async created() {
      await this.initMenuConfig()
      await this.handleQueryMenu()
    },
    methods: {
      ...mapMenuActions(['initMenuConfig']),
      //添加菜单
      handleAddMenu() {
        this.$refs['edit-menu'].showEdit()
      },
      //编辑
      handleEditMenu(data) {
        this.$refs['edit-menu'].showEdit(this.menuDataDict[data.id])
      },
      //删除
      handleDeleteMenu(row) {
        if (row.id) {
          this.$baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg, success } = await doDeleteMenu([row.id])
            if (success) {
              this.$baseMessage('删除成功', 'success')
              this.handleQueryMenu()
            }
          })
        }
      },
      //菜单授权
      handleMenuPermission(data) {
        this.$refs['menu-permission'].showMenuPermission(data)
      },
      //查询菜单
      async handleQueryMenu() {
        this.menuDataDict = {}
        this.treeTableData = []
        this.pageStatus.showPageLoading('正在加载菜单...')
        const { data, success } = await doQueryMenu({})
        if (success) {
          this.treeTableData = this.convertToTreeTableData(data, '0')
        }
        this.pageStatus.closePageLoading()
      },
      //转换菜单数据
      convertToTreeTableData(menus, parentId) {
        let newTreeTableData = []
        if (menus && menus.length > 0) {
          let vm = this
          let topDatas = menus.filter((item) => {
            vm.menuDataDict[item.id] = item
            return item.parent == parentId
          })
          topDatas.map((m) => {
            let rowData = {
              id: m.id,
              path: m.path,
              component: m.component,
              redirect: m.redirect,
              alwaysShow: m.alwaysShow,
              hidden: m.hidden,
              name: m.name,
              status: m.status,
              usage: m.usage,
              meta: {
                title: m.title,
                icon: m.icon,
                affix: m.affix,
                badge: m.badge,
                breadCrumb: m.breadCrumb,
                permissions: m.permissions,
                remixIcon: m.remixIcon,
                noKeepAlive: m.noKeepAlive,
              },
            }
            let childDatas = this.convertToTreeTableData(menus, m.id)
            rowData.children = childDatas
            newTreeTableData.push(rowData)
          })
        }
        return newTreeTableData
      },
    },
  }
</script>
